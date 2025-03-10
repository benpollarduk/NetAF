﻿using NetAF.Assets.Locations;
using NetAF.Assets;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;

namespace NetAF.Targets.Console.Rendering
{
    /// <summary>
    /// Represents any object that can build room maps targeting the console.
    /// </summary>
    public interface IConsoleRoomMapBuilder : IRoomMapBuilder
    {
        /// <summary>
        /// Build a map for a room.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The viewpoint from the room.</param>
        /// <param name="key">The key type.</param>
        /// <param name="startPosition">The position to start building at.</param>
        /// <param name="endX">The end position, x.</param>
        /// <param name="endY">The end position, x.</param>
        void BuildRoomMap(Room room, ViewPoint viewPoint, KeyType key, Point2D startPosition, out int endX, out int endY);
    }
}
