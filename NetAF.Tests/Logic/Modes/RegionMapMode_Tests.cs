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
        public void GivenNew_WhenRender_ThenNoExceptionThrown()
        {
            Assertions.NoExceptionThrown(() =>
            {
                RegionMaker regionMaker = new(string.Empty, string.Empty);
                Room room = new(string.Empty, string.Empty);
                regionMaker[0, 0, 0] = room;
                OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
                var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
                game.Overworld.CurrentRegion.Enter();
                var mode = new RegionMapMode(RegionMapMode.Player);

                mode.Render(game);
            });
        }

        [TestMethod]
        public void GivenPositionOutOfBounds_WhenPanToPosition_ThenReturnFalse()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            var region = regionMaker.Make();
            region.Enter();

            var result = RegionMapMode.CanPanToPosition(region, new Point3D(1, 1, 1));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenPositionInBoundsAndVisited_WhenPanToPosition_ThenReturnTrue()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            var region = regionMaker.Make();
            region.Enter();

            var result = RegionMapMode.CanPanToPosition(region, new Point3D(0, 0, 0));

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
            var region = regionMaker.Make();
            region.Enter();

            var result = RegionMapMode.CanPanToPosition(region, new Point3D(1, 0, 0));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenPositionInBoundsButNotVisitedButRegionIsVisibleWithoutDiscoveryIsTrue_WhenPanToPosition_ThenReturnTrue()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty);
            Room room2 = new(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            regionMaker[1, 0, 0] = room2;
            var region = regionMaker.Make();
            region.IsVisibleWithoutDiscovery = true;
            region.Enter();

            var result = RegionMapMode.CanPanToPosition(region, new Point3D(1, 0, 0));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenAnOffsetZ_WhenPanToPosition_ThenReturnTrue()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty);
            Room room2 = new(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            regionMaker[1, 0, 1] = room2;
            var region = regionMaker.Make();
            region.JumpToRoom(new(1, 0, 1));
            region.JumpToRoom(new(0, 0, 0));
            region.Enter();

            var result = RegionMapMode.CanPanToPosition(region, new Point3D(0, 0, 1));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenAnOffsetZPositionButNotVisited_WhenPanToPosition_ThenReturnFalse()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty);
            Room room2 = new(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            regionMaker[1, 0, 1] = room2;
            var region = regionMaker.Make();
            region.Enter();

            var result = RegionMapMode.CanPanToPosition(region, new Point3D(0, 0, 1));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenAnOffsetZPositionButNotVisitedButRegionIsVisibleWithoutDiscoveryIsTrue_WhenPanToPosition_ThenReturnTrue()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty);
            Room room2 = new(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            regionMaker[1, 0, 1] = room2;
            var region = regionMaker.Make();
            region.IsVisibleWithoutDiscovery = true;
            region.Enter();

            var result = RegionMapMode.CanPanToPosition(region, new Point3D(0, 0, 1));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenAnOffsetZPosition_WhenRender_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                RegionMaker regionMaker = new(string.Empty, string.Empty);
                Room room = new(string.Empty, string.Empty);
                Room room2 = new(string.Empty, string.Empty);
                regionMaker[0, 0, 0] = room;
                regionMaker[1, 0, 1] = room2;
                OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
                var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
                game.Overworld.CurrentRegion.Enter();
                var mode = new RegionMapMode(new Point3D(0, 0, 1));

                game.Overworld.CurrentRegion.JumpToRoom(new(1, 0, 1));
                game.Overworld.CurrentRegion.JumpToRoom(new(0, 0, 0));

                mode.Render(game);
            });
        }
    }
}
