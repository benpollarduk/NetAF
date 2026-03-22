namespace NetAF.Persistence
{
    /// <summary>
    /// Enumeration of auto-save events.
    /// </summary>
    public enum AutoSaveEvent
    {
        /// <summary>
        /// Never auto-save.
        /// </summary>
        None = 0,
        /// <summary>
        /// Auto-save when a room is entered.
        /// </summary>
        RoomEntered,
        /// <summary>
        /// Auto-save when a region is entered.
        /// </summary>
        RegionEntered,
        /// <summary>
        /// Auto-save when an item is received.
        /// </summary>
        ItemReceived
    }
}
