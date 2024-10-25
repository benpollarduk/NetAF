﻿using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Interaction;
using NetAF.Assets.Locations;
using NetAF.Commands.Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetAF.Tests.Commands.Game
{
    [TestClass]
    public class Drop_Tests
    {
        [TestMethod]
        public void GivenNoCharacter_WhenInvoke_ThenError()
        {
            var game = NetAF.Logic.Game.Create(string.Empty, string.Empty, string.Empty, null, null, null, null).Invoke();
            var command = new Drop(null);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenNoItem_WhenInvoke_ThenError()
        {
            var character = new PlayableCharacter(Identifier.Empty, Description.Empty);
            var game = NetAF.Logic.Game.Create(string.Empty, string.Empty, string.Empty, null, () => character, null, null).Invoke();
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
            var game = NetAF.Logic.Game.Create(string.Empty, string.Empty, string.Empty, () => overworld, () => character, null, null).Invoke();
            var command = new Drop(item);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenItemIsDroppable_WhenInvoke_ThenOK()
        {
            var room = new Room(Identifier.Empty, Description.Empty);
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(room, 0, 0, 0);
            var overworld = new Overworld(string.Empty, string.Empty);
            overworld.AddRegion(region);
            var character = new PlayableCharacter(Identifier.Empty, Description.Empty);
            var item = new Item(new Identifier("A"), Description.Empty, true);
            character.AcquireItem(item);
            var game = NetAF.Logic.Game.Create(string.Empty, string.Empty, string.Empty, () => overworld, () => character, null, null).Invoke();
            var command = new Drop(item);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.OK, result.Result);
        }
    }
}
