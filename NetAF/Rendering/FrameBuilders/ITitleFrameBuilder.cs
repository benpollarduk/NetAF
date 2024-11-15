using NetAF.Assets;
using NetAF.Rendering.Frames;

namespace NetAF.Rendering.FrameBuilders
{
    /// <summary>
    /// Represents any object that can build title frames.
    /// </summary>
    public interface ITitleFrameBuilder
    {
        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="size">The size of the frame.</param>
        IFrame Build(string title, string description, Size size);
    }
}
