using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Logic;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Assets;
using NetAF.Utilities;
using System.IO;
using NetAF.Commands.Persistence;
using NetAF.Persistence;
using NetAF.Commands;

namespace NetAF.Tests.Commands.Persistence
{
    [TestClass]
    public class Load_Tests
    {
        [TestMethod]
        public void GivenValidNameAndFileExists_WhenInvoke_ThenInform()
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
            var command = new Load(name);

            var result = command.Invoke(game);

            var path = RestorePointManager.GetFilePath(game, name);
            var fileExists = File.Exists(path);

            if (fileExists)
                File.Delete(path);

            Assert.AreEqual(ReactionResult.Inform, result.Result);
        }

        [TestMethod]
        public void GivenEmptyString_WhenInvoke_ThenError()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var item = new Item(string.Empty, string.Empty) { IsPlayerVisible = false };
            var room = new Room(string.Empty, string.Empty, null, [item]);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var command = new Load(string.Empty);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenInvalidName_WhenInvoke_ThenError()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var item = new Item(string.Empty, string.Empty) { IsPlayerVisible = false };
            var room = new Room(string.Empty, string.Empty, null, [item]);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var path = Path.Combine("abc");
            var command = new Load(path);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenGameWhenSavedToTempDirectory_WhenGetPrompts_ThenArrayWithOneEntry()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo("test", string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var command = new Load(string.Empty);

            var tempDir = Directory.CreateTempSubdirectory();
            RestorePointManager.RootDirectory = tempDir.FullName;

            RestorePointManager.Save(game, "Test", out _);

            var result = command.GetPrompts(game);

            Assert.HasCount(1, result);
        }
    }
}
