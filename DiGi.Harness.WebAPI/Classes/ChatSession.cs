using DiGi.Harness.Classes;
using DiGi.Harness.WebAPI.Interfaces;
using System;
using System.Threading;

namespace DiGi.Harness.WebAPI.Classes
{
    /// <summary>
    /// One chat session: its owner, its conversation (locked to one provider) and the gate that lets one turn run at a time.
    /// </summary>
    public class ChatSession : IHarnessWebAPIObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChatSession"/> class.
        /// </summary>
        /// <param name="id">The session id.</param>
        /// <param name="ownerId">The id of the user who owns the session.</param>
        /// <param name="conversation">The conversation, locked to one provider.</param>
        /// <param name="createdAt">The creation time.</param>
        public ChatSession(string id, string ownerId, Conversation conversation, DateTimeOffset createdAt)
        {
            Id = id;
            OwnerId = ownerId;
            Conversation = conversation;
            CreatedAt = createdAt;
            LastActivity = createdAt;
        }

        /// <summary>
        /// Gets the session id.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Gets the id of the user who owns the session.
        /// </summary>
        public string OwnerId { get; }

        /// <summary>
        /// Gets the conversation, locked to one provider.
        /// </summary>
        public Conversation Conversation { get; }

        /// <summary>
        /// Gets the creation time.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Gets or sets the time of the last request on the session; the idle timeout counts from it.
        /// </summary>
        public DateTimeOffset LastActivity { get; set; }

        /// <summary>
        /// Gets the gate that lets one turn run at a time; a second request while a turn runs answers 409.
        /// </summary>
        public SemaphoreSlim SemaphoreSlim { get; } = new(1, 1);
    }
}
