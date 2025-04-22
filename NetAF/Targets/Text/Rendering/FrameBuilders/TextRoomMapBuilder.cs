using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Hosted.Rendering.FrameBuilders;
using System.Text;

namespace NetAF.Targets.Text.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a room map builder.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    public sealed class TextRoomMapBuilder(StringBuilder builder) : HostedRoomMapBuilder
    {
        #region Overrides of HostedRoomMapBuilder

        /// <summary>
        /// Adapt the room map for the target.
        /// </summary>
        /// <param name="roomMapBuilder">The room map builder.</param>
        protected override void Adapt(GridStringBuilder roomMapBuilder)
        {
            var roomAsString = TextAdapter.ConvertGridStringBuilderToString(roomMapBuilder.ToCropped());
            builder.AppendLine(roomAsString);
        }

        #endregion
    }
}
