using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Interpretation;
using NetAF.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Commands.Movement;

namespace NetAF.Tests.Interpretation
{
    [TestClass]
    public class MovementCommandInterpreter_Tests
    {
        [TestInitialize]
        public void Setup()
        {
            overworld = new Overworld(Identifier.Empty, Description.Empty);
            var region = new Region(Identifier.Empty, Description.Empty);
            region.AddRoom(new(Identifier.Empty, Description.Empty, [new Exit(Direction.North, false, new Identifier(northExitName)), new Exit(Direction.South), new Exit(Direction.East), new Exit(Direction.West), new Exit(Direction.Up), new Exit(Direction.Down)]), 0, 0, 0);
            region.AddRoom(new(Identifier.Empty, Description.Empty, [new Exit(Direction.North)]), 0, 1, 0);
            region.AddRoom(new(Identifier.Empty, Description.Empty, [new Exit(Direction.West)]), 1, 0, 0);
            overworld.AddRegion(region);
            region.Enter();
        }

        private Overworld overworld;
        private static readonly string northExitName = "North_Name";

        [TestMethod]
        public void GivenEmptyString_WhenInterpret_ThenReturnFalse()
        {
            var interpreter = new MovementCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(string.Empty, game);

            Assert.IsFalse(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenEast_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new MovementCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(Move.EastCommandHelp.Command, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenEastShort_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new MovementCommandInterpreter();
            var game= Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(Move.EastCommandHelp.Shortcut, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenNorth_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new MovementCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(Move.NorthCommandHelp.Command, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenNorthShort_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new MovementCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(Move.NorthCommandHelp.Shortcut, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenSouth_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new MovementCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(Move.SouthCommandHelp.Command, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenSouthShort_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new MovementCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(Move.SouthCommandHelp.Shortcut, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenWest_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new MovementCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(Move.WestCommandHelp.Command, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenWestShort_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new MovementCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(Move.WestCommandHelp.Shortcut, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenUp_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new MovementCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(Move.UpCommandHelp.Command, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenUpShort_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new MovementCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(Move.UpCommandHelp.Shortcut, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenDown_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new MovementCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(Move.DownCommandHelp.Command, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenDownShort_WhenInterpret_ThenReturnTrue()
        {
            var interpreter = new MovementCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(Move.DownCommandHelp.Shortcut, game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenInterpreter_WhenGetSupportedCommands_ThenReturnArrayWithSomeItems()
        {
            var interpreter = new MovementCommandInterpreter();

            var result = interpreter.SupportedCommands;

            Assert.IsNotEmpty(result);
        }

        [TestMethod]
        public void GivenInterpreter_WhenGetContextualCommandHelp_ThenReturnArrayWithSomeItems()
        {
            overworld.CurrentRegion.CurrentRoom.AddItem(new Item("a", "b", isTakeable: true));
            overworld.CurrentRegion.CurrentRoom.AddCharacter(new NonPlayableCharacter("a", "b", new NetAF.Conversations.Conversation()));
            PlayableCharacter player = new(string.Empty, string.Empty, [new Item("a", "b", isTakeable: true)]);
            var interpreter = new MovementCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, player), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

            var result = interpreter.GetContextualCommandHelp(game);

            Assert.IsNotEmpty(result);
        }
    }
}
