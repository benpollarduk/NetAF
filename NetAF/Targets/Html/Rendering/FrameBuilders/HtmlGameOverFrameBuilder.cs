using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;

namespace NetAF.Targets.Html.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of game over frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    public sealed class HtmlGameOverFrameBuilder(HtmlBuilder builder) : IGameOverFrameBuilder
    {
        #region Implementation of IGameOverFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="message">The message to display to the user.</param>
        /// <param name="reason">The reason the game ended.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        public IFrame Build(string message, string reason, Size size)
        {
            builder.Clear();

            builder.H1(message);

            builder.P(reason.EnsureFinishedSentence());

            return new HtmlFrame(builder);
        }

        #endregion
    }
}
