using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Assets;
using NetAF.Logic;
using NetAF.Utilities;

namespace NetAF.Tests.Logic
{
    [TestClass]
    public class AssetCatalog_Tests
    {
        [TestMethod]
        public void Given1Item_WhenFromGame_ThenCatalogContains1Item()
        {
            RegionMaker regionMaker = new("REGION", string.Empty);
            Item item = new("ITEM", string.Empty) { IsPlayerVisible = false };
            Room room = new("ROOM", string.Empty, null, item);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter("PLAYER", string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();

            var catalog = AssetCatalog.FromGame(game);

            Assert.AreEqual(1, catalog.Items.Length);
        }

        [TestMethod]
        public void Given1Room_WhenFromGame_ThenCatalogContains1Room()
        {
            RegionMaker regionMaker = new("REGION", string.Empty);
            Item item = new("ITEM", string.Empty) { IsPlayerVisible = false };
            Room room = new("ROOM", string.Empty, null, item);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter("PLAYER", string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();

            var catalog = AssetCatalog.FromGame(game);

            Assert.AreEqual(1, catalog.Rooms.Length);
        }

        [TestMethod]
        public void Given1Character_WhenFromGame_ThenCatalogContains1Character()
        {
            RegionMaker regionMaker = new("REGION", string.Empty);
            Item item = new("ITEM", string.Empty) { IsPlayerVisible = false };
            Room room = new("ROOM", string.Empty, null, item);
            room.AddCharacter(new(string.Empty, string.Empty));
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter("PLAYER", string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();

            var catalog = AssetCatalog.FromGame(game);

            Assert.AreEqual(1, catalog.Characters.Length);
        }

        [TestMethod]
        public void Given1Character1RoomAndPlayableCharacter_WhenFromGame_ThenCatalogContains3ItemContainers()
        {
            RegionMaker regionMaker = new("REGION", string.Empty);
            Item item = new("ITEM", string.Empty) { IsPlayerVisible = false };
            Room room = new("ROOM", string.Empty, null, item);
            room.AddCharacter(new(string.Empty, string.Empty));
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter("PLAYER", string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();

            var catalog = AssetCatalog.FromGame(game);

            Assert.AreEqual(3, catalog.ItemContainers.Length);
        }
    }
}
