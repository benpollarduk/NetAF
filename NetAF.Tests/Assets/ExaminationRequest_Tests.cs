﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Logic;

namespace NetAF.Tests.Assets
{
    [TestClass]
    public class ExaminationRequest_Tests
    {
        [TestMethod]
        public void GivenCreate_WhenGameSpecified_ThenExaminerSetFromPlayer()
        {
            var player = new PlayableCharacter(string.Empty, string.Empty);
            var room = new Room(string.Empty, string.Empty);
            var region = new Region(string.Empty, string.Empty);
            var overworld = new Overworld(string.Empty, string.Empty);
            region.AddRoom(room, 0, 0, 0);
            overworld.AddRegion(region);
            var gameCreator = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, player), GameEndConditions.NoEnd, TestGameConfiguration.Default);

            var result = new ExaminationRequest(player, gameCreator.Invoke());

            Assert.AreEqual(player, result.Scene.Examiner);
        }

        [TestMethod]
        public void GivenCreate_WhenGameSpecified_ThenRoomSetFromPlayer()
        {
            var player = new PlayableCharacter(string.Empty, string.Empty);
            var room = new Room(string.Empty, string.Empty);
            var region = new Region(string.Empty, string.Empty);
            var overworld = new Overworld(string.Empty, string.Empty);
            region.AddRoom(room, 0, 0, 0);
            overworld.AddRegion(region);
            region.Enter();
            var gameCreator = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworld, player), GameEndConditions.NoEnd, TestGameConfiguration.Default);

            var result = new ExaminationRequest(player, gameCreator.Invoke());

            Assert.AreEqual(room, result.Scene.Room);
        }
    }
}
