using NetAF.Logic;
using NetAF.Logic.Modes;

namespace NetAF.Commands.Conversation
{
    /// <summary>
    /// Represents the Next command.
    /// </summary>
    public sealed class Next : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help.
        /// </summary>
        public static CommandHelp CommandHelp { get; } = new("Next", "Continue the conversation", CommandCategory.Conversation);

        /// <summary>
        /// Get the command help.
        /// </summary>
        internal static CommandHelp SilentCommandHelp { get; } = new(string.Empty, "Continue the conversation", CommandCategory.Conversation);

        #endregion

        #region Implementation of ICommand

        /// <summary>
        /// Get the help for this command.
        /// </summary>
        public CommandHelp Help => CommandHelp;

        /// <summary>
        /// Invoke the command.
        /// </summary>
        /// <param name="game">The game to invoke the command on.</param>
        /// <returns>The reaction.</returns>
        public Reaction Invoke(Game game)
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

        /// <summary>
        /// Get all prompts for this command.
        /// </summary>
        /// <param name="game">The game to get the prompts for.</param>
        /// <returns>And array of prompts.</returns>
        public Prompt[] GetPrompts(Game game)
        {
            return [];
        }

        #endregion
    }
}