using NetAF.Assets.Interaction;
using NetAF.Logic.Modes;

namespace NetAF.Commands.Conversation
{
    /// <summary>
    /// Represents the Next command.
    /// </summary>
    public sealed class Next : ICommand
    {
        #region Implementation of ICommand

        /// <summary>
        /// Invoke the command.
        /// </summary>
        /// <param name="game">The game to invoke the command on.</param>
        /// <returns>The reaction.</returns>
        public Reaction Invoke(Logic.Game game)
        {
            if (game == null)
                return new(ReactionResult.Error, "No game specified.");

            var mode = game.Mode as ConversationMode;

            if (mode?.Converser == null)
                return new(ReactionResult.Error, "No converser.");

            if (mode.Converser.Conversation == null)
                return new(ReactionResult.Error, "No conversation.");

            return mode.Converser.Conversation.Next(game);
        }

        #endregion
    }
}