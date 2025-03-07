﻿using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Rendering.FrameBuilders;
using NetAF.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;

namespace NetAF.Tests.Rendering.FrameBuilders
{
    [TestClass]
    public class SceneHelper_Tests
    {
        [TestMethod]
        public void GivenRoomWithNoView_WhenCreateViewpointAsString_ThenEmptyString()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var room = new Room(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            var region = regionMaker.Make();
            region.Enter();
            var viewPoint = ViewPoint.Create(region);

            var result = SceneHelper.CreateViewpointAsString(room, viewPoint);

            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void GivenRoomWithAView_WhenCreateViewpointAsString_ThenNonEmptyString()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var room = new Room(string.Empty, string.Empty, [new Exit(Direction.East)]);
            regionMaker[0, 0, 0] = room;
            regionMaker[1, 0, 0] = new Room("Test", "Test", [new Exit(Direction.West)]);
            var region = regionMaker.Make();
            region.Enter();
            var viewPoint = ViewPoint.Create(region);

            var result = SceneHelper.CreateViewpointAsString(room, viewPoint);

            Assert.AreNotEqual(string.Empty, result);
        }

        [TestMethod]
        public void GivenRoomWithAViewAndANamedLockedExit_WhenCreateViewpointAsString_ThenStringReturnsExitName()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var room = new Room(string.Empty, string.Empty, [new Exit(Direction.East, true, new Identifier("TEST_EXIT"))]);
            regionMaker[0, 0, 0] = room;
            regionMaker[1, 0, 0] = new Room("Test", "Test", [new Exit(Direction.West)]);
            var region = regionMaker.Make();
            region.Enter();
            var viewPoint = ViewPoint.Create(region);

            var result = SceneHelper.CreateViewpointAsString(room, viewPoint);

            Assert.IsTrue(result.Contains("TEST_EXIT", System.StringComparison.InvariantCultureIgnoreCase));
        }

        [TestMethod]
        public void GivenRoomWithAViewInAllDirections_WhenCreateViewpointAsString_ThenNonEmptyString()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var centralExits = new[]
            {
                new Exit(Direction.North),
                new Exit(Direction.East),
                new Exit(Direction.South),
                new Exit(Direction.West),
                new Exit(Direction.Up),
                new Exit(Direction.Down)
            };
            regionMaker[1, 1, 1] = new(string.Empty, string.Empty, centralExits);
            regionMaker[0, 1, 1] = new("Test", "Test", [new Exit(Direction.East)]);
            regionMaker[1, 0, 1] = new("Test", "Test", [new Exit(Direction.North)]);
            regionMaker[2, 1, 1] = new("Test", "Test", [new Exit(Direction.West)]);
            regionMaker[1, 2, 1] = new("Test", "Test", [new Exit(Direction.South)]);
            regionMaker[1, 1, 2] = new("Test", "Test", [new Exit(Direction.Down)]);
            regionMaker[1, 1, 0] = new("Test", "Test", [new Exit(Direction.Up)]);
            var region = regionMaker.Make();
            region.Enter();
            var viewPoint = ViewPoint.Create(region);

            var result = SceneHelper.CreateViewpointAsString(regionMaker[1, 1, 1], viewPoint);

            Assert.AreNotEqual(string.Empty, result);
        }

        [TestMethod]
        public void GivenRoomWithNoNPCs_WhenCreateNPCString_ThenEmptyString()
        {
            var room = new Room(string.Empty, string.Empty);

            var result = SceneHelper.CreateNPCString(room);

            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void GivenRoomWithASingleNPC_WhenCreateNPCString_ThenNonEmptyString()
        {
            var room = new Room(string.Empty, string.Empty);
            room.AddCharacter(new NonPlayableCharacter("Test", "Test"));

            var result = SceneHelper.CreateNPCString(room);

            Assert.AreNotEqual(string.Empty, result);
        }

        [TestMethod]
        public void GivenRoomWithTwoNPCs_WhenCreateNPCString_ThenNonEmptyString()
        {
            var room = new Room(string.Empty, string.Empty);
            room.AddCharacter(new NonPlayableCharacter("Test", "Test"));
            room.AddCharacter(new NonPlayableCharacter("Test2", "Test"));

            var result = SceneHelper.CreateNPCString(room);

            Assert.AreNotEqual(string.Empty, result);
        }
    }
}
