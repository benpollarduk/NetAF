using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;

namespace NetAF.Targets.Html.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of completion frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    public sealed class HtmlCompletionFrameBuilder(HtmlBuilder builder) : ICompletionFrameBuilder
    {
        #region Implementation of ICompletionFrameBuilder

        /// <inheritdoc/>
        public IFrame Build(string message, string reason, Size size)
        {
            builder.Clear();

            builder.H1(message);

            builder.P(reason.EnsureFinishedSentence());

            return new HtmlFrame(builder);
        }

        #endregion
    }
}
