using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            var gameCreator = Game.Create(string.Empty, string.Empty, string.Empty, () => overworld, () => player, g => EndCheckResult.NotEnded, g => EndCheckResult.NotEnded);

            var result = new ExaminationRequest(player, gameCreator());

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
            var gameCreator = Game.Create(string.Empty, string.Empty, string.Empty, () => overworld, () => player, g => EndCheckResult.NotEnded, g => EndCheckResult.NotEnded);

            var result = new ExaminationRequest(player, gameCreator());

            Assert.AreEqual(room, result.Scene.Room);
        }
    }
}
