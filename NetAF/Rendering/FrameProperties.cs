namespace NetAF.Rendering
{
    /// <summary>
    /// Provides global properties for frames.
    /// </summary>
    public static class FrameProperties
    {
        /// <summary>
        /// Get or set if the command list is displayed.
        /// </summary>
        public static bool DisplayCommandList { get; set; } = true;

        /// <summary>
        /// Get or set the type of key to use on the map.
        /// </summary>
        public static KeyType KeyType { get; set; } = KeyType.Dynamic;

        /// <summary>
        /// Get or set the detail to use on the map.
        /// </summary>
        public static RegionMapDetail MapDetail { get; set; } = RegionMapDetail.Normal;
    }
}
