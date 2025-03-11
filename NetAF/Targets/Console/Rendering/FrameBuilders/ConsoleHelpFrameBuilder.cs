using NetAF.Assets;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using System.Text;

namespace NetAF.Targets.Console.Rendering.FrameBuilders
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
        public AnsiColor BackgroundColor { get; set; } = AnsiColor.Black;

        /// <summary>
        /// Get or set the border color.
        /// </summary>
        public AnsiColor BorderColor { get; set; } = AnsiColor.BrightBlack;

        /// <summary>
        /// Get or set the title color.
        /// </summary>
        public AnsiColor TitleColor { get; set; } = AnsiColor.White;

        /// <summary>
        /// Get or set the command color.
        /// </summary>
        public AnsiColor CommandColor { get; set; } = NetAFPalette.NetAFGreen;

        /// <summary>
        /// Get or set the description color.
        /// </summary>
        public AnsiColor CommandDescriptionColor { get; set; } = NetAFPalette.NetAFYellow;

        /// <summary>
        /// Get or set the prompts color.
        /// </summary>
        public AnsiColor PromptsColor { get; set; } = NetAFPalette.NetAFYellow;

        /// <summary>
        /// Get or set if prompts should be shown.
        /// </summary>
        public bool ShowPrompts { get; set; } = true;

        #endregion

        #region Implementation of IHelpFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="commandHelp">The command help.</param>
        /// <param name="prompts">The prompts to display for the command.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        public IFrame Build(string title, CommandHelp commandHelp, Prompt[] prompts, Size size)
        {
            gridStringBuilder.Resize(size);

            gridStringBuilder.DrawBoundary(BorderColor);

            var availableWidth = size.Width - 4;
            const int leftMargin = 2;

            gridStringBuilder.DrawWrapped(title, leftMargin, 2, availableWidth, TitleColor, out _, out var lastY);
            gridStringBuilder.DrawUnderline(leftMargin, lastY + 1, title.Length, TitleColor);

            lastY += 3;

            if (commandHelp != null)
            {
                gridStringBuilder.DrawWrapped($"Command: {commandHelp.Command}", leftMargin, lastY, availableWidth, CommandColor, out _, out lastY);

                if (!string.IsNullOrEmpty(commandHelp.Shortcut))
                    gridStringBuilder.DrawWrapped($"Shortcut: {commandHelp.Shortcut}", leftMargin, lastY + 2, availableWidth, CommandColor, out _, out lastY);

                gridStringBuilder.DrawWrapped($"Description: {commandHelp.Description.EnsureFinishedSentence()}", leftMargin, lastY + 2, availableWidth, CommandDescriptionColor, out _, out lastY);

                if (!string.IsNullOrEmpty(commandHelp.Instructions))
                    gridStringBuilder.DrawWrapped($"Instructions: {commandHelp.Instructions.EnsureFinishedSentence()}", leftMargin, lastY + 2, availableWidth, CommandDescriptionColor, out _, out lastY);

                if (!string.IsNullOrEmpty(commandHelp.DisplayAs))
                    gridStringBuilder.DrawWrapped($"Example: {commandHelp.DisplayAs}", leftMargin, lastY + 2, availableWidth, CommandDescriptionColor, out _, out lastY);

                if (ShowPrompts && prompts != null && prompts.Length > 0)
                {
                    StringBuilder promptBuilder = new();

                    foreach (var prompt in prompts)
                        promptBuilder.Append($"'{prompt.Entry}' ");

                    var promptString = promptBuilder.ToString();

                    if (!string.IsNullOrEmpty(promptString))
                        gridStringBuilder.DrawWrapped($"Prompts: {promptString}", leftMargin, lastY + 2, availableWidth, PromptsColor, out _, out _);
                }
            }

            return new GridTextFrame(gridStringBuilder, 0, 0, BackgroundColor) { ShowCursor = false };
        }

        #endregion
    }
}
