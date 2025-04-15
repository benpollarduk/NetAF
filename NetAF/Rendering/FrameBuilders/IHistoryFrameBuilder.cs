using NetAF.Assets;
using NetAF.Logging.History;

namespace NetAF.Rendering.FrameBuilders
{
    /// <summary>
    /// Represents any object that can build history frames.
    /// </summary>
    public interface IHistoryFrameBuilder : IFrameBuilder
    {
        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="entries">The history entries.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        IFrame Build(string title, string description, HistoryEntry[] entries, Size size);
    }
}
