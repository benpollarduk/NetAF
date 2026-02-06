using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;

namespace NetAF.Targets.Markup.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of reaction frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    public sealed class MarkupReactionFrameBuilder(MarkupBuilder builder) : IReactionFrameBuilder
    {
        #region Implementation of IReactionFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title to display to the user.</param>
        /// <param name="message">The message to display to the user.</param>
        /// <param name="isError">If the message is an error.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        public IFrame Build(string title, string message, bool isError, Size size)
        {
            builder.Clear();

            if (!string.IsNullOrEmpty(title))
            {
                builder.Heading(title, HeadingLevel.H1);
                builder.Newline();
            }

            builder.WriteLine(message.EnsureFinishedSentence());

            return new MarkupFrame(builder);
        }

        #endregion
    }
}
