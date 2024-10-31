using NetAF.Conversations;
using System.Linq;

namespace NetAF.Serialization.Assets
{
    /// <summary>
    /// Represents a serialization of a Conversation.
    /// </summary>
    /// <param name="conversation">The conversation to serialize.</param>
    public sealed class ConversationSerialization(Conversation conversation) : IObjectSerialization<Conversation>
    {
        #region Constants

        /// <summary>
        /// Get the value for no current paragraph.
        /// </summary>
        public const int NoCurrentParagraph = -1;

        #endregion

        #region Properties

        /// <summary>
        /// Get if the index of the current paragraph.
        /// </summary>
        public readonly int CurrentParagraph = conversation?.Paragraphs?.ToList()?.IndexOf(conversation.CurrentParagraph) ?? NoCurrentParagraph;

        #endregion

        #region Implementation of IObjectSerialization<Conversation>

        /// <summary>
        /// Restore an instance from this serialization.
        /// </summary>
        /// <param name="conversation">The conversation to restore.</param>
        public void Restore(Conversation conversation)
        {
            conversation.RestoreFrom(this);
        }

        #endregion
    }
}
