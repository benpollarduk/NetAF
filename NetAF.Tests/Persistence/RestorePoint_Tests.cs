using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Characters;
using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Logic;
using NetAF.Utilities;
using System;
using NetAF.Persistence;
using NetAF.Commands;
using NetAF.Persistence.Json;

namespace NetAF.Tests.Persistence
{
    [TestClass]
    public class RestorePoint_Tests
    {
        [TestMethod]
        public void GivenNameTest_WhenCreate_ThenReturnedSaveNameIsTest()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var item = new Item(string.Empty, string.Empty) { IsPlayerVisible = false };
            var room = new Room(string.Empty, string.Empty, null, [item]);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

            var result = RestorePoint.Create("Test", game);

            Assert.AreEqual("Test", result.Name);
        }

        [TestMethod]
        public void GivenNewGame_WhenCreate_ThenReturnedSaveCreationTimeRoughlyNow()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var item = new Item(string.Empty, string.Empty) { IsPlayerVisible = false };
            var room = new Room(string.Empty, string.Empty, null, [item]);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var now = DateTime.Now;

            var result = RestorePoint.Create(string.Empty, game);

            Assert.IsTrue((result.CreationTime - now).TotalMinutes <= 1);
        }

        [TestMethod]
        public void GivenNewGame_WhenCreate_ThenReturnedSaveGameNotNull()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var item = new Item(string.Empty, string.Empty) { IsPlayerVisible = false };
            var room = new Room(string.Empty, string.Empty, null, [item]);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

            var result = RestorePoint.Create(string.Empty, game);

            Assert.IsNotNull(result.Game);
        }

        [TestMethod]
        public void GivenSimpleGameWithCustomCommand_WhenFromJsonFromNewlyCreatedJson_ThenRestorePointIsNotNull()
        {
            var command = new CustomCommand(new CommandHelp("TEST COMMAND", string.Empty), true, true, null);
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var room = new Room(string.Empty, string.Empty, exits: null, commands: [command]);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

            var restore = RestorePoint.Create(string.Empty, game);
            var s = JsonSave.ToJson(restore);

            var r = JsonSave.FromJson(s);

            Assert.IsNotNull(r);
        }
    }
}
