using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Commands.Global;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Console.Rendering.FrameBuilders.Legacy;
using NetAF.Utilities;
using System;
using System.Linq;

namespace NetAF.Targets.Console.Rendering.FrameBuilders.Modern
{
    /// <summary>
    /// Provides a builder for scene frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    /// <param name="properties">The render properties.</param>
    /// <param name="roomMapBuilder">A builder to use for room maps.</param>
    public sealed class ConsoleModernSceneFrameBuilder(GridStringBuilder gridStringBuilder, ConsoleRenderProperties properties, IRoomMapBuilder roomMapBuilder) : ConsoleLegacyFrameBuilder, ISceneFrameBuilder
    {
        #region Properties

        /// <summary>
        /// Get or set the hint displayed to the player in place of the full command menu.
        /// </summary>
        public string CommandHint { get; set; } = $"Type {CommandList.CommandHelp.Command} or {CommandList.CommandHelp.Shortcut} to view commands.";

        /// <summary>
        /// Get or set the inventory hint.
        /// </summary>
        public string InventoryHint { get; set; } = "Inventory:";

        #endregion

        #region Implementation of ISceneFrameBuilder

        /// <inheritdoc/>
        public IFrame Build(Room room, ViewPoint viewPoint, PlayableCharacter player, CommandHelp[] contextualCommands, string[] keywords, bool showMap, RoomMapRenderOptions options, Size size)
        {
            const int xMargin = 2;
            const int linePadding = 2;
            const int roomMapLeftPadding = 8;
            const int roomMapRightPadding = xMargin;
            var availableWidth = size.Width - (xMargin * 2);
            var availableHeight = size.Height - 2;

            gridStringBuilder.Resize(size);

            var useRoomMap = roomMapBuilder != null && showMap;
            var roomMapSize = useRoomMap ? roomMapBuilder.Measure(room, viewPoint, options) : new Size(0, 0);
            var proposedNarrativeWidth = availableWidth - (useRoomMap ? roomMapSize.Width + roomMapLeftPadding + roomMapRightPadding : 0);

            // constrain the narrative text to a comfortable reading width
            var narrativeWidth = GetConstrainedWidth(proposedNarrativeWidth, properties.MaximumNarrativeLineWidth);

            if (!properties.HighlightKeywords)
                keywords = [];

            gridStringBuilder.DrawWrapped(room.Identifier.Name, xMargin, 2, narrativeWidth, properties.TitleColor, out _, out var lastY);

            // display the scene

            var descriptionY = lastY + 3;
            var description = room.Description.GetDescription().EnsureFinishedSentence();
            gridStringBuilder.DrawWrapped(description, xMargin, descriptionY, narrativeWidth, properties.MainTextColor, properties.HighlightTextColor, StringUtilities.LocateStringMarkers(description, keywords), out _, out lastY);

            if (viewPoint.Any)
            {
                var viewPointDescription = SceneHelper.CreateViewpointAsString(room, viewPoint);
                gridStringBuilder.DrawWrapped(viewPointDescription, xMargin, lastY + linePadding, narrativeWidth, properties.MainTextColor, properties.HighlightTextColor, StringUtilities.LocateStringMarkers(viewPointDescription, keywords), out _, out lastY);
            }

            // display the contents of the room (items and characters) beneath the main narrative text
            var itemsString = SceneHelper.CreateItemsString(room);
            var charactersString = SceneHelper.CreateCharactersString(room);
            var roomContents = string.Join(" ", new[] { itemsString, charactersString }.Where(x => !string.IsNullOrWhiteSpace(x)));

            if (!string.IsNullOrWhiteSpace(roomContents))
                gridStringBuilder.DrawWrapped(roomContents, xMargin, lastY + linePadding, narrativeWidth, properties.MainTextColor, properties.HighlightTextColor, StringUtilities.LocateStringMarkers(roomContents, keywords), out _, out lastY);

            // display the inventory beneath the main narrative text
            if (player.Items.Length != 0)
            {
                var inventory = $"{InventoryHint} " + StringUtilities.ConstructExaminablesAsSentence(player.Items?.Cast<IExaminable>().ToArray());
                gridStringBuilder.DrawWrapped(inventory, xMargin, lastY + linePadding, narrativeWidth, properties.MainTextColor, properties.HighlightTextColor, StringUtilities.LocateStringMarkers(inventory, keywords), out _, out lastY);
            }

            // position the map as a right-hand column, aligned with the top of the description
            var columnX = narrativeWidth + roomMapLeftPadding;

            if (columnX >= availableWidth)
                columnX = xMargin;

            var columnY = descriptionY;

            if (roomMapBuilder != null && showMap)
            {
                if (roomMapBuilder is IConsoleRoomMapBuilder consoleRoomMapBuilder)
                    consoleRoomMapBuilder.BuildRoomMap(room, viewPoint, options, new Point2D(columnX, columnY), out _, out _);
                else
                    roomMapBuilder.BuildRoomMap(room, viewPoint, options);
            }

            var anyCommands = contextualCommands != null && contextualCommands.Length > 0;

            switch (FrameProperties.CommandListType)
            {
                case CommandListType.All:
                case CommandListType.Light:

                    if (anyCommands)
                    {
                        var commands = string.Join(", ", contextualCommands.Select(x => x.DisplayCommand).Where(x => !string.IsNullOrWhiteSpace(x)));

                        // wrap over as many lines as needed, up to a maximum, only truncating the final line if it does not fit
                        const int maxCommandLines = 3;
                        var lineWidth = narrativeWidth - xMargin;
                        var commandLines = new System.Collections.Generic.List<string>();
                        var remaining = commands;
                        const string ellipsis = "...";

                        while (!string.IsNullOrEmpty(remaining) && remaining.Length > 0 && commandLines.Count < maxCommandLines && lineWidth > 0)
                        {
                            var line = StringUtilities.CutLineFromParagraph(ref remaining, lineWidth);

                            // if this is the last available line and more content remains, truncate with an ellipsis
                            if (commandLines.Count == maxCommandLines - 1 && !string.IsNullOrWhiteSpace(remaining))
                            {
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
                    else
                    {
                        gridStringBuilder.DrawWrapped(CommandHint, xMargin, availableHeight - 2, narrativeWidth, properties.CommandColor, out _, out _);
                    }

                    break;

                case CommandListType.None:

                    gridStringBuilder.DrawWrapped(CommandHint, xMargin, availableHeight - 2, narrativeWidth, properties.CommandColor, out _, out _);

                    break;
            }

            if (properties.ShowPrompt)
                gridStringBuilder.DrawWrapped(properties.Prompt, xMargin, availableHeight, availableWidth, properties.PromptColor, out _, out _);

            var promptLengthAndPadding = properties.ShowPrompt ? properties.Prompt.Length + 1 : 0;
            return new GridTextFrame(gridStringBuilder, xMargin + promptLengthAndPadding, availableHeight, true, properties.BackgroundColor, properties.InputColor);
        }

        #endregion
    }
}
