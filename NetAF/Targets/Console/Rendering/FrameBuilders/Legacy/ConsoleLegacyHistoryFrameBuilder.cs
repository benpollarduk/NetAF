using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Logging.History;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using System.Linq;

namespace NetAF.Targets.Console.Rendering.FrameBuilders.Legacy
{
    /// <summary>
    /// Provides a builder of history frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    /// <param name="properties">The render properties.</param>
    public sealed class ConsoleLegacyHistoryFrameBuilder(GridStringBuilder gridStringBuilder, ConsoleRenderProperties properties) : ConsoleLegacyFrameBuilder, IHistoryFrameBuilder
    {
        #region Properties

        /// <summary>
        /// Get or set the maximum number of entries to be displayed. For unlimited use HistoryManager.NoLimit.
        /// </summary>
        public int MaxEntries { get; set; } = HistoryManager.NoLimit;

        #endregion

        #region Implementation of IHistoryFrameBuilder

        /// <inheritdoc/>
        public IFrame Build(string title, string description, HistoryEntry[] entries, Size size)
        {
            gridStringBuilder.Resize(size);

            const int xMargin = 2;
            var availableWidth = size.Width - (xMargin * 2);
            var constrainedWidth = GetConstrainedWidth(availableWidth, properties.MaximumNarrativeLineWidth);

            entries ??= [];

            // sort entries, newest first
            entries = [.. entries.OrderByDescending(x => x.CreationTime)];

            if (MaxEntries >= 0)
                entries = [.. entries.Take(MaxEntries)];

            gridStringBuilder.DrawWrapped(title, xMargin, 2, constrainedWidth, properties.TitleColor, out _, out var lastY);
            gridStringBuilder.DrawUnderline(xMargin, lastY + 1, title.Length, properties.TitleColor);

            if (!string.IsNullOrEmpty(description))
                gridStringBuilder.DrawCentralisedWrapped(description, lastY + 3, constrainedWidth, properties.MainTextColor, out _, out lastY);

            lastY += 2;

            if (entries.Length != 0)
            {
                foreach (var entry in entries)
                {
                    if (lastY >= size.Height - 1)
                        break;

                    gridStringBuilder.DrawWrapped(entry.Content.EnsureFinishedSentence(), xMargin, lastY + 1, constrainedWidth, properties.InputColor, out _, out lastY);
                    lastY++;
                }
            }
            else
            {
                gridStringBuilder.DrawWrapped("No entries.", xMargin, lastY + 1, constrainedWidth, properties.InputColor, out _, out lastY);
            }

            gridStringBuilder.DrawBoundary(properties.LeftBoundary, properties.RightBoundary, properties.HorizontalDivider, properties.BorderColor);

            return new GridTextFrame(gridStringBuilder, 0, 0, false, properties.BackgroundColor, properties.InputColor);
        }

        #endregion
    }
}
