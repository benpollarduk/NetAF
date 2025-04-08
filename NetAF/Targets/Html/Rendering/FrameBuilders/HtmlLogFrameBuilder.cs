using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Log;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;

namespace NetAF.Targets.Html.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of log frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    public sealed class HtmlLogFrameBuilder(HtmlBuilder builder) : ILogFrameBuilder
    {
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
            entries ??= [];

            builder.Clear();

            builder.H1(title);
            builder.Br();

            if (!string.IsNullOrEmpty(description))
                builder.P(description);

            if (entries.Length > 0)
            {
                for (var i = 0; i < entries.Length; i++)
                {
                    if (entries[i].HasExpired)
                        builder.Raw("<s>");

                    builder.P($"{entries[i].Content.EnsureFinishedSentence()}");

                    if (entries[i].HasExpired)
                        builder.Raw("</s>");

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
