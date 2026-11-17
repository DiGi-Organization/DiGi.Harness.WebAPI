using DiGi.Harness.WebAPI.Interfaces;
using System;
using System.Collections.Concurrent;

namespace DiGi.Harness.WebAPI.Classes
{
    /// <summary>
    /// The in-memory chat sessions of a host, keyed by session id.
    /// <para>Sessions are lost when the application restarts. Read and change them through <see cref="Query.ChatSession(ChatSessionStore?, string?, string?, DateTimeOffset)"/>, <see cref="Modify.Add(ChatSessionStore?, ChatSession?)"/> and <see cref="Modify.Remove(ChatSessionStore?, string?, string?)"/>, which also drop expired sessions.</para>
    /// </summary>
    public class ChatSessionStore : IHarnessWebAPIObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChatSessionStore"/> class.
        /// </summary>
        /// <param name="idleTimeout">How long a session lives without a request.</param>
        /// <param name="maxSessionsPerUser">How many sessions one user may hold; creating one more removes that user's least recently used session.</param>
        public ChatSessionStore(TimeSpan idleTimeout, int maxSessionsPerUser)
        {
            IdleTimeout = idleTimeout;
            MaxSessionsPerUser = maxSessionsPerUser;
        }

        /// <summary>
        /// Gets the sessions keyed by session id.
        /// </summary>
        public ConcurrentDictionary<string, ChatSession> ChatSessions { get; } = new(StringComparer.Ordinal);

        /// <summary>
        /// Gets how long a session lives without a request.
        /// </summary>
        public TimeSpan IdleTimeout { get; }

        /// <summary>
        /// Gets how many sessions one user may hold.
        /// </summary>
        public int MaxSessionsPerUser { get; }
    }
}
