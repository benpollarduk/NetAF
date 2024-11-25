using System.Linq;
using NetAF.Assets;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Rendering.FrameBuilders;

namespace NetAF.Rendering.Console.FrameBuilders
{
    /// <summary>
    /// Provides a builder of command list frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    public sealed class ConsoleCommandListFrameBuilder(GridStringBuilder gridStringBuilder) : ICommandListFrameBuilder
    {
        #region Properties

        /// <summary>
        /// Get or set the background color.
        /// </summary>
        public AnsiColor BackgroundColor { get; set; }

        /// <summary>
        /// Get or set the border color.
        /// </summary>
        public AnsiColor BorderColor { get; set; } = AnsiColor.BrightBlack;

        /// <summary>
        /// Get or set the title color.
        /// </summary>
        public AnsiColor TitleColor { get; set; } = AnsiColor.White;

        /// <summary>
        /// Get or set the description color.
        /// </summary>
        public AnsiColor DescriptionColor { get; set; } = AnsiColor.White;

        /// <summary>
        /// Get or set the command color.
        /// </summary>
        public AnsiColor CommandColor { get; set; } = AnsiColor.Green;

        /// <summary>
        /// Get or set the description color.
        /// </summary>
        public AnsiColor CommandDescriptionColor { get; set; } = AnsiColor.Yellow;

        #endregion

        #region Implementation of ICommandListFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="commandHelp">The command help.</param>
        /// <param name="size">The size of the frame.</param>
        public IFrame Build(string title, string description, CommandHelp[] commandHelp, Size size)
        {
            gridStringBuilder.Resize(size);

            gridStringBuilder.DrawBoundary(BorderColor);

            var availableWidth = size.Width - 4;
            const int leftMargin = 2;
            var padding = (commandHelp.Any() ? commandHelp.Max(x => x.DisplayCommand.Length) : 0) + 1;

            gridStringBuilder.DrawWrapped(title, leftMargin, 2, availableWidth, TitleColor, out _, out var lastY);
            gridStringBuilder.DrawUnderline(leftMargin, lastY + 1, title.Length, TitleColor);

            if (!string.IsNullOrEmpty(description))
                gridStringBuilder.DrawCentralisedWrapped(description, lastY + 3, availableWidth, DescriptionColor, out _, out lastY);

            lastY += 2;

            foreach (var command in commandHelp)
            {
                if (lastY >= size.Height - 1)
                    break;

                if (!string.IsNullOrEmpty(command.DisplayCommand) && !string.IsNullOrEmpty(command.Description))
                {
                    gridStringBuilder.DrawWrapped(command.DisplayCommand, leftMargin, lastY + 1, availableWidth, CommandColor, out _, out lastY);
                    gridStringBuilder.DrawWrapped("-", leftMargin + padding, lastY, availableWidth, CommandColor, out _, out lastY);
                    gridStringBuilder.DrawWrapped(command.Description.EnsureFinishedSentence(), leftMargin + padding + 2, lastY, availableWidth, CommandDescriptionColor, out _, out lastY);
                }
                else if (!string.IsNullOrEmpty(command.DisplayCommand) && string.IsNullOrEmpty(command.Description))
                {
                    gridStringBuilder.DrawWrapped(command.DisplayCommand, leftMargin, lastY + 1, availableWidth, CommandColor, out _, out lastY);
                }
            }

            return new GridTextFrame(gridStringBuilder, 0, 0, BackgroundColor) { ShowCursor = false };
        }

        #endregion
    }
}
