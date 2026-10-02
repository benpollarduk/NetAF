using NetAF.Targets.Console.Rendering;
using NetAF.Targets.General.FrameBuilders;

namespace NetAF.Targets.Markup.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder for region maps.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    public sealed class MarkupRegionMapBuilder(MarkupBuilder builder) : GeneralRegionMapBuilder
    {
        #region Overrides of HostedRegionMapBuilder

        /// <inheritdoc/>
        protected override void Adapt(GridStringBuilder regionMapBuilder)
        {
            builder.Raw(MarkupAdapter.ConvertGridStringBuilderToMarkupString(regionMapBuilder, useMonospace: true));
        }

        #endregion
    }
}
