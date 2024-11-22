using NetAF.Assets;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Rendering.Frames;

namespace NetAF.Rendering.FrameBuilders.Console
{
    /// <summary>
    /// Provides a builder of help frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    public sealed class ConsoleHelpFrameBuilder(GridStringBuilder gridStringBuilder) : IHelpFrameBuilder
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
        /// Get or set the command color.
        /// </summary>
        public AnsiColor CommandColor { get; set; } = AnsiColor.Green;

        /// <summary>
        /// Get or set the description color.
        /// </summary>
        public AnsiColor CommandDescriptionColor { get; set; } = AnsiColor.Yellow;

        #endregion

        #region Implementation of IHelpFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="commandHelp">The command help.</param>
        /// <param name="size">The size of the frame.</param>
        public IFrame Build(CommandHelp commandHelp, Size size)
        {
            gridStringBuilder.Resize(size);

            gridStringBuilder.DrawBoundary(BorderColor);

            var availableWidth = size.Width - 4;
            const int leftMargin = 2;

            gridStringBuilder.DrawWrapped(commandHelp.Command, leftMargin, 2, availableWidth, CommandColor, out _, out var lastY);
            gridStringBuilder.DrawUnderline(leftMargin, lastY + 1, commandHelp.Command.Length, CommandColor);

            lastY += 3;

            var description = !string.IsNullOrEmpty(commandHelp.Instructions) ? commandHelp.Instructions : commandHelp.Description;

            gridStringBuilder.DrawWrapped(description.EnsureFinishedSentence(), leftMargin, lastY, availableWidth, CommandDescriptionColor, out _, out _);

            if (!string.IsNullOrEmpty(commandHelp.DisplayAs))
                gridStringBuilder.DrawWrapped($"Example: {commandHelp.DisplayAs}", leftMargin, lastY + 2, availableWidth, CommandDescriptionColor, out _, out _);

            return new GridTextFrame(gridStringBuilder, 0, 0, BackgroundColor) { ShowCursor = false };
        }

        #endregion
    }
}
