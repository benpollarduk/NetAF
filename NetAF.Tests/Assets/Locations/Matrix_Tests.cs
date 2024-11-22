using NetAF.Assets.Locations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using NetAF.Assets;

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
                new(new(string.Empty, string.Empty), new Point3D(0, 0, 0)),
                new(new(string.Empty, string.Empty), new Point3D(0, 1, 0)),
                new(new(string.Empty, string.Empty), new Point3D(0, 1, 1)),
                new(new(string.Empty, string.Empty), new Point3D(0, 1, 2))
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
                new(new(string.Empty, string.Empty), new Point3D(0, 0, 0)),
                new(new(string.Empty, string.Empty), new Point3D(0, 1, 0)),
                new(new(string.Empty, string.Empty), new Point3D(0, 1, 1)),
                new(new(string.Empty, string.Empty), new Point3D(0, 1, 2))
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
                new(new(string.Empty, string.Empty), new Point3D(0, 0, 0)),
                new(new(string.Empty, string.Empty), new Point3D(0, 1, 0)),
                new(new(string.Empty, string.Empty), new Point3D(0, 1, 1)),
                new(new(string.Empty, string.Empty), new Point3D(0, 1, 2))
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
                new(new(string.Empty, string.Empty), new Point3D(0, 0, 0)),
                new(new(string.Empty, string.Empty), new Point3D(0, 1, 0)),
                new(new(string.Empty, string.Empty), new Point3D(0, 1, 1)),
                new(new(string.Empty, string.Empty), new Point3D(0, 1, 2))
            ];
            var matrix = new Matrix([.. roomPositions]);

            var result = matrix.ToRooms();

            Assert.AreEqual(4, result.Length);
        }

        [TestMethod]
        public void Given2RoomsOnZ2_WhenFindAllRoomsOnZ_Then2Rooms()
        {
            List<RoomPosition> roomPositions =
            [
                new(new(string.Empty, string.Empty), new Point3D(0, 0, 0)),
                new(new(string.Empty, string.Empty), new Point3D(0, 1, 0)),
                new(new(string.Empty, string.Empty), new Point3D(0, 1, 1)),
                new(new(string.Empty, string.Empty), new Point3D(0, 1, 2))
            ];
            var matrix = new Matrix([.. roomPositions]);

            var result = matrix.FindAllRoomsOnZ(0);

            Assert.AreEqual(2, result.Length);
        }

        [TestMethod]
        public void Given1RoomOnZ1_WhenFindAllRoomsOnZ_Then1Room()
        {
            List<RoomPosition> roomPositions =
            [
                new(new(string.Empty, string.Empty), new Point3D(0, 0, 0)),
                new(new(string.Empty, string.Empty), new Point3D(0, 1, 0)),
                new(new(string.Empty, string.Empty), new Point3D(0, 1, 1)),
                new(new(string.Empty, string.Empty), new Point3D(0, 1, 2))
            ];
            var matrix = new Matrix([.. roomPositions]);

            var result = matrix.FindAllRoomsOnZ(1);

            Assert.AreEqual(1, result.Length);
        }

        [TestMethod]
        public void Given2Point1UnitApart_WhenDistanceBetweenPoints_Then1()
        {
            Point3D a = new(0, 0, 0);
            Point3D b = new(1, 0, 0);

            var result = Matrix.DistanceBetweenPoints(a, b);

            Assert.AreEqual(1, (int)result);
        }
    }
}
