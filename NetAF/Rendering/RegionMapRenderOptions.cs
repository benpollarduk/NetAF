namespace NetAF.Rendering
{
    /// <summary>
    /// Render options for region maps.
    /// </summary>
    public class RegionMapRenderOptions
    {
        /// <summary>
        /// Get or set the detail to use on the map.
        /// </summary>
        public RegionMapDetail MapDetail { get; set; } = RegionMapDetail.Normal;
    }
}
