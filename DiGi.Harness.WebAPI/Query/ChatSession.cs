using DiGi.Harness.WebAPI.Classes;
using System;

namespace DiGi.Harness.WebAPI
{
    public static partial class Query
    {
        /// <summary>
        /// Finds a session owned by a user that has not expired.
        /// <para>A session of another user answers like a missing one, so a client cannot tell foreign session ids from unknown ones.</para>
        /// </summary>
        /// <param name="chatSessionStore">The store.</param>
        /// <param name="id">The session id.</param>
        /// <param name="ownerId">The id of the requesting user.</param>
        /// <param name="now">The current time, compared with <see cref="Classes.ChatSession.LastActivity"/> plus <see cref="ChatSessionStore.IdleTimeout"/>.</param>
        /// <returns>The session, or null when it is missing, foreign or expired.</returns>
        public static Classes.ChatSession? ChatSession(this ChatSessionStore? chatSessionStore, string? id, string? ownerId, DateTimeOffset now)
        {
            if (chatSessionStore is null || string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(ownerId))
            {
                return null;
            }

            if (!chatSessionStore.ChatSessions.TryGetValue(id, out Classes.ChatSession? chatSession))
            {
                return null;
            }

            if (!string.Equals(chatSession.OwnerId, ownerId, StringComparison.Ordinal))
            {
                return null;
            }

            if (chatSession.LastActivity + chatSessionStore.IdleTimeout < now)
            {
                return null;
            }

            return chatSession;
        }
    }
}
