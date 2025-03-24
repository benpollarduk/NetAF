namespace NetAF.Rendering
{
    /// <summary>
    /// Enumeration of region map display modes.
    /// </summary>
    public enum RegionMapDisplayMode
    {
        /// <summary>
        /// Shows rooms at a detailed level.
        /// </summary>
        Detailed = 0,
        /// <summary>
        /// Shows rooms as one character, which allows larger maps to be displayed in a limited area.
        /// </summary>
        Undetailed,
        /// <summary>
        /// Dynamic region map - uses detailed if there is room, else map will be undetailed.
        /// </summary>
        Dynamic
    }
}