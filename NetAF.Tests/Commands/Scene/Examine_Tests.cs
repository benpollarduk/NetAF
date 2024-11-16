using NetAF.Assets;
using NetAF.Assets.Interaction;
using NetAF.Assets.Locations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Logic;
using NetAF.Commands.Scene;

namespace NetAF.Tests.Commands.Scene
{
    [TestClass]
    public class Examine_Tests
    {
        [TestMethod]
        public void GivenNothingToExamine_WhenInvoke_ThenError()
        {
            var command = new Examine(null);

            var result = command.Invoke(null);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenSomethingToExamine_WhenInvoke_ThenInform()
        {
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(null, null), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var region = new Region(Identifier.Empty, Description.Empty);
            var command = new Examine(region);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Inform, result.Result);
        }
    }
}
