using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Locations;
using NetAF.Logic.Arrangement;

namespace NetAF.Tests.Logic.Arrangement
{
    [TestClass]
    public class RoomExitChange_Tests
    {
        [TestMethod]
        public void GivenRoom_WhenConstructed_ThenRoomIsSet()
        {
            Room room = new("ROOM", string.Empty);
            Exit exit = new(Direction.North);

            RoomExitChange result = new(room, exit, ExitChange.Add);

            Assert.AreEqual(room, result.Room);
        }

        [TestMethod]
        public void GivenExit_WhenConstructed_ThenExitIsSet()
        {
            Room room = new("ROOM", string.Empty);
            Exit exit = new(Direction.North);

            RoomExitChange result = new(room, exit, ExitChange.Add);

            Assert.AreEqual(exit, result.Exit);
        }

        [TestMethod]
        public void GivenAddChange_WhenConstructed_ThenChangeIsAdd()
        {
            Room room = new("ROOM", string.Empty);
            Exit exit = new(Direction.North);

            RoomExitChange result = new(room, exit, ExitChange.Add);

            Assert.AreEqual(ExitChange.Add, result.Change);
        }

        [TestMethod]
        public void GivenRemoveChange_WhenConstructed_ThenChangeIsRemove()
        {
            Room room = new("ROOM", string.Empty);
            Exit exit = new(Direction.North);

            RoomExitChange result = new(room, exit, ExitChange.Remove);

            Assert.AreEqual(ExitChange.Remove, result.Change);
        }
    }
}
