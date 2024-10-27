using NetAF.Assets.Interaction;
using NetAF.Commands.Frame;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Logic;

namespace NetAF.Tests.Commands.Frame
{
    [TestClass]
    public class KeyOff_Tests
    {
        [TestMethod]
        public void GivenNullGame_WhenInvoke_ThenError()
        {
            var command = new KeyOff();

            var result = command.Invoke(null);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenValidGame_WhenInvoke_ThenOK()
        {
            var game = NetAF.Logic.Game.Create(new GameInfo(string.Empty, string.Empty), string.Empty, new GameAssetGenerators(() => null, () => null), new GameEndConditions(GameEndConditions.NotEnded, GameEndConditions.NotEnded), GameConfiguration.Default).Invoke();
            var command = new KeyOff();

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.OK, result.Result);
        }
    }
}
