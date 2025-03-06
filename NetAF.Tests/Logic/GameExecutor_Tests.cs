using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Logic.Configuration;
using NetAF.Logic;
using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Console;
using NetAF.Utilities;
using System;
using System.IO;

namespace NetAF.Tests.Logic
{
    [TestClass]
    public class GameExecutor_Tests
    {
        [TestMethod]
        public void GivenSimpleGameWithNoConsoleAccess_WhenExecuteManual_ThenIOExceptionThrown()
        {
            Assert.ThrowsException<IOException>(() =>
            {
                GameExecutor.Cancel();
                RegionMaker regionMaker = new(string.Empty, string.Empty);
                Room room = new("Room", string.Empty);
                regionMaker[0, 0, 0] = room;
                OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
                var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, new GameConfiguration(new ConsoleAdapter(), FrameBuilderCollections.Console, new(80, 50)));

                GameExecutor.Execute(game, GameExecutionMode.Manual);
            });
        }

        [TestMethod]
        public void GivenSimpleGameWithMockConsoleAccessAndCompletionConditionReached_WhenExecuteManual_ThenNoExceptionThrown()
        {
            Assertions.NoExceptionThrown(() =>
            {
                GameExecutor.Cancel();
                RegionMaker regionMaker = new(string.Empty, string.Empty);
                Room room = new("Room", string.Empty);
                regionMaker[0, 0, 0] = room;
                OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
                var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), new GameEndConditions(_ => new EndCheckResult(true, string.Empty, string.Empty), GameEndConditions.NotEnded), TestGameConfiguration.Default);

                GameExecutor.Execute(game, GameExecutionMode.Manual);
            });
        }

        [TestMethod]
        public void GivenSimpleGameWithMockConsoleAccessAndGameOverConditionReached_WhenExecuteManual_ThenNoExceptionThrown()
        {
            Assertions.NoExceptionThrown(() =>
            {
                GameExecutor.Cancel();
                RegionMaker regionMaker = new(string.Empty, string.Empty);
                Room room = new("Room", string.Empty);
                regionMaker[0, 0, 0] = room;
                OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
                var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), new GameEndConditions(GameEndConditions.NotEnded, _ => new EndCheckResult(true, string.Empty, string.Empty)), TestGameConfiguration.Default);

                GameExecutor.Execute(game, GameExecutionMode.Manual);
            });
        }

        [TestMethod]
        public void GivenSimpleGameWithMockConsoleAccess_WhenExecuteManual_ThenNoExceptionThrown()
        {
            Assertions.NoExceptionThrown(() =>
            {
                GameExecutor.Cancel();
                RegionMaker regionMaker = new(string.Empty, string.Empty);
                Room room = new("Room", string.Empty);
                regionMaker[0, 0, 0] = room;
                OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
                var startTime = Environment.TickCount;
                EndCheckResult callback(Game _) => new(Environment.TickCount - startTime > 1000, string.Empty, string.Empty);
                var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), new GameEndConditions(callback, GameEndConditions.NotEnded), new GameConfiguration(new TestConsoleAdapter(), FrameBuilderCollections.Console, new(80, 50), ExitMode.ExitApplication));

                GameExecutor.Execute(game, GameExecutionMode.Manual);
            });
        }

        [TestMethod]
        public void GivenSimpleGameWithMockConsoleAccess_WhenExecuteAutomatic_ThenNoExceptionThrown()
        {
            Assertions.NoExceptionThrown(() =>
            {
                GameExecutor.Cancel();
                RegionMaker regionMaker = new(string.Empty, string.Empty);
                Room room = new("Room", string.Empty);
                regionMaker[0, 0, 0] = room;
                OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
                var startTime = Environment.TickCount;
                EndCheckResult callback(Game _) => new(Environment.TickCount - startTime > 1000, string.Empty, string.Empty);
                var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), new GameEndConditions(callback, GameEndConditions.NotEnded), new GameConfiguration(new TestConsoleAdapter(), FrameBuilderCollections.Console, new(80, 50), ExitMode.ExitApplication));

                GameExecutor.Execute(game, GameExecutionMode.Automatic);
            });
        }

        [TestMethod]
        public void GivenSimpleGameWithMockConsoleAccess_WhenExecuteBackgroundAutomatic_ThenNoExceptionThrown()
        {
            Assertions.NoExceptionThrown(() =>
            {
                GameExecutor.Cancel();
                RegionMaker regionMaker = new(string.Empty, string.Empty);
                Room room = new("Room", string.Empty);
                regionMaker[0, 0, 0] = room;
                OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
                var startTime = Environment.TickCount;
                EndCheckResult callback(Game _) => new(Environment.TickCount - startTime > 1000, string.Empty, string.Empty);
                var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), new GameEndConditions(callback, GameEndConditions.NotEnded), new GameConfiguration(new TestConsoleAdapter(), FrameBuilderCollections.Console, new(80, 50), ExitMode.ExitApplication));

                GameExecutor.Execute(game, GameExecutionMode.BackgroundAutomatic);
            });
        }

        [TestMethod]
        public void GivenAGameAlreadyExecuting_WhenExecute_ThenGameExecutionExceptionThrown()
        {
            Assert.ThrowsException<GameExecutionException>(() =>
            {
                GameExecutor.Cancel();
                RegionMaker regionMaker = new(string.Empty, string.Empty);
                Room room = new("Room", string.Empty);
                regionMaker[0, 0, 0] = room;
                OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
                var startTime = Environment.TickCount;
                EndCheckResult callback(Game _) => new(Environment.TickCount - startTime > 1000, string.Empty, string.Empty);
                var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), new GameEndConditions(callback, GameEndConditions.NotEnded), new GameConfiguration(new TestConsoleAdapter(), FrameBuilderCollections.Console, new(80, 50), ExitMode.ExitApplication));

                GameExecutor.Execute(game, GameExecutionMode.Manual);
                GameExecutor.Execute(game, GameExecutionMode.Manual);
            });
        }
    }
}
