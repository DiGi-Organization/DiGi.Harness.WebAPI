using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DiGi.Harness.WebAPI
{
    public static partial class Modify
    {
        /// <summary>
        /// Writes one server-sent event to a response and flushes it, so the client receives it at once.
        /// <para>A <paramref name="data"/> of null writes a comment line (<c>: keep-alive</c>) that keeps an idle stream open through proxies. Data must be a single line (compact JSON).</para>
        /// </summary>
        /// <param name="httpResponse">The response, already started with <c>text/event-stream</c>.</param>
        /// <param name="data">The event data, or null for a keep-alive comment.</param>
        /// <param name="cancellationToken">The token that cancels the write.</param>
        /// <returns>A task that completes when the event is flushed.</returns>
        public static async Task WriteServerSentEventAsync(this HttpResponse httpResponse, string? data, CancellationToken cancellationToken = default)
        {
            if (httpResponse is null)
            {
                return;
            }

            await httpResponse.WriteAsync(data is null ? ": keep-alive\n\n" : string.Concat("data: ", data, "\n\n"), cancellationToken).ConfigureAwait(false);
            await httpResponse.Body.FlushAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
