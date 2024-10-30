using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Locations;
using NetAF.Serialization.Assets;

namespace NetAF.Tests.Serialization.Assets
{
    [TestClass]
    public class RoomSerialization_Tests
    {
        [TestMethod]
        public void GivenHasBeenVisitedIsFalse_ThenHasBeenVisitedIsFalse()
        {
            var room = new Room(string.Empty, string.Empty);

            var result = new RoomSerialization(room);

            Assert.IsFalse(result.HasBeenVisited);
        }

        [TestMethod]
        public void GivenHasBeenVisitedIsTrue_ThenHasBeenVisitedIsTrue()
        {
            var room = new Room(string.Empty, string.Empty);
            room.MovedInto(Direction.North);

            var result = new RoomSerialization(room);

            Assert.IsTrue(result.HasBeenVisited);
        }

        [TestMethod]
        public void GivenNoItems_ThenItemsLengthIs0()
        {
            var room = new Room(string.Empty, string.Empty);

            var result = new RoomSerialization(room);

            Assert.AreEqual(0, result.Items.Length);
        }

        [TestMethod]
        public void Given1Item_ThenItemsLengthIs1()
        {
            var room = new Room(string.Empty, string.Empty);
            room.AddItem(new(string.Empty, string.Empty));

            var result = new RoomSerialization(room);

            Assert.AreEqual(1, result.Items.Length);
        }

        [TestMethod]
        public void GivenNoCharacters_ThenCharactersLengthIs0()
        {
            var room = new Room(string.Empty, string.Empty);

            var result = new RoomSerialization(room);

            Assert.AreEqual(0, result.Characters.Length);
        }

        [TestMethod]
        public void Given1Character_ThenCharactersLengthIs1()
        {
            var room = new Room(string.Empty, string.Empty);
            room.AddCharacter(new(string.Empty, string.Empty));

            var result = new RoomSerialization(room);

            Assert.AreEqual(1, result.Characters.Length);
        }

        [TestMethod]
        public void GivenNoExits_ThenExitsLengthIs0()
        {
            var room = new Room(string.Empty, string.Empty);

            var result = new RoomSerialization(room);

            Assert.AreEqual(0, result.Exits.Length);
        }

        [TestMethod]
        public void Given1Exit_ThenExitsLengthIs1()
        {
            var room = new Room(string.Empty, string.Empty);
            room.AddExit(new(Direction.North));

            var result = new RoomSerialization(room);

            Assert.AreEqual(1, result.Exits.Length);
        }
    }
}
