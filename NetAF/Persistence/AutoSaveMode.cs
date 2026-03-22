namespace NetAF.Persistence
{
    /// <summary>
    /// Enumeration of auto-save modes.
    /// </summary>
    public enum AutoSaveMode
    {
        /// <summary>
        /// Never auto-save.
        /// </summary>
        Off = 0,
        /// <summary>
        /// Auto-save when a room is entered.
        /// </summary>
        RoomEntered,
        /// <summary>
        /// Auto-save when a region is entered.
        /// </summary>
        RegionEntered,
        /// <summary>
        /// Auto-save when an item is obtained.
        /// </summary>
        ItemObtained
    }
}
