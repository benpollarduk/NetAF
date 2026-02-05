using NetAF.Assets;
using NetAF.Assets.Locations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Commands;
using NetAF.Assets.Characters;

namespace NetAF.Tests.Assets.Locations
{
    [TestClass]
    public class Region_Tests
{
        [TestMethod]
        public void Given0Rooms_WhenGetCurrentRoom_ThenNull()
        {
            var region = new Region(string.Empty, string.Empty);
          
            Assert.IsNull(region.CurrentRoom);
        }

        [TestMethod]
        public void Given1Room_WhenGetCurrentRoom_ThenNotNull()
        {
            var room1 = new Room(Identifier.Empty, Description.Empty);
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room1, 0, 0, 0);
            region.Enter();

            Assert.IsNotNull(region.CurrentRoom);
        }

        [TestMethod]
        public void Given3Rooms_WhenGetCurrentRoom_ThenFirstRoom()
        {
            var room1 = new Room(Identifier.Empty, Description.Empty);
            var room2 = new Room(Identifier.Empty, Description.Empty);
            var room3 = new Room(Identifier.Empty, Description.Empty);

            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room1, 0, 0, 0);
            region.AddRoom(room2, 0, 1, 0);
            region.AddRoom(room3, 1, 0, 0);
            region.Enter();

            Assert.AreEqual(room1, region.CurrentRoom);
        }

        [TestMethod]
        public void GivenX1Y1_WhenSetStartRoom_ThenLastRoom()
        {
            var room1 = new Room(Identifier.Empty, Description.Empty);
            var room2 = new Room(Identifier.Empty, Description.Empty);
            var room3 = new Room(Identifier.Empty, Description.Empty);

            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room1, 0, 0, 0);
            region.AddRoom(room2, 0, 1, 0);
            region.AddRoom(room3, 1, 1, 0);

            region.SetStartRoom(1, 1, 0);
            region.Enter();

            Assert.AreEqual(room3, region.CurrentRoom);
        }

        [TestMethod]
        public void Given4Rooms_WhenGetRooms_Then4()
        {
            var room1 = new Room(Identifier.Empty, Description.Empty);
            var room2 = new Room(Identifier.Empty, Description.Empty);
            var room3 = new Room(Identifier.Empty, Description.Empty);
            var room4 = new Room(Identifier.Empty, Description.Empty);
            
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room1, 0, 0, 0);
            region.AddRoom(room2, 0, 1, 0);
            region.AddRoom(room3, 1, 0, 0);
            region.AddRoom(room4, 1, 1, 0);
            
            Assert.AreEqual(4, region.Rooms);
        }

        [TestMethod]
        public void GivenNoRooms_WhenAddingRoom_ThenReturnTrue()
        {
            var room1 = new Room(Identifier.Empty, Description.Empty);

            var region = new Region(string.Empty, string.Empty);
            var result = region.AddRoom(room1, 0, 0, 0);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Given1Room_WhenAddingRoomToSameLocation_ThenReturnFalse()
        {
            var room1 = new Room(Identifier.Empty, Description.Empty);
            var room2 = new Room(Identifier.Empty, Description.Empty);

            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room1, 0, 0, 0);
            var result = region.AddRoom(room2, 0, 0, 0);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Given1Room_WhenAddingDuplicateRoomToDifferentLocation_ThenReturnFalse()
        {
            var room1 = new Room(Identifier.Empty, Description.Empty);

            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room1, 0, 0, 0);
            var result = region.AddRoom(room1, 1, 0, 0);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Given1Room_WhenAdding1Room_Then2Rooms()
        {
            var room1 = new Room(Identifier.Empty, Description.Empty);
            var room2 = new Room(Identifier.Empty, Description.Empty);

            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room1, 0, 0, 0);
            region.AddRoom(room2, 0, 1, 0);
            region.Enter();

            Assert.AreEqual(2, region.Rooms);
        }

        [TestMethod]
        public void GivenLastRoom_WhenSetStartRoom_ThenCurrentRoomIsLastRoom()
        {
            var room1 = new Room(Identifier.Empty, Description.Empty);
            var room2 = new Room(Identifier.Empty, Description.Empty);

            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room1, 0, 0, 0);
            region.AddRoom(room2, 0, 1, 0);

            region.SetStartRoom(room2);
            region.Enter();

            Assert.AreEqual(room2, region.CurrentRoom);
        }

        [TestMethod]
        public void GivenCanMove_WhenMove_ThenSilent()
        {
            var room1 = new Room(Identifier.Empty, Description.Empty, [new Exit(Direction.East)]);
            var room2 = new Room(Identifier.Empty, Description.Empty, [new Exit(Direction.West)]);

            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room1, 0, 0, 0);
            region.AddRoom(room2, 1, 0, 0);
            region.Enter();

            var result = region.Move(Direction.East);

            Assert.AreEqual(ReactionResult.Silent, result.Result);
        }

        [TestMethod]
        public void GivenCanMoveButEnterCallbackPreventsMove_WhenMove_ThenCurrentRoomDoesNotChange()
        {
            static RoomTransitionReaction movedInto(RoomTransition _)
            {
                return new RoomTransitionReaction(Reaction.Silent, false);
            }

            var room1 = new Room(Identifier.Empty, Description.Empty, [new Exit(Direction.East)]);
            var room2 = new Room(Identifier.Empty, Description.Empty, [new Exit(Direction.West)], enterCallback: movedInto);

            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room1, 0, 0, 0);
            region.AddRoom(room2, 1, 0, 0);
            region.Enter();

            var result = region.Move(Direction.East);

            Assert.AreEqual(room1, region.CurrentRoom);
        }

        [TestMethod]
        public void GivenCanMoveButExitCallbackPreventsMove_WhenMove_ThenCurrentRoomDoesNotChange()
        {
            static RoomTransitionReaction movedOutOf(RoomTransition _)
            {
                return new RoomTransitionReaction(Reaction.Silent, false);
            }

            var room1 = new Room(Identifier.Empty, Description.Empty, [new Exit(Direction.East)], exitCallback: movedOutOf);
            var room2 = new Room(Identifier.Empty, Description.Empty, [new Exit(Direction.West)]);

            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room1, 0, 0, 0);
            region.AddRoom(room2, 1, 0, 0);
            region.Enter();

            var result = region.Move(Direction.East);

            Assert.AreEqual(room1, region.CurrentRoom);
        }

        [TestMethod]
        public void GivenCantMove_WhenMove_ThenError()
        {
            var room1 = new Room(Identifier.Empty, Description.Empty, [new Exit(Direction.East)]);
            var room2 = new Room(Identifier.Empty, Description.Empty, [new Exit(Direction.West)]);

            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room1, 0, 0, 0);
            region.AddRoom(room2, 1, 0, 0);
            region.Enter();

            var result = region.Move(Direction.West);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenCanMove_WhenMove_ThenCurrentRoomIsNewRoom()
        {
            var room1 = new Room(Identifier.Empty, Description.Empty, [new Exit(Direction.East)]);
            var room2 = new Room(Identifier.Empty, Description.Empty, [new Exit(Direction.West)]);

            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room1, 0, 0, 0);
            region.AddRoom(room2, 1, 0, 0);
            region.Enter();

            region.Move(Direction.East);

            Assert.AreEqual(room2, region.CurrentRoom);
        }

        [TestMethod]
        public void GivenCantMove_WhenMove_ThenCurrentRoomIsNotChanged()
        {
            var room1 = new Room(Identifier.Empty, Description.Empty, [new Exit(Direction.East)]);
            var room2 = new Room(Identifier.Empty, Description.Empty, [new Exit(Direction.West)]);

            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room1, 0, 0, 0);
            region.AddRoom(room2, 1, 0, 0);
            region.Enter();

            region.Move(Direction.West);
            
            Assert.AreEqual(room1, region.CurrentRoom);
        }

        [TestMethod]
        public void GivenNoAdjoiningRoom_WhenGetAdjoiningRoom_ThenNull()
        {
            var room1 = new Room(Identifier.Empty, Description.Empty, [new Exit(Direction.East)]);
            var room2 = new Room(Identifier.Empty, Description.Empty, [new Exit(Direction.West)]);

            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room1, 0, 0, 0);
            region.AddRoom(room2, 1, 0, 0);

            var result = region.GetAdjoiningRoom(Direction.West, region.CurrentRoom);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GivenAdjoiningRoom_WhenGetAdjoiningRoom_ThenNotNull()
        {
            var room1 = new Room(Identifier.Empty, Description.Empty, [new Exit(Direction.East)]);
            var room2 = new Room(Identifier.Empty, Description.Empty, [new Exit(Direction.West)]);

            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room1, 0, 0, 0);
            region.AddRoom(room2, 1, 0, 0);
            region.Enter();

            var result = region.GetAdjoiningRoom(Direction.East, region.CurrentRoom);
            
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Given2LockedDoors_WhenUnlockDoorPair_ThenTrue()
        {
            var room1 = new Room(Identifier.Empty, Description.Empty, [new Exit(Direction.East, true)]);
            var room2 = new Room(Identifier.Empty, Description.Empty, [new Exit(Direction.West, true)]);

            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room1, 0, 0, 0);
            region.AddRoom(room2, 1, 0, 0);
            region.Enter();

            var result = region.UnlockDoorPair(Direction.East);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Given2LockedDoors_WhenUnlockDoorPair_ThenBothDoorsUnlocked()
        {
            var room1 = new Room(Identifier.Empty, Description.Empty, [new Exit(Direction.East, true)]);
            var room2 = new Room(Identifier.Empty, Description.Empty, [new Exit(Direction.West, true)]);

            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room1, 0, 0, 0);
            region.AddRoom(room2, 1, 0, 0);
            region.Enter();

            room1.FindExit(Direction.East, true, out var room1Exit);
            room2.FindExit(Direction.West, true, out var room2Exit);

            region.UnlockDoorPair(Direction.East);

            Assert.IsFalse(room1Exit.IsLocked);
            Assert.IsFalse(room2Exit.IsLocked);
        }

        [TestMethod]
        public void GivenNorth_WhenNextPosition_ThenXTheSameYIncrements()
        {
            Region.NextPosition(new Point3D(0, 0, 0), Direction.North, out var next);

            Assert.AreEqual(0, next.X);
            Assert.AreEqual(1, next.Y);
        }

        [TestMethod]
        public void GivenSouth_WhenNextPosition_ThenXTheSameYDecrements()
        {
            Region.NextPosition(new Point3D(0, 0, 0), Direction.South, out var next);

            Assert.AreEqual(0, next.X);
            Assert.AreEqual(-1, next.Y);
        }

        [TestMethod]
        public void GivenWest_WhenNextPosition_ThenXDecrementsYTheSame()
        {
            Region.NextPosition(new Point3D(0, 0, 0), Direction.West, out var next);

            Assert.AreEqual(-1, next.X);
            Assert.AreEqual(0, next.Y);
        }

        [TestMethod]
        public void GivenEast_WhenNextPosition_ThenXIncrementsYTheSame()
        {
            Region.NextPosition(new Point3D(0, 0, 0), Direction.East, out var next);

            Assert.AreEqual(1, next.X);
            Assert.AreEqual(0, next.Y);
        }

        [TestMethod]
        public void GivenRoomNotInRegion_WhenNextPosition_ThenNull()
        {
            var region = new Region(string.Empty, string.Empty);
            var room = new Room(Identifier.Empty, Description.Empty);

            var result = region.GetPositionOfRoom(room);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GivenRoomAtX0Y0Z0_WhenNextPosition_ThenNotNull()
        {
            var region = new Region(string.Empty, string.Empty);
            var room = new Room(Identifier.Empty, Description.Empty);
            region.AddRoom(room, 0, 0, 0);

            var result = region.GetPositionOfRoom(room);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GivenRoomAtX0Y0Z0_WhenNextPosition_ThenResultRoomEqualsInputRoom()
        {
            var region = new Region(string.Empty, string.Empty);
            var room = new Room(Identifier.Empty, Description.Empty);
            region.AddRoom(room, 0, 0, 0);

            var result = region.GetPositionOfRoom(room);

            Assert.AreEqual(room, result.Room);
        }

        [TestMethod]
        public void GivenRoomAtX0Y0Z0_WhenNextPosition_ThenX0Y0Z0()
        {
            var region = new Region(string.Empty, string.Empty);
            var room = new Room(Identifier.Empty, Description.Empty);
            region.AddRoom(room, 0, 0, 0);

            var result = region.GetPositionOfRoom(room);

            Assert.AreEqual(0, result.Position.X);
            Assert.AreEqual(0, result.Position.Y);
            Assert.AreEqual(0, result.Position.Z);
        }

        [TestMethod]
        public void GivenRoomInRegion_WhenTryFindRoom_ThenTrue()
        {
            var region = new Region(string.Empty, string.Empty);
            var room = new Room("a", string.Empty);
            region.AddRoom(room, 0, 0, 0);

            var result = region.TryFindRoom("a", out _);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenRoomInRegion_WhenTryFindRoom_ThenReturnCorrectRoom()
        {
            var region = new Region(string.Empty, string.Empty);
            var room = new Room("a", string.Empty);
            region.AddRoom(room, 0, 0, 0);

            region.TryFindRoom("a", out var result);

            Assert.AreEqual(room, result);
        }

        [TestMethod]
        public void GivenRoomNotInRegion_WhenTryFindRoom_ThenFalse()
        {
            var region = new Region(string.Empty, string.Empty);
            var room = new Room("a", string.Empty);
            region.AddRoom(room, 0, 0, 0);

            var result = region.TryFindRoom("b", out _);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenRoomNotInRegion_WhenTryFindRoom_ThenReturnNull()
        {
            var region = new Region(string.Empty, string.Empty);
            var room = new Room("a", string.Empty);
            region.AddRoom(room, 0, 0, 0);

            region.TryFindRoom("b", out var result);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GivenInvalidCoordinates_WhenJumpToRoom_ThenReturnError()
        {
            var region = new Region(string.Empty, string.Empty);
            var room = new Room("a", string.Empty);
            region.AddRoom(room, 0, 0, 0);
            region.AddRoom(room, 1, 0, 0);

            var result = region.JumpToRoom(new Point3D(0, 100, 0));

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenValidCoordinates_WhenJumpToRoom_ThenReturnSilent()
        {
            var region = new Region(string.Empty, string.Empty);
            var room1 = new Room(string.Empty, string.Empty);
            var room2 = new Room(string.Empty, string.Empty);
            region.AddRoom(room1, 0, 0, 0);
            region.AddRoom(room2, 1, 0, 0);
            region.SetStartRoom(room1);

            var result = region.JumpToRoom(new Point3D(1, 0, 0));

            Assert.AreEqual(ReactionResult.Silent, result.Result);
        }

        [TestMethod]
        public void GivenCurrentRoomNull_WhenEnter_ThenCurrentRoomNotNull()
        {
            var region = new Region(string.Empty, string.Empty);
            var room = new Room(string.Empty, string.Empty);
            region.AddRoom(room, 0, 0, 0);
            region.SetStartRoom(room);

            region.Enter();

            Assert.IsNotNull(region.CurrentRoom);
        }

        [TestMethod]
        public void GivenCurrentRoomNullAndStartRoomHasNoIntroduction_WhenEnter_ThenSilentReaction()
        {
            var region = new Region(string.Empty, string.Empty);
            var room = new Room(string.Empty, string.Empty);
            region.AddRoom(room, 0, 0, 0);
            region.SetStartRoom(room);

            var result = region.Enter();

            Assert.AreEqual(ReactionResult.Silent, result.Result);
        }

        [TestMethod]
        public void GivenCurrentRoomNullAndStartRoomHasNoIntroduction_WhenEnter_ThenInformReaction()
        {
            var region = new Region(string.Empty, string.Empty);
            var room = new Room(string.Empty, string.Empty, "ABC");
            region.AddRoom(room, 0, 0, 0);
            region.SetStartRoom(room);

            var result = region.Enter();

            Assert.AreEqual(ReactionResult.Inform, result.Result);
        }

        [TestMethod]
        public void GivenCurrentRoomNullAndNoStartRoomAssigned_WhenEnter_ThenCurrentRoomIsFirstRoom()
        {
            var region = new Region(string.Empty, string.Empty);
            var room = new Room(string.Empty, string.Empty);
            region.AddRoom(room, 0, 0, 0);

            region.Enter();

            Assert.AreEqual(room, region.CurrentRoom);
        }

        [TestMethod]
        public void GivenCurrentRoomNullAndNoRooms_WhenEnter_ThenCurrentRoomIsNull()
        {
            var region = new Region(string.Empty, string.Empty);

            region.Enter();

            Assert.IsNull(region.CurrentRoom);
        }

        [TestMethod]
        public void GivenNotRegion_WhenExamine_ThenReturnNonEmptyString()
        {
            var region = new Region(string.Empty, "A region");

            var result = region.Examination(new ExaminationRequest(new PlayableCharacter("a", "b"), new ExaminationScene(null, new Room(string.Empty, string.Empty))));

            Assert.IsGreaterThan(0, result.Description.Length);
        }

        [TestMethod]
        public void GivenRegion_WhenExamine_ThenReturnNonEmptyString()
        {
            var region = new Region(string.Empty, "A region");

            var result = region.Examination(new ExaminationRequest(region, new ExaminationScene(null, new Room(string.Empty, string.Empty))));

            Assert.IsGreaterThan(0, result.Description.Length);
        }
    }
}
