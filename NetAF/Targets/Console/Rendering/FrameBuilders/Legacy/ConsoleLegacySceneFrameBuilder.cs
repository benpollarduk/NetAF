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
    /// <param name="renderPrompt">Specify if the prompt should be rendered.</param>
    public sealed class ConsoleLegacySceneFrameBuilder(GridStringBuilder gridStringBuilder, ConsoleRenderProperties properties, IRoomMapBuilder roomMapBuilder, bool renderPrompt = true) : ConsoleLegacyFrameBuilder, ISceneFrameBuilder
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

        #endregion

        #region Implementation of ISceneFrameBuilder

        /// <inheritdoc/>
        public IFrame Build(Room room, ViewPoint viewPoint, PlayableCharacter player, CommandHelp[] contextualCommands, bool showMap, RoomMapRenderOptions options, Size size)
        {
            const int xMargin = 2;
            var availableWidth = size.Width - (xMargin * 2);
            var availableHeight = size.Height - 2;
            var constrainedWidth = GetConstrainedWidth(availableWidth, properties.MaximumNarrativeLineWidth);
            const int linePadding = 2;

            gridStringBuilder.Resize(size);

            gridStringBuilder.DrawWrapped(room.Identifier.Name, xMargin, 2, constrainedWidth, properties.TitleColor, out _, out var lastY);
            gridStringBuilder.DrawUnderline(xMargin, lastY + 1, room.Identifier.Name.Length, properties.TitleColor);

            // display the scene

            string[] keywords = properties.HighlightKeywords switch
            {
                true => StringUtilities.FindKeywords(new ExaminationScene(player, room), viewPoint),
                false => []
            };

            var description = room.Description.GetDescription().EnsureFinishedSentence();

            if (!string.IsNullOrEmpty(description))
                gridStringBuilder.DrawWrapped(description, xMargin, lastY + 3, constrainedWidth, properties.MainTextColor, properties.HighlightTextColor, StringUtilities.LocateStringMarkers(description, keywords), out _, out lastY);

            if (viewPoint.Any)
            {
                var viewPointDescription = SceneHelper.CreateViewpointAsString(room, viewPoint);
                gridStringBuilder.DrawWrapped(viewPointDescription, xMargin, lastY + linePadding, constrainedWidth, properties.MainTextColor, properties.HighlightTextColor, StringUtilities.LocateStringMarkers(viewPointDescription, keywords), out _, out lastY);
            }

            if (roomMapBuilder != null && showMap)
            {
                if (roomMapBuilder is IConsoleRoomMapBuilder consoleRoomMapBuilder)
                    consoleRoomMapBuilder.BuildRoomMap(room, viewPoint, options, new Point2D(xMargin, lastY + linePadding), out _, out lastY);
                else
                    roomMapBuilder.BuildRoomMap(room, viewPoint, options);
            }

            if (DisplayExaminables)
            {
                var roomString = SceneHelper.CreateRoomString(room);

                if (!string.IsNullOrEmpty(roomString))
                    gridStringBuilder.DrawWrapped(roomString, xMargin, lastY + 2, constrainedWidth, properties.MainTextColor, properties.HighlightTextColor, StringUtilities.LocateStringMarkers(roomString, keywords), out _, out lastY);
            }

            if (player.Items.Length != 0)
            {
                var itemString = $"You have {StringUtilities.ConstructExaminablesAsSentence(player.Items?.Cast<IExaminable>().ToArray()).StartWithLower()}";
                gridStringBuilder.DrawWrapped(itemString, xMargin, lastY + 2, constrainedWidth, properties.MainTextColor, properties.HighlightTextColor, StringUtilities.LocateStringMarkers(itemString, keywords), out _, out lastY);
            }

            if (contextualCommands != null && contextualCommands.Length > 0)
                AddCommands(gridStringBuilder, contextualCommands, new Size(constrainedWidth, size.Height), xMargin, lastY + 2, renderPrompt ? 3 : 1);
            
            if (renderPrompt)
            {
                gridStringBuilder.DrawHorizontalDivider(availableHeight - 1, properties.HorizontalDivider, properties.BorderColor);
                gridStringBuilder.DrawWrapped(">", xMargin, availableHeight, constrainedWidth, properties.InputColor, out _, out _);
            }

            gridStringBuilder.DrawBoundary(properties.LeftBoundary, properties.RightBoundary, properties.HorizontalDivider, properties.BorderColor);

            return new GridTextFrame(gridStringBuilder, 4, availableHeight, properties.BackgroundColor) { ShowCursor = true };
        }

        #endregion
    }
}
