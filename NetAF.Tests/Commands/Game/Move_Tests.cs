﻿using NetAF.Assets;
using NetAF.Assets.Interaction;
using NetAF.Assets.Locations;
using NetAF.Commands.Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetAF.Tests.Commands.Game
{
    [TestClass]
    public class Move_Tests
    {
        [TestMethod]
        public void GivenCantMove_WhenInvoke_ThenError()
        {
            var region = new Region(Identifier.Empty, Description.Empty);
            region.AddRoom(new Room(Identifier.Empty, Description.Empty, new Exit(Direction.North)), 0, 0, 0);
            region.AddRoom(new Room(Identifier.Empty, Description.Empty, new Exit(Direction.South)), 0, 1, 0);
            var overworld = new Overworld(string.Empty, string.Empty);
            overworld.AddRegion(region);
            var game = NetAF.Logic.Game.Create(string.Empty, string.Empty, string.Empty, () => overworld, null, null, null).Invoke();
            var command = new Move(Direction.East);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenCanMove_WhenInvoke_ThenOK()
        {
            var region = new Region(Identifier.Empty, Description.Empty);
            region.AddRoom(new Room(Identifier.Empty, Description.Empty, new Exit(Direction.North)), 0, 0, 0);
            region.AddRoom(new Room(Identifier.Empty, Description.Empty, new Exit(Direction.South)), 0, 1, 0);
            var overworld = new Overworld(string.Empty, string.Empty);
            overworld.AddRegion(region);
            var game = NetAF.Logic.Game.Create(string.Empty, string.Empty, string.Empty, () => overworld, null, null, null).Invoke();
            var command = new Move(Direction.North);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.OK, result.Result);
        }
    }
}
