using DiGi.Harness.Classes;
using DiGi.Harness.Enums;
using DiGi.Harness.Interfaces;
using DiGi.Harness.WebAPI.Interfaces;
using DiGi.WebAPI.Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.Harness.WebAPI.Classes
{
    /// <summary>
    /// The chat endpoints of the harness (<c>harness/harnesschat</c>): configuration, sessions, and one streamed turn per message.
    /// <para>Every endpoint needs a signed-in user (resolved by the host's <see cref="IHarnessHost"/>) allowed by the configuration, and answers 401 otherwise; <c>configuration</c> instead answers <c>Enabled: false</c> so a client can hide its chat button. A disabled harness or an unusable provider answers 503. Once a message stream has started its status cannot change, so later failures arrive as <see cref="ErrorEvent"/>s followed by <see cref="TurnCompletedEvent"/>.</para>
    /// </summary>
    [ApiController]
    [Route("harness/[controller]")]
    public class HarnessChatController : WebAPIController
    {
        private readonly HarnessConfiguration harnessConfiguration;
        private readonly ChatSessionStore chatSessionStore;
        private readonly IHarnessHost harnessHost;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly TimeProvider timeProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="HarnessChatController"/> class.
        /// </summary>
        /// <param name="harnessConfiguration">The harness configuration.</param>
        /// <param name="chatSessionStore">The session store.</param>
        /// <param name="harnessHost">The host's user, tool and prompt provider.</param>
        /// <param name="httpClientFactory">The factory of the HTTP client used for model requests.</param>
        /// <param name="timeProvider">The clock for session activity and expiry.</param>
        public HarnessChatController(HarnessConfiguration harnessConfiguration, ChatSessionStore chatSessionStore, IHarnessHost harnessHost, IHttpClientFactory httpClientFactory, TimeProvider timeProvider)
        {
            this.harnessConfiguration = harnessConfiguration;
            this.chatSessionStore = chatSessionStore;
            this.harnessHost = harnessHost;
            this.httpClientFactory = httpClientFactory;
            this.timeProvider = timeProvider;
        }

        /// <summary>
        /// Returns whether the chat is available to the signed-in user, the selectable providers (name, display name and model; never a URI or key) and the thinking visibility options.
        /// </summary>
        /// <param name="cancellationToken">The token that cancels the request.</param>
        /// <returns>200 with the configuration (<c>Enabled: false</c> when the harness is off or the user is not allowed); 401 when nobody is signed in.</returns>
        [HttpGet("configuration", Name = $"{nameof(HarnessChatController)}_{nameof(ConfigurationAsync)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ConfigurationAsync(CancellationToken cancellationToken = default)
        {
            HarnessUser? harnessUser = await harnessHost.UserAsync(HttpContext, cancellationToken);
            if (harnessUser is null)
            {
                return Unauthorized();
            }

            bool enabled = harnessConfiguration.IsAllowed(harnessUser.Id);

            JsonArray jsonArray_Providers = [];
            string? defaultProviderName = null;
            if (enabled && harnessConfiguration.Providers is not null)
            {
                foreach (ProviderConfiguration providerConfiguration in harnessConfiguration.Providers)
                {
                    if (!providerConfiguration.IsUsable())
                    {
                        continue;
                    }

                    jsonArray_Providers.Add(new JsonObject
                    {
                        ["Name"] = providerConfiguration.Name,
                        ["DisplayName"] = providerConfiguration.DisplayName ?? providerConfiguration.Name,
                        ["Model"] = providerConfiguration.Model,
                    });
                }

                defaultProviderName = harnessConfiguration.ProviderConfiguration()?.Name;
            }

            JsonObject jsonObject = new()
            {
                ["Enabled"] = enabled && jsonArray_Providers.Count != 0,
                ["DefaultProviderName"] = defaultProviderName,
                ["Providers"] = jsonArray_Providers,
                ["ThinkingVisibilities"] = new JsonArray(nameof(ThinkingVisibility.Full), nameof(ThinkingVisibility.Summary), nameof(ThinkingVisibility.None)),
            };

            return Content(jsonObject.ToJsonString(), "application/json");
        }

        /// <summary>
        /// Starts a chat session with a provider; the session stays locked to it.
        /// </summary>
        /// <param name="jsonObject">The request body: <c>{"Provider": "name"}</c>; an empty name or body selects the default provider.</param>
        /// <param name="cancellationToken">The token that cancels the request.</param>
        /// <returns>200 with <c>{"Id", "ProviderName", "DisplayName"}</c>; 400 for an unknown provider; 401; 503 when the harness is disabled or has no usable provider.</returns>
        [HttpPost("sessions", Name = $"{nameof(HarnessChatController)}_{nameof(CreateSessionAsync)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> CreateSessionAsync([FromBody] JsonObject? jsonObject, CancellationToken cancellationToken = default)
        {
            if (!harnessConfiguration.Enabled)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }

            HarnessUser? harnessUser = await harnessHost.UserAsync(HttpContext, cancellationToken);
            if (harnessUser is null || !harnessConfiguration.IsAllowed(harnessUser.Id))
            {
                return Unauthorized();
            }

            string? providerName = (jsonObject?["Provider"] as JsonValue)?.TryGetValue(out string? providerName_Temp) == true ? providerName_Temp : null;
            ProviderConfiguration? providerConfiguration = harnessConfiguration.ProviderConfiguration(providerName);
            if (providerConfiguration?.Name is null)
            {
                return string.IsNullOrWhiteSpace(providerName) ? StatusCode(StatusCodes.Status503ServiceUnavailable) : BadRequest();
            }

            DateTimeOffset now = timeProvider.GetUtcNow();
            chatSessionStore.RemoveExpired(now);

            ChatSession chatSession = new(Guid.NewGuid().ToString("N"), harnessUser.Id, new Conversation(providerConfiguration.Name, null), now);
            chatSessionStore.Add(chatSession);

            Serilog.Modify.Log("Harness: session {SessionId} created (Provider={Provider}).", chatSession.Id, providerConfiguration.Name);

            JsonObject jsonObject_Result = new()
            {
                ["Id"] = chatSession.Id,
                ["ProviderName"] = providerConfiguration.Name,
                ["DisplayName"] = providerConfiguration.DisplayName ?? providerConfiguration.Name,
            };

            return Content(jsonObject_Result.ToJsonString(), "application/json");
        }

        /// <summary>
        /// Returns the transcript of a session, with thinking filtered by the requested visibility and no signatures.
        /// </summary>
        /// <param name="id">The session id.</param>
        /// <param name="thinkingVisibility">The thinking visibility: <c>Full</c>, <c>Summary</c> (default) or <c>None</c>.</param>
        /// <param name="cancellationToken">The token that cancels the request.</param>
        /// <returns>200 with the conversation; 401; 404 for an unknown, foreign or expired session; 503 when the harness is disabled.</returns>
        [HttpGet("sessions/{id}", Name = $"{nameof(HarnessChatController)}_{nameof(SessionAsync)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> SessionAsync([FromRoute] string? id, [FromQuery(Name = "thinkingvisibility")] ThinkingVisibility? thinkingVisibility = null, CancellationToken cancellationToken = default)
        {
            if (!harnessConfiguration.Enabled)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }

            HarnessUser? harnessUser = await harnessHost.UserAsync(HttpContext, cancellationToken);
            if (harnessUser is null || !harnessConfiguration.IsAllowed(harnessUser.Id))
            {
                return Unauthorized();
            }

            ChatSession? chatSession = chatSessionStore.ChatSession(id, harnessUser.Id, timeProvider.GetUtcNow());
            if (chatSession is null)
            {
                return NotFound();
            }

            Conversation? conversation = chatSession.Conversation.Transcript(thinkingVisibility ?? ThinkingVisibility.Summary);
            return Content(Core.Convert.ToSystem_String(conversation) ?? "null", "application/json");
        }

        /// <summary>
        /// Ends a session and drops its conversation.
        /// </summary>
        /// <param name="id">The session id.</param>
        /// <param name="cancellationToken">The token that cancels the request.</param>
        /// <returns>204 when removed; 401; 404 for an unknown, foreign or expired session; 503 when the harness is disabled.</returns>
        [HttpDelete("sessions/{id}", Name = $"{nameof(HarnessChatController)}_{nameof(DeleteSessionAsync)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> DeleteSessionAsync([FromRoute] string? id, CancellationToken cancellationToken = default)
        {
            if (!harnessConfiguration.Enabled)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }

            HarnessUser? harnessUser = await harnessHost.UserAsync(HttpContext, cancellationToken);
            if (harnessUser is null || !harnessConfiguration.IsAllowed(harnessUser.Id))
            {
                return Unauthorized();
            }

            return chatSessionStore.Remove(id, harnessUser.Id) ? NoContent() : NotFound();
        }

        /// <summary>
        /// Exports a session as a JSON file download: a <see cref="ConversationExport"/> built by <see cref="Harness.Create.ConversationExport(Conversation?, ExportMode, DateTimeOffset, string?)"/>.
        /// <para><c>Messages</c> (the default) keeps the user and assistant text only; <c>Full</c> also keeps the complete thinking text whatever the client's thinking visibility, the tool calls and the tool results, so the file can be handed to a model for bug fixing. Signatures and redacted thinking data never leave the server. The response carries <c>Content-Disposition: attachment; filename="chat-&lt;yyyyMMdd-HHmmss&gt;-&lt;messages|full&gt;.json"</c> (UTC).</para>
        /// </summary>
        /// <param name="id">The session id.</param>
        /// <param name="mode">The export mode by name: <c>Messages</c> (default when omitted) or <c>Full</c>, case-insensitive.</param>
        /// <param name="cancellationToken">The token that cancels the request.</param>
        /// <returns>200 with the export; 400 for an unknown mode; 401; 404 for an unknown, foreign or expired session; 409 while a turn of the session is running; 503 when the harness is disabled.</returns>
        [HttpGet("sessions/{id}/export", Name = $"{nameof(HarnessChatController)}_{nameof(ExportAsync)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> ExportAsync([FromRoute] string? id, [FromQuery(Name = "mode")] string? mode = null, CancellationToken cancellationToken = default)
        {
            if (!harnessConfiguration.Enabled)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }

            HarnessUser? harnessUser = await harnessHost.UserAsync(HttpContext, cancellationToken);
            if (harnessUser is null || !harnessConfiguration.IsAllowed(harnessUser.Id))
            {
                return Unauthorized();
            }

            ExportMode exportMode;
            if (string.IsNullOrWhiteSpace(mode) || string.Equals(mode, nameof(ExportMode.Messages), StringComparison.OrdinalIgnoreCase))
            {
                exportMode = ExportMode.Messages;
            }
            else if (string.Equals(mode, nameof(ExportMode.Full), StringComparison.OrdinalIgnoreCase))
            {
                exportMode = ExportMode.Full;
            }
            else
            {
                return BadRequest($"Unknown export mode '{mode}'; use Messages or Full.");
            }

            DateTimeOffset now = timeProvider.GetUtcNow();

            ChatSession? chatSession = chatSessionStore.ChatSession(id, harnessUser.Id, now);
            if (chatSession is null)
            {
                return NotFound();
            }

            // The history is appended while a turn runs; export only a settled conversation.
            if (chatSession.SemaphoreSlim.CurrentCount == 0)
            {
                return Conflict("A reply is still running; export once it has finished.");
            }

            string? version = typeof(Conversation).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;

            ConversationExport? conversationExport = Harness.Create.ConversationExport(chatSession.Conversation, exportMode, now, version);

            string fileName = string.Format(CultureInfo.InvariantCulture, "chat-{0:yyyyMMdd-HHmmss}-{1}.json", now.UtcDateTime, exportMode == ExportMode.Full ? "full" : "messages");
            Response.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = fileName }.ToString();

            return Content(Core.Convert.ToSystem_String(conversationExport) ?? "null", "application/json");
        }

        /// <summary>
        /// Sends a user message and streams one turn of the agent loop as <c>text/event-stream</c>: one compact JSON event per <c>data:</c> line, the serialized type name in <c>_type</c>, ending with <see cref="TurnCompletedEvent"/>.
        /// <para>Thinking is filtered on the server by <c>ThinkingVisibility</c> (<see cref="Query.VisibleHarnessEvent(HarnessEvent?, ThinkingVisibility)"/>), so <c>None</c> sends no thinking at all. Closing the connection cancels the turn. A comment line every few seconds keeps the stream open through proxies while a tool runs.</para>
        /// </summary>
        /// <param name="id">The session id.</param>
        /// <param name="jsonObject">The request body: <c>{"Text": "...", "ThinkingVisibility": "Full" | "Summary" | "None"}</c>.</param>
        /// <param name="cancellationToken">The token that cancels the turn; ASP.NET signals it when the client disconnects.</param>
        /// <returns>The event stream; before it starts: 400 for empty or too long text, 401, 404 for an unknown session, 409 when a turn of the session is already running, 503 when the harness is disabled or the session's provider is no longer usable.</returns>
        [HttpPost("sessions/{id}/messages", Name = $"{nameof(HarnessChatController)}_{nameof(MessageAsync)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> MessageAsync([FromRoute] string? id, [FromBody] JsonObject? jsonObject, CancellationToken cancellationToken = default)
        {
            if (!harnessConfiguration.Enabled)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }

            HarnessUser? harnessUser = await harnessHost.UserAsync(HttpContext, cancellationToken);
            if (harnessUser is null || !harnessConfiguration.IsAllowed(harnessUser.Id))
            {
                return Unauthorized();
            }

            DateTimeOffset now = timeProvider.GetUtcNow();
            ChatSession? chatSession = chatSessionStore.ChatSession(id, harnessUser.Id, now);
            if (chatSession is null)
            {
                return NotFound();
            }

            string? text = (jsonObject?["Text"] as JsonValue)?.TryGetValue(out string? text_Temp) == true ? text_Temp?.Trim() : null;
            if (string.IsNullOrWhiteSpace(text) || text.Length > Constants.Default.MaxMessageCharacters)
            {
                return BadRequest();
            }

            ThinkingVisibility thinkingVisibility = ThinkingVisibility.Summary;
            string? thinkingVisibility_Text = (jsonObject?["ThinkingVisibility"] as JsonValue)?.TryGetValue(out string? thinkingVisibility_Temp) == true ? thinkingVisibility_Temp : null;
            if (Enum.TryParse(thinkingVisibility_Text, true, out ThinkingVisibility thinkingVisibility_Parsed) && Enum.IsDefined(thinkingVisibility_Parsed) && thinkingVisibility_Parsed != ThinkingVisibility.Undefined)
            {
                thinkingVisibility = thinkingVisibility_Parsed;
            }

            ProviderConfiguration? providerConfiguration = harnessConfiguration.ProviderConfiguration(chatSession.Conversation.ProviderName);
            if (providerConfiguration is null)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }

            if (!await chatSession.SemaphoreSlim.WaitAsync(0, cancellationToken))
            {
                return Conflict();
            }

            try
            {
                chatSession.LastActivity = now;

                IReadOnlyList<ITool> tools = await harnessHost.ToolsAsync(HttpContext, harnessUser, cancellationToken);
                string? systemPrompt = await harnessHost.SystemPromptAsync(cancellationToken);

                RunOptions runOptions = new() { SystemPrompt = systemPrompt };
                if (harnessConfiguration.MaxToolIterations > 0)
                {
                    runOptions.MaxToolIterations = harnessConfiguration.MaxToolIterations;
                }

                if (harnessConfiguration.MaxToolResultCharacters > 0)
                {
                    runOptions.MaxToolResultCharacters = harnessConfiguration.MaxToolResultCharacters;
                }

                HttpContext.Features.Get<IHttpResponseBodyFeature>()?.DisableBuffering();
                Response.StatusCode = StatusCodes.Status200OK;
                Response.ContentType = "text/event-stream; charset=utf-8";
                Response.Headers.CacheControl = "no-cache, no-transform";
                Response.Headers["X-Accel-Buffering"] = "no";
                await Response.StartAsync(cancellationToken);

                using SemaphoreSlim semaphoreSlim_Write = new(1, 1);
                using CancellationTokenSource cancellationTokenSource_KeepAlive = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                Task task_KeepAlive = KeepAliveAsync(cancellationTokenSource_KeepAlive.Token);

                chatSession.Conversation.Add(new Message(Role.User, [new TextContent(text)]));

                Serilog.Modify.Log("Harness: turn started (SessionId={SessionId}, Provider={Provider}, ThinkingVisibility={ThinkingVisibility}).", chatSession.Id, providerConfiguration.Name ?? string.Empty, thinkingVisibility);

                try
                {
                    HttpClient httpClient = httpClientFactory.CreateClient(Constants.Default.HttpClientName);
                    await chatSession.Conversation.RunTurnAsync(httpClient, providerConfiguration, tools, runOptions, WriteAsync, cancellationToken);
                }
                catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
                {
                    Serilog.Modify.Log("Harness: turn cancelled by the client (SessionId={SessionId}).", chatSession.Id);
                }
                catch (Exception exception)
                {
                    Serilog.Modify.Log(exception, "Harness: turn failed (SessionId={SessionId}).", chatSession.Id);
                    await WriteAsync(new ErrorEvent(ErrorCategory.UpstreamFault, "The turn failed on the server.", 0), CancellationToken.None);
                    await WriteAsync(new TurnCompletedEvent(new TurnResult(StopReason.Undefined, ErrorCategory.UpstreamFault, "The turn failed on the server.", 0, 0, null, 0), 0), CancellationToken.None);
                }
                finally
                {
                    await cancellationTokenSource_KeepAlive.CancelAsync();
                    try
                    {
                        await task_KeepAlive;
                    }
                    catch (Exception exception) when (exception is OperationCanceledException or ObjectDisposedException or System.IO.IOException)
                    {
                        // The keep-alive loop ends with the turn, or earlier when the client is gone.
                    }
                }

                return new EmptyResult();

                async Task WriteAsync(HarnessEvent harnessEvent, CancellationToken cancellationToken_Write)
                {
                    HarnessEvent? harnessEvent_Visible = harnessEvent.VisibleHarnessEvent(thinkingVisibility);
                    string? data = harnessEvent_Visible?.ToJsonObject()?.ToJsonString();
                    if (data is null)
                    {
                        return;
                    }

                    await semaphoreSlim_Write.WaitAsync(cancellationToken_Write);
                    try
                    {
                        await Response.WriteServerSentEventAsync(data, cancellationToken_Write);
                    }
                    catch (Exception exception) when (exception is ObjectDisposedException or System.IO.IOException)
                    {
                        // The client is gone; RequestAborted cancels the turn, so the event is dropped here.
                    }
                    finally
                    {
                        semaphoreSlim_Write.Release();
                    }
                }

                async Task KeepAliveAsync(CancellationToken cancellationToken_KeepAlive)
                {
                    using PeriodicTimer periodicTimer = new(TimeSpan.FromSeconds(Constants.Default.KeepAliveSeconds), timeProvider);
                    while (await periodicTimer.WaitForNextTickAsync(cancellationToken_KeepAlive))
                    {
                        await semaphoreSlim_Write.WaitAsync(cancellationToken_KeepAlive);
                        try
                        {
                            await Response.WriteServerSentEventAsync(null, cancellationToken_KeepAlive);
                        }
                        finally
                        {
                            semaphoreSlim_Write.Release();
                        }
                    }
                }
            }
            finally
            {
                chatSession.LastActivity = timeProvider.GetUtcNow();
                chatSession.SemaphoreSlim.Release();
            }
        }
    }
}
