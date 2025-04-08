using NetAF.Assets;
using NetAF.Log;

namespace NetAF.Rendering.FrameBuilders
{
    /// <summary>
    /// Represents any object that can build log frames.
    /// </summary>
    public interface ILogFrameBuilder : IFrameBuilder
    {
        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="entries">The log entries.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        IFrame Build(string title, string description, LogEntry[] entries, Size size);
    }
}
