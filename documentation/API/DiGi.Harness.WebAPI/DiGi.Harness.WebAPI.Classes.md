#### [DiGi\.Harness\.WebAPI](DiGi.Harness.WebAPI.Overview.md 'DiGi\.Harness\.WebAPI\.Overview')

## DiGi\.Harness\.WebAPI\.Classes Namespace
### Classes

<a name='DiGi.Harness.WebAPI.Classes.ChatSession'></a>

## ChatSession Class

One chat session: its owner, its conversation \(locked to one provider\) and the gate that lets one turn run at a time\.

```csharp
public class ChatSession : DiGi.Harness.WebAPI.Interfaces.IHarnessWebAPIObject, DiGi.Core.Interfaces.IObject
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → ChatSession

Implements [IHarnessWebAPIObject](DiGi.Harness.WebAPI.Interfaces.md#DiGi.Harness.WebAPI.Interfaces.IHarnessWebAPIObject 'DiGi\.Harness\.WebAPI\.Interfaces\.IHarnessWebAPIObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject')
### Constructors

<a name='DiGi.Harness.WebAPI.Classes.ChatSession.ChatSession(string,string,DiGi.Harness.Classes.Conversation,System.DateTimeOffset)'></a>

## ChatSession\(string, string, Conversation, DateTimeOffset\) Constructor

Initializes a new instance of the [ChatSession](DiGi.Harness.WebAPI.Classes.md#DiGi.Harness.WebAPI.Classes.ChatSession 'DiGi\.Harness\.WebAPI\.Classes\.ChatSession') class\.

```csharp
public ChatSession(string id, string ownerId, DiGi.Harness.Classes.Conversation conversation, System.DateTimeOffset createdAt);
```
#### Parameters

<a name='DiGi.Harness.WebAPI.Classes.ChatSession.ChatSession(string,string,DiGi.Harness.Classes.Conversation,System.DateTimeOffset).id'></a>

`id` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The session id\.

<a name='DiGi.Harness.WebAPI.Classes.ChatSession.ChatSession(string,string,DiGi.Harness.Classes.Conversation,System.DateTimeOffset).ownerId'></a>

`ownerId` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The id of the user who owns the session\.

<a name='DiGi.Harness.WebAPI.Classes.ChatSession.ChatSession(string,string,DiGi.Harness.Classes.Conversation,System.DateTimeOffset).conversation'></a>

`conversation` [DiGi\.Harness\.Classes\.Conversation](https://learn.microsoft.com/en-us/dotnet/api/digi.harness.classes.conversation 'DiGi\.Harness\.Classes\.Conversation')

The conversation, locked to one provider\.

<a name='DiGi.Harness.WebAPI.Classes.ChatSession.ChatSession(string,string,DiGi.Harness.Classes.Conversation,System.DateTimeOffset).createdAt'></a>

`createdAt` [System\.DateTimeOffset](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset 'System\.DateTimeOffset')

The creation time\.
### Properties

<a name='DiGi.Harness.WebAPI.Classes.ChatSession.Conversation'></a>

## ChatSession\.Conversation Property

Gets the conversation, locked to one provider\.

```csharp
public DiGi.Harness.Classes.Conversation Conversation { get; }
```

#### Property Value
[DiGi\.Harness\.Classes\.Conversation](https://learn.microsoft.com/en-us/dotnet/api/digi.harness.classes.conversation 'DiGi\.Harness\.Classes\.Conversation')

<a name='DiGi.Harness.WebAPI.Classes.ChatSession.CreatedAt'></a>

## ChatSession\.CreatedAt Property

Gets the creation time\.

```csharp
public System.DateTimeOffset CreatedAt { get; }
```

#### Property Value
[System\.DateTimeOffset](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset 'System\.DateTimeOffset')

<a name='DiGi.Harness.WebAPI.Classes.ChatSession.Id'></a>

## ChatSession\.Id Property

Gets the session id\.

```csharp
public string Id { get; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.Harness.WebAPI.Classes.ChatSession.LastActivity'></a>

## ChatSession\.LastActivity Property

Gets or sets the time of the last request on the session; the idle timeout counts from it\.

```csharp
public System.DateTimeOffset LastActivity { get; set; }
```

#### Property Value
[System\.DateTimeOffset](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset 'System\.DateTimeOffset')

<a name='DiGi.Harness.WebAPI.Classes.ChatSession.OwnerId'></a>

## ChatSession\.OwnerId Property

Gets the id of the user who owns the session\.

```csharp
public string OwnerId { get; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.Harness.WebAPI.Classes.ChatSession.SemaphoreSlim'></a>

## ChatSession\.SemaphoreSlim Property

Gets the gate that lets one turn run at a time; a second request while a turn runs answers 409\.

```csharp
public System.Threading.SemaphoreSlim SemaphoreSlim { get; }
```

#### Property Value
[System\.Threading\.SemaphoreSlim](https://learn.microsoft.com/en-us/dotnet/api/system.threading.semaphoreslim 'System\.Threading\.SemaphoreSlim')

<a name='DiGi.Harness.WebAPI.Classes.ChatSessionStore'></a>

## ChatSessionStore Class

The in\-memory chat sessions of a host, keyed by session id\.

Sessions are lost when the application restarts. Read and change them through [ChatSession\(this ChatSessionStore, string, string, DateTimeOffset\)](DiGi.Harness.WebAPI.md#DiGi.Harness.WebAPI.Query.ChatSession(thisDiGi.Harness.WebAPI.Classes.ChatSessionStore,string,string,System.DateTimeOffset) 'DiGi\.Harness\.WebAPI\.Query\.ChatSession\(this DiGi\.Harness\.WebAPI\.Classes\.ChatSessionStore, string, string, System\.DateTimeOffset\)'), [Add\(this ChatSessionStore, ChatSession\)](DiGi.Harness.WebAPI.md#DiGi.Harness.WebAPI.Modify.Add(thisDiGi.Harness.WebAPI.Classes.ChatSessionStore,DiGi.Harness.WebAPI.Classes.ChatSession) 'DiGi\.Harness\.WebAPI\.Modify\.Add\(this DiGi\.Harness\.WebAPI\.Classes\.ChatSessionStore, DiGi\.Harness\.WebAPI\.Classes\.ChatSession\)') and [Remove\(this ChatSessionStore, string, string\)](DiGi.Harness.WebAPI.md#DiGi.Harness.WebAPI.Modify.Remove(thisDiGi.Harness.WebAPI.Classes.ChatSessionStore,string,string) 'DiGi\.Harness\.WebAPI\.Modify\.Remove\(this DiGi\.Harness\.WebAPI\.Classes\.ChatSessionStore, string, string\)'), which also drop expired sessions.

```csharp
public class ChatSessionStore : DiGi.Harness.WebAPI.Interfaces.IHarnessWebAPIObject, DiGi.Core.Interfaces.IObject
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → ChatSessionStore

Implements [IHarnessWebAPIObject](DiGi.Harness.WebAPI.Interfaces.md#DiGi.Harness.WebAPI.Interfaces.IHarnessWebAPIObject 'DiGi\.Harness\.WebAPI\.Interfaces\.IHarnessWebAPIObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject')
### Constructors

<a name='DiGi.Harness.WebAPI.Classes.ChatSessionStore.ChatSessionStore(System.TimeSpan,int)'></a>

## ChatSessionStore\(TimeSpan, int\) Constructor

Initializes a new instance of the [ChatSessionStore](DiGi.Harness.WebAPI.Classes.md#DiGi.Harness.WebAPI.Classes.ChatSessionStore 'DiGi\.Harness\.WebAPI\.Classes\.ChatSessionStore') class\.

```csharp
public ChatSessionStore(System.TimeSpan idleTimeout, int maxSessionsPerUser);
```
#### Parameters

<a name='DiGi.Harness.WebAPI.Classes.ChatSessionStore.ChatSessionStore(System.TimeSpan,int).idleTimeout'></a>

`idleTimeout` [System\.TimeSpan](https://learn.microsoft.com/en-us/dotnet/api/system.timespan 'System\.TimeSpan')

How long a session lives without a request\.

<a name='DiGi.Harness.WebAPI.Classes.ChatSessionStore.ChatSessionStore(System.TimeSpan,int).maxSessionsPerUser'></a>

`maxSessionsPerUser` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

How many sessions one user may hold; creating one more removes that user's least recently used session\.
### Properties

<a name='DiGi.Harness.WebAPI.Classes.ChatSessionStore.ChatSessions'></a>

## ChatSessionStore\.ChatSessions Property

Gets the sessions keyed by session id\.

```csharp
public System.Collections.Concurrent.ConcurrentDictionary<string,DiGi.Harness.WebAPI.Classes.ChatSession> ChatSessions { get; }
```

#### Property Value
[System\.Collections\.Concurrent\.ConcurrentDictionary&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentdictionary-2 'System\.Collections\.Concurrent\.ConcurrentDictionary\`2')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[,](https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentdictionary-2 'System\.Collections\.Concurrent\.ConcurrentDictionary\`2')[ChatSession](DiGi.Harness.WebAPI.Classes.md#DiGi.Harness.WebAPI.Classes.ChatSession 'DiGi\.Harness\.WebAPI\.Classes\.ChatSession')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentdictionary-2 'System\.Collections\.Concurrent\.ConcurrentDictionary\`2')

<a name='DiGi.Harness.WebAPI.Classes.ChatSessionStore.IdleTimeout'></a>

## ChatSessionStore\.IdleTimeout Property

Gets how long a session lives without a request\.

```csharp
public System.TimeSpan IdleTimeout { get; }
```

#### Property Value
[System\.TimeSpan](https://learn.microsoft.com/en-us/dotnet/api/system.timespan 'System\.TimeSpan')

<a name='DiGi.Harness.WebAPI.Classes.ChatSessionStore.MaxSessionsPerUser'></a>

## ChatSessionStore\.MaxSessionsPerUser Property

Gets how many sessions one user may hold\.

```csharp
public int MaxSessionsPerUser { get; }
```

#### Property Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.Harness.WebAPI.Classes.HarnessChatController'></a>

## HarnessChatController Class

The chat endpoints of the harness \(`harness/harnesschat`\): configuration, sessions, and one streamed turn per message\.

Every endpoint needs a signed-in user (resolved by the host's [IHarnessHost](DiGi.Harness.WebAPI.Interfaces.md#DiGi.Harness.WebAPI.Interfaces.IHarnessHost 'DiGi\.Harness\.WebAPI\.Interfaces\.IHarnessHost')) allowed by the configuration, and answers 401 otherwise; `configuration` instead answers `Enabled: false` so a client can hide its chat button. A disabled harness or an unusable provider answers 503. Once a message stream has started its status cannot change, so later failures arrive as [DiGi\.Harness\.Classes\.ErrorEvent](https://learn.microsoft.com/en-us/dotnet/api/digi.harness.classes.errorevent 'DiGi\.Harness\.Classes\.ErrorEvent')s followed by [DiGi\.Harness\.Classes\.TurnCompletedEvent](https://learn.microsoft.com/en-us/dotnet/api/digi.harness.classes.turncompletedevent 'DiGi\.Harness\.Classes\.TurnCompletedEvent').

```csharp
public class HarnessChatController : DiGi.WebAPI.Classes.WebAPIController
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [Microsoft\.AspNetCore\.Mvc\.ControllerBase](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.controllerbase 'Microsoft\.AspNetCore\.Mvc\.ControllerBase') → [DiGi\.WebAPI\.Classes\.WebAPIController](https://learn.microsoft.com/en-us/dotnet/api/digi.webapi.classes.webapicontroller 'DiGi\.WebAPI\.Classes\.WebAPIController') → HarnessChatController
### Constructors

<a name='DiGi.Harness.WebAPI.Classes.HarnessChatController.HarnessChatController(DiGi.Harness.Classes.HarnessConfiguration,DiGi.Harness.WebAPI.Classes.ChatSessionStore,DiGi.Harness.WebAPI.Interfaces.IHarnessHost,System.Net.Http.IHttpClientFactory,System.TimeProvider)'></a>

## HarnessChatController\(HarnessConfiguration, ChatSessionStore, IHarnessHost, IHttpClientFactory, TimeProvider\) Constructor

Initializes a new instance of the [HarnessChatController](DiGi.Harness.WebAPI.Classes.md#DiGi.Harness.WebAPI.Classes.HarnessChatController 'DiGi\.Harness\.WebAPI\.Classes\.HarnessChatController') class\.

```csharp
public HarnessChatController(DiGi.Harness.Classes.HarnessConfiguration harnessConfiguration, DiGi.Harness.WebAPI.Classes.ChatSessionStore chatSessionStore, DiGi.Harness.WebAPI.Interfaces.IHarnessHost harnessHost, System.Net.Http.IHttpClientFactory httpClientFactory, System.TimeProvider timeProvider);
```
#### Parameters

<a name='DiGi.Harness.WebAPI.Classes.HarnessChatController.HarnessChatController(DiGi.Harness.Classes.HarnessConfiguration,DiGi.Harness.WebAPI.Classes.ChatSessionStore,DiGi.Harness.WebAPI.Interfaces.IHarnessHost,System.Net.Http.IHttpClientFactory,System.TimeProvider).harnessConfiguration'></a>

`harnessConfiguration` [DiGi\.Harness\.Classes\.HarnessConfiguration](https://learn.microsoft.com/en-us/dotnet/api/digi.harness.classes.harnessconfiguration 'DiGi\.Harness\.Classes\.HarnessConfiguration')

The harness configuration\.

<a name='DiGi.Harness.WebAPI.Classes.HarnessChatController.HarnessChatController(DiGi.Harness.Classes.HarnessConfiguration,DiGi.Harness.WebAPI.Classes.ChatSessionStore,DiGi.Harness.WebAPI.Interfaces.IHarnessHost,System.Net.Http.IHttpClientFactory,System.TimeProvider).chatSessionStore'></a>

`chatSessionStore` [ChatSessionStore](DiGi.Harness.WebAPI.Classes.md#DiGi.Harness.WebAPI.Classes.ChatSessionStore 'DiGi\.Harness\.WebAPI\.Classes\.ChatSessionStore')

The session store\.

<a name='DiGi.Harness.WebAPI.Classes.HarnessChatController.HarnessChatController(DiGi.Harness.Classes.HarnessConfiguration,DiGi.Harness.WebAPI.Classes.ChatSessionStore,DiGi.Harness.WebAPI.Interfaces.IHarnessHost,System.Net.Http.IHttpClientFactory,System.TimeProvider).harnessHost'></a>

`harnessHost` [IHarnessHost](DiGi.Harness.WebAPI.Interfaces.md#DiGi.Harness.WebAPI.Interfaces.IHarnessHost 'DiGi\.Harness\.WebAPI\.Interfaces\.IHarnessHost')

The host's user, tool and prompt provider\.

<a name='DiGi.Harness.WebAPI.Classes.HarnessChatController.HarnessChatController(DiGi.Harness.Classes.HarnessConfiguration,DiGi.Harness.WebAPI.Classes.ChatSessionStore,DiGi.Harness.WebAPI.Interfaces.IHarnessHost,System.Net.Http.IHttpClientFactory,System.TimeProvider).httpClientFactory'></a>

`httpClientFactory` [System\.Net\.Http\.IHttpClientFactory](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.ihttpclientfactory 'System\.Net\.Http\.IHttpClientFactory')

The factory of the HTTP client used for model requests\.

<a name='DiGi.Harness.WebAPI.Classes.HarnessChatController.HarnessChatController(DiGi.Harness.Classes.HarnessConfiguration,DiGi.Harness.WebAPI.Classes.ChatSessionStore,DiGi.Harness.WebAPI.Interfaces.IHarnessHost,System.Net.Http.IHttpClientFactory,System.TimeProvider).timeProvider'></a>

`timeProvider` [System\.TimeProvider](https://learn.microsoft.com/en-us/dotnet/api/system.timeprovider 'System\.TimeProvider')

The clock for session activity and expiry\.
### Methods

<a name='DiGi.Harness.WebAPI.Classes.HarnessChatController.ConfigurationAsync(System.Threading.CancellationToken)'></a>

## HarnessChatController\.ConfigurationAsync\(CancellationToken\) Method

Returns whether the chat is available to the signed\-in user, the selectable providers \(name, display name and model; never a URI or key\) and the thinking visibility options\.

```csharp
public System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> ConfigurationAsync(System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.Harness.WebAPI.Classes.HarnessChatController.ConfigurationAsync(System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The token that cancels the request\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[Microsoft\.AspNetCore\.Mvc\.IActionResult](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.iactionresult 'Microsoft\.AspNetCore\.Mvc\.IActionResult')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
200 with the configuration \(`Enabled: false` when the harness is off or the user is not allowed\); 401 when nobody is signed in\.

<a name='DiGi.Harness.WebAPI.Classes.HarnessChatController.CreateSessionAsync(System.Text.Json.Nodes.JsonObject,System.Threading.CancellationToken)'></a>

## HarnessChatController\.CreateSessionAsync\(JsonObject, CancellationToken\) Method

Starts a chat session with a provider; the session stays locked to it\.

```csharp
public System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> CreateSessionAsync(System.Text.Json.Nodes.JsonObject? jsonObject, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.Harness.WebAPI.Classes.HarnessChatController.CreateSessionAsync(System.Text.Json.Nodes.JsonObject,System.Threading.CancellationToken).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The request body: `{"Provider": "name"}`; an empty name or body selects the default provider\.

<a name='DiGi.Harness.WebAPI.Classes.HarnessChatController.CreateSessionAsync(System.Text.Json.Nodes.JsonObject,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The token that cancels the request\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[Microsoft\.AspNetCore\.Mvc\.IActionResult](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.iactionresult 'Microsoft\.AspNetCore\.Mvc\.IActionResult')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
200 with `{"Id", "ProviderName", "DisplayName"}`; 400 for an unknown provider; 401; 503 when the harness is disabled or has no usable provider\.

<a name='DiGi.Harness.WebAPI.Classes.HarnessChatController.DeleteSessionAsync(string,System.Threading.CancellationToken)'></a>

## HarnessChatController\.DeleteSessionAsync\(string, CancellationToken\) Method

Ends a session and drops its conversation\.

```csharp
public System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> DeleteSessionAsync(string? id, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.Harness.WebAPI.Classes.HarnessChatController.DeleteSessionAsync(string,System.Threading.CancellationToken).id'></a>

`id` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The session id\.

<a name='DiGi.Harness.WebAPI.Classes.HarnessChatController.DeleteSessionAsync(string,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The token that cancels the request\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[Microsoft\.AspNetCore\.Mvc\.IActionResult](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.iactionresult 'Microsoft\.AspNetCore\.Mvc\.IActionResult')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
204 when removed; 401; 404 for an unknown, foreign or expired session; 503 when the harness is disabled\.

<a name='DiGi.Harness.WebAPI.Classes.HarnessChatController.MessageAsync(string,System.Text.Json.Nodes.JsonObject,System.Threading.CancellationToken)'></a>

## HarnessChatController\.MessageAsync\(string, JsonObject, CancellationToken\) Method

Sends a user message and streams one turn of the agent loop as `text/event-stream`: one compact JSON event per `data:` line, the serialized type name in `_type`, ending with [DiGi\.Harness\.Classes\.TurnCompletedEvent](https://learn.microsoft.com/en-us/dotnet/api/digi.harness.classes.turncompletedevent 'DiGi\.Harness\.Classes\.TurnCompletedEvent')\.

Thinking is filtered on the server by `ThinkingVisibility` ([VisibleHarnessEvent\(this HarnessEvent, ThinkingVisibility\)](DiGi.Harness.WebAPI.md#DiGi.Harness.WebAPI.Query.VisibleHarnessEvent(thisDiGi.Harness.Classes.HarnessEvent,DiGi.Harness.Enums.ThinkingVisibility) 'DiGi\.Harness\.WebAPI\.Query\.VisibleHarnessEvent\(this DiGi\.Harness\.Classes\.HarnessEvent, DiGi\.Harness\.Enums\.ThinkingVisibility\)')), so `None` sends no thinking at all. Closing the connection cancels the turn. A comment line every few seconds keeps the stream open through proxies while a tool runs.

```csharp
public System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> MessageAsync(string? id, System.Text.Json.Nodes.JsonObject? jsonObject, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.Harness.WebAPI.Classes.HarnessChatController.MessageAsync(string,System.Text.Json.Nodes.JsonObject,System.Threading.CancellationToken).id'></a>

`id` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The session id\.

<a name='DiGi.Harness.WebAPI.Classes.HarnessChatController.MessageAsync(string,System.Text.Json.Nodes.JsonObject,System.Threading.CancellationToken).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The request body: `{"Text": "...", "ThinkingVisibility": "Full" | "Summary" | "None"}`\.

<a name='DiGi.Harness.WebAPI.Classes.HarnessChatController.MessageAsync(string,System.Text.Json.Nodes.JsonObject,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The token that cancels the turn; ASP\.NET signals it when the client disconnects\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[Microsoft\.AspNetCore\.Mvc\.IActionResult](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.iactionresult 'Microsoft\.AspNetCore\.Mvc\.IActionResult')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
The event stream; before it starts: 400 for empty or too long text, 401, 404 for an unknown session, 409 when a turn of the session is already running, 503 when the harness is disabled or the session's provider is no longer usable\.

<a name='DiGi.Harness.WebAPI.Classes.HarnessChatController.SessionAsync(string,System.Nullable_DiGi.Harness.Enums.ThinkingVisibility_,System.Threading.CancellationToken)'></a>

## HarnessChatController\.SessionAsync\(string, Nullable\<ThinkingVisibility\>, CancellationToken\) Method

Returns the transcript of a session, with thinking filtered by the requested visibility and no signatures\.

```csharp
public System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> SessionAsync(string? id, System.Nullable<DiGi.Harness.Enums.ThinkingVisibility> thinkingVisibility=null, System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='DiGi.Harness.WebAPI.Classes.HarnessChatController.SessionAsync(string,System.Nullable_DiGi.Harness.Enums.ThinkingVisibility_,System.Threading.CancellationToken).id'></a>

`id` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The session id\.

<a name='DiGi.Harness.WebAPI.Classes.HarnessChatController.SessionAsync(string,System.Nullable_DiGi.Harness.Enums.ThinkingVisibility_,System.Threading.CancellationToken).thinkingVisibility'></a>

`thinkingVisibility` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[DiGi\.Harness\.Enums\.ThinkingVisibility](https://learn.microsoft.com/en-us/dotnet/api/digi.harness.enums.thinkingvisibility 'DiGi\.Harness\.Enums\.ThinkingVisibility')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The thinking visibility: `Full`, `Summary` \(default\) or `None`\.

<a name='DiGi.Harness.WebAPI.Classes.HarnessChatController.SessionAsync(string,System.Nullable_DiGi.Harness.Enums.ThinkingVisibility_,System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System\.Threading\.CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken 'System\.Threading\.CancellationToken')

The token that cancels the request\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[Microsoft\.AspNetCore\.Mvc\.IActionResult](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.iactionresult 'Microsoft\.AspNetCore\.Mvc\.IActionResult')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
200 with the conversation; 401; 404 for an unknown, foreign or expired session; 503 when the harness is disabled\.

<a name='DiGi.Harness.WebAPI.Classes.HarnessUser'></a>

## HarnessUser Class

The signed\-in user of a chat request, as resolved by the host\.

```csharp
public class HarnessUser : DiGi.Harness.WebAPI.Interfaces.IHarnessWebAPIObject, DiGi.Core.Interfaces.IObject
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → HarnessUser

Implements [IHarnessWebAPIObject](DiGi.Harness.WebAPI.Interfaces.md#DiGi.Harness.WebAPI.Interfaces.IHarnessWebAPIObject 'DiGi\.Harness\.WebAPI\.Interfaces\.IHarnessWebAPIObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject')
### Constructors

<a name='DiGi.Harness.WebAPI.Classes.HarnessUser.HarnessUser(string,string,string)'></a>

## HarnessUser\(string, string, string\) Constructor

Initializes a new instance of the [HarnessUser](DiGi.Harness.WebAPI.Classes.md#DiGi.Harness.WebAPI.Classes.HarnessUser 'DiGi\.Harness\.WebAPI\.Classes\.HarnessUser') class\.

```csharp
public HarnessUser(string id, string? displayName, string? credential);
```
#### Parameters

<a name='DiGi.Harness.WebAPI.Classes.HarnessUser.HarnessUser(string,string,string).id'></a>

`id` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The stable user id; sessions are owned by it\.

<a name='DiGi.Harness.WebAPI.Classes.HarnessUser.HarnessUser(string,string,string).displayName'></a>

`displayName` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The name shown to the user, or null\.

<a name='DiGi.Harness.WebAPI.Classes.HarnessUser.HarnessUser(string,string,string).credential'></a>

`credential` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The credential the host's tools act with \(for example the user's bearer token\), or null; never logged or returned to a client\.
### Properties

<a name='DiGi.Harness.WebAPI.Classes.HarnessUser.Credential'></a>

## HarnessUser\.Credential Property

Gets the credential the host's tools act with, or null; never logged or returned to a client\.

```csharp
public string? Credential { get; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.Harness.WebAPI.Classes.HarnessUser.DisplayName'></a>

## HarnessUser\.DisplayName Property

Gets the name shown to the user, or null\.

```csharp
public string? DisplayName { get; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.Harness.WebAPI.Classes.HarnessUser.Id'></a>

## HarnessUser\.Id Property

Gets the stable user id; sessions are owned by it\.

```csharp
public string Id { get; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')