using NetAF.Assets.Characters;
using NetAF.Interpretation;
using NetAF.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Locations;
using NetAF.Assets;
using NetAF.Logic.Modes;

namespace NetAF.Tests.Interpretation
{
    [TestClass]
    public class InputInterpreter_Tests
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
            var interpreter = new InputInterpreter(new GlobalCommandInterpreter());
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(string.Empty, game);

            Assert.IsFalse(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenInterpreter_WhenGetSupportedCommands_ThenReturnArrayWithSomeItems()
        {
            var interpreter = new InputInterpreter(new GlobalCommandInterpreter());

            var result = interpreter.SupportedCommands;

            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public void GivenInterpreter_WhenGetContextualCommandHelp_ThenReturnArrayWithSomeItems()
        {
            var interpreter = new InputInterpreter(new GlobalCommandInterpreter());
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.ChangeMode(new SceneMode(interpreter));

            var result = interpreter.GetContextualCommandHelp(game);

            Assert.IsTrue(result.Length > 0);
        }
    }
}
