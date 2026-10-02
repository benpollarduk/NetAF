namespace NetAF.Rendering
{
    /// <summary>
    /// Render options for region maps.
    /// </summary>
    public class RegionMapRenderOptions
    {
        #region StaticProperties

        /// <summary>
        /// Get the default options.
        /// </summary>
        public static RegionMapRenderOptions Default => new()
        {
            MapDetail = RegionMapDetail.Normal
        };

        #endregion

        #region Properties

        /// <summary>
        /// Get or set the detail to use on the map.
        /// </summary>
        public RegionMapDetail MapDetail { get; set; }

        #endregion
    }
}
