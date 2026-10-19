using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Console.Rendering.FrameBuilders.Legacy;

namespace NetAF.Targets.Console.Rendering.FrameBuilders.Modern
{
    /// <summary>
    /// Provides a builder of completion frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    /// <param name="properties">The render properties.</param>
    public sealed class ConsoleModernCompletionFrameBuilder(GridStringBuilder gridStringBuilder, ConsoleRenderProperties properties) : ConsoleLegacyFrameBuilder, ICompletionFrameBuilder
    {
        #region Implementation of ICompletionFrameBuilder

        /// <inheritdoc/>
        public IFrame Build(string message, string reason, Size size)
        {
            gridStringBuilder.Resize(size);

            const int xMargin = 2;
            var availableWidth = size.Width - (xMargin * 2);
            var constrainedWidth = GetConstrainedWidth(availableWidth, properties.MaximumNarrativeLineWidth);

            gridStringBuilder.DrawWrapped(message, xMargin, 2, constrainedWidth, properties.TitleColor, out _, out var lastY);

            gridStringBuilder.DrawWrapped(reason.EnsureFinishedSentence(), xMargin, lastY + 3, constrainedWidth, properties.MainTextColor, out _, out _);

            return new GridTextFrame(gridStringBuilder, 0, 0, false, properties.BackgroundColor, properties.InputColor);
        }

        #endregion
    }
}
