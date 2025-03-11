﻿using NetAF.Commands.Frame;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Logic;
using NetAF.Commands;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Utilities;

namespace NetAF.Tests.Commands.Frame
{
    [TestClass]
    public class KeyOn_Tests
    {
        [TestMethod]
        public void GivenNullGame_WhenInvoke_ThenError()
        {
            var command = new KeyOn();

            var result = command.Invoke(null);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenValidGame_WhenInvoke_ThenInform()
        {
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(null, null), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var command = new KeyOn();

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Inform, result.Result);
        }

        [TestMethod]
        public void GivenGame_WhenGetPrompts_ThenEmptyArray()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var command = new KeyOn();

            var result = command.GetPrompts(game);

            Assert.AreEqual([], result);
        }
    }
}
