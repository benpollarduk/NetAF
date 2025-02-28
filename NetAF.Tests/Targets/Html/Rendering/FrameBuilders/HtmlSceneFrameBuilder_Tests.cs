using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Rendering;
using NetAF.Targets.Html.Rendering;
using NetAF.Targets.Html.Rendering.FrameBuilders;
using NetAF.Utilities;

namespace NetAF.Tests.Targets.Html.Rendering.FrameBuilders
{
    [TestClass]
    public class HtmlSceneFrameBuilder_Tests
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
                var htmlBuilder = new HtmlBuilder();
                var builder = new HtmlSceneFrameBuilder(htmlBuilder, new HtmlRoomMapBuilder(htmlBuilder));
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
                var htmlBuilder = new HtmlBuilder();
                var builder = new HtmlSceneFrameBuilder(htmlBuilder, new HtmlRoomMapBuilder(htmlBuilder));
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
                var htmlBuilder = new HtmlBuilder();
                var builder = new HtmlSceneFrameBuilder(htmlBuilder, new HtmlRoomMapBuilder(htmlBuilder));
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
                var htmlBuilder = new HtmlBuilder();
                var builder = new HtmlSceneFrameBuilder(htmlBuilder, new HtmlRoomMapBuilder(htmlBuilder));
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
    }
}
