using NetAF.Targets.Console.Rendering;
using NetAF.Targets.General.FrameBuilders;

namespace NetAF.Targets.Markup.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a room map builder.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    /// <param name="properties">The render properties.</param>
    public sealed class MarkupRoomMapBuilder(MarkupBuilder builder, ConsoleMapRenderProperties properties) : GeneralRoomMapBuilder(properties)
    {
        #region Overrides of HostedRoomMapBuilder

        /// <inheritdoc/>
        protected override void Adapt(GridStringBuilder roomMapBuilder)
        {
            builder.Raw(MarkupAdapter.ConvertGridStringBuilderToMarkupString(roomMapBuilder, useMonospace: true));
        }

        #endregion
    }
}
