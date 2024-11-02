using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Characters;
using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Logic;
using NetAF.Utilities;
using System;
using NetAF.Serialization.Persistence;

namespace NetAF.Tests.Serialization.Persistence
{
    [TestClass]
    public class RestorePoint_Tests
    {
        [TestMethod]
        public void GivenNameTest_WhenCreate_ThenReturnedSaveNameIsTest()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var item = new Item(string.Empty, string.Empty) { IsPlayerVisible = false };
            var room = new Room(string.Empty, string.Empty, null, item);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();

            var result = RestorePoint.Create("Test", game);

            Assert.AreEqual("Test", result.Name);
        }

        [TestMethod]
        public void GivenNewGame_WhenCreate_ThenReturnedSaveCreationTimeRoughlyNow()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var item = new Item(string.Empty, string.Empty) { IsPlayerVisible = false };
            var room = new Room(string.Empty, string.Empty, null, item);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();
            var now = DateTime.Now;

            var result = RestorePoint.Create(string.Empty, game);

            Assert.IsTrue((result.CreationTime - now).TotalMinutes <= 1);
        }

        [TestMethod]
        public void GivenNewGame_WhenCreate_ThenReturnedSaveGameNotNull()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var item = new Item(string.Empty, string.Empty) { IsPlayerVisible = false };
            var room = new Room(string.Empty, string.Empty, null, item);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();

            var result = RestorePoint.Create(string.Empty, game);

            Assert.IsNotNull(result.Game);
        }
    }
}
