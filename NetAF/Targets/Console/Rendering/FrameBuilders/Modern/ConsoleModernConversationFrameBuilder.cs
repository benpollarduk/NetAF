using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Commands;
using NetAF.Conversations;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Console.Rendering.FrameBuilders.Legacy;
using NetAF.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetAF.Targets.Console.Rendering.FrameBuilders.Modern
{
    /// <summary>
    /// Provides a builder of conversation frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    /// <param name="properties">The render properties.</param>
    /// <param name="renderPrompt">Specify if the prompt should be rendered.</param>
    public sealed class ConsoleModernConversationFrameBuilder(GridStringBuilder gridStringBuilder, ConsoleRenderProperties properties, bool renderPrompt = true) : ConsoleLegacyFrameBuilder, IConversationFrameBuilder
    {
        #region Properties

        /// <summary>
        /// Get or set the prompt.
        /// </summary>
        public string Prompt { get; set; } = ">";

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
            var lastY = 2;

            // constrain the narrative text to a comfortable reading width
            var narrativeWidth = GetConstrainedWidth(availableWidth, properties.MaximumNarrativeLineWidth);

            if (!string.IsNullOrEmpty(title))
            {
                gridStringBuilder.DrawWrapped(title, xMargin, lastY, constrainedWidth, properties.TitleColor, out _, out lastY);
                lastY += 2;
            }

            if (converser?.Conversation?.Log != null)
            {
                var spaceForLog = availableHeight - 6;
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

            // display the available responses the player can choose from
            var currentParagraph = converser?.Conversation?.CurrentParagraph;

            if (currentParagraph?.CanRespond ?? false)
            {
                lastY++;

                for (var i = 0; i < currentParagraph.Responses.Length; i++)
                {
                    var response = currentParagraph.Responses[i];
                    gridStringBuilder.DrawWrapped($"{i + 1}. {response.Line}", xMargin, lastY + 1, constrainedWidth, properties.CommandColor, out _, out lastY);
                }
            }

            var anyCommands = contextualCommands != null && contextualCommands.Length > 0;

            switch (FrameProperties.CommandListType)
            {
                case CommandListType.All:
                case CommandListType.Minimal:

                    if (anyCommands)
                    {
                        var commands = string.Join(", ", contextualCommands.Select(x => x.DisplayCommand).Where(x => !string.IsNullOrWhiteSpace(x)));

                        // wrap over as many lines as needed, up to a maximum, only truncating the final line if it does not fit
                        const int maxCommandLines = 3;
                        var lineWidth = narrativeWidth - xMargin;
                        var commandLines = new List<string>();
                        var remaining = commands;

                        while (remaining.Length > 0 && commandLines.Count < maxCommandLines && lineWidth > 0)
                        {
                            var line = StringUtilities.CutLineFromParagraph(ref remaining, lineWidth);

                            // if this is the last available line and more content remains, truncate with an ellipsis
                            if (commandLines.Count == maxCommandLines - 1 && !string.IsNullOrWhiteSpace(remaining))
                            {
                                const string ellipsis = "...";
                                var trimLength = Math.Max(0, lineWidth - ellipsis.Length);

                                if (line.Length > trimLength)
                                    line = line[..trimLength].TrimEnd();

                                line += ellipsis;
                            }

                            commandLines.Add(line);
                        }

                        var startY = availableHeight - 2 - (commandLines.Count - 1);

                        foreach (var line in commandLines)
                        {
                            gridStringBuilder.DrawWrapped(line, xMargin, startY, narrativeWidth, properties.CommandColor, out _, out _);
                            startY++;
                        }
                    }

                    break;

                case CommandListType.None:

                    break;
            }

            if (renderPrompt)
                gridStringBuilder.DrawWrapped(Prompt, xMargin, availableHeight, availableWidth, properties.InputColor, out _, out _);

            return new GridTextFrame(gridStringBuilder, xMargin + Prompt.Length + 1, availableHeight, properties.BackgroundColor) { ShowCursor = true };
        }

        #endregion
    }
}
