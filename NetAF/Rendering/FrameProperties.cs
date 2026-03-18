namespace NetAF.Rendering
{
    /// <summary>
    /// Provides global properties for frames.
    /// </summary>
    public static class FrameProperties
    {
        /// <summary>
        /// Get or set the type of command list.
        /// </summary>
        public static CommandListType CommandListType { get; set; } = CommandListType.Minimal;

        /// <summary>
        /// Get or set the type of key to use on the map.
        /// </summary>
        public static KeyType KeyType { get; set; } = KeyType.Dynamic;

        /// <summary>
        /// Get or set if the map should be shown in scenes.
        /// </summary>
        public static bool ShowMapInScenes { get; set; } = true;

        /// <summary>
        /// Get or set the detail to use on the map.
        /// </summary>
        public static RegionMapDetail MapDetail { get; set; } = RegionMapDetail.Normal;
    }
}
