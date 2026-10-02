using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using System.Text;

namespace NetAF.Targets.Text.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of reaction frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    public sealed class TextReactionFrameBuilder(StringBuilder builder) : IReactionFrameBuilder
    {
        #region Implementation of IReactionFrameBuilder

        /// <inheritdoc/>
        public IFrame Build(string title, string message, bool isError, Size size)
        {
            builder.Clear();

            if (!string.IsNullOrEmpty(title))
            {
                builder.AppendLine(title);
                builder.AppendLine();
            }

            builder.AppendLine(message.EnsureFinishedSentence());

            return new TextFrame(builder);
        }

        #endregion
    }
}
