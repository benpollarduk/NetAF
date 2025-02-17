using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Console.Rendering;
using System.Collections.Generic;
using System.Linq;
using System;
namespace NetAF.Targets.Html.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a room map builder.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    public sealed class HtmlRoomMapBuilder(HtmlBuilder builder) : IRoomMapBuilder
    {
        #region Properties

        /// <summary>
        /// Get or set the character used for representing a locked exit.
        /// </summary>
        public char LockedExit { get; set; } = 'x';

        /// <summary>
        /// Get or set the character used for representing there is an item or a character in the room.
        /// </summary>
        public char ItemOrCharacterInRoom { get; set; } = '?';

        /// <summary>
        /// Get or set the character to use for vertical boundaries.
        /// </summary>
        public char VerticalBoundary { get; set; } = '|';

        /// <summary>
        /// Get or set the character to use for horizontal boundaries.
        /// </summary>
        public char HorizontalBoundary { get; set; } = '-';

        /// <summary>
        /// Get or set the character to use for vertical exit borders.
        /// </summary>
        public char VerticalExitBorder { get; set; } = '|';

        /// <summary>
        /// Get or set the character to use for horizontal exit borders.
        /// </summary>
        public char HorizontalExitBorder { get; set; } = '-';

        /// <summary>
        /// Get or set the character to use for corners.
        /// </summary>
        public char Corner { get; set; } = '+';

        /// <summary>
        /// Get or set the padding between the key and the map.
        /// </summary>
        public int KeyPadding { get; set; } = 6;

        #endregion

        #region Methods

        /// <summary>
        /// Draw the room.
        /// </summary>
        /// <param name="room">The room.</param>
        private void DrawRoom(Room room)
        {
            builder.Raw("<div style='border: 2px solid black; width: 200px; height: 150px; position: relative;'>");

            // draw north wall and exit
            builder.Raw("<div style='position: absolute; top: 0; left: 0; width: 100%; height: 20px; border-bottom: 2px solid black;'>");
            if (room.FindExit(Direction.North, false, out var north))
            {
                string exitStyle = north.IsLocked ? "background-color: red;" : "background-color: green;";
                builder.Raw($"<div style='position: absolute; left: 50%; transform: translateX(-50%); top: -10px; width: 40px; height: 20px; border: 2px solid black; {exitStyle}'>N</div>");
            }
            builder.Raw("</div>");

            // draw east wall and exit
            builder.Raw("<div style='position: absolute; top: 0; right: 0; height: 100%; width: 20px; border-left: 2px solid black;'>");
            if (room.FindExit(Direction.East, false, out var east))
            {
                string exitStyle = east.IsLocked ? "background-color: red;" : "background-color: green;";
                builder.Raw($"<div style='position: absolute; top: 50%; transform: translateY(-50%); right: -10px; width: 20px; height: 40px; border: 2px solid black; {exitStyle}; transform: rotate(90deg) translateY(-50%);'>E</div>");
            }
            builder.Raw("</div>");

            // draw south wall and exit
            builder.Raw("<div style='position: absolute; bottom: 0; left: 0; width: 100%; height: 20px; border-top: 2px solid black;'>");
            if (room.FindExit(Direction.South, false, out var south))
            {
                string exitStyle = south.IsLocked ? "background-color: red;" : "background-color: green;";
                builder.Raw($"<div style='position: absolute; left: 50%; transform: translateX(-50%); bottom: -10px; width: 40px; height: 20px; border: 2px solid black; {exitStyle}'>S</div>");
            }
            builder.Raw("</div>");

            // draw west wall and exit
            builder.Raw("<div style='position: absolute; top: 0; left: 0; height: 100%; width: 20px; border-right: 2px solid black;'>");
            if (room.FindExit(Direction.West, false, out var west))
            {
                string exitStyle = west.IsLocked ? "background-color: red;" : "background-color: green;";
                builder.Raw($"<div style='position: absolute; top: 50%; transform: translateY(-50%); left: -10px; width: 20px; height: 40px; border: 2px solid black; {exitStyle}; transform: rotate(-90deg) translateY(-50%);'>W</div>");
            }
            builder.Raw("</div>");

            builder.Raw("</div>");
        }

        /// <summary>
        /// Draw the key.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The viewpoint from the room.</param>
        /// <param name="key">The key type.</param>
        private void DrawKey(Room room, ViewPoint viewPoint, KeyType key)
        {
            List<string> keyLines = [];
            var lockedExitString = $"{LockedExit} = Locked Exit";
            var notVisitedExitString = "N/E/S/W/U/D = Unvisited";
            var visitedExitString = "n/e/s/w/u/d = Visited";
            var itemsString = $"{ItemOrCharacterInRoom} = Item(s) or Character(s) in Room";

            switch (key)
            {
                case KeyType.Dynamic:

                    if (room.Exits.Where(x => x.IsPlayerVisible).Any(x => x.IsLocked))
                        keyLines.Add(lockedExitString);

                    if (viewPoint.AnyNotVisited)
                        keyLines.Add(notVisitedExitString);

                    if (viewPoint.AnyVisited)
                        keyLines.Add(visitedExitString);

                    if (room.EnteredFrom.HasValue)
                        keyLines.Add($"{room.EnteredFrom.Value.ToString().ToLower().Substring(0, 1)} = Entrance");

                    if (Array.Exists(room.Items, x => x.IsPlayerVisible) || Array.Exists(room.Characters, x => x.IsPlayerVisible))
                        keyLines.Add(itemsString);

                    break;

                case KeyType.Full:

                    keyLines.Add(lockedExitString);
                    keyLines.Add(notVisitedExitString);
                    keyLines.Add(visitedExitString);
                    keyLines.Add(itemsString);

                    break;

                case KeyType.None:
                    break;
                default:
                    throw new NotImplementedException();
            }

            if (!keyLines.Any())
                return;

            foreach (var keyLine in keyLines)
                builder.P(keyLine);
        }

        #endregion

        #region Implementation of IRoomMapBuilder

        /// <summary>
        /// Build a map for a room.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The viewpoint from the room.</param>
        /// <param name="key">The key type.</param>
        public void BuildRoomMap(Room room, ViewPoint viewPoint, KeyType key)
        {
            /*
                * *-| N |-*
                * |       |
                * - U   D -
                * W   ?   E
                * -       -
                * |       |
                * *-| S |-*
                */

            DrawRoom(room);

            if (key != KeyType.None)
            {
                builder.Br();
                DrawKey(room, viewPoint, key);
            }
        }

        #endregion
    }
}
