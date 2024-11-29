using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Commands;

namespace NetAF.Rendering.FrameBuilders
{
    /// <summary>
    /// Represents any object that can build conversation frames.
    /// </summary>
    public interface IConversationFrameBuilder
    {
        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title to display to the user.</param>
        /// <param name="converser">The converser.</param>
        /// <param name="contextualCommands">The contextual commands to display.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        IFrame Build(string title, IConverser converser, CommandHelp[] contextualCommands, Size size);
    }
}
