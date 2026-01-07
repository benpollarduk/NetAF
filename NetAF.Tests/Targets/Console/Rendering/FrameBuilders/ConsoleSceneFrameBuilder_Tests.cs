using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Rendering;
using NetAF.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Commands;
using NetAF.Assets;
using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Console.Rendering.FrameBuilders;
using System.Text;

namespace NetAF.Tests.Targets.Console.Rendering.FrameBuilders
{
    [TestClass]
    public class ConsoleSceneFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenFullyFeaturedScene_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var room = new Room("Test", "Test", [new Exit(Direction.Up), new Exit(Direction.Down), new Exit(Direction.North), new Exit(Direction.East), new Exit(Direction.South), new Exit(Direction.West)]);
                room.AddItem(new("Test", "Test"));

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
                var gridStringBuilder = new GridStringBuilder();
                var builder = new ConsoleSceneFrameBuilder(gridStringBuilder, new ConsoleHighDetailRoomMapBuilder(gridStringBuilder));
                var player = new PlayableCharacter(string.Empty, string.Empty);
                player.AddItem(new("Test", "Test"));
                player.Attributes.Add("Test", 10);

                builder.Build(room, ViewPoint.Create(region), player, [], KeyType.Full, new Size(80, 50));
            });
        }

        [TestMethod]
        public void GivenFullyFeaturedSceneWithMessage_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var room = new Room("Test", "Test", [new Exit(Direction.Up), new Exit(Direction.Down), new Exit(Direction.North), new Exit(Direction.East), new Exit(Direction.South), new Exit(Direction.West)]);
                room.AddItem(new("Test", "Test"));

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
                var gridStringBuilder = new GridStringBuilder();
                var builder = new ConsoleSceneFrameBuilder(gridStringBuilder, new ConsoleHighDetailRoomMapBuilder(gridStringBuilder));
                var player = new PlayableCharacter(string.Empty, string.Empty);
                player.AddItem(new("Test", "Test"));
                player.Attributes.Add("Test", 10);

                builder.Build(room, ViewPoint.Create(region), player, [], KeyType.Full, new Size(80, 50));
            });
        }

        [TestMethod]
        public void GivenFullyFeaturedSceneWithDynamicKey_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var room = new Room("Test", "Test", [new Exit(Direction.Up), new Exit(Direction.Down), new Exit(Direction.North), new Exit(Direction.East), new Exit(Direction.South), new Exit(Direction.West)]);
                room.AddItem(new("Test", "Test"));

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
                var gridStringBuilder = new GridStringBuilder();
                var builder = new ConsoleSceneFrameBuilder(gridStringBuilder, new ConsoleHighDetailRoomMapBuilder(gridStringBuilder));
                var player = new PlayableCharacter(string.Empty, string.Empty);
                player.AddItem(new("Test", "Test"));
                player.Attributes.Add("Test", 10);

                builder.Build(room, ViewPoint.Create(region), player, [], KeyType.Dynamic, new Size(80, 50));
            });
        }

        [TestMethod]
        public void GivenFullyFeaturedSceneWithCommands_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var room = new Room("Test", "Test", [new Exit(Direction.Up), new Exit(Direction.Down), new Exit(Direction.North), new Exit(Direction.East), new Exit(Direction.South), new Exit(Direction.West)]);
                room.AddItem(new("Test", "Test"));

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
                var gridStringBuilder = new GridStringBuilder();
                var builder = new ConsoleSceneFrameBuilder(gridStringBuilder, new ConsoleHighDetailRoomMapBuilder(gridStringBuilder));
                var player = new PlayableCharacter(string.Empty, string.Empty);
                player.AddItem(new("Test", "Test"));
                player.Attributes.Add("Test", 10);
                var commands = new[]
                {
                    new CommandHelp("Test", "Test"),
                    new CommandHelp("Test", "Test")
                };

                builder.Build(room, ViewPoint.Create(region), player, commands, KeyType.Full, new Size(80, 50));
            });
        }

        [TestMethod]
        public void GivenDescriptionOfRoomThatWillNotFit_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                Size size = new(10, 10);
                StringBuilder description = new();

                for (var i = 0; i < size.Width * size.Height; i++)
                    description.Append('A');

                var room = new Room("Test", description.ToString(), [new Exit(Direction.Up), new Exit(Direction.Down), new Exit(Direction.North), new Exit(Direction.East), new Exit(Direction.South), new Exit(Direction.West)]);

                var regionMaker = new RegionMaker(string.Empty, string.Empty)
                {
                    [0, 0, 0] = new(string.Empty, string.Empty),
                };

                var region = regionMaker.Make(1, 1, 0);
                region.Enter();
                var gridStringBuilder = new GridStringBuilder();
                var builder = new ConsoleSceneFrameBuilder(gridStringBuilder, new ConsoleHighDetailRoomMapBuilder(gridStringBuilder));
                var player = new PlayableCharacter(string.Empty, string.Empty);

                builder.Build(room, ViewPoint.Create(region), player, null, KeyType.Full, size);
            });
        }
    }
}
