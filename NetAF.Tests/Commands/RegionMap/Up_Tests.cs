using NetAF.Assets.Interaction;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Logic;
using NetAF.Commands.RegionMap;
using NetAF.Logic.Modes;

namespace NetAF.Tests.Commands.Frame
{
    [TestClass]
    public class Up_Tests
    {
        [TestMethod]
        public void GivenNullGame_WhenInvoke_ThenError()
        {
            var command = new Up();

            var result = command.Invoke(null);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenValidGame_WhenInvokeAndNotInRegionMapMode_ThenError()
        {
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(null, null), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var command = new Up();

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenValidGame_WhenInvokeAndInRegionMapMode_ThenSilent()
        {
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(null, null), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var command = new Up();
            game.ChangeMode(new RegionMapMode(RegionMapMode.PlayerLevel));

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Silent, result.Result);
        }
    }
}
