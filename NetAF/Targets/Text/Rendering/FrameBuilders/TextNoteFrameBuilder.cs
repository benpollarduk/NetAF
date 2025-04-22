using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Logging.Notes;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using System.Text;

namespace NetAF.Targets.Text.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of note frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    public sealed class TextNoteFrameBuilder(StringBuilder builder) : INoteFrameBuilder
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

            builder.AppendLine(title);
            builder.AppendLine();

            if (!string.IsNullOrEmpty(description))
                builder.AppendLine(description);

            if (entries.Length > 0)
            {
                for (var i = 0; i < entries.Length; i++)
                {
                    builder.AppendLine($"{entries[i].Content.EnsureFinishedSentence()}");

                    if (i < entries.Length - 1)
                        builder.AppendLine();
                }
            }
            else
            {
                builder.AppendLine("No entries.");
            }

            return new TextFrame(builder);
        }

        #endregion
    }
}
