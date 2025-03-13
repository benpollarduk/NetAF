using NetAF.Logic;
using NetAF.Logic.Modes;
using NetAF.Rendering;

namespace NetAF.Commands.Frame
{
    /// <summary>
    /// Represents the KeyOn command.
    /// </summary>
    public sealed class KeyOn : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help.
        /// </summary>
        public static CommandHelp CommandHelp { get; } = new("KeyOn", "Turn the key on", CommandCategory.Frame);

        #endregion

        #region Implementation of ICommand

        /// <summary>
        /// Invoke the command.
        /// </summary>
        /// <param name="game">The game to invoke the command on.</param>
        /// <returns>The reaction.</returns>
        public Reaction Invoke(Game game)
        {
            if (game == null)
                return new(ReactionResult.Error, "No game specified.");

            SceneMode.KeyType = KeyType.Dynamic;
            return new(ReactionResult.Inform, "Key has been turned on.");
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