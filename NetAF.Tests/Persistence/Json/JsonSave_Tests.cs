using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Characters;
using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Logic;
using NetAF.Utilities;
using System.IO;
using NetAF.Persistence;
using NetAF.Persistence.Json;

namespace NetAF.Tests.Persistence.Json
{
    [TestClass]
    public class JsonSave_Tests
    {
        [TestMethod]
        public void GivenSimpleGame_WhenToJson_ThenExpectedStringReturned()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var item = new Item(string.Empty, string.Empty) { IsPlayerVisible = false };
            var room = new Room(string.Empty, string.Empty, null, [item]);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();
            var save = RestorePoint.Create("Test", game);
            var expectedMinusDateTime = """{"Game":{"ActivePlayerIdentifier":"","Players":[{"Items":[],"IsAlive":true,"Identifier":"","IsPlayerVisible":true,"AttributeManager":{"Values":{}},"Commands":[]}],"Overworld":{"Regions":[{"Rooms":[{"HasBeenVisited":true,"Items":[{"Identifier":"","IsPlayerVisible":false,"AttributeManager":{"Values":{}},"Commands":[]}],"Exits":[],"Characters":[],"Identifier":"","IsPlayerVisible":true,"AttributeManager":{"Values":{}},"Commands":[]}],"CurrentRoom":"","Identifier":"","IsPlayerVisible":true,"AttributeManager":{"Values":{}},"Commands":[]}],"CurrentRegion":"","Identifier":"","IsPlayerVisible":true,"AttributeManager":{"Values":{}},"Commands":[]},"InactivePlayerLocations":[]},"Name":"Test""";
            var json = JsonSave.ToJson(save);

            Assert.IsTrue(json.StartsWith(expectedMinusDateTime));
        }

        [TestMethod]
        public void GivenSimpleJson_WhenFromJson_ThenValidSaveReturned()
        {
            var json = """{"Game":{"ActivePlayerIdentifier":"","Players":[{"Items":[],"IsAlive":true,"Identifier":"","IsPlayerVisible":true,"AttributeManager":{"Values":{}},"Commands":[]}],"Overworld":{"Regions":[{"Rooms":[{"HasBeenVisited":true,"Items":[{"Identifier":"","IsPlayerVisible":false,"AttributeManager":{"Values":{}},"Commands":[]}],"Exits":[],"Characters":[],"Identifier":"","IsPlayerVisible":true,"AttributeManager":{"Values":{}},"Commands":[]}],"CurrentRoom":"","Identifier":"","IsPlayerVisible":true,"AttributeManager":{"Values":{}},"Commands":[]}],"CurrentRegion":"","Identifier":"","IsPlayerVisible":true,"AttributeManager":{"Values":{}},"Commands":[]},"InactivePlayerLocations":[]},"Name":"Test","CreationTime":"2024-11-03T21:39:27.6030984+00:00"}""";

            var result = JsonSave.FromJson(json);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GivenSimpleGame_WhenToFileWithValidPath_ThenTrueReturned()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var item = new Item(string.Empty, string.Empty) { IsPlayerVisible = false };
            var room = new Room(string.Empty, string.Empty, null, [item]);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();
            var save = RestorePoint.Create("Test", game);
            var path = Path.Combine(Path.GetTempPath(), Path.GetTempFileName());

            var result = JsonSave.ToFile(path, save, out _);
            var fileExists = File.Exists(path);

            if (fileExists)
                File.Delete(path);

            Assert.IsTrue(fileExists);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenSimpleGame_WhenToFileWithInvalidPath_ThenFalseReturned()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var item = new Item(string.Empty, string.Empty) { IsPlayerVisible = false };
            var room = new Room(string.Empty, string.Empty, null, [item]);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();
            var save = RestorePoint.Create("Test", game);
            var path = "abc";

            var result = JsonSave.ToFile(path, save, out _);
            var fileExists = File.Exists(path);

            if (fileExists)
                File.Delete(path);

            Assert.IsFalse(fileExists);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenSimpleGame_WhenToFileWithInvalidPath_ThenMessageNotEmpty()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var item = new Item(string.Empty, string.Empty) { IsPlayerVisible = false };
            var room = new Room(string.Empty, string.Empty, null, [item]);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();
            var save = RestorePoint.Create("Test", game);
            var path = "abc";

            JsonSave.ToFile(path, save, out var message);
            var fileExists = File.Exists(path);

            if (fileExists)
                File.Delete(path);

            Assert.IsFalse(fileExists);
            Assert.IsFalse(string.IsNullOrEmpty(message));
        }

        [TestMethod]
        public void GivenSimpleGame_WhenFromFileWithValidPath_ThenTrueReturned()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var item = new Item(string.Empty, string.Empty) { IsPlayerVisible = false };
            var room = new Room(string.Empty, string.Empty, null, [item]);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();
            var save = RestorePoint.Create("Test", game);
            var path = Path.Combine(Path.GetTempPath(), Path.GetTempFileName());

            JsonSave.ToFile(path, save, out _);
            var fileExists = File.Exists(path);

            var result = JsonSave.FromFile(path, out var _, out _);

            if (fileExists)
                File.Delete(path);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenSimpleGame_WhenFromFileWithInvalidPath_ThenFalseReturned()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var item = new Item(string.Empty, string.Empty) { IsPlayerVisible = false };
            var room = new Room(string.Empty, string.Empty, null, [item]);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();
            var save = RestorePoint.Create("Test", game);
            var path = "abc";

            JsonSave.ToFile(path, save, out _);
            var fileExists = File.Exists(path);

            var result = JsonSave.FromFile(path, out var _, out _);

            if (fileExists)
                File.Delete(path);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenSimpleGame_WhenFromFileWithInvalidPath_ThenMessageNotEmpty()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var item = new Item(string.Empty, string.Empty) { IsPlayerVisible = false };
            var room = new Room(string.Empty, string.Empty, null, [item]);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();
            var save = RestorePoint.Create("Test", game);
            var path = "abc";

            JsonSave.ToFile(path, save, out _);
            var fileExists = File.Exists(path);

            JsonSave.FromFile(path, out var _, out var message);

            if (fileExists)
                File.Delete(path);

            Assert.IsFalse(string.IsNullOrEmpty(message));
        }
    }
}
