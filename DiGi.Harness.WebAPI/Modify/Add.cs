using DiGi.Harness.WebAPI.Classes;
using System.Collections.Generic;
using System.Linq;

namespace DiGi.Harness.WebAPI
{
    public static partial class Modify
    {
        /// <summary>
        /// Adds a session to the store; when its owner then holds more than <see cref="ChatSessionStore.MaxSessionsPerUser"/> sessions, removes that owner's least recently used ones.
        /// </summary>
        /// <param name="chatSessionStore">The store.</param>
        /// <param name="chatSession">The session to add.</param>
        /// <returns>True when the session was added.</returns>
        public static bool Add(this ChatSessionStore? chatSessionStore, ChatSession? chatSession)
        {
            if (chatSessionStore is null || chatSession is null)
            {
                return false;
            }

            if (!chatSessionStore.ChatSessions.TryAdd(chatSession.Id, chatSession))
            {
                return false;
            }

            int maxSessionsPerUser = chatSessionStore.MaxSessionsPerUser > 0 ? chatSessionStore.MaxSessionsPerUser : Constants.Default.MaxSessionsPerUser;

            List<ChatSession> chatSessions_Owner = [.. chatSessionStore.ChatSessions.Values
                .Where(x => x.OwnerId == chatSession.OwnerId && x.Id != chatSession.Id)
                .OrderBy(x => x.LastActivity)];

            int count_Remove = chatSessions_Owner.Count + 1 - maxSessionsPerUser;
            for (int i = 0; i < count_Remove && i < chatSessions_Owner.Count; i++)
            {
                chatSessionStore.ChatSessions.TryRemove(chatSessions_Owner[i].Id, out _);
            }

            return true;
        }
    }
}
