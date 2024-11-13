using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Interaction;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Logic;
using NetAF.Commands.Scene;

namespace NetAF.Tests.Commands.Scene
{
    [TestClass]
    public class Talk_Tests
    {
        [TestMethod]
        public void GivenNoTarget_WhenInvoke_ThenError()
        {
            var command = new Talk(null);

            var result = command.Invoke(null);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenNoPlayer_WhenInvoke_ThenError()
        {
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(null, null), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();
            var command = new Talk(null);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenPlayerThatCannotConverse_WhenInvoke_ThenError()
        {
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(null, new PlayableCharacter(string.Empty, string.Empty, false)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();
            var command = new Talk(null);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenTargetIsDead_WhenInvoke_ThenError()
        {
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(null, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();
            var npc = new NonPlayableCharacter(Identifier.Empty, Description.Empty, false);
            var command = new Talk(npc);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenTarget_WhenInvoke_ThenInternal()
        {
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(null, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke();
            var npc = new NonPlayableCharacter(Identifier.Empty, Description.Empty);
            var command = new Talk(npc);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Internal, result.Result);
        }
    }
}
