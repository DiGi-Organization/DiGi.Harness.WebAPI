namespace DiGi.Harness.WebAPI.Constants
{
    /// <summary>
    /// Defaults of the harness web layer.
    /// </summary>
    public static class Default
    {
        /// <summary>
        /// The name of the <see cref="System.Net.Http.IHttpClientFactory"/> client used for model requests.
        /// </summary>
        public const string HttpClientName = "DiGi.Harness";

        /// <summary>
        /// How long a chat session lives without a request, in minutes.
        /// </summary>
        public const int SessionIdleTimeoutMinutes = 120;

        /// <summary>
        /// How many chat sessions one user may hold.
        /// </summary>
        public const int MaxSessionsPerUser = 5;

        /// <summary>
        /// The maximum length of one user message, in characters.
        /// </summary>
        public const int MaxMessageCharacters = 8000;

        /// <summary>
        /// The maximum length of a thinking summary line, in characters.
        /// </summary>
        public const int MaxThinkingSummaryCharacters = 200;

        /// <summary>
        /// How often a comment line keeps an idle event stream open through proxies, in seconds.
        /// </summary>
        public const int KeepAliveSeconds = 15;
    }
}
