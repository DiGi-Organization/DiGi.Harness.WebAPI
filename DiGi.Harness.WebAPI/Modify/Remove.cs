using DiGi.Harness.WebAPI.Classes;
using System;
using System.Collections.Generic;

namespace DiGi.Harness.WebAPI
{
    public static partial class Modify
    {
        /// <summary>
        /// Removes a session owned by a user.
        /// </summary>
        /// <param name="chatSessionStore">The store.</param>
        /// <param name="id">The session id.</param>
        /// <param name="ownerId">The id of the requesting user; a session of another user is not removed.</param>
        /// <returns>True when the session was removed.</returns>
        public static bool Remove(this ChatSessionStore? chatSessionStore, string? id, string? ownerId)
        {
            if (chatSessionStore is null || string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(ownerId))
            {
                return false;
            }

            if (!chatSessionStore.ChatSessions.TryGetValue(id, out ChatSession? chatSession) || !string.Equals(chatSession.OwnerId, ownerId, StringComparison.Ordinal))
            {
                return false;
            }

            return ((ICollection<KeyValuePair<string, ChatSession>>)chatSessionStore.ChatSessions).Remove(new KeyValuePair<string, ChatSession>(id, chatSession));
        }
    }
}
