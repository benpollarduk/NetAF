using NetAF.Assets;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using System.Text;

namespace NetAF.Targets.Text.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of command list frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    public sealed class TextCommandListFrameBuilder(StringBuilder builder) : ICommandListFrameBuilder
    {
        #region Implementation of ICommandListFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="commandHelp">The command help.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        public IFrame Build(string title, string description, CommandHelp[] commandHelp, Size size)
        {
            builder.Clear();

            builder.AppendLine(title);
            builder.AppendLine();

            if (!string.IsNullOrEmpty(description))
                builder.AppendLine(description);

            foreach (var command in commandHelp)
            {
                if (!string.IsNullOrEmpty(command.DisplayCommand) && !string.IsNullOrEmpty(command.Description))
                {
                    builder.AppendLine($"{command.DisplayCommand} - {command.Description.EnsureFinishedSentence()}");
                }
                else if (!string.IsNullOrEmpty(command.DisplayCommand) && string.IsNullOrEmpty(command.Description))
                {
                    builder.AppendLine(command.DisplayCommand);
                }
            }

            return new TextFrame(builder);
        }

        #endregion
    }
}
