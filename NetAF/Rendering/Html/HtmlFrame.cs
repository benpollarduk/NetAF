using System.Text;

namespace NetAF.Rendering.Html
{
    /// <summary>
    /// Provides an HTML frame for displaying a command based interface.
    /// </summary>
    /// <param name="builder">The builder that creates the frame.</param>
    public sealed class HtmlFrame(HtmlBuilder builder) : IFrame
    {
        #region Properties

        /// <summary>
        /// Get or set the CSS to use for styling the frame.
        /// </summary>
        public string Css { get; set; } = string.Empty;

        #endregion

        #region Overrides of Object

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            const string openDoc = @"<!DOCTYPE html><html lang=""en"">";
            const string closeDoc = @"</html>";
            const string openHeadStyle = @"<head><style>";
            const string closeHeadStyle = @"</style></head>";
            const string openBody = @"<body><div>";
            const string closeBody = @"</div></body>";

            StringBuilder htmlBuilder = new();
            htmlBuilder.Append(openDoc);
            htmlBuilder.Append(openHeadStyle);
            htmlBuilder.Append(Css);
            htmlBuilder.Append(closeHeadStyle);
            htmlBuilder.Append(openBody);
            htmlBuilder.Append(builder.ToString());
            htmlBuilder.Append(closeBody);
            htmlBuilder.Append(closeDoc);

            return htmlBuilder.ToString();
        }

        #endregion

        #region Implementation of IFrame<Inline>

        /// <summary>
        /// Render this frame on a presenter.
        /// </summary>
        /// <param name="presenter">The presenter.</param>
        public void Render(IFramePresenter presenter)
        {
            presenter.Write(ToString());
        }

        #endregion
    }
}