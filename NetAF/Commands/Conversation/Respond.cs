using NetAF.Assets.Interaction;
using NetAF.Conversations;

namespace NetAF.Commands.Conversation
{
    /// <summary>
    /// Represents the Respond command.
    /// </summary>
    /// <param name="response">The response.</param>
    internal class Respond(Response response) : ICommand
    {
        #region Properties

        /// <summary>
        /// Get the conversation response.
        /// </summary>
        public Response Response { get; } = response;

        #endregion

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

            if (Response == null)
                return new(ReactionResult.Error, "No response specified.");

            if (game.ActiveConverser?.Conversation == null)
                return new(ReactionResult.Error, "No active conversation.");

            return game.ActiveConverser.Conversation.Respond(Response, game);
        }

        #endregion
    }
}