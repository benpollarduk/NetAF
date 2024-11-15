using NetAF.Assets;
using NetAF.Commands;
using NetAF.Rendering.Frames;

namespace NetAF.Rendering.FrameBuilders
{
    /// <summary>
    /// Represents any object that can build help frames.
    /// </summary>
    public interface IHelpFrameBuilder
    {
        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="commandHelp">The command help.</param>
        /// <param name="size">The size of the frame.</param>
        IFrame Build(string title, string description, CommandHelp[] commandHelp, Size size);
    }
}
