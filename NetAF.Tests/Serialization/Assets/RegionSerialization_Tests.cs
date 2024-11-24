using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Locations;
using NetAF.Serialization.Assets;

namespace NetAF.Tests.Serialization.Assets
{
    [TestClass]
    public class RegionSerialization_Tests
    {
        [TestMethod]
        public void GivenInRoomA_WhenFromRegion_ThenCurrentRoomIsA()
        {
            Region region = new(string.Empty, string.Empty);
            region.AddRoom(new("A", string.Empty), 0, 0, 0);
            region.Enter();

            RegionSerialization result = RegionSerialization.FromRegion(region);

            Assert.AreEqual("A", result.CurrentRoom);
        }

        [TestMethod]
        public void GivenNoRooms_WhenFromRegion_ThenRoomsLengthIs0()
        {
            Region region = new(string.Empty, string.Empty);

            RegionSerialization result = RegionSerialization.FromRegion(region);

            Assert.AreEqual(0, result.Rooms.Length);
        }

        [TestMethod]
        public void Given1Room_WhenFromRegion_ThenRoomsLengthIs1()
        {
            Region region = new(string.Empty, string.Empty);
            region.AddRoom(new(string.Empty, string.Empty), 0, 0, 0);
            region.Enter();

            RegionSerialization result = RegionSerialization.FromRegion(region);

            Assert.AreEqual(1, result.Rooms.Length);
        }

        [TestMethod]
        public void GivenRegionWith2Rooms_WhenRestoreFromRegionWhereSecondRoomIsCurrentRoom_ThenCurrentRoomIsSecondRoom()
        {
            Region region = new(string.Empty, string.Empty);
            region.AddRoom(new(string.Empty, string.Empty, [new Exit(Direction.North)]), 0, 0, 0);
            region.AddRoom(new("TARGET", string.Empty, [new Exit(Direction.South)]), 0, 1, 0);
            Region region2 = new(string.Empty, string.Empty);
            region2.AddRoom(new(string.Empty, string.Empty, [new Exit(Direction.North)]), 0, 0, 0);
            region2.AddRoom(new("TARGET", string.Empty, [new Exit(Direction.South)]), 0, 1, 0);
            region2.Enter();
            region2.Move(Direction.North);
            RegionSerialization serialization = RegionSerialization.FromRegion(region2);

            serialization.Restore(region);

            Assert.AreEqual("TARGET", region.CurrentRoom.Identifier.Name);
        }
    }
}
