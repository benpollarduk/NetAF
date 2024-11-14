namespace NetAF.Assets.Interaction
{
    /// <summary>
    /// Enumeration of reaction results.
    /// </summary>
    public enum ReactionResult
    {
        /// <summary>
        /// An error reaction.
        /// </summary>
        Error = 0,
        /// <summary>
        /// An OK reaction.
        /// </summary>
        OK,
        /// <summary>
        /// A silent reaction.
        /// </summary>
        Silent,
        /// <summary>
        /// A mode change reaction.
        /// </summary>
        ModeChanged,
        /// <summary>
        /// A reaction that has a fatal effect on the player.
        /// </summary>
        Fatal
    }
}
