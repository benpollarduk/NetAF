using NetAF.Targets.Console.Rendering;
using NetAF.Targets.General.FrameBuilders;

namespace NetAF.Targets.Markup.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a room map builder.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    public sealed class MarkupRoomMapBuilder(MarkupBuilder builder) : GeneralRoomMapBuilder
    {
        #region Overrides of HostedRoomMapBuilder

        /// <summary>
        /// Adapt the room map for the target.
        /// </summary>
        /// <param name="roomMapBuilder">The room map builder.</param>
        protected override void Adapt(GridStringBuilder roomMapBuilder)
        {
            builder.Raw(MarkupAdapter.ConvertGridStringBuilderToMarkupString(roomMapBuilder, useMonospace: true));
        }

        #endregion
    }
}
