using NetAF.Assets.Interaction;
using NetAF.Interpretation;
using NetAF.Persistence.Json;

namespace NetAF.Commands.Persistence
{
    /// <summary>
    /// Represents the Load command.
    /// </summary>
    public class Load : CustomCommand
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Load class.
        /// </summary>
        public Load() : base(new CommandHelp("Load", "Load the game state from a file. The path should be specified as an absolute path"), true, LoadGameFromFile) { }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Load the game from a file.
        /// </summary>
        /// <param name="game">The game to load.</param>
        /// <param name="args">The arguments. The file path must be the first element in the array.</param>
        /// <returns>The reaction.</returns>
        private static Reaction LoadGameFromFile(Logic.Game game, string[] args)
        {
            var result = JsonSave.FromFile(args[0], out var restorePoint, out var message);

            if (!result)
                return new(ReactionResult.Error, $"Failed to load: {message}");

            restorePoint.Game.Restore(game);

            return new(ReactionResult.OK, $"Loaded.");
        }

        #endregion
    }
}
