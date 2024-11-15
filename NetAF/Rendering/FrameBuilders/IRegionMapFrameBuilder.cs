using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Rendering.Frames;

namespace NetAF.Rendering.FrameBuilders
{
    /// <summary>
    /// Represents any object that can build region map frames.
    /// </summary>
    public interface IRegionMapFrameBuilder
    {
        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="size">The size of the frame.</param>
        IFrame Build(Region region, Size size);
    }
}
