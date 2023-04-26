﻿using System;
using System.Collections.Generic;
using System.Linq;
using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Utils.Generation.Simple;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Utils.Generation.Simple
{
    [TestClass]
    public class RoomGenerator_Tests
    {
        [TestMethod]
        public void GivenNoPreviousRooms_WhenTryGetNextRoomLocation_ThenReturnTrue()
        {
            var positions = new List<RoomPosition>();
            
            var result = RoomGenerator.TryGetNextRoomLocation(0, 0, positions, new Random(1234), out _, out _);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenSurroundedRoom_WhenTryGetNextRoomLocation_ThenReturnFalse()
        {
            var positions = new List<RoomPosition>
            {
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 0, 0),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 1, 0),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 2, 0),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 0, 1),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 1, 1),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 2, 1),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 0, 2),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 1, 2),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 2, 2)
            };

            var result = RoomGenerator.TryGetNextRoomLocation(1, 1, positions, new Random(1234), out _, out _);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenSurroundedExceptNorth_WhenTryGetNextRoomLocation_ThenReturnNorth()
        {
            var positions = new List<RoomPosition>
            {
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 0, 0),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 1, 0),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 2, 0),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 0, 1),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 1, 1),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 2, 1),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 0, 2),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 1, 2),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 2, 2)
            };

            RoomGenerator.TryGetNextRoomLocation(1, 2, positions, new Random(1234), out _, out var y);

            Assert.AreEqual(3, y);
        }

        [TestMethod]
        public void GivenSurroundedExceptSouth_WhenTryGetNextRoomLocation_ThenReturnSouth()
        {
            var positions = new List<RoomPosition>
            {
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 0, 0),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 1, 0),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 2, 0),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 0, 1),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 1, 1),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 2, 1),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 0, 2),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 1, 2),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 2, 2)
            };

            RoomGenerator.TryGetNextRoomLocation(1, 0, positions, new Random(1234), out _, out var y);

            Assert.AreEqual(-1, y);
        }

        [TestMethod]
        public void GivenSurroundedExceptEast_WhenTryGetNextRoomLocation_ThenReturnEast()
        {
            var positions = new List<RoomPosition>
            {
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 0, 0),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 1, 0),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 2, 0),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 0, 1),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 1, 1),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 2, 1),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 0, 2),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 1, 2),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 2, 2)
            };

            RoomGenerator.TryGetNextRoomLocation(2, 1, positions, new Random(1234), out var x, out _);

            Assert.AreEqual(3, x);
        }

        [TestMethod]
        public void GivenSurroundedExceptWest_WhenTryGetNextRoomLocation_ThenReturnWest()
        {
            var positions = new List<RoomPosition>
            {
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 0, 0),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 1, 0),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 2, 0),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 0, 1),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 1, 1),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 2, 1),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 0, 2),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 1, 2),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 2, 2)
            };

            RoomGenerator.TryGetNextRoomLocation(0, 1, positions, new Random(1234), out var x, out _);

            Assert.AreEqual(-1, x);
        }

        [TestMethod]
        public void Given1_WhenGetRoomPositions_ThenReturn1Room()
        {
            var positions = RoomGenerator.GetRoomPositions(1, new Random(1234));

            Assert.AreEqual(1, positions.Count);
        }

        [TestMethod]
        public void Given3_WhenGetRoomPositions_ThenReturn3Rooms()
        {
            var positions = RoomGenerator.GetRoomPositions(3, new Random(1234));

            Assert.AreEqual(3, positions.Count);
        }

        [TestMethod]
        public void Given1RoomWithNoExits_ResolveRequiredExits_ThenNoExits()
        {
            var positions = new[]
            {
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 0, 0)
            };

            RoomGenerator.ResolveRequiredExits(positions);

            Assert.AreEqual(0, positions[0].Room.Exits.Count);
        }

        [TestMethod]
        public void Given2RoomsWithNoExits_ResolveRequiredExits_Then2Exits()
        {
            var positions = new[]
            {
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 0, 0),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 0, 1)
            };

            RoomGenerator.ResolveRequiredExits(positions);

            Assert.AreEqual(1, positions[0].Room.Exits.Count);
            Assert.AreEqual(1, positions[1].Room.Exits.Count);
        }

        [TestMethod]
        public void Given2RoomsWithNoExits_ResolveRequiredExits_ThenExitsAtNorthAndSouth()
        {
            var positions = new[]
            {
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 0, 0),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 0, 1)
            };

            RoomGenerator.ResolveRequiredExits(positions);

            Assert.AreEqual(CardinalDirection.North, positions[0].Room.Exits.First().Direction);
            Assert.AreEqual(CardinalDirection.South, positions[1].Room.Exits.First().Direction);
        }

        [TestMethod]
        public void Given2RoomsWithNoExits_ResolveRequiredExits_ThenExitsAtEastAndWest()
        {
            var positions = new[]
            {
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 0, 0),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 1, 0)
            };

            RoomGenerator.ResolveRequiredExits(positions);

            Assert.AreEqual(CardinalDirection.East, positions[0].Room.Exits.First().Direction);
            Assert.AreEqual(CardinalDirection.West, positions[1].Room.Exits.First().Direction);
        }

        [TestMethod]
        public void Given4RoomsWithNoExits_ResolveRequiredExits_ThenEachRoomHasValidExits()
        {
            var positions = new[]
            {
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 0, 0),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 0, 1),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 1, 1),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 1, 0)
            };

            RoomGenerator.ResolveRequiredExits(positions);

            Assert.AreEqual(1, positions[0].Room.Exits.Count);
            Assert.AreEqual(2, positions[1].Room.Exits.Count);
            Assert.AreEqual(2, positions[2].Room.Exits.Count);
            Assert.AreEqual(1, positions[3].Room.Exits.Count);

            Assert.IsTrue(positions[0].Room.Exits.Any(x => x.Direction == CardinalDirection.North));
            Assert.IsTrue(positions[1].Room.Exits.Any(x => x.Direction == CardinalDirection.South));
            Assert.IsTrue(positions[1].Room.Exits.Any(x => x.Direction == CardinalDirection.East));
            Assert.IsTrue(positions[2].Room.Exits.Any(x => x.Direction == CardinalDirection.West));
            Assert.IsTrue(positions[2].Room.Exits.Any(x => x.Direction == CardinalDirection.South));
            Assert.IsTrue(positions[3].Room.Exits.Any(x => x.Direction == CardinalDirection.North));
        }

        [TestMethod]
        public void Given4RoomsWithNoExits_ResolveAllPossibleExits_ThenEachRoomHas2ExitsOnInternalWalls()
        {
            var positions = new[]
            {
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 0, 0),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 0, 1),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 1, 1),
                new RoomPosition(new Room(Identifier.Empty, Description.Empty), 1, 0)
            };

            RoomGenerator.ResolveAllPossibleExits(positions);

            Assert.AreEqual(2, positions[0].Room.Exits.Count);
            Assert.AreEqual(2, positions[1].Room.Exits.Count);
            Assert.AreEqual(2, positions[2].Room.Exits.Count);
            Assert.AreEqual(2, positions[3].Room.Exits.Count);

            Assert.IsTrue(positions[0].Room.Exits.Any(x => x.Direction == CardinalDirection.North));
            Assert.IsTrue(positions[0].Room.Exits.Any(x => x.Direction == CardinalDirection.East));
            Assert.IsTrue(positions[1].Room.Exits.Any(x => x.Direction == CardinalDirection.South));
            Assert.IsTrue(positions[1].Room.Exits.Any(x => x.Direction == CardinalDirection.East));
            Assert.IsTrue(positions[2].Room.Exits.Any(x => x.Direction == CardinalDirection.South));
            Assert.IsTrue(positions[2].Room.Exits.Any(x => x.Direction == CardinalDirection.West));
            Assert.IsTrue(positions[3].Room.Exits.Any(x => x.Direction == CardinalDirection.North));
            Assert.IsTrue(positions[3].Room.Exits.Any(x => x.Direction == CardinalDirection.West));
        }
    }
}
