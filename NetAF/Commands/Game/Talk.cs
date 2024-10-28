using NetAF.Assets.Characters;
using NetAF.Assets.Interaction;

namespace NetAF.Commands.Game
{
    /// <summary>
    /// Represents the Talk command.
    /// </summary>
    /// <param name="converser">The converser.</param>
    internal class Talk(IConverser converser) : ICommand
    {
        #region Properties

        /// <summary>
        /// Get the converser.
        /// </summary>
        public IConverser Converser { get; } = converser;

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

            if (game.Player == null)
                return new(ReactionResult.Error, "No player specified.");

            if (!game.Player.CanConverse)
                return new(ReactionResult.Error, $"{game.Player.Identifier.Name} cannot converse.");

            if (Converser == null)
                return new(ReactionResult.Error, "No-one is around to talk to.");

            if (Converser is Character character && !character.IsAlive)
                return new(ReactionResult.Error, $"{character.Identifier.Name} is dead.");

            game.StartConversation(Converser);
            return new(ReactionResult.Internal, "Engaged in conversation.");
        }

        #endregion
    }
}
