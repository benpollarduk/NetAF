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

        /// <inheritdoc/>
        public CommandHelp Help => CommandHelp;

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public Prompt[] GetPrompts(Game game)
        {
            return [];
        }

        #endregion
    }
}