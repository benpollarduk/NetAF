namespace NetAF.Assets
{
    /// <summary>
    /// Enumeration of interaction results.
    /// </summary>
    public enum InteractionResult
    {
        /// <summary>
        /// Neither the item or the target expired.
        /// </summary>
        NeitherItemOrTargetExpired = 0,
        /// <summary>
        /// The item expired.
        /// </summary>
        ItemExpired,
        /// <summary>
        /// The target expired.
        /// </summary>
        TargetExpired,
        /// <summary>
        /// The item and the target expired.
        /// </summary>
        ItemAndTargetExpired
    }
}