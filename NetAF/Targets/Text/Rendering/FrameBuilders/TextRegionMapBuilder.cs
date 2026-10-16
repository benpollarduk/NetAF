using NetAF.Targets.Console.Rendering;
using NetAF.Targets.General.FrameBuilders;
using System.Text;

namespace NetAF.Targets.Text.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder for region maps.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    /// <param name="properties">The render properties.</param>
    public sealed class TextRegionMapBuilder(StringBuilder builder, ConsoleMapRenderProperties properties) : GeneralRegionMapBuilder(properties)
    {
        #region Overrides of HostedRegionMapBuilder

        /// <inheritdoc/>
        protected override void Adapt(GridStringBuilder regionMapBuilder)
        {
            var regionAsString = TextAdapter.ConvertGridStringBuilderToString(regionMapBuilder);
            builder.AppendLine(regionAsString);
        }

        #endregion
    }
}
