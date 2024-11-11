﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Characters;
using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Logic;
using NetAF.Utilities;
using NetAF.Serialization;

namespace NetAF.Tests.Serialization
{
    [TestClass]
    public class GameSerialization_Tests
    {
        [TestMethod]
        public void GivenAPlayer_ThenPlayersIsNotNull()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Item item = new(string.Empty, string.Empty) { IsPlayerVisible = false };
            Room room = new(string.Empty, string.Empty, null, [item]);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();

            GameSerialization result = new(game);

            Assert.IsNotNull(result.Players);
        }

        [TestMethod]
        public void GivenAPlayer_ThenActivePlayerIdentifierIsNotNullOrEmpty()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Item item = new(string.Empty, string.Empty) { IsPlayerVisible = false };
            Room room = new(string.Empty, string.Empty, null, [item]);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter("player", string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();

            GameSerialization result = new(game);

            Assert.IsNotNull(result.ActivePlayerIdentifier);
            Assert.AreNotEqual(string.Empty, result.ActivePlayerIdentifier);
        }

        [TestMethod]
        public void GivenAnOverworld_ThenOverworldIsNotNull()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Item item = new(string.Empty, string.Empty) { IsPlayerVisible = false };
            Room room = new(string.Empty, string.Empty, null, [item]);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();

            GameSerialization result = new(game);

            Assert.IsNotNull(result.Overworld);
        }

        [TestMethod]
        public void Given2PlayableCharacterLocations_ThenInactivePlayerLocationsContains2Elements()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Item item = new(string.Empty, string.Empty) { IsPlayerVisible = false };
            Room room = new(string.Empty, string.Empty, null, [item]);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter("a", string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();
            game.ChangePlayer(new PlayableCharacter("b", string.Empty));
            game.ChangePlayer(new PlayableCharacter("c", string.Empty));

            GameSerialization result = new(game);

            Assert.AreEqual(2, result.InactivePlayerLocations.Length);
        }

        [TestMethod]
        public void GivenAGame_WhenRestoreFromSerializedAndPlayerTookAnItemFromARoom_ThenPlayerHasItemInRestoredGame()
        {
            RegionMaker regionMaker = new("REGION", string.Empty);
            Item item = new("ITEM", string.Empty) { IsPlayerVisible = false };
            Room room = new("ROOM", string.Empty, null, [item]);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter("PLAYER", string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();

            RegionMaker regionMaker2 = new("REGION", string.Empty);
            Item item2 = new("ITEM", string.Empty) { IsPlayerVisible = false };
            Room room2 = new("ROOM", string.Empty, null, [item2]);
            regionMaker2[0, 0, 0] = room2;
            OverworldMaker overworldMaker2 = new(string.Empty, string.Empty, regionMaker2);
            var game2 = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker2.Make(), new PlayableCharacter("PLAYER", string.Empty)), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();

            game2.Player.AddItem(item2);
            room2.RemoveItem(item2);

            GameSerialization serialization = new(game2);

            serialization.Restore(game);

            Assert.AreEqual(1, game.Player.Items.Length);
            Assert.AreEqual(0, room.Items.Length);
        }

        [TestMethod]
        public void GivenAGame_WhenRestoreFromSerializedAndPlayerDroppedAnItemInARoom_ThenPlayerDoesNotHaveItemInRestoredGame()
        {
            RegionMaker regionMaker = new("REGION", string.Empty);
            PlayableCharacter player = new("PLAYER", string.Empty, [new Item("ITEM", string.Empty)]);
            Room room = new("ROOM", string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), player), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();

            RegionMaker regionMaker2 = new("REGION", string.Empty);
            PlayableCharacter player2 = new("PLAYER", string.Empty, [new Item("ITEM", string.Empty)]);
            Room room2 = new("ROOM", string.Empty);
            regionMaker2[0, 0, 0] = room2;
            OverworldMaker overworldMaker2 = new(string.Empty, string.Empty, regionMaker2);
            var game2 = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker2.Make(), player2), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();

            var i = game2.Player.Items[0];
            game2.Player.RemoveItem(i);
            room2.AddItem(i);

            GameSerialization serialization = new(game2);

            serialization.Restore(game);

            Assert.AreEqual(0, game.Player.Items.Length);
            Assert.AreEqual(1, room.Items.Length);
        }

        [TestMethod]
        public void GivenAGame_WhenRestoreFromSerializedAndCharacterMovedFromRoom1ToRoom2_ThenCharacterInRoom2InRestoredGame()
        {
            RegionMaker regionMaker = new("REGION", string.Empty);
            PlayableCharacter player = new("PLAYER", string.Empty, [new Item("ITEM", string.Empty)]);
            NonPlayableCharacter npc = new("NPC", string.Empty);
            Room roomA = new("ROOM A", string.Empty);
            Room roomB = new("ROOM B", string.Empty);
            roomA.AddCharacter(npc);
            regionMaker[0, 0, 0] = roomA;
            regionMaker[1, 0, 0] = roomB;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), player), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();

            RegionMaker regionMaker2 = new("REGION", string.Empty);
            PlayableCharacter player2 = new("PLAYER", string.Empty, [new Item("ITEM", string.Empty)]);
            NonPlayableCharacter npc2 = new("NPC", string.Empty);
            Room roomA2 = new("ROOM A", string.Empty);
            Room roomB2 = new("ROOM B", string.Empty);
            roomA2.AddCharacter(npc2);
            regionMaker2[0, 0, 0] = roomA2;
            regionMaker2[1, 0, 0] = roomB2;
            OverworldMaker overworldMaker2 = new(string.Empty, string.Empty, regionMaker2);
            var game2 = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker2.Make(), player2), GameEndConditions.NoEnd, GameConfiguration.Default).Invoke();

            roomA2.RemoveCharacter(npc2);
            roomB2.AddCharacter(npc2);

            GameSerialization serialization = new(game2);

            serialization.Restore(game);

            Assert.AreEqual(0, roomA.Characters.Length);
            Assert.AreEqual(1, roomB.Characters.Length);
        }
    }
}
