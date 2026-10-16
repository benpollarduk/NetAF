using NetAF.Assets;
using NetAF.Extensions;
using NetAF.Logic;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Console.Rendering.FrameBuilders.Legacy;
using NetAF.Utilities;

namespace NetAF.Targets.Console.Rendering.FrameBuilders.Modern
{
    /// <summary>
    /// Provides a builder of console about frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    /// <param name="properties">The render properties.</param>
    public sealed class ConsoleModernAboutFrameBuilder(GridStringBuilder gridStringBuilder, ConsoleRenderProperties properties) : ConsoleLegacyFrameBuilder, IAboutFrameBuilder
    {
        #region Implementation of IAboutFrameBuilder

        /// <inheritdoc/>
        public IFrame Build(string title, Game game, Size size)
        {
            gridStringBuilder.Resize(size);

            const int xMargin = 2;
            var availableWidth = size.Width - (xMargin * 2);
            var constrainedWidth = GetConstrainedWidth(availableWidth, properties.MaximumNarrativeLineWidth);

            gridStringBuilder.DrawWrapped(title, xMargin, 2, constrainedWidth, properties.TitleColor, out _, out var lastY);

            gridStringBuilder.DrawWrapped(game.Info.Name, xMargin, lastY + 3, constrainedWidth, properties.TitleColor, out _, out lastY);

            gridStringBuilder.DrawWrapped(game.Info.Description.EnsureFinishedSentence(), xMargin, lastY + 2, constrainedWidth, properties.MainTextColor, out _, out lastY);

            if (!string.IsNullOrEmpty(game.Info.Author))
                gridStringBuilder.DrawWrapped($"Created by: {game.Info.Author}".EnsureFinishedSentence(), xMargin, lastY + 2, constrainedWidth, properties.MainTextColor, out _, out _);
            else
                gridStringBuilder.DrawWrapped(Info.AboutNetAF, xMargin, lastY + 2, constrainedWidth, properties.MainTextColor, out _, out _);

            return new GridTextFrame(gridStringBuilder, 0, 0, properties.BackgroundColor) { ShowCursor = false };
        }

        #endregion
    }
}
