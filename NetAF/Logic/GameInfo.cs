namespace NetAF.Logic
{
    /// <summary>
    /// Provides information about a game.
    /// </summary>
    /// <param name="name">The name of the game.</param>
    /// <param name="description">A description of the game.</param>
    /// <param name="author">A author of the game.</param>
    public sealed class GameInfo(string name, string description, string author)
    {
        #region Properties

        /// <summary>
        /// Get the name of the game.
        /// </summary>
        public string Name { get; private set; } = name;

        /// <summary>
        /// Get the description of the game.
        /// </summary>
        public string Description { get; private set; } = description;

        /// <summary>
        /// Get the author.
        /// </summary>
        public string Author { get; set; } = author;

        #endregion
    }
}
