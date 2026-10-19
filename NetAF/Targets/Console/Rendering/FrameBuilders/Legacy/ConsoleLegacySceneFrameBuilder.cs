using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Utilities;
using System.Linq;

namespace NetAF.Targets.Console.Rendering.FrameBuilders.Legacy
{
    /// <summary>
    /// Provides a builder for scene frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    /// <param name="properties">The render properties.</param>
    /// <param name="roomMapBuilder">A builder to use for room maps.</param>
    public sealed class ConsoleLegacySceneFrameBuilder(GridStringBuilder gridStringBuilder, ConsoleRenderProperties properties, IRoomMapBuilder roomMapBuilder) : ConsoleLegacyFrameBuilder, ISceneFrameBuilder
    {
        #region Properties

        /// <summary>
        /// Get or set the command title.
        /// </summary>
        public string CommandTitle { get; set; } = "You can:";

        /// <summary>
        /// Get or set if examinables should be displayed.
        /// </summary>
        public bool DisplayExaminables { get; set; } = true;

        #endregion

        #region Methods

        /// <summary>
        /// Add commands.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="commands">The commands.</param>
        /// <param name="availableSize">The available size.</param>
        /// <param name="xMargin">The left margin</param>
        /// <param name="minimumY">The minimum Y position.</param>
        /// <param name="requiredPromtHeight">The required height for the prompt.</param>
        private void AddCommands(GridStringBuilder builder, CommandHelp[] commands, Size availableSize, int xMargin, int minimumY, int requiredPromtHeight)
        {
            const int requiredSpaceForDivider = 2;
            const int requiredSpaceForCommandHeader = 4;
            var commandSpace = requiredSpaceForCommandHeader + requiredPromtHeight + requiredSpaceForDivider + commands.Length;
            var requiredYToFitAllCommands = availableSize.Height - commandSpace;

            if (requiredYToFitAllCommands < minimumY)
                requiredYToFitAllCommands = minimumY;

            // ensure that the command header and at least a single command can fit
            if (availableSize.Height - requiredPromtHeight - minimumY < requiredSpaceForCommandHeader + 1)
                return;

            builder.DrawHorizontalDivider(requiredYToFitAllCommands, properties.HorizontalDivider, properties.BorderColor);
            builder.DrawWrapped(CommandTitle, xMargin, requiredYToFitAllCommands + 2, availableSize.Width, properties.OptionColor, out _, out var lastY);

            var maxCommandLength = commands.Max(x => x.DisplayCommand.Length);
            const int padding = 4;
            var dashStartX = xMargin + maxCommandLength + padding;
            var descriptionStartX = dashStartX + 2;
            lastY++;

            for (var index = 0; index < commands.Length; index++)
            {
                var contextualCommand = commands[index];
                builder.DrawWrapped(contextualCommand.DisplayCommand, xMargin, lastY + 1, availableSize.Width, properties.CommandColor, out _, out lastY);
                builder.DrawWrapped("-", dashStartX, lastY, availableSize.Width, properties.CommandDescriptionColor, out _, out lastY);
                builder.DrawWrapped(contextualCommand.Description.EnsureFinishedSentence(), descriptionStartX, lastY, availableSize.Width, properties.CommandDescriptionColor, out _, out lastY);

                // only continue if not run out of space
                if (index < commands.Length - 1 && lastY + requiredPromtHeight + requiredPromtHeight >= availableSize.Height)
                {
                    builder.DrawWrapped("...", xMargin, lastY + 1, availableSize.Width, properties.OptionColor, out _, out lastY);
                    break;
                }
            }
        }

        /// <summary>
        /// Draw the title.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="xMargin">The left margin.</param>
        /// <param name="constrainedWidth">The constrained width.</param>
        /// <param name="lastY">The last Y position that was drawn to.</param>
        private void DrawTitle(Room room, int xMargin, int constrainedWidth, out int lastY)
        {
            gridStringBuilder.DrawWrapped(room.Identifier.Name, xMargin, 2, constrainedWidth, properties.TitleColor, out _, out lastY);
            gridStringBuilder.DrawUnderline(xMargin, lastY + 1, room.Identifier.Name.Length, properties.TitleColor);
        }

        /// <summary>
        /// Draw the scene description and viewpoint.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The viewpoint from the room.</param>
        /// <param name="keywords">The keywords to highlight.</param>
        /// <param name="xMargin">The left margin.</param>
        /// <param name="constrainedWidth">The constrained width.</param>
        /// <param name="linePadding">The line padding.</param>
        /// <param name="lastY">The last Y position that was drawn to.</param>
        private void DrawScene(Room room, ViewPoint viewPoint, string[] keywords, int xMargin, int constrainedWidth, int linePadding, ref int lastY)
        {
            var description = room.Description.GetDescription().EnsureFinishedSentence();

            if (!string.IsNullOrEmpty(description))
                gridStringBuilder.DrawWrapped(description, xMargin, lastY + 3, constrainedWidth, properties.MainTextColor, properties.HighlightTextColor, StringUtilities.LocateStringMarkers(description, keywords), out _, out lastY);

            if (!viewPoint.Any)
                return;

            var viewPointDescription = SceneHelper.CreateViewpointAsString(room, viewPoint);
            gridStringBuilder.DrawWrapped(viewPointDescription, xMargin, lastY + linePadding, constrainedWidth, properties.MainTextColor, properties.HighlightTextColor, StringUtilities.LocateStringMarkers(viewPointDescription, keywords), out _, out lastY);
        }

        /// <summary>
        /// Draw the room map.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The viewpoint from the room.</param>
        /// <param name="showMap">Specify if the map should be shown.</param>
        /// <param name="options">The render options.</param>
        /// <param name="xMargin">The left margin.</param>
        /// <param name="linePadding">The line padding.</param>
        /// <param name="lastY">The last Y position that was drawn to.</param>
        private void DrawMap(Room room, ViewPoint viewPoint, bool showMap, RoomMapRenderOptions options, int xMargin, int linePadding, ref int lastY)
        {
            if (roomMapBuilder == null || !showMap)
                return;

            if (roomMapBuilder is IConsoleRoomMapBuilder consoleRoomMapBuilder)
                consoleRoomMapBuilder.BuildRoomMap(room, viewPoint, options, new Point2D(xMargin, lastY + linePadding), out _, out lastY);
            else
                roomMapBuilder.BuildRoomMap(room, viewPoint, options);
        }

        /// <summary>
        /// Draw the examinables and player inventory.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="player">The player.</param>
        /// <param name="keywords">The keywords to highlight.</param>
        /// <param name="xMargin">The left margin.</param>
        /// <param name="constrainedWidth">The constrained width.</param>
        /// <param name="lastY">The last Y position that was drawn to.</param>
        private void DrawExaminablesAndInventory(Room room, PlayableCharacter player, string[] keywords, int xMargin, int constrainedWidth, ref int lastY)
        {
            if (DisplayExaminables)
            {
                var roomString = SceneHelper.CreateRoomString(room);

                if (!string.IsNullOrEmpty(roomString))
                    gridStringBuilder.DrawWrapped(roomString, xMargin, lastY + 2, constrainedWidth, properties.MainTextColor, properties.HighlightTextColor, StringUtilities.LocateStringMarkers(roomString, keywords), out _, out lastY);
            }

            if (player.Items.Length == 0)
                return;

            var itemString = $"You have {StringUtilities.ConstructExaminablesAsSentence(player.Items?.Cast<IExaminable>().ToArray()).StartWithLower()}";
            gridStringBuilder.DrawWrapped(itemString, xMargin, lastY + 2, constrainedWidth, properties.MainTextColor, properties.HighlightTextColor, StringUtilities.LocateStringMarkers(itemString, keywords), out _, out lastY);
        }

        /// <summary>
        /// Draw the prompt.
        /// </summary>
        /// <param name="xMargin">The left margin.</param>
        /// <param name="constrainedWidth">The constrained width.</param>
        /// <param name="availableHeight">The available height.</param>
        private void DrawPrompt(int xMargin, int constrainedWidth, int availableHeight)
        {
            if (!properties.ShowPrompt)
                return;

            gridStringBuilder.DrawHorizontalDivider(availableHeight - 1, properties.HorizontalDivider, properties.BorderColor);
            gridStringBuilder.DrawWrapped(properties.Prompt, xMargin, availableHeight, constrainedWidth, properties.PromptColor, out _, out _);
        }

        #endregion

        #region Implementation of ISceneFrameBuilder

        /// <inheritdoc/>
        public IFrame Build(Room room, ViewPoint viewPoint, PlayableCharacter player, CommandHelp[] contextualCommands, string[] keywords, bool showMap, RoomMapRenderOptions options, Size size)
        {
            const int xMargin = 2;
            var availableWidth = size.Width - (xMargin * 2);
            var availableHeight = size.Height - 2;
            var constrainedWidth = GetConstrainedWidth(availableWidth, properties.MaximumNarrativeLineWidth);
            const int linePadding = 2;

            gridStringBuilder.Resize(size);

            DrawTitle(room, xMargin, constrainedWidth, out var lastY);

            // display the scene

            if (!properties.HighlightKeywords)
                keywords = [];

            DrawScene(room, viewPoint, keywords, xMargin, constrainedWidth, linePadding, ref lastY);
            DrawMap(room, viewPoint, showMap, options, xMargin, linePadding, ref lastY);
            DrawExaminablesAndInventory(room, player, keywords, xMargin, constrainedWidth, ref lastY);

            if (contextualCommands != null && contextualCommands.Length > 0)
                AddCommands(gridStringBuilder, contextualCommands, new Size(constrainedWidth, size.Height), xMargin, lastY + 2, properties.ShowPrompt ? 3 : 1);

            DrawPrompt(xMargin, constrainedWidth, availableHeight);

            gridStringBuilder.DrawBoundary(properties.LeftBoundary, properties.RightBoundary, properties.HorizontalDivider, properties.BorderColor);

            var promptLengthAndPadding = properties.ShowPrompt ? properties.Prompt.Length + 1 : 0;

            return new GridTextFrame(gridStringBuilder, xMargin + promptLengthAndPadding, availableHeight, true, properties.BackgroundColor, properties.InputColor);
        }

        #endregion
    }
}
