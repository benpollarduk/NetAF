using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Logic;
using NetAF.Rendering.FrameBuilders;
using NetAF.Utilities;
using System;

namespace NetAF.Tests.Logic
{
    [TestClass]
    public class GameExecutor_Tests
    {
        [TestMethod]
        public void GivenSimpleGameWithMockConsoleAccessAndCompletionConditionReached_WhenExecute_ThenNoExceptionThrown()
        {
            Assertions.NoExceptionThrown(() =>
            {
                GameExecutor.CancelExecution();
                RegionMaker regionMaker = new(string.Empty, string.Empty);
                Room room = new("Room", string.Empty);
                regionMaker[0, 0, 0] = room;
                OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
                var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), new GameEndConditions(_ => new EndCheckResult(true, string.Empty, string.Empty), GameEndConditions.NotEnded), TestGameConfiguration.Default);

                GameExecutor.Execute(game);
            });
        }

        [TestMethod]
        public void GivenSimpleGameWithMockConsoleAccessAndGameOverConditionReached_WhenExecute_ThenNoExceptionThrown()
        {
            Assertions.NoExceptionThrown(() =>
            {
                GameExecutor.CancelExecution();
                RegionMaker regionMaker = new(string.Empty, string.Empty);
                Room room = new("Room", string.Empty);
                regionMaker[0, 0, 0] = room;
                OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
                var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), new GameEndConditions(GameEndConditions.NotEnded, _ => new EndCheckResult(true, string.Empty, string.Empty)), TestGameConfiguration.Default);

                GameExecutor.Execute(game);
            });
        }

        [TestMethod]
        public void GivenSimpleGameWithMockConsoleAccess_WhenExecute_ThenNoExceptionThrown()
        {
            Assertions.NoExceptionThrown(() =>
            {
                GameExecutor.CancelExecution();
                RegionMaker regionMaker = new(string.Empty, string.Empty);
                Room room = new("Room", string.Empty);
                regionMaker[0, 0, 0] = room;
                OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
                var startTime = Environment.TickCount;
                EndCheckResult callback(Game _) => new(Environment.TickCount - startTime > 1000, string.Empty, string.Empty);
                var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), new GameEndConditions(callback, GameEndConditions.NotEnded), new GameConfiguration(new TestConsoleAdapter(), FrameBuilderCollections.Console, new(80, 50), ExitMode.ExitApplication));

                GameExecutor.Execute(game);
            });
        }

        [TestMethod]
        public void GivenAGameAlreadyExecuting_WhenExecute_ThenGameExecutionExceptionThrown()
        {
            Assert.ThrowsException<GameExecutionException>(() =>
            {
                GameExecutor.CancelExecution();
                RegionMaker regionMaker = new(string.Empty, string.Empty);
                Room room = new("Room", string.Empty);
                regionMaker[0, 0, 0] = room;
                OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
                var startTime = Environment.TickCount;
                EndCheckResult callback(Game _) => new(Environment.TickCount - startTime > 1000, string.Empty, string.Empty);
                var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), new GameEndConditions(callback, GameEndConditions.NotEnded), new GameConfiguration(new TestConsoleAdapter(), FrameBuilderCollections.Console, new(80, 50), ExitMode.ExitApplication));

                GameExecutor.Execute(game);
                GameExecutor.Execute(game);
            });
        }

        [TestMethod]
        public void GivenGame_WhenUpdate_ThenCompletedTrue()
        {
            GameExecutor.CancelExecution();
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new("Room", string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var startTime = Environment.TickCount;
            EndCheckResult callback(Game _) => new(Environment.TickCount - startTime > 1000, string.Empty, string.Empty);
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), new GameEndConditions(callback, GameEndConditions.NotEnded), new GameConfiguration(new TestConsoleAdapter(), FrameBuilderCollections.Console, new(80, 50), ExitMode.ExitApplication));

            GameExecutor.Execute(game);
            var result = GameExecutor.Update();

            Assert.IsTrue(result.Completed);
        }

        [TestMethod]
        public void GivenAGameIsExecuting_WhenGetIsExecuting_ThenTrue()
        {
            GameExecutor.CancelExecution();
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new("Room", string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var startTime = Environment.TickCount;
            EndCheckResult callback(Game _) => new(Environment.TickCount - startTime > 1000, string.Empty, string.Empty);
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), new GameEndConditions(callback, GameEndConditions.NotEnded), new GameConfiguration(new TestConsoleAdapter(), FrameBuilderCollections.Console, new(80, 50), ExitMode.ExitApplication));

            GameExecutor.Execute(game);

            Assert.IsTrue(GameExecutor.IsExecuting);
        }

        [TestMethod]
        public void GivenNoGameIsExecuting_WhenGetIsExecuting_ThenFalse()
        {
            GameExecutor.CancelExecution();

            Assert.IsFalse(GameExecutor.IsExecuting);
        }
    }
}
