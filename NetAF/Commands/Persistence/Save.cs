using System;
using System.IO;
using NetAF.Persistence;
using NetAF.Persistence.Json;

namespace NetAF.Commands.Persistence
{
    /// <summary>
    /// Represents the Save command.
    /// </summary>
    public sealed class Save : CustomCommand
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Save class.
        /// </summary>
        public Save() : base(new CommandHelp("Save", "Save the game state to a file. The path should be specified as an absolute path."), true, true, SaveGameToFile) { }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Save the game to a file.
        /// </summary>
        /// <param name="game">The game to save.</param>
        /// <param name="args">The arguments. The file path must be the first element in the array.</param>
        /// <returns>The reaction.</returns>
        private static Reaction SaveGameToFile(Logic.Game game, string[] args)
        {
            try
            {
                var path = args[0];
                var fileName = Path.GetFileName(path);
                var restorePoint = RestorePoint.Create(fileName, game);
                var result = JsonSave.ToFile(path, restorePoint, out var message);

                if (!result)
                    return new(ReactionResult.Error, $"Failed to save: {message}");

                return new(ReactionResult.Inform, $"Saved.");
            }
            catch (Exception e)
            {
                return new(ReactionResult.Error, $"Error saving to file: {e.Message}");
            }
        }

        #endregion
    }
}
