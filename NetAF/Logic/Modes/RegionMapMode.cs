using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Interpretation;
using System.Collections.Generic;
using System.Linq;

namespace NetAF.Logic.Modes
{
    /// <summary>
    /// Provides a display mode for region map.
    /// </summary>
    /// <param name="focusPosition">The position to focus on. To use the player position use RegionMapMode.Player.</param>
    public sealed class RegionMapMode(Point3D focusPosition) : IGameMode
    {
        #region StaticProperties

        /// <summary>
        /// Get the value to use to display the player level.
        /// </summary>
        public static Point3D Player => new Point3D(int.MinValue, int.MinValue, int.MinValue);

        #endregion

        #region Properties

        /// <summary>
        /// Get or set the position to focus on. To use the player position use RegionMapMode.Player.
        /// </summary>
        public Point3D FocusPosition { get; set; } = focusPosition;

        #endregion

        #region Implementation of IGameMode

        /// <summary>
        /// Get the interpreter.
        /// </summary>
        public IInterpreter Interpreter { get; } = Interpreters.RegionMapCommandInterpreter;

        /// <summary>
        /// Get the type of mode this provides.
        /// </summary>
        public GameModeType Type { get; } = GameModeType.Interactive;

        /// <summary>
        /// Render the current state of a game.
        /// </summary>
        /// <param name="game">The game.</param>
        public void Render(Game game)
        {
            var region = game.Overworld.CurrentRegion;

            // if focusing on the player, find their location
            if (FocusPosition.Equals(Player))
                FocusPosition = region.GetPositionOfRoom(game.Overworld.CurrentRegion.CurrentRoom).Position;

            FocusPosition = GetSnappedLocation(region, FocusPosition);

            var frame = game.Configuration.FrameBuilders.RegionMapFrameBuilder.Build(region, FocusPosition, Interpreter?.GetContextualCommandHelp(game) ?? [], game.Configuration.DisplaySize);
            game.Configuration.Adapter.RenderFrame(frame);
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Determine if a pan position is valid.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="position">The position.</param>
        /// <returns>True if the pan position is valid, else false.</returns>
        public static bool CanPanToPosition(Region region, Point3D position)
        {
            var currentRoom = region.CurrentRoom;
            var positionOfCurrentRoom = region.GetPositionOfRoom(currentRoom);

            if (positionOfCurrentRoom.Position.Z != position.Z)
                return CanPanToZ(region, position);
            else
                return CanPanToFixedLocation(region, position);
        }

        /// <summary>
        /// Determine if a room can be panned to.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="regionIsVisibleWithoutDiscovery">If the region is visible without discovery..</param>
        /// <returns>True if the room can be panned to, else false.</returns>
        private static bool IsRoomPannable(Room room, bool regionIsVisibleWithoutDiscovery)
        {
            return room != null && (room.HasBeenVisited || regionIsVisibleWithoutDiscovery);
        }

        /// <summary>
        /// Determine if a pan position is valid on a different Z.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="position">The position.</param>
        /// <returns>True if the pan position is valid, else false.</returns>
        private static bool CanPanToZ(Region region, Point3D position)
        {
            if (CanPanToFixedLocation(region, position))
                return true;

            // may still be able to pan if there is a room on the Z plane and the region is visible without discovery OR a room on that Z plane has been visited
            return region.ToMatrix().FindAllRoomsOnZ(position.Z).Where(x => IsRoomPannable(x, region.IsVisibleWithoutDiscovery)).ToArray().Length > 0;
        }

        /// <summary>
        /// Determine if a pan position is valid.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="position">The position.</param>
        /// <returns>True if the pan position is valid, else false.</returns>
        private static bool CanPanToFixedLocation(Region region, Point3D position)
        {
            var matrix = region.ToMatrix();
            var room = matrix[position.X, position.Y, position.Z];
            return IsRoomPannable(room, region.IsVisibleWithoutDiscovery);
        }

        /// <summary>
        /// Get a snapped pan location. This will handle situations where a position may not be available but a nearby point is.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="position">The position.</param>
        /// <returns>The snapped position.</returns>
        private static Point3D GetSnappedLocation(Region region, Point3D position)
        {
            if (CanPanToFixedLocation(region, position))
                return position;

            var matrix = region.ToMatrix();
            var allRoomsOnSpecifiedZLevel = matrix.FindAllRoomsOnZ(position.Z);
            var allSnabbableRooms = allRoomsOnSpecifiedZLevel.Where(x => IsRoomPannable(x, region.IsVisibleWithoutDiscovery));
            Dictionary<Room, double> distances = [];

            foreach (var room in allSnabbableRooms)
                distances.Add(room, Matrix.DistanceBetweenPoints(position, matrix[room].Value));

            var targetRoom = distances.OrderBy(x => x.Value).First().Key;
            return matrix[targetRoom].Value;
        }

        #endregion
    }
}
