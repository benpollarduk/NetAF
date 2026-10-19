using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using System.Linq;

namespace NetAF.Targets.Console.Rendering.FrameBuilders.Legacy
{
    /// <summary>
    /// Provides a builder of region map frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    /// <param name="properties">The render properties.</param>
    /// <param name="regionMapBuilder">A builder for region maps.</param>
    public sealed class ConsoleLegacyRegionMapFrameBuilder(GridStringBuilder gridStringBuilder, ConsoleRenderProperties properties, IRegionMapBuilder regionMapBuilder) : ConsoleLegacyFrameBuilder, IRegionMapFrameBuilder
    {
        #region Properties

        /// <summary>
        /// Get the region map builder.
        /// </summary>
        private IRegionMapBuilder RegionMapBuilder { get; } = regionMapBuilder;

        /// <summary>
        /// Get or set the command title.
        /// </summary>
        public string CommandTitle { get; set; } = "You can:";

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
            var availableWidth = size.Width - 4;
            var availableHeight = size.Height - 2;
            var matrix = region.ToMatrix();
            var room = matrix[focusPosition.X, focusPosition.Y, focusPosition.Z];
            var title = $"{region.Identifier.Name} - {room?.Identifier.Name}";
            gridStringBuilder.DrawWrapped(title, xMargin, 2, availableWidth, properties.TitleColor, out _, out var lastY);
            gridStringBuilder.DrawUnderline(xMargin, lastY + 1, title.Length, properties.TitleColor);

            int commandSpace = 0;
            var mapStartY = lastY + 2;

            if (contextualCommands?.Any() ?? false)
            {
                const int requiredSpaceForDivider = 2;
                const int requiredSpaceForCommandHeader = 3;
                int requiredSpaceForPrompt = properties.ShowPrompt ? 3 : 1;
                commandSpace = requiredSpaceForCommandHeader + requiredSpaceForPrompt + requiredSpaceForDivider + contextualCommands.Length;
                var requiredYToFitAllCommands = size.Height - commandSpace;

                gridStringBuilder.DrawHorizontalDivider(requiredYToFitAllCommands, properties.HorizontalDivider, properties.BorderColor);
                gridStringBuilder.DrawWrapped(CommandTitle, xMargin, requiredYToFitAllCommands + 2, availableWidth, properties.OptionColor, out _, out lastY);

                var maxCommandLength = contextualCommands.Max(x => x.DisplayCommand.Length);
                const int padding = 4;
                var dashStartX = xMargin + maxCommandLength + padding;
                var descriptionStartX = dashStartX + 2;
                lastY++;

                for (var index = 0; index < contextualCommands.Length; index++)
                {
                    var contextualCommand = contextualCommands[index];
                    gridStringBuilder.DrawWrapped(contextualCommand.DisplayCommand, xMargin, lastY + 1, availableWidth, properties.CommandColor, out _, out lastY);
                    gridStringBuilder.DrawWrapped("-", dashStartX, lastY, availableWidth, properties.CommandDescriptionColor, out _, out lastY);
                    gridStringBuilder.DrawWrapped(contextualCommand.Description.EnsureFinishedSentence(), descriptionStartX, lastY, availableWidth, properties.CommandDescriptionColor, out _, out lastY);
                }
            }

            var startMapPosition = new Point2D(xMargin, mapStartY);
            var mapSize = new Size(availableWidth, size.Height - 4 - commandSpace);

            if (RegionMapBuilder is IConsoleRegionMapBuilder consoleRegionMapBuilder)
                consoleRegionMapBuilder.BuildRegionMap(region, focusPosition, regionMapOptions, roomMapOptions, mapSize, startMapPosition);
            else
                RegionMapBuilder?.BuildRegionMap(region, focusPosition, regionMapOptions, roomMapOptions, mapSize);

            if (properties.ShowPrompt)
            {
                gridStringBuilder.DrawHorizontalDivider(availableHeight - 1, properties.HorizontalDivider, properties.BorderColor);
                gridStringBuilder.DrawWrapped(properties.Prompt, xMargin, availableHeight, availableWidth, properties.PromptColor, out _, out _);
            }

            gridStringBuilder.DrawBoundary(properties.LeftBoundary, properties.RightBoundary, properties.HorizontalDivider, properties.BorderColor);

            var promptLengthAndPadding = properties.ShowPrompt ? properties.Prompt.Length + 1 : 0;
            return new GridTextFrame(gridStringBuilder, xMargin + promptLengthAndPadding, availableHeight, true, properties.BackgroundColor, properties.InputColor);
        }

        #endregion
    }
}
