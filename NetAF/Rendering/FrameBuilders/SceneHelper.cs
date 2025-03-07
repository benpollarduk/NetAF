using System;
using System.Linq;
using System.Text;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Extensions;

namespace NetAF.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides helper functionality for scenes.
    /// </summary>
    public static class SceneHelper
    {
        /// <summary>
        /// Get a description of a direction.
        /// </summary>
        /// <param name="currentRoom">The current room.</param>
        /// <param name="roomInDirection">The room in the specified direction.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>A description of the room.</returns>
        private static string GetDescriptionOfDirection(Room currentRoom, Room roomInDirection, Direction direction)
        {
            if (!currentRoom[direction].IsLocked)
                return $"the {roomInDirection.Identifier.Name}";

            var exitIdentifier = currentRoom[direction].Identifier;
            var exitHasExplicitlySetName = !exitIdentifier.Equals(direction.ToString());
            var lockedExitString = "a locked exit";

            if (exitHasExplicitlySetName)
                lockedExitString += $" ({exitIdentifier.Name})";

            return lockedExitString;
        }

        /// <summary>
        /// Create a view point string.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The view point.</param>
        /// <returns>The view point, as a string.</returns>
        public static string CreateViewpointAsString(Room room, ViewPoint viewPoint)
        {
            StringBuilder view = new();

            foreach (var direction in new[] { Direction.West, Direction.North, Direction.East, Direction.South, Direction.Up, Direction.Down })
            {
                var roomInDirection = viewPoint[direction];

                if (roomInDirection == null)
                    continue;

                var description = GetDescriptionOfDirection(room, roomInDirection, direction);

                if (view.Length == 0)
                {
                    switch (direction)
                    {
                        case Direction.North:
                        case Direction.East:
                        case Direction.South:
                        case Direction.West:
                            view.Append($"To the {direction.ToString().ToLower()} is {description}, ");
                            break;
                        case Direction.Up:
                            view.Append($"Above is {description}, ");
                            break;
                        case Direction.Down:
                            view.Append($"Below is {description}, ");
                            break;
                        default:
                            throw new NotImplementedException($"No implementation for {direction}.");
                    }
                }
                else
                {
                    switch (direction)
                    {
                        case Direction.North:
                        case Direction.East:
                        case Direction.South:
                        case Direction.West:
                            view.Append($"{direction.ToString().ToLower()} is {description}, ");
                            break;
                        case Direction.Up:
                            view.Append($"above is {description}, ");
                            break;
                        case Direction.Down:
                            view.Append($"below is {description}, ");
                            break;
                        default:
                            throw new NotImplementedException($"No implementation for {direction}.");
                    }
                }
            }

            var viewAsString = view.ToString();

            return string.IsNullOrEmpty(viewAsString) ? string.Empty : viewAsString.Remove(viewAsString.Length - 2).EnsureFinishedSentence();
        }

        /// <summary>
        /// Create a description of the NPC's as a string.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <returns>The characters, as a string.</returns>
        public static string CreateNPCString(Room room)
        {
            var characters = room.Characters.Where(c => c.IsPlayerVisible && c.IsAlive).ToArray<Character>();

            if (!characters.Any())
                return string.Empty;

            if (characters.Length == 1)
                return characters[0].Identifier + " is in this area.";

            StringBuilder builder = new();

            foreach (var character in characters)
                builder.Append(character.Identifier + ", ");

            builder.Remove(builder.Length - 2, 2);

            var sentenceSoFar = builder.ToString();
            builder.Clear();

            builder.Append(sentenceSoFar.Substring(0, sentenceSoFar.LastIndexOf(",", StringComparison.Ordinal)));
            builder.Append(" and ");
            builder.Append(sentenceSoFar.Substring(sentenceSoFar.LastIndexOf(",", StringComparison.Ordinal) + 2));
            builder.Append(" are in the ");
            builder.Append(room.Identifier);
            builder.Append(".");

            return builder.ToString();
        }
    }
}
