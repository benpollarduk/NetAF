using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Locations;
using NetAF.Serialization.Assets;

namespace NetAF.Tests.Serialization.Assets
{
    [TestClass]
    public class RegionSerialization_Tests
    {
        [TestMethod]
        public void GivenInRoomA_ThenCurrentRoomIsA()
        {
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(new("A", string.Empty), 0, 0, 0);

            var result = new RegionSerialization(region);

            Assert.AreEqual("A", result.CurrentRoom);
        }

        [TestMethod]
        public void GivenNoRooms_ThenRoomsLengthIs0()
        {
            var region = new Region(string.Empty, string.Empty);

            var result = new RegionSerialization(region);

            Assert.AreEqual(0, result.Rooms.Length);
        }

        [TestMethod]
        public void Given1Room_ThenRoomsLengthIs1()
        {
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(new(string.Empty, string.Empty), 0, 0, 0);

            var result = new RegionSerialization(region);

            Assert.AreEqual(1, result.Rooms.Length);
        }
    }
}
