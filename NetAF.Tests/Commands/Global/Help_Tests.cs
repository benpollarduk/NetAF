using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Interaction;
using NetAF.Assets.Locations;
using NetAF.Commands.Global;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Logic;

namespace NetAF.Tests.Commands.Global
{
    [TestClass]
    public class Help_Tests
    {
        [TestMethod]
        public void GivenNullGame_WhenInvoke_ThenError()
        {
            var command = new Help();

            var result = command.Invoke(null);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenValidGame_WhenInvoke_ThenInternal()
        {
            var room = new Room(Identifier.Empty, Description.Empty);
            var character = new PlayableCharacter(Identifier.Empty, Description.Empty);
            var item = new Item(new Identifier("A"), Description.Empty, true);
            room.AddItem(item);
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room, 0, 0, 0);
            var overworld = new Overworld(string.Empty, string.Empty);
            overworld.AddRegion(region);
            var game = NetAF.Logic.Game.Create(new GameInfo(string.Empty, string.Empty), string.Empty, new GameAssetGenerators(() => overworld, () => character), new GameEndConditions(GameEndConditions.NotEnded, GameEndConditions.NotEnded), GameConfiguration.Default).Invoke();
            var command = new Help();

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Internal, result.Result);
        }
    }
}
