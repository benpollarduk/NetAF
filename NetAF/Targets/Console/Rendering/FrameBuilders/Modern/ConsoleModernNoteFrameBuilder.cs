using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Logging.Notes;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Console.Rendering.FrameBuilders.Legacy;

namespace NetAF.Targets.Console.Rendering.FrameBuilders.Modern
{
    /// <summary>
    /// Provides a builder of note frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    /// <param name="properties">The render properties.</param>
    public sealed class ConsoleModernNoteFrameBuilder(GridStringBuilder gridStringBuilder, ConsoleRenderProperties properties) : ConsoleLegacyFrameBuilder, INoteFrameBuilder
    {
        #region Implementation of INoteFrameBuilder

        /// <inheritdoc/>
        public IFrame Build(string title, string description, NoteEntry[] entries, Size size)
        {
            gridStringBuilder.Resize(size);

            const int xMargin = 2;
            var availableWidth = size.Width - (xMargin * 2);
            var constrainedWidth = GetConstrainedWidth(availableWidth, properties.MaximumNarrativeLineWidth);

            entries ??= [];

            gridStringBuilder.DrawWrapped(title, xMargin, 2, constrainedWidth, properties.TitleColor, out _, out var lastY);

            if (!string.IsNullOrEmpty(description))
                gridStringBuilder.DrawWrapped(description, xMargin, lastY + 3, constrainedWidth, properties.MainTextColor, out _, out lastY);

            lastY += 2;

            if (entries.Length != 0)
            {
                foreach (var entry in entries)
                {
                    if (lastY >= size.Height - 1)
                        break;

                    gridStringBuilder.DrawWrapped(entry.Content.EnsureFinishedSentence(), xMargin, lastY + 1, constrainedWidth, entry.HasExpired ? properties.SecondaryTextColor : properties.MainTextColor, out _, out lastY);
                    lastY++;
                }
            }
            else
            {
                gridStringBuilder.DrawWrapped("No entries.", xMargin, lastY + 1, constrainedWidth, properties.InputColor, out _, out lastY);
            }

            return new GridTextFrame(gridStringBuilder, 0, 0, false, properties.BackgroundColor, properties.InputColor);
        }

        #endregion
    }
}
