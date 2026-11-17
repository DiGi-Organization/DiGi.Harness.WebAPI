using DiGi.Harness.WebAPI.Interfaces;

namespace DiGi.Harness.WebAPI.Classes
{
    /// <summary>
    /// The signed-in user of a chat request, as resolved by the host.
    /// </summary>
    public class HarnessUser : IHarnessWebAPIObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HarnessUser"/> class.
        /// </summary>
        /// <param name="id">The stable user id; sessions are owned by it.</param>
        /// <param name="displayName">The name shown to the user, or null.</param>
        /// <param name="credential">The credential the host's tools act with (for example the user's bearer token), or null; never logged or returned to a client.</param>
        public HarnessUser(string id, string? displayName, string? credential)
        {
            Id = id;
            DisplayName = displayName;
            Credential = credential;
        }

        /// <summary>
        /// Gets the stable user id; sessions are owned by it.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Gets the name shown to the user, or null.
        /// </summary>
        public string? DisplayName { get; }

        /// <summary>
        /// Gets the credential the host's tools act with, or null; never logged or returned to a client.
        /// </summary>
        public string? Credential { get; }
    }
}
