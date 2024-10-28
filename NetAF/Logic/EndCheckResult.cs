namespace NetAF.Logic
{
    /// <summary>
    /// Represents the result of an end check.
    /// </summary>
    /// <param name="hasEnded">If the game has ended.</param>
    /// <param name="title">A title to describe the end.</param>
    /// <param name="description">A description of the end.</param>
    public class EndCheckResult(bool hasEnded, string title, string description)
    {
        #region StaticProperties

        /// <summary>
        /// Get a default result for not ended.
        /// </summary>
        public static EndCheckResult NotEnded { get; } = new(false, string.Empty, string.Empty);

        #endregion

        #region Properties

        /// <summary>
        /// Get if the game has come to an end.
        /// </summary>
        public bool HasEnded { get; } = hasEnded;

        /// <summary>
        /// Get a title to describe the end.
        /// </summary>
        public string Title { get; } = title;

        /// <summary>
        /// Get a description of the end.
        /// </summary>
        public string Description { get; } = description;

        #endregion
    }
}
