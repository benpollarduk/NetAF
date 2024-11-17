namespace NetAF.Assets.Interaction
{
    /// <summary>
    /// Enumeration of interaction effects.
    /// </summary>
    public enum InteractionEffect
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