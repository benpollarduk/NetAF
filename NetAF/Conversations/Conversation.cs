using System.Linq;
using NetAF.Extensions;
using NetAF.Logic;
using NetAF.Serialization.Assets;
using NetAF.Serialization;
using NetAF.Commands;

namespace NetAF.Conversations
{
    /// <summary>
    /// Represents a conversation.
    /// </summary>
    /// <param name="paragraphs">The paragraphs.</param>
    public sealed class Conversation(params Paragraph[] paragraphs) : IRestoreFromObjectSerialization<ConversationSerialization>
    {
        #region Fields

        private Response selectedResponse;

        #endregion

        #region Properties

        /// <summary>
        /// Get the current paragraph in the conversation.
        /// </summary>
        public Paragraph CurrentParagraph { get; private set; }

        /// <summary>
        /// Get the current paragraph in the conversation.
        /// </summary>
        public Paragraph[] Paragraphs { get; } = paragraphs;

        /// <summary>
        /// Get the log.
        /// </summary>
        public LogItem[] Log { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Shift the conversation to a new paragraph.
        /// </summary>
        /// <param name="index">The index of the paragraph to shift to.</param>
        /// <returns>The new paragraph.</returns>
        private Paragraph Shift(int index)
        {
            return index >= 0 && index < Paragraphs.Length ? Paragraphs[index] : CurrentParagraph;
        }

        /// <summary>
        /// Trigger the next line in this conversation.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns>The reaction to the line.</returns>
        public Reaction Next(Game game)
        {
            if (Paragraphs == null || !Paragraphs.Any())
                return new(ReactionResult.Silent, "No paragraphs.");

            var entryParagraph = CurrentParagraph;

            if (CurrentParagraph?.CanRespond ?? false)
            {
                if (selectedResponse != null)
                {
                    CurrentParagraph = Shift(selectedResponse.Instruction.GetIndexOfNext(CurrentParagraph, Paragraphs));
                    selectedResponse = null;
                }
                else
                {
                    return new(ReactionResult.Silent, "Awaiting response.");
                }
            }
            else if (CurrentParagraph == null)
            {
                CurrentParagraph = Paragraphs[0];
            }
            else
            {
                CurrentParagraph = Shift(CurrentParagraph.Instruction.GetIndexOfNext(CurrentParagraph, Paragraphs));
            }

            if ((CurrentParagraph == null) || (CurrentParagraph == entryParagraph))
                return new(ReactionResult.Silent, "End of conversation.");

            CurrentParagraph.Action?.Invoke(game);

            var line = CurrentParagraph.Line.ToSpeech();
            Log = Log.Add(new(Participant.Other, line));

            return new(ReactionResult.Silent, line);
        }

        /// <summary>
        /// Respond to the conversation.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="game">The game.</param>
        /// <returns>The reaction to the response.</returns>
        public Reaction Respond(Response response, Game game)
        {
            if (response == null)
                return new(ReactionResult.Error, "No response.");

            if (CurrentParagraph == null)
                return new(ReactionResult.Error, "No paragraph.");

            if (!CurrentParagraph.Responses?.Contains(response) ?? true)
                return new(ReactionResult.Error, "Invalid response.");

            Log = Log.Add(new(Participant.Player, response.Line.EnsureFinishedSentence().ToSpeech()));

            selectedResponse = response;

            return Next(game);
        }

        #endregion

        #region Implementation of IRestoreFromObjectSerialization<Conversation>

        /// <summary>
        /// Restore this object from a serialization.
        /// </summary>
        /// <param name="serialization">The serialization to restore from.</param>
        void IRestoreFromObjectSerialization<ConversationSerialization>.RestoreFrom(ConversationSerialization serialization)
        {
            if (serialization.CurrentParagraph == ConversationSerialization.NoCurrentParagraph)
                CurrentParagraph = null;
            else
                CurrentParagraph = Paragraphs[serialization.CurrentParagraph];
        }

        #endregion
    }
}
