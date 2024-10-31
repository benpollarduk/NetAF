using NetAF.Logic;
using System;

namespace NetAF.Serialization.Saves
{
    /// <summary>
    /// Represents a save for restoring a Game.
    /// </summary>
    public class Save
    {
        #region Properties

        /// <summary>
        /// Get the serialized game.
        /// </summary>
        public GameSerialization Game { get; private set; }

        /// <summary>
        /// Get the name of this save.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Get the creation time of this save.
        /// </summary>
        public DateTime CreationTime { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Save class.
        /// </summary>
        private Save() { }

        #endregion

        /// <summary>
        /// Create a new Save.
        /// </summary>
        /// <param name="name">The name of the save.</param>
        /// <param name="game">The game to save.</param>
        /// <returns>The created Save.</returns>
        public static Save Create(string name, Game game)
        {
            return new()
            {
                Name = name,
                CreationTime = DateTime.Now,
                Game = new(game)
            };
        }
    }
}
