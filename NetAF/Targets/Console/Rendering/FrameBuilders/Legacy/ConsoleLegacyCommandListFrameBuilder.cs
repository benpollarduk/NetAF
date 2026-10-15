using System.Linq;
using NetAF.Assets;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;

namespace NetAF.Targets.Console.Rendering.FrameBuilders.Legacy
{
    /// <summary>
    /// Provides a builder of command list frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    /// <param name="properties">The render properties.</param>
    public sealed class ConsoleLegacyCommandListFrameBuilder(GridStringBuilder gridStringBuilder, ConsoleRenderProperties properties) : ConsoleLegacyFrameBuilder, ICommandListFrameBuilder
    {
        #region Implementation of ICommandListFrameBuilder

        /// <inheritdoc/>
        public IFrame Build(string title, string description, CommandHelp[] commandHelp, Size size)
        {
            gridStringBuilder.Resize(size);

            const int xMargin = 2;
            var availableWidth = size.Width - (xMargin * 2);
            var constrainedWidth = GetConstrainedWidth(availableWidth, properties.MaximumNarrativeLineWidth);
            var padding = (commandHelp.Any() ? commandHelp.Max(x => x.DisplayCommand.Length) : 0) + 1;

            gridStringBuilder.DrawWrapped(title, xMargin, 2, constrainedWidth, properties.TitleColor, out _, out var lastY);
            gridStringBuilder.DrawUnderline(xMargin, lastY + 1, title.Length, properties.TitleColor);

            if (!string.IsNullOrEmpty(description))
                gridStringBuilder.DrawCentralisedWrapped(description, lastY + 3, constrainedWidth, properties.MainTextColor, out _, out lastY);

            lastY += 2;

            foreach (var command in commandHelp)
            {
                if (lastY >= size.Height - 1)
                    break;

                if (!string.IsNullOrEmpty(command.DisplayCommand) && !string.IsNullOrEmpty(command.Description))
                {
                    gridStringBuilder.DrawWrapped(command.DisplayCommand, xMargin, lastY + 1, constrainedWidth, properties.CommandColor, out _, out lastY);
                    gridStringBuilder.DrawWrapped("-", xMargin + padding, lastY, constrainedWidth, properties.CommandDescriptionColor, out _, out lastY);
                    gridStringBuilder.DrawWrapped(command.Description.EnsureFinishedSentence(), xMargin + padding + 2, lastY, constrainedWidth, properties.CommandDescriptionColor, out _, out lastY);
                }
                else if (!string.IsNullOrEmpty(command.DisplayCommand) && string.IsNullOrEmpty(command.Description))
                {
                    gridStringBuilder.DrawWrapped(command.DisplayCommand, xMargin, lastY + 1, constrainedWidth, properties.CommandColor, out _, out lastY);
                }
            }

            gridStringBuilder.DrawBoundary(properties.LeftBoundary, properties.RightBoundary, properties.HorizontalDivider, properties.BorderColor);

            return new GridTextFrame(gridStringBuilder, 0, 0, properties.BackgroundColor) { ShowCursor = false };
        }

        #endregion
    }
}
