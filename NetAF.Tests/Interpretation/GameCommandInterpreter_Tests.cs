using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Interpretation;
using NetAF.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetAF.Tests.Interpretation
{
    [TestClass]
    public class GameCommandInterpreter_Tests
    {
        [TestInitialize]
        public void Setup()
        {
            overworld = new Overworld(Identifier.Empty, Description.Empty);
            var region = new Region(Identifier.Empty, Description.Empty);
            region.AddRoom(new(Identifier.Empty, Description.Empty, [new Exit(Direction.North)]), 0, 0, 0);
            region.AddRoom(new(Identifier.Empty, Description.Empty, [new Exit(Direction.South)]), 0, 1, 0);
            overworld.AddRegion(region);
        }

        private Overworld overworld;

        [TestMethod]
        public void GivenEmptyString_WhenInterpret_ThenReturnFalse()
        {
            var interpreter = new GameCommandInterpreter();
            var game= Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(string.Empty, game);

            Assert.IsFalse(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenEast_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new GameCommandInterpreter();
            var game= Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(GameCommandInterpreter.East, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenEastShort_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new GameCommandInterpreter();
            var game= Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(GameCommandInterpreter.EastShort, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenNorth_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new GameCommandInterpreter();
            var game= Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(GameCommandInterpreter.North, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenNorthShort_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new GameCommandInterpreter();
            var game= Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(GameCommandInterpreter.NorthShort, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenSouth_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new GameCommandInterpreter();
            var game= Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(GameCommandInterpreter.South, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenSouthShort_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new GameCommandInterpreter();
            var game= Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(GameCommandInterpreter.SouthShort, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenWest_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new GameCommandInterpreter();
            var game= Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(GameCommandInterpreter.West, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenWestShort_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new GameCommandInterpreter();
            var game= Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(GameCommandInterpreter.WestShort, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenExamine_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new GameCommandInterpreter();
            var game= Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(GameCommandInterpreter.Examine, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenExamineRoom_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new GameCommandInterpreter();
            var game= Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(GameCommandInterpreter.Examine + " " + GameCommandInterpreter.Room, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenExamineRegion_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new GameCommandInterpreter();
            var game= Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(GameCommandInterpreter.Examine + " " + GameCommandInterpreter.Region, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenExamineOverworld_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new GameCommandInterpreter();
            var game= Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(GameCommandInterpreter.Examine + " " + GameCommandInterpreter.Overworld, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenExamineMe_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new GameCommandInterpreter();
            var game= Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(GameCommandInterpreter.Examine + " " + GameCommandInterpreter.Me, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenTake_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new GameCommandInterpreter();
            var game= Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();
            overworld.CurrentRegion.CurrentRoom.AddItem(new(Identifier.Empty, Description.Empty, true));

            var result = interpreter.Interpret(GameCommandInterpreter.Take, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenTakeNonTakeable_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new GameCommandInterpreter();
            var game= Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();
            overworld.CurrentRegion.CurrentRoom.AddItem(new(Identifier.Empty, Description.Empty));

            var result = interpreter.Interpret(GameCommandInterpreter.Take, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenDrop_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new GameCommandInterpreter();
            var game= Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(GameCommandInterpreter.Drop, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenTalk_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new GameCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(GameCommandInterpreter.Talk, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenUse_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new GameCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(GameCommandInterpreter.Use, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenUseOn_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new GameCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret($"{GameCommandInterpreter.Use} test {GameCommandInterpreter.On} test", game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }
    }
}
