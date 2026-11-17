using DiGi.Harness.Interfaces;
using DiGi.Harness.WebAPI.Classes;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.Harness.WebAPI.Interfaces
{
    /// <summary>
    /// What a web host provides to the harness: who the user is, which tools that user gets, and the system prompt.
    /// <para>Everything project-specific lives behind this interface, in the host (for example <c>GISHarnessHost</c> in DiGi.GIS.WebAPI.UI); DiGi.Harness.WebAPI holds none of it. The host registers its implementation as a singleton after calling <see cref="Modify.InitializeAsync(Microsoft.Extensions.DependencyInjection.IServiceCollection)"/>.</para>
    /// </summary>
    public interface IHarnessHost : IHarnessWebAPIObject
    {
        /// <summary>
        /// Resolves the signed-in user of a request.
        /// </summary>
        /// <param name="httpContext">The request.</param>
        /// <param name="cancellationToken">The token that cancels the lookup.</param>
        /// <returns>The user, or null when the request is not signed in; the chat endpoints then answer 401.</returns>
        Task<HarnessUser?> UserAsync(HttpContext httpContext, CancellationToken cancellationToken = default);

        /// <summary>
        /// Creates the tools offered to the model for one turn of a user's request, bound to that user's credentials.
        /// </summary>
        /// <param name="httpContext">The request.</param>
        /// <param name="harnessUser">The signed-in user.</param>
        /// <param name="cancellationToken">The token that cancels the setup.</param>
        /// <returns>The tools; empty for a plain chat.</returns>
        Task<IReadOnlyList<ITool>> ToolsAsync(HttpContext httpContext, HarnessUser harnessUser, CancellationToken cancellationToken = default);

        /// <summary>
        /// Builds the system prompt sent with every request of a turn.
        /// <para>Keep it stable between turns: it is the cached prefix of every Anthropic request.</para>
        /// </summary>
        /// <param name="cancellationToken">The token that cancels the build.</param>
        /// <returns>The system prompt, or null for none.</returns>
        Task<string?> SystemPromptAsync(CancellationToken cancellationToken = default);
    }
}
