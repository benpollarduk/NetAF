namespace NetAF.Assets.Interaction
{
    /// <summary>
    /// Represents a reaction.
    /// </summary>
    /// <param name="result">The result.</param>
    /// <param name="description">A description of the result.</param>
    public sealed class Reaction(ReactionResult result, string description)
    {
        #region Properties

        /// <summary>
        /// Get the result.
        /// </summary>
        public ReactionResult Result { get; } = result;

        /// <summary>
        /// Get a description of the result.
        /// </summary>
        public string Description { get; } = description;

        #endregion
    }
}
