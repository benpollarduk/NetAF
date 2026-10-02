using NetAF.Assets;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using System.Text;

namespace NetAF.Targets.Text.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of title frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    public sealed class TextTitleFrameBuilder(StringBuilder builder) : ITitleFrameBuilder
    {
        #region Implementation of ITitleFrameBuilder

        /// <inheritdoc/>
        public IFrame Build(string title, string description, Size size)
        {
            builder.Clear();

            builder.AppendLine(title);
            builder.AppendLine();
            builder.AppendLine(description);

            return new TextFrame(builder);
        }

        #endregion
    }
}
