namespace NetAF.Rendering
{
    /// <summary>
    /// Render options for room maps.
    /// </summary>
    public class RoomMapRenderOptions
    {
        #region StaticProperties

        /// <summary>
        /// Get the default room map render options.
        /// </summary>
        public static RoomMapRenderOptions Default => new() 
        {
            KeyType = KeyType.Dynamic, 
            PointOfInterestDetail = PointOfInterestDetail.Low,
            KeyPlacement = KeyPlacement.Below
        };

        #endregion

        #region Properties

        /// <summary>
        /// Get or set the type of key to use on the map.
        /// </summary>
        public KeyType KeyType { get; set; }

        /// <summary>
        /// Get or set the detail to use for points of interest on the map.
        /// </summary>
        public PointOfInterestDetail PointOfInterestDetail { get; set; }

        /// <summary>
        /// Get or set the placement of the key relative to the map.
        /// </summary>
        public KeyPlacement KeyPlacement { get; set; }

        #endregion
    }
}
