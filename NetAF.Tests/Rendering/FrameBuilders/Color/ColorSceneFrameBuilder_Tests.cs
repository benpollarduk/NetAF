﻿using System;
using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Interpretation;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Rendering.FrameBuilders.Color;
using NetAF.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetAF.Tests.Rendering.FrameBuilders.Color
{
    [TestClass]
    public class ColorSceneFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenFullyFeaturedScene_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var room = new Room("Test", "Test", new Exit(Direction.Up), new Exit(Direction.Down), new Exit(Direction.North), new Exit(Direction.East), new Exit(Direction.South), new Exit(Direction.West));
                room.AddItem(new Item("Test", "Test"));

                var regionMaker = new RegionMaker(string.Empty, string.Empty)
                {
                    [1, 2, 0] = new Room(string.Empty, string.Empty, new Exit(Direction.South)),
                    [0, 1, 0] = new Room(string.Empty, string.Empty, new Exit(Direction.East)),
                    [2, 1, 0] = new Room(string.Empty, string.Empty, new Exit(Direction.West)),
                    [1, 0, 0] = new Room(string.Empty, string.Empty, new Exit(Direction.North)),
                    [1, 1, 1] = new Room(string.Empty, string.Empty, new Exit(Direction.Down)),
                    [1, 1, -1] = new Room(string.Empty, string.Empty, new Exit(Direction.Up)),
                    [1, 1, 0] = room
                };

                var region = regionMaker.Make(1, 1, 0);
                var gridStringBuilder = new GridStringBuilder();
                var builder = new ColorSceneFrameBuilder(gridStringBuilder, new ColorRoomMapBuilder());
                var player = new PlayableCharacter(string.Empty, string.Empty);
                player.AcquireItem(new Item("Test", "Test"));
                player.Attributes.Add("Test", 10);

                builder.Build(room, ViewPoint.Create(region), player, string.Empty, Array.Empty<CommandHelp>(), KeyType.Full, 80, 50);
            });
        }

        [TestMethod]
        public void GivenFullyFeaturedSceneWithMessage_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var room = new Room("Test", "Test", new Exit(Direction.Up), new Exit(Direction.Down), new Exit(Direction.North), new Exit(Direction.East), new Exit(Direction.South), new Exit(Direction.West));
                room.AddItem(new Item("Test", "Test"));

                var regionMaker = new RegionMaker(string.Empty, string.Empty)
                {
                    [1, 2, 0] = new Room(string.Empty, string.Empty, new Exit(Direction.South)),
                    [0, 1, 0] = new Room(string.Empty, string.Empty, new Exit(Direction.East)),
                    [2, 1, 0] = new Room(string.Empty, string.Empty, new Exit(Direction.West)),
                    [1, 0, 0] = new Room(string.Empty, string.Empty, new Exit(Direction.North)),
                    [1, 1, 1] = new Room(string.Empty, string.Empty, new Exit(Direction.Down)),
                    [1, 1, -1] = new Room(string.Empty, string.Empty, new Exit(Direction.Up)),
                    [1, 1, 0] = room
                };

                var region = regionMaker.Make(1, 1, 0);
                var gridStringBuilder = new GridStringBuilder();
                var builder = new ColorSceneFrameBuilder(gridStringBuilder, new ColorRoomMapBuilder());
                var player = new PlayableCharacter(string.Empty, string.Empty);
                player.AcquireItem(new Item("Test", "Test"));
                player.Attributes.Add("Test", 10);

                builder.Build(room, ViewPoint.Create(region), player, "Test", Array.Empty<CommandHelp>(), KeyType.Full, 80, 50);
            });
        }

        [TestMethod]
        public void GivenFullyFeaturedSceneWithDynamicKey_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var room = new Room("Test", "Test", new Exit(Direction.Up), new Exit(Direction.Down), new Exit(Direction.North), new Exit(Direction.East), new Exit(Direction.South), new Exit(Direction.West));
                room.AddItem(new Item("Test", "Test"));

                var regionMaker = new RegionMaker(string.Empty, string.Empty)
                {
                    [1, 2, 0] = new Room(string.Empty, string.Empty, new Exit(Direction.South)),
                    [0, 1, 0] = new Room(string.Empty, string.Empty, new Exit(Direction.East)),
                    [2, 1, 0] = new Room(string.Empty, string.Empty, new Exit(Direction.West)),
                    [1, 0, 0] = new Room(string.Empty, string.Empty, new Exit(Direction.North)),
                    [1, 1, 1] = new Room(string.Empty, string.Empty, new Exit(Direction.Down)),
                    [1, 1, -1] = new Room(string.Empty, string.Empty, new Exit(Direction.Up)),
                    [1, 1, 0] = room
                };

                var region = regionMaker.Make(1, 1, 0);
                var gridStringBuilder = new GridStringBuilder();
                var builder = new ColorSceneFrameBuilder(gridStringBuilder, new ColorRoomMapBuilder());
                var player = new PlayableCharacter(string.Empty, string.Empty);
                player.AcquireItem(new Item("Test", "Test"));
                player.Attributes.Add("Test", 10);

                builder.Build(room, ViewPoint.Create(region), player, string.Empty, Array.Empty<CommandHelp>(), KeyType.Dynamic, 80, 50);
            });
        }

        [TestMethod]
        public void GivenFullyFeaturedSceneWithCommands_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var room = new Room("Test", "Test", new Exit(Direction.Up), new Exit(Direction.Down), new Exit(Direction.North), new Exit(Direction.East), new Exit(Direction.South), new Exit(Direction.West));
                room.AddItem(new Item("Test", "Test"));

                var regionMaker = new RegionMaker(string.Empty, string.Empty)
                {
                    [1, 2, 0] = new Room(string.Empty, string.Empty, new Exit(Direction.South)),
                    [0, 1, 0] = new Room(string.Empty, string.Empty, new Exit(Direction.East)),
                    [2, 1, 0] = new Room(string.Empty, string.Empty, new Exit(Direction.West)),
                    [1, 0, 0] = new Room(string.Empty, string.Empty, new Exit(Direction.North)),
                    [1, 1, 1] = new Room(string.Empty, string.Empty, new Exit(Direction.Down)),
                    [1, 1, -1] = new Room(string.Empty, string.Empty, new Exit(Direction.Up)),
                    [1, 1, 0] = room
                };

                var region = regionMaker.Make(1, 1, 0);
                var gridStringBuilder = new GridStringBuilder();
                var builder = new ColorSceneFrameBuilder(gridStringBuilder, new ColorRoomMapBuilder());
                var player = new PlayableCharacter(string.Empty, string.Empty);
                player.AcquireItem(new Item("Test", "Test"));
                player.Attributes.Add("Test", 10);
                var commands = new[]
                {
                    new CommandHelp("Test", "Test"),
                    new CommandHelp("Test", "Test")
                };

                builder.Build(room, ViewPoint.Create(region), player, string.Empty, commands, KeyType.Full, 80, 50);
            });
        }
    }
}
