using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Hosted.Rendering.FrameBuilders;

namespace NetAF.Targets.Html.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a room map builder.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    public sealed class HtmlRoomMapBuilder(HtmlBuilder builder) : HostedRoomMapBuilder
    {
        #region Overrides of HostedRoomMapBuilder

        /// <summary>
        /// Adapt the room map for the target.
        /// </summary>
        /// <param name="roomMapBuilder">The room map builder.</param>
        protected override void Adapt(GridStringBuilder roomMapBuilder)
        {
            var roomAsString = HtmlAdapter.ConvertGridStringBuilderToHtmlString(roomMapBuilder.ToCropped());

            // append as raw HTML using styling to specify monospace for correct horizontal alignment and pre to preserve whitespace
            builder.Raw($"<pre style=\"font-family: 'Courier New', Courier, monospace;\">{roomAsString}</pre>");
        }

        #endregion
    }
}
