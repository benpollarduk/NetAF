using NetAF.Assets;
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
        /// <param name="focusPosition">The position to focus on.</param>
        /// <param name="options">The render options to use.</param>
        /// <param name="maxSize">The maximum size available in which to build the map.</param>
        void BuildRegionMap(Region region, Point3D focusPosition, RegionMapRenderOptions options, Size maxSize);
    }
}
