using NetAF.Assets;
using NetAF.Assets.Locations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Logic;
using NetAF.Commands.Scene;
using NetAF.Commands;

namespace NetAF.Tests.Commands.Scene
{
    [TestClass]
    public class Move_Tests
    {
        [TestMethod]
        public void GivenCantMove_WhenInvoke_ThenError()
        {
            var region = new Region(Identifier.Empty, Description.Empty);
            region.AddRoom(new Room(Identifier.Empty, Description.Empty, [new Exit(Direction.North)]), 0, 0, 0);
            region.AddRoom(new Room(Identifier.Empty, Description.Empty, [new Exit(Direction.South)]), 0, 1, 0);
            var overworld = new Overworld(string.Empty, string.Empty);
            overworld.AddRegion(region);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, null), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var command = new Move(Direction.East);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenCanMove_WhenInvoke_ThenSilent()
        {
            var region = new Region(Identifier.Empty, Description.Empty);
            region.AddRoom(new Room(Identifier.Empty, Description.Empty, [new Exit(Direction.North)]), 0, 0, 0);
            region.AddRoom(new Room(Identifier.Empty, Description.Empty, [new Exit(Direction.South)]), 0, 1, 0);
            var overworld = new Overworld(string.Empty, string.Empty);
            overworld.AddRegion(region);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, null), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var command = new Move(Direction.North);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Silent, result.Result);
        }
    }
}
