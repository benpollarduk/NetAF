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

        /// <summary>
        /// Get the help for this command.
        /// </summary>
        public CommandHelp Help => SilentCommandHelp;

        /// <summary>
        /// Invoke the command.
        /// </summary>
        /// <param name="game">The game to invoke the command on.</param>
        /// <returns>The reaction.</returns>
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