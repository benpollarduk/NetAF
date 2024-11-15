﻿using NetAF.Assets.Interaction;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Logic;
using NetAF.Commands.Global;
using NetAF.Assets.Locations;
using NetAF.Utilities;
using NetAF.Assets.Characters;

namespace NetAF.Tests.Commands.Global
{
    [TestClass]
    public class End_Tests
    {
        [TestMethod]
        public void GivenNullGame_WhenInvoke_ThenError()
        {
            var command = new End();

            var result = command.Invoke(null);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenValidGame_WhenInvoke_ThenOK()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), null, TestGameConfiguration.Default).Invoke();
            var command = new End();

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.OK, result.Result);
        }
    }
}