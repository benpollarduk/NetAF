using NetAF.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Utilities;
using NetAF.Logic.Modes;
using NetAF.Commands.Scene;
using NetAF.Commands.Global;

namespace NetAF.Tests.Logic.Modes
{
    [TestClass]
    public class ReactionMode_Tests
    {
        [TestMethod]
        public void GivenNew_WhenRenderWithEmptyString_ThenReturnAborted()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var mode = new ReactionMode(string.Empty, string.Empty);

            var result = mode.Render(game);

            Assert.AreEqual(RenderState.Aborted, result);
        }

        [TestMethod]
        public void GivenNew_WhenRenderWithSuccessfulMove_ThenReturnAborted()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var mode = new ReactionMode(string.Empty, Move.SuccessfulMove);

            var result = mode.Render(game);

            Assert.AreEqual(RenderState.Aborted, result);
        }

        [TestMethod]
        public void GivenNew_WhenRenderWithSuccessfulEnd_ThenReturnAborted()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var mode = new ReactionMode(string.Empty, End.SuccessfulEnd);

            var result = mode.Render(game);

            Assert.AreEqual(RenderState.Aborted, result);
        }

        [TestMethod]
        public void GivenNew_WhenRenderWithNonEmptyString_ThenReturnCompleted()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var mode = new ReactionMode(string.Empty, "a");

            var result = mode.Render(game);

            Assert.AreEqual(RenderState.Completed, result);
        }
    }
}
