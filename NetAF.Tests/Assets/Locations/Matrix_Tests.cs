using NetAF.Assets.Locations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace NetAF.Tests.Assets.Locations
{
    [TestClass]
    public class Matrix_Tests
    {
        [TestMethod]
        public void GivenNoRooms_WhenGetWidth_Then0()
        {
            var matrix = new Matrix([]);

            var result = matrix.Width;

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GivenNoRooms_WhenGetHeight_Then0()
        {
            var matrix = new Matrix([]);

            var result = matrix.Height;

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GivenNoRooms_WhenGetDepth_Then0()
        {
            var matrix = new Matrix([]);

            var result = matrix.Depth;

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Given1RoomWide_WhenGetWidth_Then1()
        {
            List<RoomPosition> roomPositions =
            [
                new(new(string.Empty, string.Empty), 0, 0, 0),
                new(new(string.Empty, string.Empty), 0, 1, 0),
                new(new(string.Empty, string.Empty), 0, 1, 1),
                new(new(string.Empty, string.Empty), 0, 1, 2)
            ];
            var matrix = new Matrix([.. roomPositions]);

            var result = matrix.Width;

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Given2RoomsHigh_WhenGetHeight_Then2()
        {
            List<RoomPosition> roomPositions =
            [
                new(new(string.Empty, string.Empty), 0, 0, 0),
                new(new(string.Empty, string.Empty), 0, 1, 0),
                new(new(string.Empty, string.Empty), 0, 1, 1),
                new(new(string.Empty, string.Empty), 0, 1, 2)
            ];
            var matrix = new Matrix([.. roomPositions]);

            var result = matrix.Height;

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void Given3RoomsDeep_WhenGetDepth_Then3()
        {
            List<RoomPosition> roomPositions =
            [
                new(new(string.Empty, string.Empty), 0, 0, 0),
                new(new(string.Empty, string.Empty), 0, 1, 0),
                new(new(string.Empty, string.Empty), 0, 1, 1),
                new(new(string.Empty, string.Empty), 0, 1, 2)
            ];
            var matrix = new Matrix([.. roomPositions]);

            var result = matrix.Depth;

            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void Given4Rooms_WhenToRooms_Then4Rooms()
        {
            List<RoomPosition> roomPositions =
            [
                new(new(string.Empty, string.Empty), 0, 0, 0),
                new(new(string.Empty, string.Empty), 0, 1, 0),
                new(new(string.Empty, string.Empty), 0, 1, 1),
                new(new(string.Empty, string.Empty), 0, 1, 2)
            ];
            var matrix = new Matrix([.. roomPositions]);

            var result = matrix.ToRooms();

            Assert.AreEqual(4, result.Length);
        }
    }
}
