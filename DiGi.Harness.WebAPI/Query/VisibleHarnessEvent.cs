using DiGi.Harness.Classes;
using DiGi.Harness.Enums;

namespace DiGi.Harness.WebAPI
{
    public static partial class Query
    {
        /// <summary>
        /// Filters one turn event by the client's thinking visibility, before it leaves the server.
        /// <para><see cref="ThinkingVisibility.Full"/> passes thinking deltas and drops the completed-block event (the client already has the text). <see cref="ThinkingVisibility.Summary"/> (also for <see cref="ThinkingVisibility.Undefined"/>) drops the deltas and turns each completed block into one <see cref="ThinkingSummaryEvent"/> line. <see cref="ThinkingVisibility.None"/> drops all thinking. Every other event passes unchanged.</para>
        /// </summary>
        /// <param name="harnessEvent">The event.</param>
        /// <param name="thinkingVisibility">The client's thinking visibility.</param>
        /// <returns>The event to send, or null to send nothing.</returns>
        public static HarnessEvent? VisibleHarnessEvent(this HarnessEvent? harnessEvent, ThinkingVisibility thinkingVisibility)
        {
            switch (harnessEvent)
            {
                case null:
                    return null;

                case ThinkingDeltaEvent:
                    return thinkingVisibility == ThinkingVisibility.Full ? harnessEvent : null;

                case ThinkingCompletedEvent thinkingCompletedEvent:
                    if (thinkingVisibility == ThinkingVisibility.Full || thinkingVisibility == ThinkingVisibility.None)
                    {
                        return null;
                    }

                    string? text = Harness.Query.SummaryLine(thinkingCompletedEvent.Text, Constants.Default.MaxThinkingSummaryCharacters);
                    return text is null ? null : new ThinkingSummaryEvent(text, thinkingCompletedEvent.ElapsedMilliseconds);

                case ThinkingSummaryEvent:
                    return thinkingVisibility == ThinkingVisibility.None || thinkingVisibility == ThinkingVisibility.Full ? null : harnessEvent;

                default:
                    return harnessEvent;
            }
        }
    }
}
