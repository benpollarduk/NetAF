using NetAF.Assets.Locations;
using NetAF.Rendering;
using NetAF.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Console.Rendering.FrameBuilders.Common;

namespace NetAF.Tests.Targets.Console.Rendering.FrameBuilders.Common
{
    [TestClass]
    public class ConsoleHighDetailRoomMapBuilder_Tests
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
                var stringBuilder = new GridStringBuilder();
                var mapBuilder = new ConsoleHighDetailRoomMapBuilder(stringBuilder, ConsoleMapRenderProperties.Default);
                stringBuilder.Resize(new(50, 50));

                mapBuilder.BuildRoomMap(room, ViewPoint.Create(region), new RoomMapRenderOptions { KeyType = KeyType.Full }, new Point2D(0, 0), out _, out _);
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
                var stringBuilder = new GridStringBuilder();
                var mapBuilder = new ConsoleHighDetailRoomMapBuilder(stringBuilder, ConsoleMapRenderProperties.Default);
                stringBuilder.Resize(new(50, 50));

                mapBuilder.BuildRoomMap(room, ViewPoint.Create(region), new RoomMapRenderOptions { KeyType = KeyType.Dynamic }, new Point2D(0, 0), out _, out _);
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
                var stringBuilder = new GridStringBuilder();
                var mapBuilder = new ConsoleHighDetailRoomMapBuilder(stringBuilder, ConsoleMapRenderProperties.Default);
                stringBuilder.Resize(new(50, 50));

                mapBuilder.BuildRoomMap(regionMaker[0, 0, 0], ViewPoint.Create(region), new RoomMapRenderOptions { KeyType = KeyType.Full }, new Point2D(0, 0), out _, out _);
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
                var stringBuilder = new GridStringBuilder();
                var mapBuilder = new ConsoleHighDetailRoomMapBuilder(stringBuilder, ConsoleMapRenderProperties.Default);
                stringBuilder.Resize(new(50, 50));

                mapBuilder.BuildRoomMap(room, ViewPoint.Create(region), new RoomMapRenderOptions { KeyType = KeyType.Full }, new Point2D(0, 0), out _, out _);
            });
        }

        private static Region CreateFullyFeaturedRegion(out Room room, bool locked = false)
        {
            room = new Room(string.Empty, string.Empty, [new Exit(Direction.Up, locked), new Exit(Direction.Down, locked), new Exit(Direction.North, locked), new Exit(Direction.East, locked), new Exit(Direction.South, locked), new Exit(Direction.West, locked)]);
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
            return region;
        }

        [TestMethod]
        public void GivenFullyFeaturedRoomAndKeyPlacementRight_WhenBuildRoomMap_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var region = CreateFullyFeaturedRegion(out var room);
                var stringBuilder = new GridStringBuilder();
                var mapBuilder = new ConsoleHighDetailRoomMapBuilder(stringBuilder, ConsoleMapRenderProperties.Default);
                stringBuilder.Resize(new(50, 50));

                mapBuilder.BuildRoomMap(room, ViewPoint.Create(region), new RoomMapRenderOptions { KeyType = KeyType.Full, KeyPlacement = KeyPlacement.Right }, new Point2D(10, 10), out _, out _);
            });
        }

        [TestMethod]
        public void GivenFullyFeaturedRoomAndKeyPlacementLeft_WhenBuildRoomMap_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var region = CreateFullyFeaturedRegion(out var room);
                var stringBuilder = new GridStringBuilder();
                var mapBuilder = new ConsoleHighDetailRoomMapBuilder(stringBuilder, ConsoleMapRenderProperties.Default);
                stringBuilder.Resize(new(50, 50));

                mapBuilder.BuildRoomMap(room, ViewPoint.Create(region), new RoomMapRenderOptions { KeyType = KeyType.Full, KeyPlacement = KeyPlacement.Left }, new Point2D(10, 10), out _, out _);
            });
        }

        [TestMethod]
        public void GivenFullyFeaturedRoomAndKeyPlacementAbove_WhenBuildRoomMap_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var region = CreateFullyFeaturedRegion(out var room);
                var stringBuilder = new GridStringBuilder();
                var mapBuilder = new ConsoleHighDetailRoomMapBuilder(stringBuilder, ConsoleMapRenderProperties.Default);
                stringBuilder.Resize(new(50, 50));

                mapBuilder.BuildRoomMap(room, ViewPoint.Create(region), new RoomMapRenderOptions { KeyType = KeyType.Full, KeyPlacement = KeyPlacement.Above }, new Point2D(10, 10), out _, out _);
            });
        }

        [TestMethod]
        public void GivenFullyFeaturedRoomAndKeyPlacementBelow_WhenBuildRoomMap_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var region = CreateFullyFeaturedRegion(out var room);
                var stringBuilder = new GridStringBuilder();
                var mapBuilder = new ConsoleHighDetailRoomMapBuilder(stringBuilder, ConsoleMapRenderProperties.Default);
                stringBuilder.Resize(new(50, 50));

                mapBuilder.BuildRoomMap(room, ViewPoint.Create(region), new RoomMapRenderOptions { KeyType = KeyType.Full, KeyPlacement = KeyPlacement.Below }, new Point2D(10, 10), out _, out _);
            });
        }

        [TestMethod]
        public void GivenFullyFeaturedRoomAndPointOfInterestDetailNone_WhenBuildRoomMap_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var region = CreateFullyFeaturedRegion(out var room);
                var stringBuilder = new GridStringBuilder();
                var mapBuilder = new ConsoleHighDetailRoomMapBuilder(stringBuilder, ConsoleMapRenderProperties.Default);
                stringBuilder.Resize(new(50, 50));

                mapBuilder.BuildRoomMap(room, ViewPoint.Create(region), new RoomMapRenderOptions { KeyType = KeyType.Dynamic, PointOfInterestDetail = PointOfInterestDetail.None }, new Point2D(0, 0), out _, out _);
            });
        }

        [TestMethod]
        public void GivenFullyFeaturedRoomAndPointOfInterestDetailLow_WhenBuildRoomMap_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var region = CreateFullyFeaturedRegion(out var room);
                var stringBuilder = new GridStringBuilder();
                var mapBuilder = new ConsoleHighDetailRoomMapBuilder(stringBuilder, ConsoleMapRenderProperties.Default);
                stringBuilder.Resize(new(50, 50));

                mapBuilder.BuildRoomMap(room, ViewPoint.Create(region), new RoomMapRenderOptions { KeyType = KeyType.Dynamic, PointOfInterestDetail = PointOfInterestDetail.Low }, new Point2D(0, 0), out _, out _);
            });
        }

        [TestMethod]
        public void GivenFullyFeaturedRoomAndPointOfInterestDetailHigh_WhenBuildRoomMap_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var region = CreateFullyFeaturedRegion(out var room);
                var stringBuilder = new GridStringBuilder();
                var mapBuilder = new ConsoleHighDetailRoomMapBuilder(stringBuilder, ConsoleMapRenderProperties.Default);
                stringBuilder.Resize(new(50, 50));

                mapBuilder.BuildRoomMap(room, ViewPoint.Create(region), new RoomMapRenderOptions { KeyType = KeyType.Dynamic, PointOfInterestDetail = PointOfInterestDetail.High }, new Point2D(0, 0), out _, out _);
            });
        }

        [TestMethod]
        public void GivenKeyTypeNone_WhenBuildRoomMap_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var region = CreateFullyFeaturedRegion(out var room);
                var stringBuilder = new GridStringBuilder();
                var mapBuilder = new ConsoleHighDetailRoomMapBuilder(stringBuilder, ConsoleMapRenderProperties.Default);
                stringBuilder.Resize(new(50, 50));

                mapBuilder.BuildRoomMap(room, ViewPoint.Create(region), new RoomMapRenderOptions { KeyType = KeyType.None }, new Point2D(0, 0), out _, out _);
            });
        }

        [TestMethod]
        public void GivenDisplayDirectionsDisabled_WhenBuildRoomMap_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var region = CreateFullyFeaturedRegion(out var room);
                var stringBuilder = new GridStringBuilder();
                var properties = ConsoleMapRenderProperties.Default;
                properties.DisplayDirections = false;
                var mapBuilder = new ConsoleHighDetailRoomMapBuilder(stringBuilder, properties);
                stringBuilder.Resize(new(50, 50));

                mapBuilder.BuildRoomMap(room, ViewPoint.Create(region), new RoomMapRenderOptions { KeyType = KeyType.Full }, new Point2D(0, 0), out _, out _);
            });
        }

        [TestMethod]
        public void GivenNoStartPosition_WhenBuildRoomMap_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var region = CreateFullyFeaturedRegion(out var room);
                var stringBuilder = new GridStringBuilder();
                var mapBuilder = new ConsoleHighDetailRoomMapBuilder(stringBuilder, ConsoleMapRenderProperties.Default);
                stringBuilder.Resize(new(50, 50));

                mapBuilder.BuildRoomMap(room, ViewPoint.Create(region), new RoomMapRenderOptions { KeyType = KeyType.Full });
            });
        }

        [TestMethod]
        public void GivenKeyTypeNoneAndPlacementRight_WhenMeasure_ThenNonZeroSizeReturned()
        {
            var region = CreateFullyFeaturedRegion(out var room);
            var stringBuilder = new GridStringBuilder();
            var mapBuilder = new ConsoleHighDetailRoomMapBuilder(stringBuilder, ConsoleMapRenderProperties.Default);

            var size = mapBuilder.Measure(room, ViewPoint.Create(region), new RoomMapRenderOptions { KeyType = KeyType.None, KeyPlacement = KeyPlacement.Right });

            Assert.IsTrue(size.Width > 0);
            Assert.IsTrue(size.Height > 0);
        }

        [TestMethod]
        public void GivenKeyTypeDynamicAndPlacementRight_WhenMeasure_ThenNonZeroSizeReturned()
        {
            var region = CreateFullyFeaturedRegion(out var room);
            var stringBuilder = new GridStringBuilder();
            var mapBuilder = new ConsoleHighDetailRoomMapBuilder(stringBuilder, ConsoleMapRenderProperties.Default);

            var size = mapBuilder.Measure(room, ViewPoint.Create(region), new RoomMapRenderOptions { KeyType = KeyType.Dynamic, KeyPlacement = KeyPlacement.Right });

            Assert.IsTrue(size.Width > 0);
            Assert.IsTrue(size.Height > 0);
        }

        [TestMethod]
        public void GivenKeyTypeFullAndPlacementLeft_WhenMeasure_ThenNonZeroSizeReturned()
        {
            var region = CreateFullyFeaturedRegion(out var room);
            var stringBuilder = new GridStringBuilder();
            var mapBuilder = new ConsoleHighDetailRoomMapBuilder(stringBuilder, ConsoleMapRenderProperties.Default);

            var size = mapBuilder.Measure(room, ViewPoint.Create(region), new RoomMapRenderOptions { KeyType = KeyType.Full, KeyPlacement = KeyPlacement.Left });

            Assert.IsTrue(size.Width > 0);
            Assert.IsTrue(size.Height > 0);
        }

        [TestMethod]
        public void GivenKeyTypeFullAndPlacementAbove_WhenMeasure_ThenNonZeroSizeReturned()
        {
            var region = CreateFullyFeaturedRegion(out var room);
            var stringBuilder = new GridStringBuilder();
            var mapBuilder = new ConsoleHighDetailRoomMapBuilder(stringBuilder, ConsoleMapRenderProperties.Default);

            var size = mapBuilder.Measure(room, ViewPoint.Create(region), new RoomMapRenderOptions { KeyType = KeyType.Full, KeyPlacement = KeyPlacement.Above });

            Assert.IsTrue(size.Width > 0);
            Assert.IsTrue(size.Height > 0);
        }

        [TestMethod]
        public void GivenKeyTypeFullAndPlacementBelow_WhenMeasure_ThenNonZeroSizeReturned()
        {
            var region = CreateFullyFeaturedRegion(out var room);
            var stringBuilder = new GridStringBuilder();
            var mapBuilder = new ConsoleHighDetailRoomMapBuilder(stringBuilder, ConsoleMapRenderProperties.Default);

            var size = mapBuilder.Measure(room, ViewPoint.Create(region), new RoomMapRenderOptions { KeyType = KeyType.Full, KeyPlacement = KeyPlacement.Below });

            Assert.IsTrue(size.Width > 0);
            Assert.IsTrue(size.Height > 0);
        }
    }
}
