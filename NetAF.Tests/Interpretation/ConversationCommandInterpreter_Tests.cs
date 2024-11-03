using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Conversations;
using NetAF.Interpretation;
using NetAF.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetAF.Tests.Interpretation
{
    [TestClass]
    public class ConversationCommandInterpreter_Tests
    {
        [TestInitialize]
        public void Setup()
        {
            overworld = new Overworld(Identifier.Empty, Description.Empty);
            var region = new Region(Identifier.Empty, Description.Empty);
            region.AddRoom(new Room(Identifier.Empty, Description.Empty, [new Exit(Direction.North)]), 0, 0, 0);
            region.AddRoom(new Room(Identifier.Empty, Description.Empty, [new Exit(Direction.South)]), 0, 1, 0);
            overworld.AddRegion(region);
        }

        private Overworld overworld;

        [TestMethod]
        public void GivenNoActiveConverser_WhenGetContextualCommands_ThenReturnEmptyArray()
        {
            var interpreter = new ConversationCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();

            var result = interpreter.GetContextualCommandHelp(game);

            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void GivenActiveConverserNoResponses_WhenGetContextualCommands_ThenReturnArrayWith1Element()
        {
            var interpreter = new ConversationCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();
            var npc = new NonPlayableCharacter(string.Empty, string.Empty)
            {
                Conversation = new Conversation(new Paragraph("Test"))
            };

            game.StartConversation(npc);

            var result = interpreter.GetContextualCommandHelp(game);

            Assert.AreEqual(1, result.Length);
        }

        [TestMethod]
        public void GivenActiveConverser1CustomCommand_WhenGetContextualCommands_ThenReturnArrayWith2Elements()
        {
            var interpreter = new ConversationCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();
            var npc = new NonPlayableCharacter(string.Empty, string.Empty)
            {
                Conversation = new Conversation(
                new Paragraph("Test")
                {
                    Responses =
                    [
                        new Response("First")
                    ]
                }
            )
            };

            game.StartConversation(npc);

            var result = interpreter.GetContextualCommandHelp(game);

            Assert.AreEqual(2, result.Length);
        }

        [TestMethod]
        public void GivenNoActiveConverser_WhenInterpret_ThenWasInterpretedSuccessfullyIsFalse()
        {
            var interpreter = new ConversationCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();

            var result = interpreter.Interpret(string.Empty, game);

            Assert.IsFalse(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenNoActiveConverserAndEnd_WhenInterpret_ThenWasInterpretedSuccessfullyIsTrue()
        {
            var interpreter = new ConversationCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();
            var npc = new NonPlayableCharacter(string.Empty, string.Empty)
            {
                Conversation = new Conversation(new Paragraph("Test"))
            };

            game.StartConversation(npc);

            var result = interpreter.Interpret("End", game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }

        [TestMethod]
        public void GivenNoActiveConverserAndEmpty_WhenInterpret_ThenWasInterpretedSuccessfullyIsTrue()
        {
            var interpreter = new ConversationCommandInterpreter();
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();
            var npc = new NonPlayableCharacter(string.Empty, string.Empty)
            {
                Conversation = new Conversation(new Paragraph("Test"))
            };

            game.StartConversation(npc);

            var result = interpreter.Interpret("", game);

            Assert.IsTrue(result.WasInterpretedSuccessfully);
        }
    }
}
