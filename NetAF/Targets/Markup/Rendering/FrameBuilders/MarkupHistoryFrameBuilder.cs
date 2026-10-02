using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Logging.History;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using System.Linq;

namespace NetAF.Targets.Markup.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of history frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    public sealed class MarkupHistoryFrameBuilder(MarkupBuilder builder) : IHistoryFrameBuilder
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
            entries ??= [];

            // sort entries, newest first
            entries = [.. entries.OrderByDescending(x => x.CreationTime)];

            if (MaxEntries >= 0)
                entries = [.. entries.Take(MaxEntries)];

            builder.Clear();

            builder.Heading(title, HeadingLevel.H1);
            builder.Newline();

            if (!string.IsNullOrEmpty(description))
                builder.WriteLine(description);

            if (entries.Length > 0)
            {
                foreach (var entry in entries)
                {
                    builder.WriteLine(entry.Content.EnsureFinishedSentence());
                    builder.Newline();
                }
            }
            else
            {
                builder.WriteLine("No entries.");
            }

            return new MarkupFrame(builder);
        }

        #endregion
    }
}
