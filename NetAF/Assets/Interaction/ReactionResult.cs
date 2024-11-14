namespace NetAF.Assets.Interaction
{
    /// <summary>
    /// Enumeration of reaction results.
    /// </summary>
    public enum ReactionResult
    {
        /// <summary>
        /// Error.
        /// </summary>
        Error = 0,
        /// <summary>
        /// OK.
        /// </summary>
        OK,
        /// <summary>
        /// A silent reaction.
        /// </summary>
        Silent,
        /// <summary>
        /// A reaction that has a fatal effect on the player.
        /// </summary>
        Fatal
    }
}
