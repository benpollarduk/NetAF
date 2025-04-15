using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Interpretation;
using NetAF.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Logic.Modes;
using NetAF.Commands.Information;

namespace NetAF.Tests.Interpretation
{
    [TestClass]
    public class GlobalCommandInterpreter_Tests
    {
        [TestInitialize]
        public void Setup()
        {
            overworld = new Overworld(Identifier.Empty, Description.Empty);
            var region = new Region(Identifier.Empty, Description.Empty);
            region.AddRoom(new(Identifier.Empty, Description.Empty, [new Exit(Direction.North)]), 0, 0, 0);
            region.AddRoom(new(Identifier.Empty, Description.Empty, [new Exit(Direction.South)]), 0, 1, 0);
            overworld.AddRegion(region);
            region.Enter();
        }

        private Overworld overworld;

        [TestMethod]
        public void GivenEmptyString_WhenInterpret_ThenReturnFalse()
        {
            var interpreter = new GlobalCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(string.Empty, game);

            Assert.IsFalse(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenAbout_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new GlobalCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(About.CommandHelp.Command, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenNotes_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new GlobalCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(Notes.CommandHelp.Command, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenHistory_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new GlobalCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(History.CommandHelp.Command, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenHelpWithCommand_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new GlobalCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.ChangeMode(new SceneMode());

            var result = interpreter.Interpret($"{NetAF.Commands.Global.Help.CommandHelp.Command} {NetAF.Commands.Scene.Examine.CommandHelp.Command}", game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenHelpWithShortcut_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new GlobalCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.ChangeMode(new SceneMode());

            var result = interpreter.Interpret($"{NetAF.Commands.Global.Help.CommandHelp.Command} {NetAF.Commands.Scene.Examine.CommandHelp.Shortcut}", game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenHelpWithNoCommand_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new GlobalCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(NetAF.Commands.Global.Help.CommandHelp.Command, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenHelpWithUnknownCommand_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new GlobalCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret($"{NetAF.Commands.Global.Help.CommandHelp.Command} ABC", game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenHelpWithHelp_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new GlobalCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret($"{NetAF.Commands.Global.Help.CommandHelp.Command} {NetAF.Commands.Global.Help.CommandHelp.Command}", game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenCommands_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new GlobalCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(NetAF.Commands.Global.CommandList.CommandHelp.Command, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenMap_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new GlobalCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(NetAF.Commands.Global.Map.CommandHelp.Command, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenInterpreter_WhenGetSupportedCommands_ThenReturnArrayWithSomeItems()
        {
            var interpreter = new GlobalCommandInterpreter();

            var result = interpreter.SupportedCommands;

            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public void GivenInterpreter_WhenGetContextualCommandHelp_ThenReturnArrayWithSomeItems()
        {
            var interpreter = new GlobalCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.ChangeMode(new SceneMode());

            var result = interpreter.GetContextualCommandHelp(game);

            Assert.IsTrue(result.Length > 0);
        }
    }
}
