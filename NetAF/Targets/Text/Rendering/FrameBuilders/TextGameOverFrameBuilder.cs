using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using System.Text;

namespace NetAF.Targets.Text.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of game over frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    public sealed class TextGameOverFrameBuilder(StringBuilder builder) : IGameOverFrameBuilder
    {
        #region Implementation of IGameOverFrameBuilder

        /// <inheritdoc/>
        public IFrame Build(string message, string reason, Size size)
        {
            builder.Clear();

            builder.AppendLine(message);

            builder.AppendLine(reason.EnsureFinishedSentence());

            return new TextFrame(builder);
        }

        #endregion
    }
}
