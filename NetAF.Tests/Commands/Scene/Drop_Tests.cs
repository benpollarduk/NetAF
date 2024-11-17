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
    public class Drop_Tests
    {
        [TestMethod]
        public void GivenNoCharacter_WhenInvoke_ThenError()
        {
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(null, null), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var command = new Drop(null);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenNoItem_WhenInvoke_ThenError()
        {
            var character = new PlayableCharacter(Identifier.Empty, Description.Empty);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(null, character), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var command = new Drop(null);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenPlayerDoesNotHaveItem_WhenInvoke_ThenError()
        {
            var room = new Room(Identifier.Empty, Description.Empty);
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room, 0, 0, 0);
            var overworld = new Overworld(string.Empty, string.Empty);
            overworld.AddRegion(region);
            var character = new PlayableCharacter(Identifier.Empty, Description.Empty);
            var item = new Item(new Identifier("A"), Description.Empty, true);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(null, character), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var command = new Drop(item);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenItemIsDroppable_WhenInvoke_ThenInform()
        {
            var room = new Room(Identifier.Empty, Description.Empty);
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room, 0, 0, 0);
            var overworld = new Overworld(string.Empty, string.Empty);
            overworld.AddRegion(region);
            var character = new PlayableCharacter(Identifier.Empty, Description.Empty);
            var item = new Item(new Identifier("A"), Description.Empty, true);
            character.AddItem(item);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, character), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var command = new Drop(item);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Inform, result.Result);
        }
    }
}
