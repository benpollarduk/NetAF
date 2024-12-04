using NetAF.Conversations;
using System.Linq;

namespace NetAF.Serialization.Assets
{
    /// <summary>
    /// Represents a serialization of a Conversation.
    /// </summary>
    public sealed class ConversationSerialization : IObjectSerialization<Conversation>
    {
        #region Constants

        /// <summary>
        /// Get the value for no current paragraph.
        /// </summary>
        public const int NoCurrentParagraph = -1;

        #endregion

        #region Properties

        /// <summary>
        /// Get or set if the index of the current paragraph.
        /// </summary>
        public int CurrentParagraph { get; set; }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Create a new serialization from a Conversation.
        /// </summary>
        /// <param name="conversation">The Conversation to create the serialization from.</param>
        /// <returns>The serialization.</returns>
        public static ConversationSerialization FromConversation(Conversation conversation)
        {
            return new()
            {
                CurrentParagraph = conversation?.Paragraphs?.ToList().IndexOf(conversation.CurrentParagraph) ?? NoCurrentParagraph
            };
        }

        #endregion

        #region Implementation of IObjectSerialization<Conversation>

        /// <summary>
        /// Restore an instance from this serialization.
        /// </summary>
        /// <param name="conversation">The conversation to restore.</param>
        void IObjectSerialization<Conversation>.Restore(Conversation conversation)
        {
            ((IRestoreFromObjectSerialization<ConversationSerialization>)conversation).RestoreFrom(this);
        }

        #endregion
    }
}
