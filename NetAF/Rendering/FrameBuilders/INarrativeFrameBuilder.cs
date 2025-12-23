using NetAF.Assets;
using NetAF.Narratives;

namespace NetAF.Rendering.FrameBuilders
{
    /// <summary>
    /// Represents any object that can build narrative frames.
    /// </summary>
    public interface INarrativeFrameBuilder : IFrameBuilder
    {
        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="narrative">The narrative.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        IFrame Build(Narrative narrative, Size size);
    }
}
