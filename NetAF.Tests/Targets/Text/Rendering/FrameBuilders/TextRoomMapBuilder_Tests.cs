﻿using NetAF.Assets.Locations;
using NetAF.Rendering;
using NetAF.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Targets.Text.Rendering.FrameBuilders;
using System.Text;

namespace NetAF.Tests.Targets.Text.Rendering.FrameBuilders
{
    [TestClass]
    public class TextRoomMapBuilder_Tests
    {
        [TestMethod]
        public void GivenFullyFeaturedRoom_WhenBuildRoomMap_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var room = new Room(string.Empty, string.Empty, [new Exit(Direction.Up), new Exit(Direction.Down), new Exit(Direction.North), new Exit(Direction.East), new Exit(Direction.South), new Exit(Direction.West)]);
                room.AddItem(new(string.Empty, string.Empty));

                var regionMaker = new RegionMaker(string.Empty, string.Empty)
                {
                    [1, 2, 0] = new(string.Empty, string.Empty, [new Exit(Direction.South)]),
                    [0, 1, 0] = new(string.Empty, string.Empty, [new Exit(Direction.East)]),
                    [2, 1, 0] = new(string.Empty, string.Empty, [new Exit(Direction.West)]),
                    [1, 0, 0] = new(string.Empty, string.Empty, [new Exit(Direction.North)]),
                    [1, 1, 1] = new(string.Empty, string.Empty, [new Exit(Direction.Down)]),
                    [1, 1, -1] = new(string.Empty, string.Empty, [new Exit(Direction.Up)]),
                    [1, 1, 0] = room
                };

                var region = regionMaker.Make(1, 1, 0);
                region.Enter();
                var stringBuilder = new StringBuilder();
                var mapBuilder = new TextRoomMapBuilder(stringBuilder);

                mapBuilder.BuildRoomMap(room, ViewPoint.Create(region), KeyType.Full);
            });
        }

        [TestMethod]
        public void GivenFullyFeaturedRoomAndDynamicKey_WhenBuildRoomMap_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var room = new Room(string.Empty, string.Empty, [new Exit(Direction.Up), new Exit(Direction.Down), new Exit(Direction.North), new Exit(Direction.East), new Exit(Direction.South), new Exit(Direction.West)]);
                room.AddItem(new(string.Empty, string.Empty));

                var regionMaker = new RegionMaker(string.Empty, string.Empty)
                {
                    [1, 2, 0] = new(string.Empty, string.Empty, [new Exit(Direction.South)]),
                    [0, 1, 0] = new(string.Empty, string.Empty, [new Exit(Direction.East)]),
                    [2, 1, 0] = new(string.Empty, string.Empty, [new Exit(Direction.West)]),
                    [1, 0, 0] = new(string.Empty, string.Empty, [new Exit(Direction.North)]),
                    [1, 1, 1] = new(string.Empty, string.Empty, [new Exit(Direction.Down)]),
                    [1, 1, -1] = new(string.Empty, string.Empty, [new Exit(Direction.Up)]),
                    [1, 1, 0] = room
                };

                var region = regionMaker.Make(1, 1, 0);
                region.Enter();
                var stringBuilder = new StringBuilder();
                var mapBuilder = new TextRoomMapBuilder(stringBuilder);

                mapBuilder.BuildRoomMap(room, ViewPoint.Create(region), KeyType.Dynamic);
            });
        }

        [TestMethod]
        public void GivenNoFeaturedRoom_WhenBuildRoomMap_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var regionMaker = new RegionMaker(string.Empty, string.Empty)
                {
                    [0, 0, 0] = new(string.Empty, string.Empty)
                };

                var region = regionMaker.Make(0, 0, 0);
                region.Enter();
                var stringBuilder = new StringBuilder();
                var mapBuilder = new TextRoomMapBuilder(stringBuilder);

                mapBuilder.BuildRoomMap(regionMaker[0, 0, 0], ViewPoint.Create(region), KeyType.Full);
            });
        }

        [TestMethod]
        public void GivenFullyFeaturedRoomWithLockedDoors_WhenBuildRoomMap_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var room = new Room(string.Empty, string.Empty, [new Exit(Direction.Up, true), new Exit(Direction.Down, true), new Exit(Direction.North, true), new Exit(Direction.East, true), new Exit(Direction.South, true), new Exit(Direction.West, true)]);
                room.AddItem(new(string.Empty, string.Empty));

                var regionMaker = new RegionMaker(string.Empty, string.Empty)
                {
                    [1, 2, 0] = new(string.Empty, string.Empty, [new Exit(Direction.South)]),
                    [0, 1, 0] = new(string.Empty, string.Empty, [new Exit(Direction.East)]),
                    [2, 1, 0] = new(string.Empty, string.Empty, [new Exit(Direction.West)]),
                    [1, 0, 0] = new(string.Empty, string.Empty, [new Exit(Direction.North)]),
                    [1, 1, 1] = new(string.Empty, string.Empty, [new Exit(Direction.Down)]),
                    [1, 1, -1] = new(string.Empty, string.Empty, [new Exit(Direction.Up)]),
                    [1, 1, 0] = room
                };

                var region = regionMaker.Make(1, 1, 0);
                region.Enter();
                var stringBuilder = new StringBuilder();
                var mapBuilder = new TextRoomMapBuilder(stringBuilder);

                mapBuilder.BuildRoomMap(room, ViewPoint.Create(region), KeyType.Full);
            });
        }
    }
}
