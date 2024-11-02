﻿using NetAF.Assets.Interaction;
using NetAF.Interpretation;
using NetAF.Serialization.Persistence.Json;
using NetAF.Serialization.Persistence;

namespace NetAF.Commands.Persistence
{
    /// <summary>
    /// Represents the Save command.
    /// </summary>
    public class Save : CustomCommand
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Save class.
        /// </summary>
        public Save() : base(new CommandHelp("Save", "Save the game state to a file. The path should be specified as an absolute path."), true, SaveGameToFile) { }

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
            var result = JsonSave.ToFile(args[0], RestorePoint.Create("Test", game), out var message);

            if (!result)
                return new(ReactionResult.Error, $"Failed to save: {message}");

            return new(ReactionResult.OK, $"Saved.");
        }

        #endregion
    }
}