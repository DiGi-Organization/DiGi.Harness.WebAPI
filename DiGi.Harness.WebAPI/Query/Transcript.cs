using DiGi.Harness.Classes;
using DiGi.Harness.Enums;
using DiGi.Harness.Interfaces;
using System.Collections.Generic;

namespace DiGi.Harness.WebAPI
{
    public static partial class Query
    {
        /// <summary>
        /// Builds the copy of a conversation sent to a client that reloads a session.
        /// <para>Thinking follows the client's visibility: full text for <see cref="ThinkingVisibility.Full"/>, one summary line for <see cref="ThinkingVisibility.Summary"/> (and <see cref="ThinkingVisibility.Undefined"/>), nothing for <see cref="ThinkingVisibility.None"/>. Signatures and redacted thinking data never leave the server. Text, tool calls and tool results are copied.</para>
        /// </summary>
        /// <param name="conversation">The conversation.</param>
        /// <param name="thinkingVisibility">The client's thinking visibility.</param>
        /// <returns>The filtered copy, or null when <paramref name="conversation"/> is null.</returns>
        public static Conversation? Transcript(this Conversation? conversation, ThinkingVisibility thinkingVisibility)
        {
            if (conversation is null)
            {
                return null;
            }

            List<Message> messages = [];
            foreach (Message message in conversation.Messages)
            {
                List<IContent> contents = [];
                foreach (IContent content in message.Contents ?? [])
                {
                    if (content is not ThinkingContent thinkingContent)
                    {
                        if (Core.Query.Clone(content) is IContent content_Clone)
                        {
                            contents.Add(content_Clone);
                        }

                        continue;
                    }

                    string? text = thinkingVisibility switch
                    {
                        ThinkingVisibility.Full => thinkingContent.Text,
                        ThinkingVisibility.None => null,
                        _ => Harness.Query.SummaryLine(thinkingContent.Text, Constants.Default.MaxThinkingSummaryCharacters),
                    };

                    if (!string.IsNullOrWhiteSpace(text))
                    {
                        contents.Add(new ThinkingContent(text, null, null));
                    }
                }

                messages.Add(new Message(message.Role, contents));
            }

            return new Conversation(conversation.ProviderName, messages);
        }
    }
}
