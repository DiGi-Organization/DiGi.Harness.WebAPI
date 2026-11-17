using DiGi.Harness.WebAPI.Classes;
using System;
using System.Collections.Generic;

namespace DiGi.Harness.WebAPI
{
    public static partial class Modify
    {
        /// <summary>
        /// Removes every session idle for longer than <see cref="ChatSessionStore.IdleTimeout"/>, except one whose turn is still running.
        /// </summary>
        /// <param name="chatSessionStore">The store.</param>
        /// <param name="now">The current time.</param>
        /// <returns>The number of sessions removed.</returns>
        public static int RemoveExpired(this ChatSessionStore? chatSessionStore, DateTimeOffset now)
        {
            if (chatSessionStore is null)
            {
                return 0;
            }

            int result = 0;
            foreach (KeyValuePair<string, ChatSession> keyValuePair in chatSessionStore.ChatSessions)
            {
                ChatSession chatSession = keyValuePair.Value;
                if (chatSession.LastActivity + chatSessionStore.IdleTimeout >= now || chatSession.SemaphoreSlim.CurrentCount == 0)
                {
                    continue;
                }

                if (((ICollection<KeyValuePair<string, ChatSession>>)chatSessionStore.ChatSessions).Remove(keyValuePair))
                {
                    result++;
                }
            }

            return result;
        }
    }
}
