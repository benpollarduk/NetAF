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
        /// <param name="regionMapOptions">The region map render options to use.</param>
        /// <param name="roomMapOptions">The room map render options to use.</param>
        /// <param name="maxSize">The maximum size available in which to build the map.</param>
        void BuildRegionMap(Region region, Point3D focusPosition, RegionMapRenderOptions regionMapOptions, RoomMapRenderOptions roomMapOptions, Size maxSize);
    }
}
