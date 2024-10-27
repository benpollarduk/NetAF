namespace NetAF.Logic
{
    /// <summary>
    /// Provides information about a game.
    /// </summary>
    public sealed class GameInfo
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

        /// <summary>
        /// Get the author.
        /// </summary>
        public string Author { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the GameInfo class.
        /// </summary>
        /// <param name="name">The name of the game.</param>
        /// <param name="description">A description of the game.</param>
        /// <param name="author">A author of the game.</param>
        public GameInfo(string name, string description, string author)
        {
            Name = name;
            Description = description;
            Author = author;
        }

        #endregion
    }
}
