using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Locations;
using NetAF.Serialization;
using NetAF.Serialization.Assets;

namespace NetAF.Tests.Serialization.Assets
{
    [TestClass]
    public class RoomSerialization_Tests
    {
        [TestMethod]
        public void GivenHasBeenVisitedIsFalse_WhenFromRoom_ThenHasBeenVisitedIsFalse()
        {
            Room room = new(string.Empty, string.Empty);

            RoomSerialization result = RoomSerialization.FromRoom(room);

            Assert.IsFalse(result.HasBeenVisited);
        }

        [TestMethod]
        public void GivenHasBeenVisitedIsTrue_WhenFromRoom_ThenHasBeenVisitedIsTrue()
        {
            Room room = new(string.Empty, string.Empty);
            room.MovedInto(null, null, Direction.North);

            RoomSerialization result = RoomSerialization.FromRoom(room);

            Assert.IsTrue(result.HasBeenVisited);
        }

        [TestMethod]
        public void GivenNoItems_WhenFromRoom_ThenItemsLengthIs0()
        {
            Room room = new(string.Empty, string.Empty);

            RoomSerialization result = RoomSerialization.FromRoom(room);

            Assert.AreEqual(0, result.Items.Length);
        }

        [TestMethod]
        public void Given1Item_WhenFromRoom_ThenItemsLengthIs1()
        {
            Room room = new(string.Empty, string.Empty);
            room.AddItem(new(string.Empty, string.Empty));

            RoomSerialization result = RoomSerialization.FromRoom(room);

            Assert.AreEqual(1, result.Items.Length);
        }

        [TestMethod]
        public void GivenNoCharacters_WhenFromRoom_ThenCharactersLengthIs0()
        {
            Room room = new(string.Empty, string.Empty);

            RoomSerialization result = RoomSerialization.FromRoom(room);

            Assert.AreEqual(0, result.Characters.Length);
        }

        [TestMethod]
        public void Given1Character_WhenFromRoom_ThenCharactersLengthIs1()
        {
            Room room = new(string.Empty, string.Empty);
            room.AddCharacter(new(string.Empty, string.Empty));

            RoomSerialization result = RoomSerialization.FromRoom(room);

            Assert.AreEqual(1, result.Characters.Length);
        }

        [TestMethod]
        public void GivenNoExits_WhenFromRoom_ThenExitsLengthIs0()
        {
            Room room = new(string.Empty, string.Empty);

            RoomSerialization result = RoomSerialization.FromRoom(room);

            Assert.AreEqual(0, result.Exits.Length);
        }

        [TestMethod]
        public void Given1Exit_WhenFromRoom_ThenExitsLengthIs1()
        {
            Room room = new(string.Empty, string.Empty, [new Exit(Direction.North)]);

            RoomSerialization result = RoomSerialization.FromRoom(room);

            Assert.AreEqual(1, result.Exits.Length);
        }

        [TestMethod]
        public void GivenRoomThatHasNotBeenVisited_WhenRestoreFromRoomThatHasBeenVisited_ThenRoomHasBeenVisitedIsTrue()
        {
            Room room = new(string.Empty, string.Empty, [new Exit(Direction.North)]);
            room.AddItem(new(string.Empty, string.Empty));
            room.AddCharacter(new(string.Empty, string.Empty));
            Room room2 = new(string.Empty, string.Empty, [new Exit(Direction.North)]);
            room.AddItem(new(string.Empty, string.Empty));
            room.AddCharacter(new(string.Empty, string.Empty));
            room2.MovedInto(null, null, Direction.North);
            RoomSerialization serialization = RoomSerialization.FromRoom(room2);

            ((IObjectSerialization<Room>)serialization).Restore(room);

            Assert.IsTrue(room.HasBeenVisited);
        }
    }
}
