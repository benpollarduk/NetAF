using NetAF.Assets.Interaction;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Logic;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Assets;
using NetAF.Utilities;
using System.IO;
using NetAF.Commands.Persistence;

namespace NetAF.Tests.Commands.Persistence
{
    [TestClass]
    public class Save_Tests
    {
        [TestMethod]
        public void GivenValidPath_WhenInvoke_ThenOK()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var item = new Item(string.Empty, string.Empty) { IsPlayerVisible = false };
            var room = new Room(string.Empty, string.Empty, null, [item]);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var path = Path.Combine(Path.GetTempPath(), Path.GetTempFileName());

            var command = new Save()
            {
                Arguments = [path]
            };

            var result = command.Invoke(game);

            var fileExists = File.Exists(path);

            if (fileExists)
                File.Delete(path);

            Assert.AreEqual(ReactionResult.OK, result.Result);
        }

        [TestMethod]
        public void GivenInvalidPath_WhenInvoke_ThenError()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var item = new Item(string.Empty, string.Empty) { IsPlayerVisible = false };
            var room = new Room(string.Empty, string.Empty, null, [item]);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            var path = Path.Combine("abc");

            var command = new Save
            {
                Arguments = [path]
            };

            var result = command.Invoke(game);

            var fileExists = File.Exists(path);

            if (fileExists)
                File.Delete(path);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }
    }
}
