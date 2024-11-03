using NetAF.Logic;
using NetAF.Serialization;
using System;

namespace NetAF.Persistence
{
    /// <summary>
    /// Represents a restore point for a Game.
    /// </summary>
    public class RestorePoint
    {
        #region Properties

        /// <summary>
        /// Get or set the serialized game.
        /// </summary>
        public GameSerialization Game { get; set; }

        /// <summary>
        /// Get or set the name of this save.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get or set the creation time of this save.
        /// </summary>
        public DateTime CreationTime { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the RestorePoint class.
        /// </summary>
        private RestorePoint() { }

        #endregion

        /// <summary>
        /// Create a new restore point.
        /// </summary>
        /// <param name="name">The name of the restore point.</param>
        /// <param name="game">The game to create the restore point for.</param>
        /// <returns>The restore point.</returns>
        public static RestorePoint Create(string name, Game game)
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
