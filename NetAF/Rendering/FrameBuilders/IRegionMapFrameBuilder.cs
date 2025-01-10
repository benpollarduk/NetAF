using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Commands;

namespace NetAF.Rendering.FrameBuilders
{
    /// <summary>
    /// Represents any object that can build region map frames.
    /// </summary>
    public interface IRegionMapFrameBuilder : IFrameBuilder
    {
        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="focusPosition">The position to focus on.</param>
        /// <param name="contextualCommands">The contextual commands to display.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        IFrame Build(Region region, Point3D focusPosition, CommandHelp[] contextualCommands, Size size);
    }
}
