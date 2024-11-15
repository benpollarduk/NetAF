using NetAF.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Utilities;
using NetAF.Logic.Modes;
using NetAF.Assets;

namespace NetAF.Tests.Logic.Modes
{
    [TestClass]
    public class RegionMapMode_Tests
    {
        [TestMethod]
        public void GivenNew_WhenRender_ThenReturnCompleted()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var mode = new RegionMapMode(RegionMapMode.Player);

            var result = mode.Render(game);

            Assert.AreEqual(RenderState.Completed, result);
        }

        [TestMethod]
        public void GivenPositionOutOfBounds_WhenPanToPosition_ThenReturnFalse()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;

            var result = RegionMapMode.CanPanToPosition(regionMaker.Make(), new Point3D(1, 1, 1));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenPositionInBoundsAndVisited_WhenPanToPosition_ThenReturnTrue()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;

            var result = RegionMapMode.CanPanToPosition(regionMaker.Make(), new Point3D(0, 0, 0));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenPositionInBoundsButNotVisited_WhenPanToPosition_ThenReturnFalse()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty);
            Room room2 = new(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            regionMaker[1, 0, 0] = room2;

            var result = RegionMapMode.CanPanToPosition(regionMaker.Make(), new Point3D(1, 0, 0));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenPositionInBoundsButNotVisitedButRegionIsVisibleWithoutDiscovery_WhenPanToPosition_ThenReturnTrue()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty);
            Room room2 = new(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            regionMaker[1, 0, 0] = room2;
            var region = regionMaker.Make();
            region.VisibleWithoutDiscovery = true;

            var result = RegionMapMode.CanPanToPosition(region, new Point3D(1, 0, 0));

            Assert.IsTrue(result);
        }
    }
}
