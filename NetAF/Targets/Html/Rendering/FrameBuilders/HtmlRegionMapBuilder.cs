using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Hosted.Rendering.FrameBuilders;

namespace NetAF.Targets.Html.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder for region maps.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    public sealed class HtmlRegionMapBuilder(HtmlBuilder builder) : HostedRegionMapBuilder
    {
        #region Overrides of HostedRegionMapBuilder

        /// <summary>
        /// Adapt the region map for the target.
        /// </summary>
        /// <param name="regionMapBuilder">The region map builder.</param>
        protected override void Adapt(GridStringBuilder regionMapBuilder)
        {
            var regionAsString = HtmlAdapter.ConvertGridStringBuilderToHtmlString(regionMapBuilder);

            // append as raw HTML using styling to specify monospace for correct horizontal alignment and pre to preserve whitespace
            builder.Raw($"<pre style=\"font-family: 'Courier New', Courier, monospace;\">{regionAsString}</pre>");
        }

        #endregion
    }
}
