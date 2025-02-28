using NetAF.Assets.Locations;
using NetAF.Assets;
using NetAF.Rendering.FrameBuilders;

namespace NetAF.Targets.Console.Rendering
{
    /// <summary>
    /// Represents any object that can build region maps targeting the console.
    /// </summary>
    public interface IConsoleRegionMapBuilder : IRegionMapBuilder
    {
        /// <summary>
        /// Build a map of a region.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="focusPosition">The position to focus on.</param> 
        /// <param name="startPosition">The position to start building at.</param>
        /// <param name="maxSize">The maximum size available in which to build the map.</param>
        void BuildRegionMap(Region region, Point3D focusPosition, Point2D startPosition, Size maxSize);
    }
}
