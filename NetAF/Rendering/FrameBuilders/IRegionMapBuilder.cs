using NetAF.Assets.Locations;

namespace NetAF.Rendering.FrameBuilders
{
    /// <summary>
    /// Represents any object that can build region maps.
    /// </summary>
    public interface IRegionMapBuilder
    {
        /// <summary>
        /// Build a map of a region.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="x">The x position to start building at.</param>
        /// <param name="y">The y position to start building at.</param>
        /// <param name="maxWidth">The maximum horizontal space available in which to build the map.</param>
        /// <param name="maxHeight">The maximum vertical space available in which to build the map.</param>
        void BuildRegionMap(Region region, int x, int y, int maxWidth, int maxHeight);
    }
}
