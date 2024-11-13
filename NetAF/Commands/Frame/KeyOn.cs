using NetAF.Assets.Interaction;
using NetAF.Rendering;

namespace NetAF.Commands.Frame
{
    /// <summary>
    /// Represents the KeyOn command.
    /// </summary>
    public class KeyOn : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help.
        /// </summary>
        public static CommandHelp CommandHelp { get; } = new("KeyOn", "Turn the key on");

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

            game.Configuration.SceneMapKeyType = KeyType.Dynamic;
            return new(ReactionResult.OK, "Key has been turned on.");
        }

        #endregion
    }
}