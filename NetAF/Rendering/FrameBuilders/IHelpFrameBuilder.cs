using NetAF.Assets;
using NetAF.Commands;

namespace NetAF.Rendering.FrameBuilders
{
    /// <summary>
    /// Represents any object that can build help frames.
    /// </summary>
    public interface IHelpFrameBuilder : IFrameBuilder
    {
        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="commandHelp">The command help.</param>
        /// <param name="prompts">The prompts to display for the command.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        IFrame Build(string title, CommandHelp commandHelp, Prompt[] prompts, Size size);
    }
}
