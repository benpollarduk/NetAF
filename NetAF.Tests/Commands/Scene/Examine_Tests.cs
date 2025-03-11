using NetAF.Assets;
using NetAF.Assets.Locations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Logic;
using NetAF.Commands.Scene;
using NetAF.Commands;
using NetAF.Assets.Characters;
using NetAF.Utilities;
using System;
using NetAF.Extensions;

namespace NetAF.Tests.Commands.Scene
{
    [TestClass]
    public class Examine_Tests
    {
        [TestMethod]
        public void GivenNothingToExamine_WhenInvoke_ThenError()
        {
            var command = new Examine(null);

            var result = command.Invoke(null);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenSomethingToExamine_WhenInvoke_ThenInform()
        {
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(null, null), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var region = new Region(Identifier.Empty, Description.Empty);
            var command = new Examine(region);

            var result = command.Invoke(game);

            Assert.AreEqual(ReactionResult.Inform, result.Result);
        }

        [TestMethod]
        public void GivenGame_WhenGetPrompts_ThenArrayContainingOverworldPlayerRegionRoom()
        {
            RegionMaker regionMaker = new("REGION", string.Empty);
            Room room = new("ROOM", string.Empty);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new("OVERWORLD", string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter("PLAYER", string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            game.Overworld.CurrentRegion.Enter();
            var command = new Examine(null);

            var prompts = command.GetPrompts(game);
            var regionResult = Array.Find(prompts, x => x.Entry.InsensitiveEquals("REGION"));
            var roomResult = Array.Find(prompts, x => x.Entry.InsensitiveEquals("ROOM"));
            var overworldResult = Array.Find(prompts, x => x.Entry.InsensitiveEquals("OVERWORLD"));
            var playerResult = Array.Find(prompts, x => x.Entry.InsensitiveEquals("PLAYER"));

            Assert.IsNotNull(regionResult);
            Assert.IsNotNull(roomResult);
            Assert.IsNotNull(overworldResult);
            Assert.IsNotNull(playerResult);
        }
    }
}
