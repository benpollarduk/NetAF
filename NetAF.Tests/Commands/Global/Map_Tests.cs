using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Commands.Global;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Logic;
using NetAF.Commands;
using NetAF.Utilities;

namespace NetAF.Tests.Commands.Global
{
    [TestClass]
    public class Map_Tests
    {
        [TestMethod]
        public void GivenNullGame_WhenInvoke_ThenError()
        {
            var command = new Map();

            var result = command.Invoke(null);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenValidGame_WhenInvoke_ThenGameModeChanged()
        {
            var overworld = new Overworld(Identifier.Empty, Description.Empty);
            var region = new Region(Identifier.Empty, Description.Empty);
            region.AddRoom(new Room(Identifier.Empty, Description.Empty, [new NetAF.Assets.Locations.Exit(Direction.North)]), 0, 0, 0);
            region.AddRoom(new Room(Identifier.Empty, Description.Empty, [new NetAF.Assets.Locations.Exit(Direction.South)]), 0, 1, 0);
            overworld.AddRegion(region);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var command = new Map();

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.GameModeChanged, result.Result);
        }

        [TestMethod]
        public void GivenGame_WhenGetPrompts_ThenEmptyArray()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var command = new Map();

            var result = command.GetPrompts(game);

            Assert.AreEqual([], result);
        }
    }
}
