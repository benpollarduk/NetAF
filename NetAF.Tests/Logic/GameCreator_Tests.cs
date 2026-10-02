using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Logic;
using NetAF.Utilities;

namespace NetAF.Tests.Logic
{
    [TestClass]
    public class GameCreator_Tests
    {
        [TestMethod]
        public void GivenCreator_WhenInvoke_ThenReturnsGameFromCallback()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var creator = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default);

            var result = creator.Invoke();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GivenCreator_WhenInvokedTwice_ThenReturnsSeparateInstances()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var creator = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default);

            var first = creator.Invoke();
            var second = creator.Invoke();

            Assert.AreNotSame(first, second);
        }
    }
}
