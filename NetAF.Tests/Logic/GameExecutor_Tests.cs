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
        public void GivenSimpleGameAndCompletionConditionReached_WhenExecute_ThenNoExceptionThrown()
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
        public void GivenSimpleGameAndGameOverConditionReached_WhenExecute_ThenNoExceptionThrown()
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
        public void GivenSimpleGame_WhenExecute_ThenNoExceptionThrown()
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
                var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), new GameEndConditions(callback, GameEndConditions.NotEnded), new GameConfiguration(new TestConsoleAdapter(), FrameBuilderCollections.Console, new(80, 50), FinishModes.Finish));

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
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), new GameEndConditions(callback, GameEndConditions.NotEnded), new GameConfiguration(new TestConsoleAdapter(), FrameBuilderCollections.Console, new(80, 50), FinishModes.Finish));

            GameExecutor.Execute(game);
            var result = GameExecutor.Update();

            Assert.IsTrue(result.Completed);
        }

        [TestMethod]
        public void GivenGameWhenEndConditionMetAndFinishModeIsFinish_WhenUpdate_ThenCompletedTrue()
        {
            GameExecutor.CancelExecution();
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new("Room", string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            EndCheckResult callback(Game _) => new(true, string.Empty, string.Empty);
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), new GameEndConditions(callback, GameEndConditions.NotEnded), new GameConfiguration(new TestConsoleAdapter(), FrameBuilderCollections.Console, new(80, 50), FinishModes.Finish));

            GameExecutor.Execute(game);
            // update until finished
            GameExecutor.Update();
            GameExecutor.Update();

            var result = GameExecutor.Update();

            Assert.IsTrue(result.Completed);
        }

        [TestMethod]
        public void GivenGameWhenEndConditionMetAndFinishModeIsReturnToTitleScreen_WhenUpdate_ThenCompletedTrue()
        {
            GameExecutor.CancelExecution();
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new("Room", string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            EndCheckResult callback(Game _) => new(true, string.Empty, string.Empty);
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), new GameEndConditions(callback, GameEndConditions.NotEnded), new GameConfiguration(new TestConsoleAdapter(), FrameBuilderCollections.Console, new(80, 50), FinishModes.ReturnToTitleScreen));

            GameExecutor.Execute(game);
            // update until finished
            GameExecutor.Update();
            GameExecutor.Update();
            GameExecutor.Update();

            var result = GameExecutor.Update();

            Assert.IsTrue(result.Completed);
        }

        [TestMethod]
        public void GivenCanceled_WhenUpdate_ThenCompletedFalse()
        {
            GameExecutor.CancelExecution();
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new("Room", string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            EndCheckResult callback(Game _) => new(true, string.Empty, string.Empty);
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), new GameEndConditions(callback, GameEndConditions.NotEnded), new GameConfiguration(new TestConsoleAdapter(), FrameBuilderCollections.Console, new(80, 50), FinishModes.ReturnToTitleScreen));

            GameExecutor.Execute(game);
            GameExecutor.CancelExecution();
            var result = GameExecutor.Update();

            Assert.IsFalse(result.Completed);
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
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), new GameEndConditions(callback, GameEndConditions.NotEnded), new GameConfiguration(new TestConsoleAdapter(), FrameBuilderCollections.Console, new(80, 50), FinishModes.Finish));

            GameExecutor.Execute(game);

            Assert.IsTrue(GameExecutor.IsExecuting);
        }

        [TestMethod]
        public void GivenNoGameIsExecuting_WhenGetIsExecuting_ThenFalse()
        {
            GameExecutor.CancelExecution();

            Assert.IsFalse(GameExecutor.IsExecuting);
        }

        [TestMethod]
        public void GivenNoGameIsExecuting_WhenGetExecutingGame_ThenNull()
        {
            GameExecutor.CancelExecution();

            Assert.IsNull(GameExecutor.ExecutingGame);
        }

        [TestMethod]
        public void GivenAGameIsExecuting_WhenGetExecutingGame_ThenNotNull()
        {
            GameExecutor.CancelExecution();
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new("Room", string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var startTime = Environment.TickCount;
            EndCheckResult callback(Game _) => new(Environment.TickCount - startTime > 1000, string.Empty, string.Empty);
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), new GameEndConditions(callback, GameEndConditions.NotEnded), new GameConfiguration(new TestConsoleAdapter(), FrameBuilderCollections.Console, new(80, 50), FinishModes.Finish));

            GameExecutor.Execute(game);

            Assert.IsNotNull(GameExecutor.ExecutingGame);
        }

        [TestMethod]
        public void GivenNoGame_WhenCancel_ThenCompletedFalse()
        {
            GameExecutor.CancelExecution();
            var result = GameExecutor.Update();

            Assert.IsFalse(result.Completed);
        }

        [TestMethod]
        public void GivenExecutingGame_WhenCancelledThroughInput_ThenCompletedTrue()
        {
            GameExecutor.CancelExecution();
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new("Room", string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var startTime = Environment.TickCount;
            EndCheckResult callback(Game _) => new(Environment.TickCount - startTime > 1000, string.Empty, string.Empty);
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), new GameEndConditions(callback, GameEndConditions.NotEnded), new GameConfiguration(new TestConsoleAdapter(), FrameBuilderCollections.Console, new(80, 50), FinishModes.Finish));
            GameExecutor.Execute(game);
            // enter game, otherwise would be on title screen
            GameExecutor.Update();

            var result = GameExecutor.Update(NetAF.Commands.Execution.Exit.CommandHelp.Command);

            Assert.IsTrue(result.Completed);
        }
    }
}
