﻿using NetAF.Logic;
using NetAF.Serialization.Assets;
using System.Linq;

namespace NetAF.Serialization
{
    /// <summary>
    /// Represents a serialization of a Game.
    /// </summary>
    public class GameSerialization : IObjectSerialization<Game>
    {
        #region Properties

        /// <summary>
        /// Get or set the active player identifier.
        /// </summary>
        public string ActivePlayerIdentifier { get; set; }

        /// <summary>
        /// Get or set the player serializations.
        /// </summary>
        public CharacterSerialization[] Players { get; set; }

        /// <summary>
        /// Get or set the overworld serialization.
        /// </summary>
        public OverworldSerialization Overworld { get; set; }

        /// <summary>
        /// Get or set the overworld serialization.
        /// </summary>
        public PlayableCharacterLocationSerialization[] InactivePlayerLocations { get; set; }

        /// <summary>
        /// Get or set the note manager serialization.
        /// </summary>
        public NoteManagerSerialization NoteManager { get; set; }

        /// <summary>
        /// Get or set the variable manager serialization.
        /// </summary>
        public VariableManagerSerialization VariableManager { get; set; }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Create a new serialization from a Game.
        /// </summary>
        /// <param name="game">The Game to create the serialization from.</param>
        /// <returns>The serialization.</returns>
        public static GameSerialization FromGame(Game game)
        {
            return new()
            {
                ActivePlayerIdentifier = game?.Player?.Identifier?.IdentifiableName,
                Players = game?.Catalog?.Players?.Select(CharacterSerialization.FromCharacter).ToArray() ?? [],
                Overworld = OverworldSerialization.FromOverworld(game?.Overworld),
                InactivePlayerLocations = game?.GetInactivePlayerLocations().Select(PlayableCharacterLocationSerialization.FromPlayableCharacterLocation).ToArray() ?? [],
                NoteManager = NoteManagerSerialization.FromNoteManager(game?.NoteManager),
                VariableManager = VariableManagerSerialization.FromVariableManager(game?.VariableManager)
            };
        }

        #endregion

        #region Implementation of IObjectSerialization<Game>

        /// <summary>
        /// Restore an instance from this serialization.
        /// </summary>
        /// <param name="game">The asset to restore.</param>
        void IObjectSerialization<Game>.Restore(Game game)
        {
            ((IRestoreFromObjectSerialization<GameSerialization>)game).RestoreFrom(this);
        }

        #endregion
    }
}
