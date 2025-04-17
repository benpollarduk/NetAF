using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Logging.History;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using System.Linq;

namespace NetAF.Targets.Html.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of history frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    public sealed class HtmlHistoryFrameBuilder(HtmlBuilder builder) : IHistoryFrameBuilder
    {
        #region Properties

        /// <summary>
        /// Get or set the maximum number of entries to be displayed. For unlimited use HistoryManager.NoLimit.
        /// </summary>
        public int MaxEntries { get; set; } = HistoryManager.NoLimit;

        #endregion

        #region Implementation of IHistoryFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="entries">The entries.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        public IFrame Build(string title, string description, HistoryEntry[] entries, Size size)
        {
            entries ??= [];

            // sort entries, newest first
            entries = [.. entries.OrderByDescending(x => x.CreationTime)];

            if (MaxEntries >= 0)
                entries = [.. entries.Take(MaxEntries)];

            builder.Clear();

            builder.H1(title);
            builder.Br();

            if (!string.IsNullOrEmpty(description))
                builder.P(description);

            if (entries.Length > 0)
            {
                for (var i = 0; i < entries.Length; i++)
                {
                    builder.P($"{entries[i].Content.EnsureFinishedSentence()}");

                    if (i < entries.Length - 1)
                        builder.Br();
                }
            }
            else
            {
                builder.P("No entries.");
            }

            return new HtmlFrame(builder);
        }

        #endregion
    }
}
