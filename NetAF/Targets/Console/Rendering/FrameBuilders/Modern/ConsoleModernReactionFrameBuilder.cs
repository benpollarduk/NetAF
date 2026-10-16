using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Console.Rendering.FrameBuilders.Legacy;
using NetAF.Utilities;

namespace NetAF.Targets.Console.Rendering.FrameBuilders.Modern
{
    /// <summary>
    /// Provides a builder of reaction frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    /// <param name="properties">The render properties.</param>
    public sealed class ConsoleModernReactionFrameBuilder(GridStringBuilder gridStringBuilder, ConsoleRenderProperties properties) : ConsoleLegacyFrameBuilder, IReactionFrameBuilder
    {
        #region Implementation of IReactionFrameBuilder

        /// <inheritdoc/>
        public IFrame Build(string title, string message, bool isError, string[] keywords, Size size)
        {
            gridStringBuilder.Resize(size);

            const int xMargin = 2;
            var availableWidth = size.Width - (xMargin * 2);
            var constrainedWidth = GetConstrainedWidth(availableWidth, properties.MaximumNarrativeLineWidth);
            var lastY = 2;

            if (!properties.HighlightKeywords)
                keywords = [];

            if (!string.IsNullOrEmpty(title))
            {
                gridStringBuilder.DrawWrapped(title, xMargin, lastY, constrainedWidth, properties.TitleColor, out _, out lastY);

                lastY += 3;
            }

            var finishedMessage = message.EnsureFinishedSentence();
            gridStringBuilder.DrawWrapped(finishedMessage, xMargin, lastY, constrainedWidth, properties.MainTextColor, properties.HighlightTextColor, StringUtilities.LocateStringMarkers(finishedMessage, keywords), out _, out _);

            return new GridTextFrame(gridStringBuilder, 0, 0, properties.BackgroundColor) { ShowCursor = false };
        }

        #endregion
    }
}
