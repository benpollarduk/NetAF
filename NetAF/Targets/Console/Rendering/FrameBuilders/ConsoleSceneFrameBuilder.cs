using System.Linq;
using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Utilities;

namespace NetAF.Targets.Console.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder for scene frames.
    /// </summary>
    /// <param name="gridStringBuilder">A builder to use for the string layout.</param>
    /// <param name="roomMapBuilder">A builder to use for room maps.</param>
    /// <param name="renderPrompt">Specify if the prompt should be rendered.</param>
    public sealed class ConsoleSceneFrameBuilder(GridStringBuilder gridStringBuilder, IRoomMapBuilder roomMapBuilder, bool renderPrompt = true) : ISceneFrameBuilder
    {
        #region Properties

        /// <summary>
        /// Get or set the background color.
        /// </summary>
        public AnsiColor BackgroundColor { get; set; } = AnsiColor.Black;

        /// <summary>
        /// Get or set the border color.
        /// </summary>
        public AnsiColor BorderColor { get; set; } = AnsiColor.BrightBlack;

        /// <summary>
        /// Get or set the text color.
        /// </summary>
        public AnsiColor TextColor { get; set; } = AnsiColor.White;

        /// <summary>
        /// Get or set the input color.
        /// </summary>
        public AnsiColor InputColor { get; set; } = AnsiColor.White;

        /// <summary>
        /// Get or set the commands color.
        /// </summary>
        public AnsiColor CommandsColor { get; set; } = AnsiColor.BrightBlack;

        /// <summary>
        /// Get or set the command title.
        /// </summary>
        public string CommandTitle { get; set; } = "You can:";

        #endregion

        #region Implementation of ISceneFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="room">Specify the Room.</param>
        /// <param name="viewPoint">Specify the viewpoint from the room.</param>
        /// <param name="player">Specify the player.</param>
        /// <param name="contextualCommands">The contextual commands to display.</param>
        /// <param name="keyType">The type of key to use.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        public IFrame Build(Room room, ViewPoint viewPoint, PlayableCharacter player, CommandHelp[] contextualCommands, KeyType keyType, Size size)
        {
            var availableWidth = size.Width - 4;
            var availableHeight = size.Height - 2;
            const int leftMargin = 2;
            const int linePadding = 2;

            gridStringBuilder.Resize(size);

            gridStringBuilder.DrawBoundary(BorderColor);

            gridStringBuilder.DrawWrapped(room.Identifier.Name, leftMargin, 2, availableWidth, TextColor, out _, out var lastY);
            gridStringBuilder.DrawUnderline(leftMargin, lastY + 1, room.Identifier.Name.Length, TextColor);

            // display the scene

            gridStringBuilder.DrawWrapped(room.Description.GetDescription().EnsureFinishedSentence(), leftMargin, lastY + 3, availableWidth, TextColor, out _, out lastY);

            var extendedDescription = string.Empty;

            if (room.Items.Length != 0)
            {
                var roomExamination = Room.DefaultRoomExamination.Invoke(new ExaminationRequest(room, new ExaminationScene(player, room)));
                extendedDescription = extendedDescription.AddSentence(roomExamination.Description.EnsureFinishedSentence());
            }
            else
            {
                extendedDescription = extendedDescription.AddSentence("There are no items in this area.");
            }

            extendedDescription = extendedDescription.AddSentence(SceneHelper.CreateNPCString(room));

            if (viewPoint.Any)
                extendedDescription = extendedDescription.AddSentence(SceneHelper.CreateViewpointAsString(room, viewPoint));

            gridStringBuilder.DrawWrapped(extendedDescription, leftMargin, lastY + linePadding, availableWidth, TextColor, out _, out lastY);

            if (roomMapBuilder is IConsoleRoomMapBuilder consoleRoomMapBuilder)
                consoleRoomMapBuilder.BuildRoomMap(room, viewPoint, keyType, new Point2D(leftMargin, lastY + linePadding), out _, out lastY);
            else
                roomMapBuilder?.BuildRoomMap(room, viewPoint, keyType);

            if (player.Items.Any())
                gridStringBuilder.DrawWrapped("You have: " + StringUtilities.ConstructExaminablesAsSentence(player.Items?.Cast<IExaminable>().ToArray()), leftMargin, lastY + 2, availableWidth, TextColor, out _, out lastY);

            if (player.Attributes.Count > 0)
                gridStringBuilder.DrawWrapped(StringUtilities.ConstructAttributesAsString(player.Attributes.GetAsDictionary()), leftMargin, lastY + 2, availableWidth, TextColor, out _, out lastY);

            if (contextualCommands?.Any() ?? false)
            {
                const int requiredSpaceForDivider = 2;
                const int requiredSpaceForCommandHeader = 3;
                int requiredSpaceForPrompt = renderPrompt ? 3 : 1;
                var commandSpace = requiredSpaceForCommandHeader + requiredSpaceForPrompt + requiredSpaceForDivider + contextualCommands.Length;
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

                    // only continue if not run out of space - the 2 is for the border the ...
                    if (index < contextualCommands.Length - 1 && lastY + 2 + requiredSpaceForPrompt >= size.Height)
                    {
                        gridStringBuilder.DrawWrapped("...", leftMargin, lastY + 1, availableWidth, CommandsColor, out _, out lastY);
                        break;
                    }
                }
            }

            if (renderPrompt)
            {
                gridStringBuilder.DrawHorizontalDivider(availableHeight - 1, BorderColor);
                gridStringBuilder.DrawWrapped(">", leftMargin, availableHeight, availableWidth, InputColor, out _, out _);
            }

            return new GridTextFrame(gridStringBuilder, 4, availableHeight, BackgroundColor) { ShowCursor = true };
        }

        #endregion
    }
}
