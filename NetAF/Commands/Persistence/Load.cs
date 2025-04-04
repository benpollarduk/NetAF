﻿using NetAF.Logic;
using NetAF.Persistence.Json;
using NetAF.Serialization;

namespace NetAF.Commands.Persistence
{
    /// <summary>
    /// Represents the Load command.
    /// </summary>
    public sealed class Load : CustomCommand
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Load class.
        /// </summary>
        public Load() : base(new CommandHelp("Load", "Load the game state from a file.", CommandCategory.Persistence, instructions: "When loading the path should be specified as an absolute path."), true, true, LoadGameFromFile) { }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Load the game from a file.
        /// </summary>
        /// <param name="game">The game to load.</param>
        /// <param name="args">The arguments. The file path must be the first element in the array.</param>
        /// <returns>The reaction.</returns>
        private static Reaction LoadGameFromFile(Game game, string[] args)
        {
            var result = JsonSave.FromFile(args[0], out var restorePoint, out var message);

            if (!result)
                return new(ReactionResult.Error, $"Failed to load: {message}");

            ((IObjectSerialization<Game>)restorePoint.Game).Restore(game);

            return new(ReactionResult.Inform, $"Loaded.");
        }

        #endregion
    }
}
