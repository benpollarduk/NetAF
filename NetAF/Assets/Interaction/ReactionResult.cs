namespace NetAF.Assets.Interaction
{
    /// <summary>
    /// Enumeration of reaction results.
    /// </summary>
    public enum ReactionResult
    {
        /// <summary>
        /// An error occurred.
        /// </summary>
        Error = 0,
        /// <summary>
        /// The user should be informed.
        /// </summary>
        Inform,
        /// <summary>
        /// The user should not be informed.
        /// </summary>
        Silent,
        /// <summary>
        /// The game mode was changed.
        /// </summary>
        GameModeChanged
    }
}
