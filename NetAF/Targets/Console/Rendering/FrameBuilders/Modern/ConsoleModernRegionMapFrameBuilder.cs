using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Commands;
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
    /// Provides a builder of region map frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    /// <param name="properties">The render properties.</param>
    /// <param name="regionMapBuilder">A builder for region maps.</param>
    /// <param name="renderPrompt">Specify if the prompt should be rendered.</param>
    public sealed class ConsoleModernRegionMapFrameBuilder(GridStringBuilder gridStringBuilder, ConsoleRenderProperties properties, IRegionMapBuilder regionMapBuilder, bool renderPrompt = true) : ConsoleLegacyFrameBuilder, IRegionMapFrameBuilder
    {
        #region Properties

        /// <summary>
        /// Get the region map builder.
        /// </summary>
        private IRegionMapBuilder RegionMapBuilder { get; } = regionMapBuilder;

        /// <summary>
        /// Get or set the prompt.
        /// </summary>
        public string Prompt { get; set; } = ">";

        #endregion

        #region Implementation of IRegionMapFrameBuilder

        /// <inheritdoc/>
        public bool SupportsPan => true;

        /// <inheritdoc/>
        public bool SupportsZoom => true;

        /// <inheritdoc/>
        public IFrame Build(Region region, Point3D focusPosition, RegionMapRenderOptions regionMapOptions, RoomMapRenderOptions roomMapOptions, CommandHelp[] contextualCommands, Size size)
        {
            gridStringBuilder.Resize(size);

            const int xMargin = 2;
            var availableWidth = size.Width - (xMargin * 2);
            var availableHeight = size.Height - 2;
            var matrix = region.ToMatrix();
            var room = matrix[focusPosition.X, focusPosition.Y, focusPosition.Z];
            var title = $"{region.Identifier.Name} - {room?.Identifier.Name}";

            // constrain the narrative text to a comfortable reading width
            var narrativeWidth = GetConstrainedWidth(availableWidth, properties.MaximumNarrativeLineWidth);

            gridStringBuilder.DrawWrapped(title, xMargin, 2, availableWidth, properties.TitleColor, out _, out var lastY);

            var mapStartY = lastY + 2;

            // reserve a couple of lines at the bottom for the command hint and prompt
            const int reservedBottom = 3;
            var startMapPosition = new Point2D(xMargin, mapStartY);
            var mapSize = new Size(availableWidth, size.Height - mapStartY - reservedBottom);

            if (RegionMapBuilder is IConsoleRegionMapBuilder consoleRegionMapBuilder)
                consoleRegionMapBuilder.BuildRegionMap(region, focusPosition, regionMapOptions, roomMapOptions, mapSize, startMapPosition);
            else
                RegionMapBuilder?.BuildRegionMap(region, focusPosition, regionMapOptions, roomMapOptions, mapSize);

            var anyCommands = contextualCommands?.Any() ?? false;

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
                        var remaining = commands ?? string.Empty;

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
