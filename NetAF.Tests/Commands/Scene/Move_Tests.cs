using NetAF.Assets;
using NetAF.Assets.Locations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Logic;
using NetAF.Commands.Scene;
using NetAF.Commands;
using NetAF.Assets.Characters;
using NetAF.Utilities;

namespace NetAF.Tests.Commands.Scene
{
    [TestClass]
    public class Move_Tests
    {
        [TestMethod]
        public void GivenCantMove_WhenInvoke_ThenError()
        {
            var region = new Region(Identifier.Empty, Description.Empty);
            region.AddRoom(new Room(new("Origin"), Description.Empty, [new Exit(Direction.North)]), 0, 0, 0);
            region.AddRoom(new Room(new("Target"), Description.Empty, [new Exit(Direction.South)]), 0, 1, 0);
            var overworld = new Overworld(string.Empty, string.Empty);
            overworld.AddRegion(region);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, null), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();
            var command = new Move(Direction.East);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenCanMove_WhenInvoke_ThenSilent()
        {
            var region = new Region(Identifier.Empty, Description.Empty);
            region.AddRoom(new Room(new("Origin"), Description.Empty, [new Exit(Direction.North)]), 0, 0, 0);
            region.AddRoom(new Room(new("Target"), Description.Empty, [new Exit(Direction.South)]), 0, 1, 0);
            var overworld = new Overworld(string.Empty, string.Empty);
            overworld.AddRegion(region);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, null), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();
            var command = new Move(Direction.North);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Silent, result.Result);
        }

        [TestMethod]
        public void GivenCanMoveToPreviouslyUnvisitedRoomWithIntroduction_WhenInvoke_ThenInform()
        {
            var region = new Region(Identifier.Empty, Description.Empty);
            region.AddRoom(new Room(new("Origin"), Description.Empty, [new Exit(Direction.North)]), 0, 0, 0);
            region.AddRoom(new Room(new("Target"), Description.Empty, new Description("ABC"), [new Exit(Direction.South)]), 0, 1, 0);
            var overworld = new Overworld(string.Empty, string.Empty);
            overworld.AddRegion(region);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, null), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();
            var command = new Move(Direction.North);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Inform, result.Result);
        }

        [TestMethod]
        public void GivenCanMoveToPreviouslyVisitedRoomWithIntroduction_WhenInvoke_ThenSilent()
        {
            var region = new Region(Identifier.Empty, Description.Empty);
            region.AddRoom(new Room(new("Origin"), Description.Empty, [new Exit(Direction.North)]), 0, 0, 0);
            region.AddRoom(new Room(new("Target"), Description.Empty, new Description("ABC"), [new Exit(Direction.South)]), 0, 1, 0);
            var overworld = new Overworld(string.Empty, string.Empty);
            overworld.AddRegion(region);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, null), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();
            new Move(Direction.North).Invoke(game);
            new Move(Direction.South).Invoke(game);

            var command = new Move(Direction.North);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Silent, result.Result);
        }

        [TestMethod]
        public void GivenGame_WhenGetPrompts_ThenEmptyArray()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var command = new Move(Direction.East);

            var result = command.GetPrompts(game);

            Assert.AreEqual([], result);
        }
    }
}
