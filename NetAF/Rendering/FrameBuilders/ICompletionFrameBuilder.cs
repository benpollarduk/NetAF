using NetAF.Assets;

namespace NetAF.Rendering.FrameBuilders
{
    /// <summary>
    /// Represents any object that can build completion frames.
    /// </summary>
    public interface ICompletionFrameBuilder : IFrameBuilder
    {
        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="message">The message to display to the user.</param>
        /// <param name="reason">The reason the game ended.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        IFrame Build(string message, string reason, Size size);
    }
}
