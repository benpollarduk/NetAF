using NetAF.Conversations;
using NetAF.Logic;
using NetAF.Logic.Modes;

namespace NetAF.Commands.Conversation
{
    /// <summary>
    /// Represents the Respond command.
    /// </summary>
    /// <param name="response">The response.</param>
    public sealed class Respond(Response response) : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help.
        /// </summary>
        private static CommandHelp SilentCommandHelp { get; } = new(string.Empty, "Respond to the conversation", CommandCategory.Conversation);

        #endregion

        #region Implementation of ICommand

        /// <inheritdoc/>
        public CommandHelp Help => SilentCommandHelp;

        /// <inheritdoc/>
        public Reaction Invoke(Game game)
        {
            if (game == null)
                return new(ReactionResult.Error, "No game specified.");

            if (response == null)
                return new(ReactionResult.Error, "No response specified.");

            var mode = game.Mode as ConversationMode;

            if (mode?.Converser?.Conversation == null)
                return new(ReactionResult.Error, "No active conversation.");

            return mode.Converser.Conversation.Respond(response, game);
        }

        /// <inheritdoc/>
        public Prompt[] GetPrompts(Game game)
        {
            return [];
        }

        #endregion
    }
}