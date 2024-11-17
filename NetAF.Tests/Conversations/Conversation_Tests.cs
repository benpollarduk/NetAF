using NetAF.Assets.Characters;
using NetAF.Conversations;
using NetAF.Conversations.Instructions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Logic;
using NetAF.Logic.Modes;
using NetAF.Assets.Locations;
using NetAF.Utilities;
using NetAF.Commands;

namespace NetAF.Tests.Conversations
{
    [TestClass]
    public class Conversation_Tests
    {
        [TestMethod]
        public void GivenConverserWithAConversationWithOneParagraph_WhenConstructed_ThenCurrentParagraphIsFirstParagraph()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var npc = new NonPlayableCharacter(string.Empty, string.Empty, conversation: new(new Paragraph(string.Empty)));
            npc.Conversation.Next(game);
            game.ChangeMode(new ConversationMode(npc));

            var result = npc.Conversation.CurrentParagraph;

            Assert.AreEqual(npc.Conversation.Paragraphs[0], result);
        }

        [TestMethod]
        public void GivenNoParagraphs_WhenNext_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var conversation = new Conversation();
                conversation.Next(null);
            });
        }

        [TestMethod]
        public void GivenNoParagraphs_WhenNext_ThenCurrentParagraphNull()
        {
            var conversation = new Conversation();

            conversation.Next(null);

            Assert.IsNull(conversation.CurrentParagraph);
        }

        [TestMethod]
        public void GivenOneParagraph_WhenNext_ThenCurrentParagraphNotNull()
        {
            var conversation = new Conversation(new Paragraph(string.Empty));

            conversation.Next(null);

            Assert.IsNotNull(conversation.CurrentParagraph);
        }

        [TestMethod]
        public void GivenConverserWithAConversation_WhenNext_ThenResultIsSilent()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var npc = new NonPlayableCharacter(string.Empty, string.Empty, conversation: new(new Paragraph(string.Empty)));
            npc.Conversation.Next(game);
            game.ChangeMode(new ConversationMode(npc));

            var result = npc.Conversation.Next(game);

            Assert.AreEqual(ReactionResult.Silent, result.Result);
        }

        [TestMethod]
        public void GivenConverserWithAConversationWithOneParagraph_WhenNext_ThenCurrentParagraphIsUnchanged()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var npc = new NonPlayableCharacter(string.Empty, string.Empty, conversation: new(new Paragraph(string.Empty)));
            npc.Conversation.Next(game);
            game.ChangeMode(new ConversationMode(npc));

            var startParagraph = npc.Conversation.CurrentParagraph;
            npc.Conversation.Next(game);
            var result = npc.Conversation.CurrentParagraph;

            Assert.AreEqual(startParagraph, result);
        }

        [TestMethod]
        public void GivenConverserWithAConversationWithTwoParagraphs_WhenNext_ThenCurrentParagraphIsSecondParagraph()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), null, TestGameConfiguration.Default).Invoke();
            var npc = new NonPlayableCharacter(string.Empty, string.Empty, conversation: new(new Paragraph(string.Empty), new Paragraph(string.Empty)));
            npc.Conversation.Next(game);
            game.ChangeMode(new ConversationMode(npc));

            npc.Conversation.Next(game);
            var result = npc.Conversation.CurrentParagraph;

            Assert.AreEqual(npc.Conversation.Paragraphs[1], result);
        }

        [TestMethod]
        public void GivenNull_WhenRespond_ThenReactionIsError()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var npc = new NonPlayableCharacter(string.Empty, string.Empty, conversation: new(new Paragraph(string.Empty)));
            npc.Conversation.Next(game);
            game.ChangeMode(new ConversationMode(npc));

            var result = npc.Conversation.Respond(null, game);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenCurrentParagraphWithNoResponses_WhenRespond_ThenReactionIsError()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var npc = new NonPlayableCharacter(string.Empty, string.Empty, conversation: new(new Paragraph(string.Empty)));
            npc.Conversation.Next(game);
            game.ChangeMode(new ConversationMode(npc));

            var result = npc.Conversation.Respond(new Response(string.Empty), game);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenCurrentParagraphWithResponse_WhenRespond_ThenReactionIsSilent()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var response = new Response(string.Empty, new Repeat());
            var paragraph = new Paragraph(string.Empty, new Repeat()) {  Responses = [response] };
            var npc = new NonPlayableCharacter(string.Empty, string.Empty, conversation: new(paragraph));
            npc.Conversation.Next(game);
            game.ChangeMode(new ConversationMode(npc));

            var result = npc.Conversation.Respond(response, game);

            Assert.AreEqual(ReactionResult.Silent, result.Result);
        }
    }
}
