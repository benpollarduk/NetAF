namespace NetAF.Logic
{
    /// <summary>
    /// Provides information about a game.
    /// </summary>
    public class GameInfo
    {
        #region Properties

        /// <summary>
        /// Get the name of the game.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Get the description of the game.
        /// </summary>
        public string Description { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the GameInfo class.
        /// </summary>
        /// <param name="name">The name of the game.</param>
        /// <param name="description">A description of the game.</param>
        public GameInfo(string name, string description)
        {
            Name = name;
            Description = description;
        }

        #endregion
    }
}
