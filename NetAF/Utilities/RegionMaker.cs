using System.Collections.Generic;
using System.Linq;
using NetAF.Assets;
using NetAF.Assets.Locations;

namespace NetAF.Utilities
{
    /// <summary>
    /// Provides a class for helping to make Regions.
    /// </summary>
    /// <param name="identifier">An identifier for the region.</param>
    /// <param name="description">A description for the region.</param>
    public sealed class RegionMaker(Identifier identifier, Description description)
    {
        #region Fields

        private readonly List<RoomPosition> rooms = [];

        #endregion

        #region Properties

        /// <summary>
        /// Get the identifier.
        /// </summary>
        private Identifier Identifier { get; } = identifier;

        /// <summary>
        /// Get the description.
        /// </summary>
        private Description Description { get; } = description;

        /// <summary>
        /// Get or set the room at a location.
        /// </summary>
        /// <param name="x">The x position.</param>
        /// <param name="y">The y position.</param>
        /// <param name="z">The z position.</param>
        /// <returns>The room.</returns>
        public Room this[int x, int y, int z]
        {
            get { return rooms.Find(r => r.IsAtPosition(x, y, z))?.Room; }
            set
            {
                var element = rooms.Find(r => r.IsAtPosition(x, y, z));

                if (element != null)
                    rooms.Remove(element);

                rooms.Add(new RoomPosition(value, x, y, z));
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the RegionMaker class.
        /// </summary>
        /// <param name="identifier">An identifier for the region.</param>
        /// <param name="description">A description for the region.</param>
        public RegionMaker(string identifier, string description) : this(new Identifier(identifier), new Description(description))
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Make a region.
        /// </summary>
        /// <returns>The created region.</returns>
        public Region Make()
        {
            return Make(rooms[0]);
        }

        /// <summary>
        /// Make a region.
        /// </summary>
        /// <param name="startPosition">The start position.</param>
        /// <returns>The created region.</returns>
        public Region Make(RoomPosition startPosition)
        {
            return Make(startPosition.X, startPosition.Y, startPosition.Z);
        }

        /// <summary>
        /// Make a region.
        /// </summary>
        /// <param name="x">The start x position.</param>
        /// <param name="y">The start y position.</param>
        /// <param name="z">The start z position.</param>
        /// <returns>The created region.</returns>
        public Region Make(int x, int y, int z)
        {
            var region = new Region(Identifier, Description);

            var matrix = ConvertToRoomMatrix(rooms);

            for (var depth = 0; depth < matrix.Depth; depth++)
            {
                for (var row = 0; row < matrix.Height; row++)
                {
                    for (var column = 0; column < matrix.Width; column++)
                    {
                        var room = matrix[column, row, depth];

                        if (room != null)
                            region.AddRoom(room, column, row, depth);
                    }
                }
            }

            // offset start room, matrix will have normalised positions
            region.SetStartRoom(x - rooms.Min(r => r.X), y - rooms.Min(r => r.Y), z - rooms.Min(r => r.Z));

            return region;
        }

        /// <summary>
        /// Determine if a room can be placed at a location
        /// </summary>
        /// <param name="x">The X position.</param>
        /// <param name="y">The Y position.</param>
        /// <param name="z">The Z position.</param>
        /// <returns>True if the room can be placed, else false.</returns>
        public bool CanPlaceRoom(int x, int y, int z)
        {
            return rooms.TrueForAll(r => !r.IsAtPosition(x, y, z));
        }

        /// <summary>
        /// Get all current room positions.
        /// </summary>
        /// <returns>The room positions.</returns>
        public RoomPosition[] GetRoomPositions()
        {
            return [.. rooms];
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Convert region to a 3D matrix of rooms.
        /// </summary>
        /// <returns>A 3D matrix.</returns>
        internal static Matrix ConvertToRoomMatrix(IReadOnlyCollection<RoomPosition> roomPositions)
        {
            if (roomPositions == null || roomPositions.Count == 0)
                return null;

            var minX = roomPositions.Min(x => x.X);
            var minY = roomPositions.Min(x => x.Y);
            var minZ = roomPositions.Min(x => x.Z);

            var xNormalisationOffset = 0 - minX;
            var yNormalisationOffset = 0 - minY;
            var zNormalisationOffset = 0 - minZ;

            List<RoomPosition> normalisedPositions = [];

            foreach (var roomPosition in roomPositions)
                normalisedPositions.Add(new RoomPosition(roomPosition.Room, roomPosition.X + xNormalisationOffset, roomPosition.Y + yNormalisationOffset, roomPosition.Z + zNormalisationOffset));

            return new([.. normalisedPositions]);
        }

        #endregion
    }
}