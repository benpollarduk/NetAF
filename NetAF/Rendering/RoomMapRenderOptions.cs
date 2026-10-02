namespace NetAF.Rendering
{
    /// <summary>
    /// Render options for room maps.
    /// </summary>
    public class RoomMapRenderOptions
    {
        /// <summary>
        /// Get or set the type of key to use on the map.
        /// </summary>
        public KeyType KeyType { get; set; } = KeyType.Dynamic;

        /// <summary>
        /// Get or set the detail to use for points of interest on the map.
        /// </summary>
        public PointOfInterestDetail PointOfInterestDetail { get; set; } = PointOfInterestDetail.High;
    }
}
