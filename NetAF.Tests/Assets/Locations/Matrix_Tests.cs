using NetAF.Assets.Locations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using NetAF.Assets;
using NetAF.Serialization.Assets;
using NetAF.Serialization;

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
            List<RoomPosition> roomPositions =
            [
                new(new(string.Empty, string.Empty), new Point3D(0, 0, 0)),
                new(new(string.Empty, string.Empty), new Point3D(0, 1, 0))
            ];
            var matrix = new Matrix([.. roomPositions]);

            var result = matrix.DistanceBetweenRooms(roomPositions[0].Room, roomPositions[1].Room);

            Assert.AreEqual(1, (int)result);
        }

        [TestMethod]
        public void GivenANull_WhenDistanceBetweenPoints_Then0()
        {
            List<RoomPosition> roomPositions =
            [
                new(new(string.Empty, string.Empty), new Point3D(0, 0, 0)),
                new(new(string.Empty, string.Empty), new Point3D(0, 1, 0))
            ];
            var matrix = new Matrix([.. roomPositions]);

            var result = matrix.DistanceBetweenRooms(null, roomPositions[1].Room);

            Assert.AreEqual(0, (int)result);
        }

        [TestMethod]
        public void GivenBNull_WhenDistanceBetweenPoints_Then0()
        {
            List<RoomPosition> roomPositions =
            [
                new(new(string.Empty, string.Empty), new Point3D(0, 0, 0)),
                new(new(string.Empty, string.Empty), new Point3D(0, 1, 0))
            ];
            var matrix = new Matrix([.. roomPositions]);

            var result = matrix.DistanceBetweenRooms(roomPositions[0].Room, null);

            Assert.AreEqual(0, (int)result);
        }

        [TestMethod]
        public void Given2Point1UnitApart_WhenDistanceBetweenRooms_Then1()
        {
            Point3D a = new(0, 0, 0);
            Point3D b = new(1, 0, 0);

            var result = Matrix.DistanceBetweenPoints(a, b);

            Assert.AreEqual(1, (int)result);
        }

        [TestMethod]
        public void GivenVisitedRoomOn1And1VisitedRoomOn3_WhenFindAllZWithVisitedRooms_ThenReturnContaining1And3()
        {
            List<RoomPosition> roomPositions =
            [
                new(new(string.Empty, string.Empty), new Point3D(0, 0, 0)),
                new(new(string.Empty, string.Empty), new Point3D(0, 1, 0)),
                new(new(string.Empty, string.Empty), new Point3D(0, 1, 1)),
                new(new(string.Empty, string.Empty), new Point3D(0, 1, 2)),
                new(new(string.Empty, string.Empty), new Point3D(0, 1, 3))
            ];

            var serialization = RoomSerialization.FromRoom(roomPositions[2].Room);
            serialization.HasBeenVisited = true;
            ((IRestoreFromObjectSerialization<RoomSerialization>)roomPositions[2].Room).RestoreFrom(serialization);
            ((IRestoreFromObjectSerialization<RoomSerialization>)roomPositions[4].Room).RestoreFrom(serialization);

            var matrix = new Matrix([.. roomPositions]);

            var result = matrix.FindAllZWithVisitedRooms();

            Assert.AreEqual(2, result.Length);
            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(3, result[1]);
        }
    }
}
