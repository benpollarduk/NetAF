﻿using System;
using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetAF.Tests.Utilities
{
    [TestClass]
    public class RegionMaker_Tests
    {
        [TestMethod]
        public void GivenNullCollection_WhenConvertToRoomMatrix_ThenIsNull()
        {
            var matrix = RegionMaker.ConvertToRoomMatrix(Array.Empty<RoomPosition>());

            Assert.IsNull(matrix);
        }

        [TestMethod]
        public void Given1RoomCollection_WhenConvertToRoomMatrix_ThenNotNull()
        {
            var room = new RoomPosition(new Room(Identifier.Empty, Description.Empty), new Point3D(0, 0, 0));

            var matrix = RegionMaker.ConvertToRoomMatrix(new[] { room });

            Assert.IsNotNull(matrix);
        }

        [TestMethod]
        public void Given1RoomCollection_WhenConvertToRoomMatrix_Then1x1Matrix()
        {
            var room = new RoomPosition(new Room(Identifier.Empty, Description.Empty), new Point3D(0, 0, 0));

            var matrix = RegionMaker.ConvertToRoomMatrix(new[] { room });

            Assert.AreEqual(1, matrix.Width);
            Assert.AreEqual(1, matrix.Height);
        }

        [TestMethod]
        public void Given2RoomCollection_WhenConvertToRoomMatrix_Then1x2Matrix()
        {
            var room1 = new RoomPosition(new Room(Identifier.Empty, Description.Empty), new Point3D(0, 0, 0));
            var room2 = new RoomPosition(new Room(Identifier.Empty, Description.Empty), new Point3D(0, 1, 0));

            var matrix = RegionMaker.ConvertToRoomMatrix(new[] { room1, room2 });

            Assert.AreEqual(1, matrix.Width);
            Assert.AreEqual(2, matrix.Height);
        }

        [TestMethod]
        public void Given2RoomCollection_WhenConvertToRoomMatrix_Then2x1Matrix()
        {
            var room1 = new RoomPosition(new Room(Identifier.Empty, Description.Empty), new Point3D(0, 0, 0));
            var room2 = new RoomPosition(new Room(Identifier.Empty, Description.Empty), new Point3D(1, 0, 0));

            var matrix = RegionMaker.ConvertToRoomMatrix(new[] { room1, room2 });

            Assert.AreEqual(2, matrix.Width);
            Assert.AreEqual(1, matrix.Height);
        }

        [TestMethod]
        public void Given4Rooms_WhenMake_Then4Rooms()
        {
            var room1 = new Room(Identifier.Empty, Description.Empty);
            var room2 = new Room(Identifier.Empty, Description.Empty);
            var room3 = new Room(Identifier.Empty, Description.Empty);
            var room4 = new Room(Identifier.Empty, Description.Empty);
            var regionMaker = new RegionMaker(string.Empty, string.Empty)
            {
                [0, 0, 0] = room1,
                [1, 0, 0] = room2,
                [2, 0, 0] = room3,
                [3, 0, 0] = room4
            };
            var region = regionMaker.Make();

            Assert.AreEqual(4, region.Rooms);
        }

        [TestMethod]
        public void GivenCantPlaceRoom_WhenCanPlaceRoom_ThenFalse()
        {
            var room = new Room(Identifier.Empty, Description.Empty);
            var regionMaker = new RegionMaker(string.Empty, string.Empty)
            {
                [0, 0, 0] = room
            };

            var result = regionMaker.CanPlaceRoom(0, 0, 0);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenCanPlaceRoom_WhenCanPlaceRoom_ThenTrue()
        {
            var room = new Room(Identifier.Empty, Description.Empty);
            var regionMaker = new RegionMaker(string.Empty, string.Empty)
            {
                [0, 0, 0] = room
            };

            var result = regionMaker.CanPlaceRoom(1, 0, 0);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenOneRoom_WhenGetRoomPositions_ThenReturn1Room()
        {
            var room = new Room(Identifier.Empty, Description.Empty);
            var regionMaker = new RegionMaker(string.Empty, string.Empty)
            {
                [0, 0, 0] = room
            };

            var result = regionMaker.GetRoomPositions();

            Assert.AreEqual(1, result.Length);
        }
    }
}
