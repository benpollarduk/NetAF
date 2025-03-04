﻿using System;
using System.IO;
using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Logic;
using NetAF.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Logic.Modes;
using NetAF.Commands;
using NetAF.Commands.Scene;
using NetAF.Targets.Console;
using NetAF.Logic.Configuration;
using NetAF.Rendering.FrameBuilders;

namespace NetAF.Tests.Logic
{
    [TestClass]
    public class Game_Tests
    {
        [TestMethod]
        public void GivenEmptyRoom_WhenGetContextualCommands_ThenNotNullOrEmpty()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();

            var result = game.GetContextualCommands();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public void GivenEmptyRoom_WhenGetAllPlayerVisibleExaminables_ThenNotNull()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();

            var result = game.GetAllPlayerVisibleExaminables();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GivenEmptyRoom_WhenGetAllPlayerVisibleExaminables_Then4Examinables()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();

            var result = game.GetAllPlayerVisibleExaminables();

            Assert.AreEqual(4, result.Length);
        }

        [TestMethod]
        public void GivenRoomWithOnePlayerVisibleItem_WhenGetAllPlayerVisibleExaminables_Then5Examinables()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            var item = new Item(string.Empty, string.Empty) { IsPlayerVisible = true };
            Room room = new(string.Empty, string.Empty, null, [item]);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();

            var result = game.GetAllPlayerVisibleExaminables();

            Assert.AreEqual(5, result.Length);
        }

        [TestMethod]
        public void GivenRoomWithOnePlayerInvisibleItem_WhenGetAllPlayerVisibleExaminables_Then4Examinables()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            var item = new Item(string.Empty, string.Empty) { IsPlayerVisible = false };
            Room room = new(string.Empty, string.Empty, null, [item]);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();

            var result = game.GetAllPlayerVisibleExaminables();

            Assert.AreEqual(4, result.Length);
        }

        [TestMethod]
        public void GivenRoomWithOnePlayerVisibleExit_WhenGetAllPlayerVisibleExaminables_Then5Examinables()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            var exit = new Exit(Direction.Down) { IsPlayerVisible = true };
            Room room = new(string.Empty, string.Empty, [exit]);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();

            var result = game.GetAllPlayerVisibleExaminables();

            Assert.AreEqual(5, result.Length);
        }

        [TestMethod]
        public void GivenRoomWithOnePlayerInvisibleExit_WhenGetAllPlayerVisibleExaminables_Then4Examinables()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            var exit = new Exit(Direction.Down) { IsPlayerVisible = false };
            Room room = new(string.Empty, string.Empty, [exit]);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();

            var result = game.GetAllPlayerVisibleExaminables();

            Assert.AreEqual(4, result.Length);
        }

        [TestMethod]
        public void GivenRoomWithOnePlayerVisibleNPC_WhenGetAllPlayerVisibleExaminables_Then5Examinables()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            var npc = new NonPlayableCharacter(string.Empty, string.Empty) { IsPlayerVisible = true };
            Room room = new(string.Empty, string.Empty);
            room.AddCharacter(npc);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();

            var result = game.GetAllPlayerVisibleExaminables();

            Assert.AreEqual(5, result.Length);
        }

        [TestMethod]
        public void GivenRoomWithOnePlayerInvisibleNPC_WhenGetAllPlayerVisibleExaminables_Then4Examinables()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            var npc = new NonPlayableCharacter(string.Empty, string.Empty) { IsPlayerVisible = false };
            Room room = new(string.Empty, string.Empty);
            room.AddCharacter(npc);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();
            var result = game.GetAllPlayerVisibleExaminables();

            Assert.AreEqual(4, result.Length);
        }

        [TestMethod]
        public void GivenSimpleGame_WhenChangeModeToTransition_ThenNoExceptionThrown()
        {
            Assertions.NoExceptionThrown(() =>
            {
                RegionMaker regionMaker = new(string.Empty, string.Empty);
                Room room = new(string.Empty, string.Empty);
                regionMaker[0, 0, 0] = room;
                OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
                var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

                game.ChangeMode(new ReactionMode("Test", Reaction.Inform));
            });
        }

        [TestMethod]
        public void GivenSimpleGame_WhenChangeModeToAbout_ThenNoExceptionThrown()
        {
            Assertions.NoExceptionThrown(() =>
            {
                RegionMaker regionMaker = new(string.Empty, string.Empty);
                Room room = new(string.Empty, string.Empty);
                regionMaker[0, 0, 0] = room;
                OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
                var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

                game.ChangeMode(new AboutMode());
            });
        }

        [TestMethod]
        public void GivenSimpleGame_WhenChangeModeToCommandList_ThenNoExceptionThrown()
        {
            Assertions.NoExceptionThrown(() =>
            {
                RegionMaker regionMaker = new(string.Empty, string.Empty);
                Room room = new(string.Empty, string.Empty);
                regionMaker[0, 0, 0] = room;
                OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
                var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

                game.ChangeMode(new CommandListMode([]));
            });
        }

        [TestMethod]
        public void GivenSimpleGame_WhenChangeModeToHelp_ThenNoExceptionThrown()
        {
            Assertions.NoExceptionThrown(() =>
            {
                RegionMaker regionMaker = new(string.Empty, string.Empty);
                Room room = new(string.Empty, string.Empty);
                regionMaker[0, 0, 0] = room;
                OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
                var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

                game.ChangeMode(new HelpMode(Take.CommandHelp));
            });
        }

        [TestMethod]
        public void GivenSimpleGame_WhenChangeModeToRegionMap_ThenNoExceptionThrown()
        {
            Assertions.NoExceptionThrown(() =>
            {
                RegionMaker regionMaker = new(string.Empty, string.Empty);
                Room room = new(string.Empty, string.Empty);
                regionMaker[0, 0, 0] = room;
                OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
                var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

                game.ChangeMode(new RegionMapMode(RegionMapMode.Player));
            });
        }

        [TestMethod]
        public void GivenPlayer_WhenFindInteractionTarget_ThenReturnPlayer()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter("Player", string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();

            var result = game.FindInteractionTarget("Player");

            Assert.AreEqual(game.Player, result);
        }

        [TestMethod]
        public void GivenRoom_WhenFindInteractionTarget_ThenReturnRoom()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new("Room", string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();

            var result = game.FindInteractionTarget("Room");

            Assert.AreEqual(game.Overworld.CurrentRegion.CurrentRoom, result);
        }

        [TestMethod]
        public void GivenPlayerItem_WhenFindInteractionTarget_ThenReturnPlayerItem()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new("Room", string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty, [new Item("Item", string.Empty)])), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();

            var result = game.FindInteractionTarget("Item");

            Assert.AreEqual(game.Player.Items[0], result);
        }

        [TestMethod]
        public void GivenRoomItem_WhenFindInteractionTarget_ThenReturnRoomItem()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new("Room", string.Empty);
            room.AddItem(new Item("Item", string.Empty));
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();

            var result = game.FindInteractionTarget("Item");

            Assert.AreEqual(game.Overworld.CurrentRegion.CurrentRoom.Items[0], result);
        }

        [TestMethod]
        public void GivenUnknown_WhenFindInteractionTarget_ThenReturnNull()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new("Room", string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, new GameConfiguration(new ConsoleAdapter(), FrameBuilderCollections.Console, new(80, 50))).Invoke();
            game.Overworld.CurrentRegion.Enter();

            var result = game.FindInteractionTarget("ABC");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GivenSimpleGameWithNoConsoleAccess_WhenExecute_ThenIOExceptionThrown()
        {
            Assert.ThrowsException<IOException>(() =>
            {
                RegionMaker regionMaker = new(string.Empty, string.Empty);
                Room room = new("Room", string.Empty);
                regionMaker[0, 0, 0] = room;
                OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
                var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, new GameConfiguration(new ConsoleAdapter(), FrameBuilderCollections.Console, new(80, 50)));

                Game.Execute(game);
            });
        }

        [TestMethod]
        public void GivenSimpleGameWithMockConsoleAccessAndCompletionConditionReached_WhenExecute_ThenNoExceptionThrown()
        {
            Assertions.NoExceptionThrown(() =>
            {
                RegionMaker regionMaker = new(string.Empty, string.Empty);
                Room room = new("Room", string.Empty);
                regionMaker[0, 0, 0] = room;
                OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
                var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), new GameEndConditions(_ => new EndCheckResult(true, string.Empty, string.Empty), GameEndConditions.NotEnded), TestGameConfiguration.Default).Invoke();

                game.Execute();
            });
        }

        [TestMethod]
        public void GivenSimpleGameWithMockConsoleAccessAndGameOverConditionReached_WhenExecute_ThenNoExceptionThrown()
        {
            Assertions.NoExceptionThrown(() =>
            {
                RegionMaker regionMaker = new(string.Empty, string.Empty);
                Room room = new("Room", string.Empty);
                regionMaker[0, 0, 0] = room;
                OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
                var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), new GameEndConditions(GameEndConditions.NotEnded, _ => new EndCheckResult(true, string.Empty, string.Empty)), TestGameConfiguration.Default).Invoke();

                game.Execute();
            });
        }

        [TestMethod]
        public void GivenSimpleGameWithMockConsoleAccess_WhenExecute_ThenNoExceptionThrown()
        {
            Assertions.NoExceptionThrown(() =>
            {
                RegionMaker regionMaker = new(string.Empty, string.Empty);
                Room room = new("Room", string.Empty);
                regionMaker[0, 0, 0] = room;
                OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
                var startTime = Environment.TickCount;
                EndCheckResult callback(Game _) => new(Environment.TickCount - startTime > 1000, string.Empty, string.Empty);
                var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), new GameEndConditions(callback, GameEndConditions.NotEnded), new GameConfiguration(new TestConsoleAdapter(), FrameBuilderCollections.Console, new(80, 50), ExitMode.ExitApplication)).Invoke();

                game.Execute();
            });
        }

        [TestMethod]
        public void GivenSimpleGame_WhenChangePlayer_ThenPlayerIsNewPlayer()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new("Room", string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();
            PlayableCharacter player2 = new(string.Empty, string.Empty);

            game.ChangePlayer(player2);

            Assert.AreEqual(player2, game.Player);
        }

        [TestMethod]
        public void GivenSimpleGame_WhenSetupProvided_ThenEnsureSetupInvoked()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new("Room", string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var result = false;
            void setup(Game _) { result = true; }

            Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default, setup).Invoke();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenSimpleGame_WhenChangePlayer_ThenPlayerChanged()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new("Room", string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter("A", string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();

            game.ChangePlayer(new("B", string.Empty));

            Assert.AreEqual("B", game.Player.Identifier.IdentifiableName);
        }

        [TestMethod]
        public void GivenSimpleGameAndChangePlayer_WhenGetInactivePlayerLocations_Then1PlayerLocation()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new("Room", string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter("A", string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();

            game.ChangePlayer(new("B", string.Empty));
            var result = game.GetInactivePlayerLocations();

            Assert.AreEqual(1, result.Length);
        }

        [TestMethod]
        public void GivenSimpleGameAndChangePlayerToADifferentRoom_WhenChangePlayer_ThenReturnToPreviousLocation()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = new Room("Room A", string.Empty, [new Exit(Direction.North)]);
            regionMaker[0, 1, 0] = new Room("Room B", string.Empty, [new Exit(Direction.South)]);
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            PlayableCharacter player1 = new("A", string.Empty);
            PlayableCharacter player2 = new("B", string.Empty);
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), player1), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();
            game.Overworld.CurrentRegion.Move(Direction.North);
            game.ChangePlayer(player2);
            game.Overworld.CurrentRegion.Move(Direction.South);
            game.ChangePlayer(player1);

            Assert.AreEqual("Room B", game.Overworld.CurrentRegion.CurrentRoom.Identifier.Name);
        }

        [TestMethod]
        public void GivenSimpleGameAndChangePlayerToADifferentRoom_WhenChangePlayerWithoutReturningToPreviousLocation_ThenRetainLocation()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = new Room("Room A", string.Empty, [new Exit(Direction.North)]);
            regionMaker[0, 1, 0] = new Room("Room B", string.Empty, [new Exit(Direction.South)]);
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            PlayableCharacter player1 = new("A", string.Empty);
            PlayableCharacter player2 = new("B", string.Empty);
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), player1), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();
            game.Overworld.CurrentRegion.Move(Direction.North);
            game.ChangePlayer(player2);
            game.Overworld.CurrentRegion.Move(Direction.South);
            game.ChangePlayer(player1, false);

            Assert.AreEqual("Room A", game.Overworld.CurrentRegion.CurrentRoom.Identifier.Name);
        }

        [TestMethod]
        public void GivenSimpleGame_WhenChangePlayerToNull_ThenPlayerUnchanged()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = new Room("Room A", string.Empty, [new Exit(Direction.North)]);
            regionMaker[0, 1, 0] = new Room("Room B", string.Empty, [new Exit(Direction.South)]);
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            PlayableCharacter player1 = new("A", string.Empty);
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), player1), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

            game.ChangePlayer(null, false);

            Assert.IsNotNull(game.Player);
        }

        [TestMethod]
        public void GivenSimpleGame_WhenChangePlayerToSamePlayerThenGetInactivePlayerLocations_ThenNoEntries()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = new Room("Room A", string.Empty, [new Exit(Direction.North)]);
            regionMaker[0, 1, 0] = new Room("Room B", string.Empty, [new Exit(Direction.South)]);
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            PlayableCharacter player1 = new("A", string.Empty);
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), player1), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

            game.ChangePlayer(game.Player, false);
            var result = game.GetInactivePlayerLocations();

            Assert.AreEqual(0, result.Length);
        }
    }
}