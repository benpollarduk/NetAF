using System.Linq;
using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Log;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;

namespace NetAF.Targets.Console.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of log frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    public sealed class ConsoleLogFrameBuilder(GridStringBuilder gridStringBuilder) : ILogFrameBuilder
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
        /// Get or set the description color.
        /// </summary>
        public AnsiColor DescriptionColor { get; set; } = AnsiColor.White;

        /// <summary>
        /// Get or set the entry color.
        /// </summary>
        public AnsiColor EntryColor { get; set; } = NetAFPalette.NetAFYellow;

        #endregion

        #region Implementation of ILogFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="entries">The entries.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        public IFrame Build(string title, string description, LogEntry[] entries, Size size)
        {
            gridStringBuilder.Resize(size);

            var availableWidth = size.Width - 4;
            const int leftMargin = 2;

            entries ??= [];

            gridStringBuilder.DrawWrapped(title, leftMargin, 2, availableWidth, TitleColor, out _, out var lastY);
            gridStringBuilder.DrawUnderline(leftMargin, lastY + 1, title.Length, TitleColor);

            if (!string.IsNullOrEmpty(description))
                gridStringBuilder.DrawCentralisedWrapped(description, lastY + 3, availableWidth, DescriptionColor, out _, out lastY);

            lastY += 2;

            if (entries.Length != 0)
            {
                foreach (var entry in entries)
                {
                    if (lastY >= size.Height - 1)
                        break;

                    gridStringBuilder.DrawWrapped(entry.Content.EnsureFinishedSentence(), leftMargin, lastY + 1, availableWidth, EntryColor, out _, out lastY);
                    lastY++;
                }
            }
            else
            {
                gridStringBuilder.DrawWrapped("No entries.", leftMargin, lastY + 1, availableWidth, EntryColor, out _, out lastY);
            }

            gridStringBuilder.DrawBoundary(BorderColor);

            return new GridTextFrame(gridStringBuilder, 0, 0, BackgroundColor) { ShowCursor = false };
        }

        #endregion
    }
}
