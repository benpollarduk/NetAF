using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Interpretation;
using NetAF.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Commands.Scene;

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
            var interpreter = new SceneCommandInterpreter();
            var game= Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(string.Empty, game);

            Assert.IsFalse(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenEast_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new SceneCommandInterpreter();
            var game= Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(Move.EastCommandHelp.Command, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenEastShort_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new SceneCommandInterpreter();
            var game= Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(Move.EastCommandHelp.Shortcut, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenNorth_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new SceneCommandInterpreter();
            var game= Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(Move.NorthCommandHelp.Command, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenNorthShort_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new SceneCommandInterpreter();
            var game= Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(Move.NorthCommandHelp.Shortcut, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenSouth_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new SceneCommandInterpreter();
            var game= Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(Move.SouthCommandHelp.Command, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenSouthShort_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new SceneCommandInterpreter();
            var game= Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(Move.SouthCommandHelp.Shortcut, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenWest_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new SceneCommandInterpreter();
            var game= Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(Move.WestCommandHelp.Command, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenWestShort_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new SceneCommandInterpreter();
            var game= Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(Move.WestCommandHelp.Shortcut, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenExamine_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new SceneCommandInterpreter();
            var game= Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(Examine.CommandHelp.Command, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenExamineRoom_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new SceneCommandInterpreter();
            var game= Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(Examine.CommandHelp.Command + " " + SceneCommandInterpreter.Room, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenExamineRegion_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new SceneCommandInterpreter();
            var game= Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(Examine.CommandHelp.Command + " " + SceneCommandInterpreter.Region, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenExamineOverworld_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new SceneCommandInterpreter();
            var game= Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(Examine.CommandHelp.Command + " " + SceneCommandInterpreter.Overworld, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenExamineMe_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new SceneCommandInterpreter();
            var game= Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(Examine.CommandHelp.Command + " " + SceneCommandInterpreter.Me, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenTake_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new SceneCommandInterpreter();
            var game= Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();
            overworld.CurrentRegion.CurrentRoom.AddItem(new(Identifier.Empty, Description.Empty, true));

            var result = interpreter.Interpret(Take.CommandHelp.Command, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenTakeNonTakeable_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new SceneCommandInterpreter();
            var game= Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();
            overworld.CurrentRegion.CurrentRoom.AddItem(new(Identifier.Empty, Description.Empty));

            var result = interpreter.Interpret(Take.CommandHelp.Command, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenDrop_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new SceneCommandInterpreter();
            var game= Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(Drop.CommandHelp.Command, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenTalk_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new SceneCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(Talk.TalkCommandHelp.Command, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenUse_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new SceneCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(UseOn.UseCommandHelp.Command, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenUseOn_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new SceneCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret($"{UseOn.UseCommandHelp.Command} test {UseOn.OnCommandHelp.Command} test", game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }
    }
}
