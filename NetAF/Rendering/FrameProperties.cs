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
        /// Get or set if the map should be shown in scenes.
        /// </summary>
        public static bool ShowMapInScenes { get; set; } = true;

        /// <summary>
        /// Get or set the render options for region maps.
        /// </summary>
        public static RegionMapRenderOptions RegionMapRenderOptions { get; set; } = RegionMapRenderOptions.Default;

        /// <summary>
        /// Get or set the render options for room maps.
        /// </summary>
        public static RoomMapRenderOptions RoomMapRenderOptions { get; set; } = RoomMapRenderOptions.Default;
    }
}
