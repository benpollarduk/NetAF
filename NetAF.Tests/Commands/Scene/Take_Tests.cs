using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Logic;
using NetAF.Commands.Scene;
using NetAF.Commands;

namespace NetAF.Tests.Commands.Scene
{
    [TestClass]
    public class Take_Tests
    {
        [TestMethod]
        public void GivenNoCharacter_WhenInvoke_ThenError()
        {
            var room = new Room(Identifier.Empty, Description.Empty);
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room, 0, 0, 0);
            var overworld = new Overworld(string.Empty, string.Empty);
            overworld.AddRegion(region);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, null), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();
            var command = new Take(null);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenCannotTakeOrDropItems_WhenInvoke_ThenError()
        {
            var room = new Room(Identifier.Empty, Description.Empty);
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room, 0, 0, 0);
            var overworld = new Overworld(string.Empty, string.Empty);
            overworld.AddRegion(region);
            var character = new PlayableCharacter(Identifier.Empty, Description.Empty, false, false);
            var item = new Item(new Identifier("A"), Description.Empty, true);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, character), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();
            var command = new Take(item);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenNoItem_WhenInvoke_ThenError()
        {
            var room = new Room(Identifier.Empty, Description.Empty);
            var character = new PlayableCharacter(Identifier.Empty, Description.Empty);
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room, 0, 0, 0);
            var overworld = new Overworld(string.Empty, string.Empty);
            overworld.AddRegion(region);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, character), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();
            var command = new Take(null);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenRoomDoesNotContainThatItem_WhenInvoke_ThenError()
        {
            var room = new Room(Identifier.Empty, Description.Empty);
            var character = new PlayableCharacter(Identifier.Empty, Description.Empty);
            var item = new Item(new Identifier("A"), Description.Empty, true);
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room, 0, 0, 0);
            var overworld = new Overworld(string.Empty, string.Empty);
            overworld.AddRegion(region);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, character), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();
            var command = new Take(item);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenItemIsNotTakeable_WhenInvoke_ThenError()
        {
            var room = new Room(Identifier.Empty, Description.Empty);
            var character = new PlayableCharacter(Identifier.Empty, Description.Empty);
            var item = new Item(new Identifier("A"), Description.Empty);
            room.AddItem(item);
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room, 0, 0, 0);
            var overworld = new Overworld(string.Empty, string.Empty);
            overworld.AddRegion(region);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, character), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();
            var command = new Take(item);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenItemIsTakeable_WhenInvoke_ThenInform()
        {
            var room = new Room(Identifier.Empty, Description.Empty);
            var character = new PlayableCharacter(Identifier.Empty, Description.Empty);
            var item = new Item(new Identifier("A"), Description.Empty, true);
            room.AddItem(item);
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room, 0, 0, 0);
            var overworld = new Overworld(string.Empty, string.Empty);
            overworld.AddRegion(region);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, character), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();
            var command = new Take(item);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Inform, result.Result);
        }
    }
}
