using NetAF.Targets.Console.Rendering;
using NetAF.Targets.General.FrameBuilders;
using System.Text;

namespace NetAF.Targets.Text.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a room map builder.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    public sealed class TextRoomMapBuilder(StringBuilder builder) : GeneralRoomMapBuilder
    {
        #region Overrides of HostedRoomMapBuilder

        /// <inheritdoc/>
        protected override void Adapt(GridStringBuilder roomMapBuilder)
        {
            var roomAsString = TextAdapter.ConvertGridStringBuilderToString(roomMapBuilder.ToCropped());
            builder.AppendLine(roomAsString);
        }

        #endregion
    }
}
