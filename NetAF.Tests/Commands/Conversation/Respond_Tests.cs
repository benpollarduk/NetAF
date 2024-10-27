using NetAF.Assets.Characters;
using NetAF.Assets.Interaction;
using NetAF.Commands.Conversation;
using NetAF.Conversations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Logic;

namespace NetAF.Tests.Commands.Conversation
{
    [TestClass]
    public class Respond_Tests
    {
        [TestMethod]
        public void GivenNullGame_WhenInvoke_ThenError()
        {
            var command = new Respond(null);

            var result = command.Invoke(null);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenNullResponse_WhenInvoke_ThenError()
        {
            var game = NetAF.Logic.Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(null, null), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();
            var command = new Respond(null);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenNoConverser_WhenInvoke_ThenError()
        {
            var game = NetAF.Logic.Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(null, null), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();
            var response = new Response("");
            var command = new Respond(response);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenValidGame_WhenInvoke_ThenInternal()
        {
            var game = NetAF.Logic.Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(null, null), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();
            var response = new Response("");
            var paragraph = new Paragraph(string.Empty) { Responses = [response] };
            var conversation = new NetAF.Conversations.Conversation(paragraph);
            var converser = new NonPlayableCharacter(string.Empty, string.Empty) { Conversation = conversation };
            game.StartConversation(converser);
            var command = new Respond(response);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Internal, result.Result);
        }
    }
}
