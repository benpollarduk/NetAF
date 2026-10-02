using System.Text;
using NetAF.Rendering;

namespace NetAF.Targets.Html.Rendering
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public void Render(IFramePresenter presenter)
        {
            presenter.Present(ToString());
        }

        #endregion
    }
}