using System;
using System.Collections.Generic;
using System.Linq;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Serialization;
using NetAF.Serialization.Assets;
using NetAF.Utilities;

namespace NetAF.Assets.Locations
{
    /// <summary>
    /// Represents a region.
    /// </summary>
    public sealed class Region : ExaminableObject, IRestoreFromObjectSerialization<RegionSerialization>
    {
        #region Fields

        private Room startRoom;
        private readonly List<RoomPosition> roomPositions = [];

        #endregion

        #region Properties

        /// <summary>
        /// Get the number of rooms region contains.
        /// </summary>
        public int Rooms => roomPositions.Count;

        /// <summary>
        /// Get the current room.
        /// </summary>
        public Room CurrentRoom { get; private set; }

        /// <summary>
        /// Get a room at a specified location.
        /// </summary>
        /// <param name="x">The x position.</param>
        /// <param name="y">The y position.</param>
        /// <param name="z">The z position.</param>
        /// <returns>The room.</returns>
        public Room this[int x, int y, int z] => roomPositions.Find(r => r.IsAtPosition(x, y, z))?.Room;

        /// <summary>
        /// Get if the current region is visible without discovery.
        /// </summary>
        public bool IsVisibleWithoutDiscovery { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Region class.
        /// </summary>s
        /// <param name="identifier">This Regions identifier.</param>
        /// <param name="description">The description of this Region.</param>
        /// <param name="commands">This objects commands.</param>
        /// <param name="examination">The examination.</param>
        public Region(string identifier, string description, CustomCommand[] commands = null, ExaminationCallback examination = null) : this(new Identifier(identifier), new Description(description), commands, examination)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Region class.
        /// </summary>
        /// <param name="identifier">This Regions identifier.</param>
        /// <param name="description">The description of this Region.</param>
        /// <param name="commands">This objects commands.</param>
        /// <param name="examination">The examination.</param>
        public Region(Identifier identifier, IDescription description, CustomCommand[] commands = null, ExaminationCallback examination = null)
        {
            Identifier = identifier;
            Description = description;
            Commands = commands ?? [];

            if (examination != null)
                Examination = examination;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Enter this region.
        /// </summary>
        /// <returns>The reaction.</returns>
        internal Reaction Enter()
        {
            if (startRoom == null && roomPositions.Count > 0)
            {
                var first = roomPositions[0].Room;
                SetStartRoom(first);
            }

            if (CurrentRoom != startRoom)
                CurrentRoom = startRoom;

            if (CurrentRoom == null)
                return new Reaction(ReactionResult.Error, "CurrentRoom is null.");

            var wasVisited = CurrentRoom.HasBeenVisited;
            CurrentRoom.MovedInto();
            var introduction = CurrentRoom.Introduction.GetDescription();

            if (wasVisited || string.IsNullOrEmpty(introduction))
                return Reaction.Silent;
            else
                return new Reaction(ReactionResult.Inform, introduction);
        }

        /// <summary>
        /// Get the position of a room.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <returns>The position of the room.</returns>
        public RoomPosition GetPositionOfRoom(Room room)
        {
            var matrix = ToMatrix();

            if (matrix == null)
                return null;

            for (var z = 0; z < matrix.Depth; z++)
            {
                for (var y = 0; y < matrix.Height; y++)
                {
                    for (var x = 0; x < matrix.Width; x++)
                    {
                        if (room == matrix[x, y, z])
                            return new(room, new Point3D(x, y, z));
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Try and find a room within this region.
        /// </summary>
        /// <param name="name">The rooms name.</param>
        /// <param name="room">The room, if found, else null.</param>
        /// <returns>True if the room could be found, else false.</returns>
        public bool TryFindRoom(string name, out Room room)
        {
            room = roomPositions.Find(x => x.Room.Identifier.Equals(name))?.Room;
            return room != null;
        }

        /// <summary>
        /// Add a Room to this region.
        /// </summary>
        /// <param name="room">The room to add.</param>
        /// <param name="x">The x position within the region.</param>
        /// <param name="y">The y position within the region.</param>
        /// <param name="z">The z position within the region.</param>
        public bool AddRoom(Room room, int x, int y, int z)
        {
            var addable = !roomPositions.Exists(r => r.Room == room ||  r.IsAtPosition(x, y, z));

            if (addable)
                roomPositions.Add(new(room, new Point3D(x, y, z)));

            return addable;
        }

        /// <summary>
        /// Get an adjoining room to the Region.CurrentRoom property.
        /// </summary>
        /// <param name="direction">The direction of the adjoining Room.</param>
        /// <returns>The adjoining Room.</returns>
        public Room GetAdjoiningRoom(Direction direction)
        {
            return GetAdjoiningRoom(direction, CurrentRoom);
        }

        /// <summary>
        /// Get an adjoining room to a room.
        /// </summary>
        /// <param name="direction">The direction of the adjoining room.</param>
        /// <param name="room">The room to use as the reference.</param>
        /// <returns>The adjoining room.</returns>
        public Room GetAdjoiningRoom(Direction direction, Room room)
        {
            var roomPosition = roomPositions.Find(r => r.Room == room);

            if (roomPosition == null)
                return null;

            NextPosition(roomPosition.Position, direction, out var next);
            return this[next.X, next.Y, next.Z];
        }

        /// <summary>
        /// Move in a direction.
        /// </summary>
        /// <param name="direction">The direction to move in.</param>
        /// <returns>True if the move was successful, else false.</returns>
        public bool Move(Direction direction)
        {
            if (!CurrentRoom.CanMove(direction)) 
                return false;

            var adjoiningRoom = GetAdjoiningRoom(direction);

            if (adjoiningRoom == null)
                return false;

            adjoiningRoom.MovedInto(direction.Inverse());
            CurrentRoom = adjoiningRoom;

            return true;
        }

        /// <summary>
        /// Set the room to start in.
        /// </summary>
        /// <param name="room">The Room to start in.</param>
        public void SetStartRoom(Room room)
        {
            startRoom = room;
        }

        /// <summary>
        /// Set the room to start in.
        /// </summary>
        /// <param name="x">The x position.</param>
        /// <param name="y">The y position.</param>
        /// <param name="z">The z position.</param>
        public void SetStartRoom(int x, int y, int z)
        {
            var room = roomPositions.Find(r => r.IsAtPosition(x, y, z))?.Room;
            SetStartRoom(room ?? roomPositions[0].Room);
        }

        /// <summary>
        /// Unlock a pair of doors in a specified direction in the CurrentRoom.
        /// </summary>
        /// <param name="direction">The direction to unlock in.</param>
        /// <returns>True if the door pair could be unlocked, else false.</returns>
        public bool UnlockDoorPair(Direction direction)
        {
            var exitInThisRoom = CurrentRoom[direction];
            var roomPosition = roomPositions.Find(x => x.Room == CurrentRoom);

            if (roomPosition == null)
                return false;

            if (exitInThisRoom == null)
                return false;

            var adjoiningRoom = GetAdjoiningRoom(direction);

            if (adjoiningRoom == null)
                return false;

            if (!adjoiningRoom.FindExit(direction.Inverse(), true, out var exit))
                return false;

            if (exit == null)
                return false;

            exit.Unlock();
            exitInThisRoom.Unlock();
            return true;
        }

        /// <summary>
        /// Get this region as a 3D matrix of rooms.
        /// </summary>
        /// <returns>This region, as a 3D matrix.</returns>
        public Matrix ToMatrix()
        {
            return RegionMaker.ConvertToRoomMatrix(roomPositions);
        }

        /// <summary>
        /// Jump to a room.
        /// </summary>
        /// <param name="location">The location of the room.</param>
        /// <returns>True if the room could be jumped to, else false.</returns>
        public bool JumpToRoom(Point3D location)
        {
            var roomPosition = roomPositions.Find(r => r.IsAtPosition(location));

            if (roomPosition == null)
                return false;

            CurrentRoom = roomPosition.Room;
            CurrentRoom.MovedInto();

            return true;
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Get the next position given a current position.
        /// </summary>
        /// <param name="current">The current position.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="next">The next position.</param>
        internal static void NextPosition(Point3D current, Direction direction, out Point3D next)
        {
            switch (direction)
            {
                case Direction.North:
                    next = new Point3D(current.X, current.Y + 1, current.Z);
                    break;
                case Direction.East:
                    next = new Point3D(current.X + 1, current.Y, current.Z);
                    break;
                case Direction.South:
                    next = new Point3D(current.X, current.Y - 1, current.Z);
                    break;
                case Direction.West:
                    next = new Point3D(current.X - 1, current.Y, current.Z);
                    break;
                case Direction.Up:
                    next = new Point3D(current.X, current.Y, current.Z + 1);
                    break;
                case Direction.Down:
                    next = new Point3D(current.X, current.Y, current.Z - 1);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        #endregion

        #region Overrides of ExaminableObject

        /// <summary>
        /// Examine this object.
        /// </summary>
        /// <param name="scene">The scene this object is being examined from.</param>
        /// <returns>A ExaminationResult detailing the examination of this object.</returns>
        public override ExaminationResult Examine(ExaminationScene scene)
        {
            return new(Identifier + ": " + Description.GetDescription());
        }

        #endregion

        #region Implementation of IRestoreFromObjectSerialization<RegionSerialization>

        /// <summary>
        /// Restore this object from a serialization.
        /// </summary>
        /// <param name="serialization">The serialization to restore from.</param>
        public void RestoreFrom(RegionSerialization serialization)
        {
            base.RestoreFrom(serialization);

            var rooms = roomPositions.Select(x => x.Room).ToArray();

            foreach (var room in rooms)
            {
                var roomSerialization = Array.Find(serialization.Rooms, x => room.Identifier.Equals(x.Identifier));
                roomSerialization?.Restore(room);
            }

            CurrentRoom = Array.Find(rooms, x => x.Identifier.Equals(serialization.CurrentRoom));
            IsVisibleWithoutDiscovery = serialization.IsVisibleWithoutDiscovery;
        }

        #endregion
    }
}