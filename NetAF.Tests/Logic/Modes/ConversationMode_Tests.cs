﻿using NetAF.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Utilities;
using NetAF.Logic.Modes;

namespace NetAF.Tests.Logic.Modes
{
    [TestClass]
    public class ConversationMode_Tests
    {
        [TestMethod]
        public void GivenNew_WhenRender_ThenReturnCompleted()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Room room = new(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var mode = new ConversationMode(new NonPlayableCharacter(string.Empty, string.Empty));

            var result = mode.Render(game);

            Assert.AreEqual(RenderState.Completed, result);
        }
    }
}