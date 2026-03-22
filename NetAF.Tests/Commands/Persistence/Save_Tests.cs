using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Commands.Persistence;
using NetAF.Logic;
using NetAF.Persistence;
using NetAF.Utilities;
using System.IO;

namespace NetAF.Tests.Commands.Persistence
{
    [TestClass]
    public class Save_Tests
    {
        [TestMethod]
        public void GivenEmptyString_WhenInvoke_ThenError()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var item = new Item(string.Empty, string.Empty) { IsPlayerVisible = false };
            var room = new Room(string.Empty, string.Empty, null, [item]);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo("Test", string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            RestorePointManager.RootDirectory = RestorePointManager.DefaultRootDirectory;

            var command = new Save(string.Empty);

            var result = command.Invoke(game);
            
            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenName_WhenInvoke_ThenInform()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var item = new Item(string.Empty, string.Empty) { IsPlayerVisible = false };
            var room = new Room(string.Empty, string.Empty, null, [item]);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo("Test", string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var name = "Test";
                
            var tempDir = Directory.CreateTempSubdirectory();
            RestorePointManager.RootDirectory = tempDir.FullName;

            RestorePointManager.Save(game, name, out _);
            var command = new Save(name);
            var path = RestorePointManager.GetFilePath(game, name);

            var result = command.Invoke(game);

            var fileExists = File.Exists(path);

            if (fileExists)
                File.Delete(path);

            Directory.Delete(tempDir.FullName, true);

            Assert.AreEqual(ReactionResult.Inform, result.Result);
        }

        [TestMethod]
        public void GivenGame_WhenGetPrompts_ThenArrayWithSingleEntry()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo("Test", string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var command = new Save(string.Empty);
            RestorePointManager.RootDirectory = RestorePointManager.DefaultRootDirectory;

            var result = command.GetPrompts(game);

            Assert.HasCount(1, result);
        }
    }
}
