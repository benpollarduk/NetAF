using System;
using System.Collections.Generic;
using System.Linq;
using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Commands;
using NetAF.Conversations;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;

namespace NetAF.Targets.Console.Rendering.FrameBuilders.Legacy
{
    /// <summary>
    /// Provides a builder of conversation frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    /// <param name="properties">The render properties.</param>
    /// <param name="renderPrompt">Specify if the prompt should be rendered.</param>
    public sealed class ConsoleLegacyConversationFrameBuilder(GridStringBuilder gridStringBuilder, ConsoleRenderProperties properties, bool renderPrompt = true) : ConsoleLegacyFrameBuilder, IConversationFrameBuilder
    {
        #region Properties

        /// <summary>
        /// Get or set the command title.
        /// </summary>
        public string CommandTitle { get; set; } = "You can:";

        #endregion

        #region StaticMethods

        /// <summary>
        /// Truncate a log.
        /// </summary>
        /// <param name="startX">The start X position, in the grid.</param>
        /// <param name="availableWidth">The available width.</param>
        /// <param name="availableHeight">The available height.</param>
        /// <param name="log">The log.</param>
        /// <returns>The truncated log.</returns>
        internal static LogItem[] TruncateLog(int startX, int availableWidth, int availableHeight, LogItem[] log)
        {
            if (log == null)
                return [];

            List<LogItem> truncated = [];

            for (var i = log.Length - 1; i >= 0; i--)
            {
                var lines = GridStringBuilder.GetNumberOfLines(log[i].Line, 0, availableWidth);

                availableHeight -= lines;

                if (availableHeight >= 0)
                    truncated.Add(log[i]);
                else
                    break;
            }

            truncated.Reverse();

            return [.. truncated];
        }

        #endregion

        #region Implementation of IConversationFrameBuilder

        /// <inheritdoc/>
        public IFrame Build(string title, IConverser converser, CommandHelp[] contextualCommands, Size size)
        {
            gridStringBuilder.Resize(size);

            const int xMargin = 2;
            var availableWidth = size.Width - (xMargin * 2);
            var availableHeight = size.Height - 2;
            var constrainedWidth = GetConstrainedWidth(availableWidth, properties.MaximumNarrativeLineWidth);
            const int linePadding = 2;
            var lastY = 2;

            if (!string.IsNullOrEmpty(title))
            {
                gridStringBuilder.DrawWrapped(title, xMargin, lastY, constrainedWidth, properties.TitleColor, out _, out lastY);
                gridStringBuilder.DrawUnderline(xMargin, lastY + 1, title.Length, properties.TitleColor);
                lastY += 2;
            }

            if (converser?.Conversation?.Log != null)
            {
                var spaceForLog = availableHeight - 10 - contextualCommands?.Length ?? 0;
                var truncatedLog = TruncateLog(xMargin, constrainedWidth, spaceForLog, converser.Conversation.Log);

                foreach (var log in truncatedLog)
                {
                    lastY++;
                    switch (log.Participant)
                    {
                        case Participant.Player:
                            gridStringBuilder.DrawWrapped("You: " + log.Line, xMargin, lastY, constrainedWidth, properties.OptionColor, out _, out lastY);
                            break;
                        case Participant.Other:
                            gridStringBuilder.DrawWrapped($"{converser.Identifier.Name}: " + log.Line, xMargin, lastY, constrainedWidth, properties.OptionDescriptionColor, out _, out lastY);
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
            }

            if (contextualCommands?.Any() ?? false)
            {
                gridStringBuilder.DrawHorizontalDivider(lastY + linePadding, properties.HorizontalDivider, properties.BorderColor);
                gridStringBuilder.DrawWrapped(CommandTitle, xMargin, lastY + 4, availableWidth, properties.OptionColor, out _, out lastY);

                var maxCommandLength = contextualCommands.Max(x => x.DisplayCommand.Length);
                const int padding = 4;
                var dashStartX = xMargin + maxCommandLength + padding;
                var descriptionStartX = dashStartX + 2;
                lastY++;

                foreach (var contextualCommand in contextualCommands)
                {
                    gridStringBuilder.DrawWrapped(contextualCommand.DisplayCommand, xMargin, lastY + 1, availableWidth, properties.CommandColor, out _, out lastY);
                    gridStringBuilder.DrawWrapped("-", dashStartX, lastY, availableWidth, properties.CommandDescriptionColor, out _, out lastY);
                    gridStringBuilder.DrawWrapped(contextualCommand.Description, descriptionStartX, lastY, availableWidth, properties.CommandDescriptionColor, out _, out lastY);
                }
            }

            if (renderPrompt)
            {
                gridStringBuilder.DrawHorizontalDivider(availableHeight - 1, properties.HorizontalDivider, properties.BorderColor);
                gridStringBuilder.DrawWrapped(">", xMargin, availableHeight, availableWidth, properties.InputColor, out _, out _);
            }

            gridStringBuilder.DrawBoundary(properties.LeftBoundary, properties.RightBoundary, properties.HorizontalDivider, properties.BorderColor);

            return new GridTextFrame(gridStringBuilder, xMargin + 2, availableHeight, properties.BackgroundColor) { ShowCursor = true };
        }

        #endregion
    }
}
