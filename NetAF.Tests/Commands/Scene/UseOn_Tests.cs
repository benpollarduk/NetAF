using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Logic;
using NetAF.Commands.Scene;
using NetAF.Commands;

namespace NetAF.Tests.Commands.Scene
{
    [TestClass]
    public class UseOn_Tests
    {
        [TestMethod]
        public void GivenNoItem_WhenInvoke_ThenError()
        {
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(null, null), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var command = new UseOn(null, null);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenNoTarget_WhenInvoke_ThenError()
        {
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(null, new PlayableCharacter("", "")), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var item = new Item(Identifier.Empty, Description.Empty, true);
            var command = new UseOn(item, null);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenNoPlayer_WhenInvoke_ThenError()
        {
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(null, null), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var item = new Item(Identifier.Empty, Description.Empty, true);
            var command = new UseOn(item, null);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenTargetIsNpc_WhenInvoke_ThenInform()
        {
            var item = new Item(Identifier.Empty, Description.Empty, true);
            var npc = new NonPlayableCharacter(Identifier.Empty, Description.Empty);
            var player = new PlayableCharacter(Identifier.Empty, Description.Empty);
            var room = new Room(string.Empty, string.Empty);
            room.Characters.Add(npc);
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room, 0, 0, 0);
            var overworld = new Overworld(string.Empty, string.Empty);
            overworld.AddRegion(region);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(null, player), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();
            var command = new UseOn(item, npc);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Inform, result.Result);
        }

        [TestMethod]
        public void GivenTargetIsPlayer_WhenInvoke_ThenInform()
        {
            var item = new Item(Identifier.Empty, Description.Empty, true);
            var pc = new PlayableCharacter(Identifier.Empty, Description.Empty);
            var room = new Room(string.Empty, string.Empty);
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room, 0, 0, 0);
            var overworld = new Overworld(string.Empty, string.Empty);
            overworld.AddRegion(region);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, pc), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();
            var command = new UseOn(item, pc);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Inform, result.Result);
        }

        [TestMethod]
        public void GivenTargetIsRoom_WhenInvoke_ThenInform()
        {
            var item = new Item(Identifier.Empty, Description.Empty, true);
            var room = new Room(Identifier.Empty, Description.Empty);
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room, 0, 0, 0);
            var overworld = new Overworld(string.Empty, string.Empty);
            overworld.AddRegion(region);
            var player = new PlayableCharacter(Identifier.Empty, Description.Empty);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, player), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();
            var command = new UseOn(item, room);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Inform, result.Result);
        }

        [TestMethod]
        public void GivenItemOnRoomInteractionCausesItemExpire_WhenInvoke_ThenItemRemovedFromRoom()
        {
            var item = new Item(Identifier.Empty, Description.Empty, true);
            var room = new Room(Identifier.Empty, Description.Empty, items: [item], interaction: (i) =>
            {
                return new Interaction(InteractionResult.ItemExpires, i);
            });
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room, 0, 0, 0);
            var overworld = new Overworld(string.Empty, string.Empty);
            overworld.AddRegion(region);
            var player = new PlayableCharacter(Identifier.Empty, Description.Empty);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, player), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();
            var command = new UseOn(item, room);
            command.Invoke(game);

            var result = room.ContainsItem(item);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenItemOnPlayerInteractionCausesTargetExpire_WhenInvoke_ThenPlayerNotAlive()
        {
            var item = new Item(Identifier.Empty, Description.Empty, true);
            var room = new Room(Identifier.Empty, Description.Empty, items: [item]);
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room, 0, 0, 0);
            var overworld = new Overworld(string.Empty, string.Empty);
            overworld.AddRegion(region);
            var player = new PlayableCharacter(Identifier.Empty, Description.Empty, interaction: (i) =>
            {
                return new Interaction(InteractionResult.TargetExpires, i);
            });
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, player), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();
            var command = new UseOn(item, player);
            command.Invoke(game);

            var result = player.IsAlive;

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenItemOnPlayerInteractionCausesItemAndTargetExpire_WhenInvoke_ThenPlayerNotAliveAndItemNotInRoom()
        {
            var item = new Item(Identifier.Empty, Description.Empty, true);
            var room = new Room(Identifier.Empty, Description.Empty, items: [item]);
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room, 0, 0, 0);
            var overworld = new Overworld(string.Empty, string.Empty);
            overworld.AddRegion(region);
            var player = new PlayableCharacter(Identifier.Empty, Description.Empty, interaction: (i) =>
            {
                return new Interaction(InteractionResult.ItemAndTargetExpires, i);
            });
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, player), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();
            var command = new UseOn(item, player);
            command.Invoke(game);

            var result1 = player.IsAlive;
            var result2 = room.ContainsItem(item);

            Assert.IsFalse(result1);
            Assert.IsFalse(result2);
        }

        [TestMethod]
        public void GivenItemOnPlayerInteractionCausesItemExpire_WhenInvoke_ThenPlayerDoesNotHaveItem()
        {
            var item = new Item(Identifier.Empty, Description.Empty, true);
            var room = new Room(Identifier.Empty, Description.Empty);
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room, 0, 0, 0);
            var overworld = new Overworld(string.Empty, string.Empty);
            overworld.AddRegion(region);
            var player = new PlayableCharacter(Identifier.Empty, Description.Empty, items: [item], interaction: (i) =>
            {
                return new Interaction(InteractionResult.ItemExpires, i);
            });
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, player), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();
            var command = new UseOn(item, player);
            command.Invoke(game);

            var result = player.HasItem(item);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenItemOnNonPlayableCharacterInteractionCausesItemExpire_WhenInvoke_ThenNonPlayableCharacterDoesNotHaveItem()
        {
            var item = new Item(Identifier.Empty, Description.Empty, true);
            var npc = new NonPlayableCharacter(Identifier.Empty, Description.Empty, interaction: (i) =>
            {
                return new Interaction(InteractionResult.ItemExpires, i);
            });
            npc.AddItem(item);
            var room = new Room(Identifier.Empty, Description.Empty);
            room.AddCharacter(npc);
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room, 0, 0, 0);
            var overworld = new Overworld(string.Empty, string.Empty);
            overworld.AddRegion(region);
            var player = new PlayableCharacter(Identifier.Empty, Description.Empty);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, player), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();
            var command = new UseOn(item, npc);
            command.Invoke(game);

            var result = npc.HasItem(item);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenItemOnNonPlayableCharacterInteractionCausesPlayerDies_WhenInvoke_ThenPlayerIsNotAlive()
        {
            var item = new Item(Identifier.Empty, Description.Empty, true);
            var npc = new NonPlayableCharacter(Identifier.Empty, Description.Empty, interaction: (i) =>
            {
                return new Interaction(InteractionResult.PlayerDies, i);
            });
            npc.AddItem(item);
            var room = new Room(Identifier.Empty, Description.Empty);
            room.AddCharacter(npc);
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room, 0, 0, 0);
            var overworld = new Overworld(string.Empty, string.Empty);
            overworld.AddRegion(region);
            var player = new PlayableCharacter(Identifier.Empty, Description.Empty);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, player), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();
            var command = new UseOn(item, npc);
            command.Invoke(game);

            var result = player.IsAlive;

            Assert.IsFalse(result);
        }
    }
}
