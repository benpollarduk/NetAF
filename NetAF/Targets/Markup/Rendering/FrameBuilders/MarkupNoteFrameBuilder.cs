using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Logging.Notes;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;

namespace NetAF.Targets.Markup.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of note frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    public sealed class MarkupNoteFrameBuilder(MarkupBuilder builder) : INoteFrameBuilder
    {
        #region Implementation of INoteFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="description">The description.</param>
        /// <param name="entries">The entries.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        public IFrame Build(string title, string description, NoteEntry[] entries, Size size)
        {
            entries ??= [];

            builder.Clear();

            builder.Heading(title, HeadingLevel.H1);
            builder.Newline();

            if (!string.IsNullOrEmpty(description))
                builder.WriteLine(description);

            if (entries.Length > 0)
            {
                for (var i = 0; i < entries.Length; i++)
                {
                    if (entries[i].HasExpired)
                        builder.WriteLine($"{entries[i].Content.EnsureFinishedSentence()}");
                    else
                        builder.WriteLine($"{entries[i].Content.EnsureFinishedSentence()}", new TextStyle(Strikethrough: true));
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
