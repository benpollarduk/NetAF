using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;

namespace NetAF.Targets.Console.Rendering.FrameBuilders.Legacy
{
    /// <summary>
    /// Provides a builder of title frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    /// <param name="properties">The render properties.</param>
    public sealed class ConsoleLegacyTitleFrameBuilder(GridStringBuilder gridStringBuilder, ConsoleRenderProperties properties) : ConsoleLegacyFrameBuilder, ITitleFrameBuilder
    {
        #region Implementation of ITitleFrameBuilder

        /// <inheritdoc/>
        public IFrame Build(string title, string description, Size size)
        {
            gridStringBuilder.Resize(size);

            const int xMargin = 2;
            var availableWidth = size.Width - (xMargin * 2);
            var constrainedWidth = GetConstrainedWidth(availableWidth, properties.MaximumNarrativeLineWidth);

            gridStringBuilder.DrawWrapped(title, xMargin, 2, constrainedWidth, properties.TitleColor, out _, out var lastY);

            gridStringBuilder.DrawUnderline(xMargin, lastY + 1, title.Length, properties.TitleColor);

            gridStringBuilder.DrawWrapped(description.EnsureFinishedSentence(), xMargin, lastY + 3, constrainedWidth, properties.MainTextColor, out _, out _);

            gridStringBuilder.DrawBoundary(properties.LeftBoundary, properties.RightBoundary, properties.HorizontalDivider, properties.BorderColor);

            return new GridTextFrame(gridStringBuilder, 0, 0, properties.BackgroundColor) { ShowCursor = false };
        }

        #endregion
    }
}
