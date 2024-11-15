using NetAF.Assets;
using NetAF.Rendering.Frames;

namespace NetAF.Rendering.FrameBuilders
{
    /// <summary>
    /// Represents any object that can build reaction frames.
    /// </summary>
    public interface IReactionFrameBuilder
    {
        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title to display to the user.</param>
        /// <param name="message">The message to display to the user.</param>
        /// <param name="size">The size of the frame.</param>
        IFrame Build(string title, string message, Size size);
    }
}
