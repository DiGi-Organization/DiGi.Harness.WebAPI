using DiGi.Harness.Classes;
using DiGi.Harness.WebAPI.Classes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Threading.Tasks;

namespace DiGi.Harness.WebAPI
{
    public static partial class Modify
    {
        /// <summary>
        /// Registers the harness services: the <see cref="HarnessConfiguration"/> loaded from <c>Harness.json</c>, the in-memory <see cref="ChatSessionStore"/>, <see cref="TimeProvider.System"/> when none is registered, and the named HTTP client used for model requests (its timeout from the configuration).
        /// <para>The host also registers its <see cref="Interfaces.IHarnessHost"/>; without one the chat controller cannot be activated. Found by reflection by DiGi.WebAPI.WindowsService (a type named <c>Modify</c> with a public static <c>InitializeAsync(IServiceCollection)</c>).</para>
        /// </summary>
        /// <param name="serviceCollection">The service collection of the host.</param>
        /// <returns>A task that completes when the services are registered.</returns>
        public static async Task InitializeAsync(this IServiceCollection serviceCollection)
        {
            if (serviceCollection is null)
            {
                return;
            }

            HarnessConfiguration harnessConfiguration = Harness.Create.HarnessConfiguration();
            int requestTimeoutSeconds = harnessConfiguration.RequestTimeoutSeconds > 0 ? harnessConfiguration.RequestTimeoutSeconds : Harness.Constants.Default.RequestTimeoutSeconds;

            serviceCollection.TryAddSingleton(harnessConfiguration);
            serviceCollection.TryAddSingleton(new ChatSessionStore(TimeSpan.FromMinutes(Constants.Default.SessionIdleTimeoutMinutes), Constants.Default.MaxSessionsPerUser));
            serviceCollection.TryAddSingleton(TimeProvider.System);
            serviceCollection.AddHttpClient(Constants.Default.HttpClientName, httpClient => httpClient.Timeout = TimeSpan.FromSeconds(requestTimeoutSeconds));

            await Task.CompletedTask;
        }
    }
}
