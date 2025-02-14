using NetAF.Assets;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;

namespace NetAF.Targets.Html.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of command list frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    public sealed class HtmlCommandListFrameBuilder(HtmlBuilder builder) : ICommandListFrameBuilder
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

            builder.H1(title);
            builder.Br();

            if (!string.IsNullOrEmpty(description))
                builder.P(description);

            foreach (var command in commandHelp)
            {
                if (!string.IsNullOrEmpty(command.DisplayCommand) && !string.IsNullOrEmpty(command.Description))
                {
                    builder.P($"{command.DisplayCommand} - {command.Description.EnsureFinishedSentence()}");
                }
                else if (!string.IsNullOrEmpty(command.DisplayCommand) && string.IsNullOrEmpty(command.Description))
                {
                    builder.P(command.DisplayCommand);
                }
            }

            return new HtmlFrame(builder);
        }

        #endregion
    }
}
