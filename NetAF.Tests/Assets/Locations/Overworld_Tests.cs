using NetAF.Assets.Locations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Characters;
using NetAF.Assets;
using NetAF.Commands;

namespace NetAF.Tests.Assets.Locations
{
    [TestClass]
    public class Overworld_Tests
    {
        [TestMethod]
        public void GivenNoRegions_WhenFindName_ThenFalse()
        {
            var overworld = new Overworld(string.Empty, string.Empty);

            var result = overworld.FindRegion("abc", out _);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenRegionNotPresent_WhenFindName_ThenFalse()
        {
            var overworld = new Overworld(string.Empty, string.Empty);
            overworld.AddRegion(new(string.Empty, string.Empty));

            var result = overworld.FindRegion("abc", out _);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenRegionPresent_WhenFindName_ThenTrue()
        {
            var overworld = new Overworld(string.Empty, string.Empty);
            overworld.AddRegion(new("abc", string.Empty));

            var result = overworld.FindRegion("abc", out _);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenRegionPresent_WhenRemoveRegion_ThenRegionRemoved()
        {
            var overworld = new Overworld(string.Empty, string.Empty);
            var region = new Region("abc", string.Empty);
            overworld.AddRegion(region);

            overworld.RemoveRegion(region);

            Assert.AreEqual(0, overworld.Regions.Length);
        }

        [TestMethod]
        public void GivenRegionPresent_WhenMoveRegion_ThenReturnNonErrorReaction()
        {
            var overworld = new Overworld(string.Empty, string.Empty);
            var region = new Region("abc", string.Empty);
            overworld.AddRegion(region);
            region.AddRoom(new Room("", ""), 0, 0, 0);

            var result = overworld.Move(region);

            Assert.AreNotEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenRegionIsNotPresent_WhenMoveRegion_ThenReturnErrorReaction()
        {
            var overworld = new Overworld(string.Empty, string.Empty);
            var region = new Region("abc", string.Empty);
            region.AddRoom(new Room("", ""), 0, 0, 0);

            var result = overworld.Move(region);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }

        [TestMethod]
        public void GivenNotOverworld_WhenExamine_ThenReturnNonEmptyString()
        {
            var overworld = new Overworld(string.Empty, "An overworld");

            var result = overworld.Examination(new ExaminationRequest(new PlayableCharacter("a", "b"), new ExaminationScene(null, new Room(string.Empty, string.Empty))));

            Assert.IsTrue(result.Description.Length > 0);
        }

        [TestMethod]
        public void GivenOverworld_WhenExamine_ThenReturnNonEmptyString()
        {
            var overworld = new Overworld(string.Empty, "An overworld");

            var result = overworld.Examination(new ExaminationRequest(overworld, new ExaminationScene(null, new Room(string.Empty, string.Empty))));

            Assert.IsTrue(result.Description.Length > 0);
        }
    }
}
