using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Logic;
using NetAF.Persistence;
using NetAF.Utilities;
using System.IO;

namespace NetAF.Tests.Persistence
{
    [TestClass]
    public class RestorePointManager_Tests
    {
        [TestMethod]
        public void GivenTestGame_WhenGetRestorePointDirectory_ThenReturnPathWithGameName()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var room = new Room(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo("Test", string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();

            var result = RestorePointManager.GetRestorePointDirectory(game);

            Assert.AreEqual(Path.Combine(RestorePointManager.RootDirectory, "Test"), result);
        }

        [TestMethod]
        public void GivenTestGame_WhenSave_ThenSuccess()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var room = new Room(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo("Test", string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var name = "TestSave";
            var tempDir = Directory.CreateTempSubdirectory();
            RestorePointManager.RootDirectory = tempDir.FullName;

            var result = RestorePointManager.Save(game, name, out _, out _);

            Directory.Delete(tempDir.FullName, true);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenTestGameAndSaveExists_WhenGetAvailableRestorePointNames_ThenArrayWithSave()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var room = new Room(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo("Test", string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var name = "TestSave";
            var tempDir = Directory.CreateTempSubdirectory();
            RestorePointManager.RootDirectory = tempDir.FullName;
            RestorePointManager.Save(game, name, out _, out _);

            var result = RestorePointManager.GetAvailableRestorePointNames(game);

            Directory.Delete(tempDir.FullName, true);

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(name, result[0]);
        }

        [TestMethod]
        public void GivenTestGameAndSaveExists_WhenExists_ThenTrue()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var room = new Room(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo("Test", string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var name = "TestSave";
            var tempDir = Directory.CreateTempSubdirectory();
            RestorePointManager.RootDirectory = tempDir.FullName;
            RestorePointManager.Save(game, name, out _, out _);

            var result = RestorePointManager.Exists(game, name);

            Directory.Delete(tempDir.FullName, true);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenNullGame_WhenSave_ThenFalse()
        {
            var result = RestorePointManager.Save(null, "TestSave", out _, out _);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenTestGame_WhenSaveAuto_ThenSuccess()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var room = new Room(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo("Test", string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var tempDir = Directory.CreateTempSubdirectory();
            RestorePointManager.RootDirectory = tempDir.FullName;

            var result = RestorePointManager.Save(game, out _, out _);

            Directory.Delete(tempDir.FullName, true);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenTestGameAndNoSaves_WhenGetAvailableRestorePointNames_ThenEmptyArray()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var room = new Room(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo("Test", string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var tempDir = Directory.CreateTempSubdirectory();
            RestorePointManager.RootDirectory = tempDir.FullName;

            var result = RestorePointManager.GetAvailableRestorePointNames(game);

            Directory.Delete(tempDir.FullName, true);

            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void GivenTestGameAndSaveExists_WhenGetAvailableRestorePoints_ThenArrayWithSave()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var room = new Room(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo("Test", string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var name = "TestSave";
            var tempDir = Directory.CreateTempSubdirectory();
            RestorePointManager.RootDirectory = tempDir.FullName;
            RestorePointManager.Save(game, name, out _, out _);

            var result = RestorePointManager.GetAvailableRestorePoints(game);

            Directory.Delete(tempDir.FullName, true);

            Assert.AreEqual(1, result.Length);
        }

        [TestMethod]
        public void GivenTestGameAndNoSaves_WhenGetAvailableRestorePoints_ThenEmptyArray()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var room = new Room(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo("Test", string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var tempDir = Directory.CreateTempSubdirectory();
            RestorePointManager.RootDirectory = tempDir.FullName;

            var result = RestorePointManager.GetAvailableRestorePoints(game);

            Directory.Delete(tempDir.FullName, true);

            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void GivenTestGameAndSaveExists_WhenApply_ThenTrue()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var room = new Room(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo("Test", string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var name = "TestSave";
            var tempDir = Directory.CreateTempSubdirectory();
            RestorePointManager.RootDirectory = tempDir.FullName;
            RestorePointManager.Save(game, name, out _, out _);

            var result = RestorePointManager.Apply(game, name, out _);

            Directory.Delete(tempDir.FullName, true);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenTestGameAndAutoSaveExists_WhenApply_ThenTrue()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var room = new Room(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo("Test", string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var tempDir = Directory.CreateTempSubdirectory();
            RestorePointManager.RootDirectory = tempDir.FullName;
            RestorePointManager.Save(game, out _, out _);

            var result = RestorePointManager.Apply(game, out _);

            Directory.Delete(tempDir.FullName, true);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenTestGameAndNoSaveExists_WhenApply_ThenFalse()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var room = new Room(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo("Test", string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var name = "TestSave";
            var tempDir = Directory.CreateTempSubdirectory();
            RestorePointManager.RootDirectory = tempDir.FullName;

            var result = RestorePointManager.Apply(game, name, out _);

            Directory.Delete(tempDir.FullName, true);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenTestGameAndNoSaveExists_WhenExists_ThenFalse()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var room = new Room(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo("Test", string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var name = "TestSave";
            var tempDir = Directory.CreateTempSubdirectory();
            RestorePointManager.RootDirectory = tempDir.FullName;

            var result = RestorePointManager.Exists(game, name);

            Directory.Delete(tempDir.FullName, true);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenTestGameAndName_WhenGetFilePath_ThenPathWithGameNameAndRestorePointName()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var room = new Room(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo("Test", string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var name = "TestSave";

            var result = RestorePointManager.GetFilePath(game, name);

            var expected = Path.Combine(RestorePointManager.RootDirectory, "Test", $"{name}.netaf");
            Assert.AreEqual(expected, result);
        }
    }
}
