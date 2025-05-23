﻿using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using System.Linq;

namespace NetAF.Targets.Console.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of region map frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    /// <param name="regionMapBuilder">A builder for region maps.</param>
    /// <param name="renderPrompt">Specify if the prompt should be rendered.</param>
    public sealed class ConsoleRegionMapFrameBuilder(GridStringBuilder gridStringBuilder, IRegionMapBuilder regionMapBuilder, bool renderPrompt = true) : IRegionMapFrameBuilder
    {
        #region Properties

        /// <summary>
        /// Get the region map builder.
        /// </summary>
        private IRegionMapBuilder RegionMapBuilder { get; } = regionMapBuilder;

        /// <summary>
        /// Get or set the background color.
        /// </summary>
        public AnsiColor BackgroundColor { get; set; } = AnsiColor.Black;

        /// <summary>
        /// Get or set the border color.
        /// </summary>
        public AnsiColor BorderColor { get; set; } = AnsiColor.BrightBlack;

        /// <summary>
        /// Get or set the title color.
        /// </summary>
        public AnsiColor TitleColor { get; set; } = AnsiColor.White;

        /// <summary>
        /// Get or set the commands color.
        /// </summary>
        public AnsiColor CommandsColor { get; set; } = AnsiColor.BrightBlack;

        /// <summary>
        /// Get or set the input color.
        /// </summary>
        public AnsiColor InputColor { get; set; } = AnsiColor.White;

        /// <summary>
        /// Get or set the command title.
        /// </summary>
        public string CommandTitle { get; set; } = "You can:";

        #endregion

        #region Implementation of IRegionMapFrameBuilder

        /// <summary>
        /// Get if this frame builder supports panning.
        /// </summary>
        public bool SupportsPan => true;

        /// <summary>
        /// Get if this frame builder supports zooming.
        /// </summary>
        public bool SupportsZoom => true;

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="focusPosition">The position to focus on.</param>
        /// <param name="detail">The level of detail to use.</param>
        /// <param name="contextualCommands">The contextual commands to display.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        public IFrame Build(Region region, Point3D focusPosition, RegionMapDetail detail, CommandHelp[] contextualCommands, Size size)
        {
            gridStringBuilder.Resize(size);

            const int leftMargin = 2;
            var availableWidth = size.Width - 4;
            var availableHeight = size.Height - 2;
            var matrix = region.ToMatrix();
            var room = matrix[focusPosition.X, focusPosition.Y, focusPosition.Z];
            var title = $"{region.Identifier.Name} - {room?.Identifier.Name}";
            gridStringBuilder.DrawWrapped(title, leftMargin, 2, availableWidth, TitleColor, out _, out var lastY);
            gridStringBuilder.DrawUnderline(leftMargin, lastY + 1, title.Length, TitleColor);

            int commandSpace = 0;
            var mapStartY = lastY + 2;

            if (contextualCommands?.Any() ?? false)
            {
                const int requiredSpaceForDivider = 2;
                const int requiredSpaceForCommandHeader = 3;
                int requiredSpaceForPrompt = renderPrompt ? 3 : 1;
                commandSpace = requiredSpaceForCommandHeader + requiredSpaceForPrompt + requiredSpaceForDivider + contextualCommands.Length;
                var requiredYToFitAllCommands = size.Height - commandSpace;

                gridStringBuilder.DrawHorizontalDivider(requiredYToFitAllCommands, BorderColor);
                gridStringBuilder.DrawWrapped(CommandTitle, leftMargin, requiredYToFitAllCommands + 2, availableWidth, CommandsColor, out _, out lastY);

                var maxCommandLength = contextualCommands.Max(x => x.DisplayCommand.Length);
                const int padding = 4;
                var dashStartX = leftMargin + maxCommandLength + padding;
                var descriptionStartX = dashStartX + 2;
                lastY++;

                for (var index = 0; index < contextualCommands.Length; index++)
                {
                    var contextualCommand = contextualCommands[index];
                    gridStringBuilder.DrawWrapped(contextualCommand.DisplayCommand, leftMargin, lastY + 1, availableWidth, CommandsColor, out _, out lastY);
                    gridStringBuilder.DrawWrapped("-", dashStartX, lastY, availableWidth, CommandsColor, out _, out lastY);
                    gridStringBuilder.DrawWrapped(contextualCommand.Description.EnsureFinishedSentence(), descriptionStartX, lastY, availableWidth, CommandsColor, out _, out lastY);
                }
            }

            var startMapPosition = new Point2D(leftMargin, mapStartY);
            var mapSize = new Size(availableWidth, size.Height - 4 - commandSpace);

            if (RegionMapBuilder is IConsoleRegionMapBuilder consoleRegionMapBuilder)
                consoleRegionMapBuilder.BuildRegionMap(region, focusPosition, detail, startMapPosition, mapSize);
            else
                RegionMapBuilder?.BuildRegionMap(region, focusPosition, detail);

            if (renderPrompt)
            {
                gridStringBuilder.DrawHorizontalDivider(availableHeight - 1, BorderColor);
                gridStringBuilder.DrawWrapped(">", leftMargin, availableHeight, availableWidth, InputColor, out _, out _);
            }

            gridStringBuilder.DrawBoundary(BorderColor);

            return new GridTextFrame(gridStringBuilder, 4, availableHeight, BackgroundColor) { ShowCursor = true };
        }

        #endregion
    }
}
