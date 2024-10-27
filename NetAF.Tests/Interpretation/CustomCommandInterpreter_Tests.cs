using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Interaction;
using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Interpretation;
using NetAF.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Utilities;

namespace NetAF.Tests.Interpretation
{
    [TestClass]
    public class CustomCommandInterpreter_Tests
    {
        [TestInitialize]
        public void Setup()
        {
            overworld = new Overworld(Identifier.Empty, Description.Empty);
            var region = new Region(Identifier.Empty, Description.Empty);
            region.AddRoom(new Room(Identifier.Empty, Description.Empty, new Exit(Direction.North)), 0, 0, 0);
            region.AddRoom(new Room(Identifier.Empty, Description.Empty, new Exit(Direction.South)), 0, 1, 0);
            overworld.AddRegion(region);
        }

        private Overworld overworld;

        [TestMethod]
        public void GivenNoCustomCommands_WhenGetContextualCommands_ThenReturnEmptyArray()
        {
            var interpreter = new CustomCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty), string.Empty, new GameAssetGenerators(overworldMaker.Make, () => new PlayableCharacter(string.Empty, string.Empty)), new GameEndConditions(GameEndConditions.NotEnded, GameEndConditions.NotEnded), GameConfiguration.Default).Invoke();

            var result = interpreter.GetContextualCommandHelp(game);

            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void GivenNoCustomCommands_WhenGetContextualCommands_ThenReturn1CommandHelp()
        {
            var interpreter = new CustomCommandInterpreter();
            overworld.Commands = [new CustomCommand(new CommandHelp("Test", string.Empty), true, (_, _) => new Reaction(ReactionResult.Error, string.Empty))];
            var game = Game.Create(new GameInfo(string.Empty, string.Empty), string.Empty, new GameAssetGenerators(() => overworld, () => new PlayableCharacter(string.Empty, string.Empty)), new GameEndConditions(GameEndConditions.NotEnded, GameEndConditions.NotEnded), GameConfiguration.Default).Invoke();

            var result = interpreter.GetContextualCommandHelp(game);

            Assert.AreEqual(1, result.Length);
        }

        [TestMethod]
        public void GivenValidCustomCommand_WhenInterpret_ThenCommandInvoked()
        {
            var interpreter = new CustomCommandInterpreter();
            overworld.Commands =
            [
                new CustomCommand(new CommandHelp("Test", string.Empty), true, (_, _) =>
                {
                    Assertions.Pass();
                    return new Reaction(ReactionResult.Error, string.Empty);

                })
            ];
            var game = Game.Create(new GameInfo(string.Empty, string.Empty), string.Empty, new GameAssetGenerators(() => overworld, () => new PlayableCharacter(string.Empty, string.Empty)), new GameEndConditions(GameEndConditions.NotEnded, GameEndConditions.NotEnded), GameConfiguration.Default).Invoke();

            interpreter.Interpret("Test", game);
        }
    }
}
