using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Interaction;
using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Interpretation;
using NetAF.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetAF.Tests.Interpretation
{
    [TestClass]
    public class CustomCommandInterpreter_Tests
    {
        [TestMethod]
        public void GivenNoCustomCommands_WhenGetContextualCommands_ThenReturnEmptyArray()
        {
            var overworld = new Overworld(Identifier.Empty, Description.Empty);
            var region = new Region(Identifier.Empty, Description.Empty);
            region.AddRoom(new(Identifier.Empty, Description.Empty, [new Exit(Direction.North)]), 0, 0, 0);
            region.AddRoom(new(Identifier.Empty, Description.Empty, [new Exit(Direction.South)]), 0, 1, 0);
            overworld.AddRegion(region);
            var interpreter = new CustomCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();

            var result = interpreter.GetContextualCommandHelp(game);

            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void GivenNoCustomCommands_WhenGetContextualCommands_ThenReturn1CommandHelp()
        {
            var interpreter = new CustomCommandInterpreter();
            CustomCommand[] commands = [new CustomCommand(new CommandHelp("Test", string.Empty), true, true, (_, _) => new Reaction(ReactionResult.Error, string.Empty))];
            var overworld = new Overworld(Identifier.Empty, Description.Empty, commands);
            var region = new Region(Identifier.Empty, Description.Empty);
            region.AddRoom(new(Identifier.Empty, Description.Empty, [new Exit(Direction.North)]), 0, 0, 0);
            region.AddRoom(new(Identifier.Empty, Description.Empty, [new Exit(Direction.South)]), 0, 1, 0);
            overworld.AddRegion(region);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();

            var result = interpreter.GetContextualCommandHelp(game);

            Assert.AreEqual(1, result.Length);
        }

        [TestMethod]
        public void GivenValidCustomCommand_WhenInterpret_ThenCommandInvoked()
        {
            var interpreter = new CustomCommandInterpreter();
            CustomCommand[] commands =
            [
                new CustomCommand(new("Test", string.Empty), true, true, (_, _) =>
                {
                    Assertions.Pass();
                    return new(ReactionResult.Error, string.Empty);

                })
            ];
            var overworld = new Overworld(Identifier.Empty, Description.Empty, commands);
            var region = new Region(Identifier.Empty, Description.Empty);
            region.AddRoom(new(Identifier.Empty, Description.Empty, [new Exit(Direction.North)]), 0, 0, 0);
            region.AddRoom(new(Identifier.Empty, Description.Empty, [new Exit(Direction.South)]), 0, 1, 0);
            overworld.AddRegion(region);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();

            interpreter.Interpret("Test", game);
        }

        [TestMethod]
        public void GivenValidCustomCommandThatIsNotPlayerVisibleButIsStillInterpreted_WhenInterpret_ThenCommandInvoked()
        {
            var interpreter = new CustomCommandInterpreter();
            CustomCommand[] commands =
            [
                new CustomCommand(new("Test", string.Empty), false, true, (_, _) =>
                {
                    Assertions.Pass();
                    return new(ReactionResult.Error, string.Empty);

                })
            ];
            var overworld = new Overworld(Identifier.Empty, Description.Empty, commands);
            var region = new Region(Identifier.Empty, Description.Empty);
            region.AddRoom(new(Identifier.Empty, Description.Empty, [new Exit(Direction.North)]), 0, 0, 0);
            region.AddRoom(new(Identifier.Empty, Description.Empty, [new Exit(Direction.South)]), 0, 1, 0);
            overworld.AddRegion(region);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();

            interpreter.Interpret("Test", game);
        }


        [TestMethod]
        public void GivenValidCustomCommandThatIsNotPlayerVisibleAndIsNotStillInterpreted_WhenInterpret_ThenResultWasInterpretedSuccessfullyIsFalse()
        {
            var interpreter = new CustomCommandInterpreter();
            CustomCommand[] commands =
            [
                new CustomCommand(new("Test", string.Empty), false, false, (_, _) =>
                {
                    return new(ReactionResult.Error, string.Empty);
                })
            ];
            var overworld = new Overworld(Identifier.Empty, Description.Empty, commands);
            var region = new Region(Identifier.Empty, Description.Empty);
            region.AddRoom(new(Identifier.Empty, Description.Empty, [new Exit(Direction.North)]), 0, 0, 0);
            region.AddRoom(new(Identifier.Empty, Description.Empty, [new Exit(Direction.South)]), 0, 1, 0);
            overworld.AddRegion(region);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();

            var result = interpreter.Interpret("Test", game);

            Assert.IsFalse(result.WasInterpretedSuccessfully);
        }
    }
}
