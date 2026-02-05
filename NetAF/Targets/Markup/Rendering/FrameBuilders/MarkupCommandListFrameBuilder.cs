using NetAF.Assets;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using System.Collections.Generic;

namespace NetAF.Targets.Markup.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of command list frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    public sealed class MarkupCommandListFrameBuilder(MarkupBuilder builder) : ICommandListFrameBuilder
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

            builder.Heading(title, HeadingLevel.H1);
            builder.Newline();

            if (!string.IsNullOrEmpty(description))
                builder.Text(description);

            List<string> commandsInList = [];

            foreach (var command in commandHelp)
            {
                if (!string.IsNullOrEmpty(command.DisplayCommand) && !string.IsNullOrEmpty(command.Description))
                {
                    commandsInList.Add($"{command.DisplayCommand} - {command.Description.EnsureFinishedSentence()}");
                }
                else if (!string.IsNullOrEmpty(command.DisplayCommand) && string.IsNullOrEmpty(command.Description))
                {
                    commandsInList.Add(command.DisplayCommand);
                }
            }

            foreach (var command in commandsInList)
                builder.Text(command);

            return new MarkupFrame(builder);
        }

        #endregion
    }
}
