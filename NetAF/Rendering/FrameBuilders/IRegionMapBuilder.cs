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
        /// <param name="startPosition">The position to start building at.</param>
        /// <param name="maxSize">The maximum size available in which to build the map.</param>
        void BuildRegionMap(Region region, Point2D startPosition, Size maxSize);
    }
}
