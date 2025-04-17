namespace NetAF.Commands
{
    /// <summary>
    /// Represents a reaction.
    /// </summary>
    /// <param name="result">The result.</param>
    /// <param name="title">A title for the reaction.</param>
    /// <param name="description">A description of the result.</param>
    public sealed class Reaction(ReactionResult result, string title, string description)
    {
        #region StaticProperties

        /// <summary>
        /// Provides a default value for Inform.
        /// </summary>
        internal static Reaction Inform { get; } = new(ReactionResult.Inform, string.Empty);

        /// <summary>
        /// Provides a default value for Silent.
        /// </summary>
        internal static Reaction Silent { get; } = new(ReactionResult.Silent, string.Empty);

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Reaction class.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="description">A description of the result.</param>
        public Reaction(ReactionResult result, string description) : this(result, string.Empty, description)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Get the result.
        /// </summary>
        public ReactionResult Result { get; } = result;

        /// <summary>
        /// Get the title for the reaction.
        /// </summary>
        public string Title { get; } = title;

        /// <summary>
        /// Get a description of the result.
        /// </summary>
        public string Description { get; } = description;

        #endregion
    }
}
