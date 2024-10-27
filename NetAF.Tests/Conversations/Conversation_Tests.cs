using NetAF.Assets.Characters;
using NetAF.Assets.Interaction;
using NetAF.Conversations;
using NetAF.Conversations.Instructions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Logic;

namespace NetAF.Tests.Conversations
{
    [TestClass]
    public class Conversation_Tests
    {
        [TestMethod]
        public void GivenConverserWithAConversationWithOneParagraph_WhenConstructed_ThenCurrentParagraphIsFirstParagraph()
        {
            var game = NetAF.Logic.Game.Create(new GameInfo(string.Empty, string.Empty), string.Empty, new GameAssetGenerators(() => null, () => null), new GameEndConditions(GameEndConditions.NotEnded, GameEndConditions.NotEnded), GameConfiguration.Default).Invoke();
            var npc = new NonPlayableCharacter(string.Empty, string.Empty) { Conversation = new Conversation(new Paragraph(string.Empty)) };
            game.StartConversation(npc);

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
        public void GivenConverserWithAConversation_WhenNext_ThenResultIsInternal()
        {
            var game = NetAF.Logic.Game.Create(new GameInfo(string.Empty, string.Empty), string.Empty, new GameAssetGenerators(() => null, () => null), new GameEndConditions(GameEndConditions.NotEnded, GameEndConditions.NotEnded), GameConfiguration.Default).Invoke();
            var npc = new NonPlayableCharacter(string.Empty, string.Empty) { Conversation = new Conversation(new Paragraph(string.Empty)) };
            game.StartConversation(npc);

            var result = npc.Conversation.Next(game);

            Assert.AreEqual(ReactionResult.Internal, result.Result);
        }

        [TestMethod]
        public void GivenConverserWithAConversationWithOneParagraph_WhenNext_ThenCurrentParagraphIsUnchanged()
        {
            var game = NetAF.Logic.Game.Create(new GameInfo(string.Empty, string.Empty), string.Empty, new GameAssetGenerators(() => null, () => null), new GameEndConditions(GameEndConditions.NotEnded, GameEndConditions.NotEnded), GameConfiguration.Default).Invoke();
            var npc = new NonPlayableCharacter(string.Empty, string.Empty) { Conversation = new Conversation(new Paragraph(string.Empty)) };
            game.StartConversation(npc);

            var startParagraph = npc.Conversation.CurrentParagraph;
            npc.Conversation.Next(game);
            var result = npc.Conversation.CurrentParagraph;

            Assert.AreEqual(startParagraph, result);
        }

        [TestMethod]
        public void GivenConverserWithAConversationWithTwoParagraphs_WhenNext_ThenCurrentParagraphIsSecondParagraph()
        {
            var game = NetAF.Logic.Game.Create(new GameInfo(string.Empty, string.Empty), string.Empty, new GameAssetGenerators(() => null, () => null), null, GameConfiguration.Default).Invoke();
            var npc = new NonPlayableCharacter(string.Empty, string.Empty) { Conversation = new Conversation(new Paragraph(string.Empty), new Paragraph(string.Empty)) };
            game.StartConversation(npc);

            npc.Conversation.Next(game);
            var result = npc.Conversation.CurrentParagraph;

            Assert.AreEqual(npc.Conversation.Paragraphs[1], result);
        }

        [TestMethod]
        public void GivenNull_WhenRespond_ThenReactionIsError()
        {
            var game = NetAF.Logic.Game.Create(new GameInfo(string.Empty, string.Empty), string.Empty, new GameAssetGenerators(() => null, () => null), new GameEndConditions(GameEndConditions.NotEnded, GameEndConditions.NotEnded), GameConfiguration.Default).Invoke();
            var npc = new NonPlayableCharacter(string.Empty, string.Empty) { Conversation = new Conversation(new Paragraph(string.Empty)) };
            game.StartConversation(npc);

            var result = npc.Conversation.Respond(null, game);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenCurrentParagraphWithNoResponses_WhenRespond_ThenReactionIsError()
        {
            var game = NetAF.Logic.Game.Create(new GameInfo(string.Empty, string.Empty), string.Empty, new GameAssetGenerators(() => null, () => null), new GameEndConditions(GameEndConditions.NotEnded, GameEndConditions.NotEnded), GameConfiguration.Default).Invoke();
            var npc = new NonPlayableCharacter(string.Empty, string.Empty) { Conversation = new Conversation(new Paragraph(string.Empty)) };
            game.StartConversation(npc);

            var result = npc.Conversation.Respond(new Response(string.Empty), game);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenCurrentParagraphWithResponse_WhenRespond_ThenReactionIsInternal()
        {
            var game = NetAF.Logic.Game.Create(new GameInfo(string.Empty, string.Empty), string.Empty, new GameAssetGenerators(() => null, () => null), new GameEndConditions(GameEndConditions.NotEnded, GameEndConditions.NotEnded), GameConfiguration.Default).Invoke();
            var response = new Response(string.Empty, new Repeat());
            var paragraph = new Paragraph(string.Empty, new Repeat()) {  Responses = [response] };
            var npc = new NonPlayableCharacter(string.Empty, string.Empty) { Conversation = new Conversation(paragraph) };
            game.StartConversation(npc);

            var result = npc.Conversation.Respond(response, game);

            Assert.AreEqual(ReactionResult.Internal, result.Result);
        }
    }
}
