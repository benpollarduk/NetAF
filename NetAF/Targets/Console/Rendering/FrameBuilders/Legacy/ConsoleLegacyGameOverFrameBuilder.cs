using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;

namespace NetAF.Targets.Console.Rendering.FrameBuilders.Legacy
{
    /// <summary>
    /// Provides a builder of game over frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    /// <param name="properties">The render properties.</param>
    public sealed class ConsoleLegacyGameOverFrameBuilder(GridStringBuilder gridStringBuilder, ConsoleRenderProperties properties) : ConsoleLegacyFrameBuilder, IGameOverFrameBuilder
    {
        #region Implementation of IGameOverFrameBuilder

        /// <inheritdoc/>
        public IFrame Build(string message, string reason, Size size)
        {
            gridStringBuilder.Resize(size);

            const int xMargin = 2;
            var availableWidth = size.Width - (xMargin * 2);
            var constrainedWidth = GetConstrainedWidth(availableWidth, properties.MaximumNarrativeLineWidth);

            gridStringBuilder.DrawWrapped(message, xMargin, 2, constrainedWidth, properties.TitleColor, out _, out var lastY);

            gridStringBuilder.DrawUnderline(xMargin, lastY + 1, message.Length, properties.TitleColor);

            gridStringBuilder.DrawWrapped(reason.EnsureFinishedSentence(), xMargin, lastY + 3, constrainedWidth, properties.MainTextColor, out _, out _);

            gridStringBuilder.DrawBoundary(properties.LeftBoundary, properties.RightBoundary, properties.HorizontalDivider, properties.BorderColor);

            return new GridTextFrame(gridStringBuilder, 0, 0, false, properties.BackgroundColor, properties.InputColor);
        }

        #endregion
    }
}
