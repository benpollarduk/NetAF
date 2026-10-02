using NetAF.Assets;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;

namespace NetAF.Targets.Html.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of title frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    public sealed class HtmlTitleFrameBuilder(HtmlBuilder builder) : ITitleFrameBuilder
    {
        #region Implementation of ITitleFrameBuilder

        /// <inheritdoc/>
        public IFrame Build(string title, string description, Size size)
        {
            builder.Clear();

            builder.H1(title);
            builder.Br();
            builder.P(description);

            return new HtmlFrame(builder);
        }

        #endregion
    }
}
