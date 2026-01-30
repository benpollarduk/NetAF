namespace NetAF.Assets
{
    /// <summary>
    /// Enumeration of interaction results.
    /// </summary>
    public enum InteractionResult
    {
        /// <summary>
        /// No change.
        /// </summary>
        NoChange = 0,
        /// <summary>
        /// The item expires.
        /// </summary>
        ItemExpires,
        /// <summary>
        /// The target expires.
        /// </summary>
        TargetExpires,
        /// <summary>
        /// The item and the target expires.
        /// </summary>
        ItemAndTargetExpires,
        /// <summary>
        /// The player dies.
        /// </summary>
        PlayerDies,
        /// <summary>
        /// The player receives an item.
        /// </summary>
        PlayerReceivesItem,
        /// <summary>
        /// A non-playable character receives an item.
        /// </summary>
        NonPlayableCharacterReceivesItem
    }
}