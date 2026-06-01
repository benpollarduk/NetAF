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

            Assert.IsEmpty(overworld.Regions);
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

            Assert.IsGreaterThan(0, result.Description.Length);
        }

        [TestMethod]
        public void GivenOverworld_WhenExamine_ThenReturnNonEmptyString()
        {
            var overworld = new Overworld(string.Empty, "An overworld");

            var result = overworld.Examination(new ExaminationRequest(overworld, new ExaminationScene(null, new Room(string.Empty, string.Empty))));

            Assert.IsGreaterThan(0, result.Description.Length);
        }

        [TestMethod]
        public void GivenNoRegions_WhenGetCurrentRegion_ThenNull()
        {
            var overworld = new Overworld(string.Empty, string.Empty);

            Assert.IsNull(overworld.CurrentRegion);
        }

        [TestMethod]
        public void GivenOneRegion_WhenGetCurrentRegion_ThenFirstRegion()
        {
            var overworld = new Overworld(string.Empty, string.Empty);
            var region = new Region("abc", string.Empty);
            overworld.AddRegion(region);

            Assert.AreEqual(region, overworld.CurrentRegion);
        }

        [TestMethod]
        public void GivenNoRegions_WhenAddRegion_Then1Region()
        {
            var overworld = new Overworld(string.Empty, string.Empty);

            overworld.AddRegion(new Region(string.Empty, string.Empty));

            Assert.AreEqual(1, overworld.Regions.Length);
        }

        [TestMethod]
        public void GivenOverworld_WhenGetDescription_ThenDescriptionIsCorrect()
        {
            var overworld = new Overworld("World", "A test world");

            var result = overworld.Description.GetDescription();

            Assert.AreEqual("A test world", result);
        }
    }
}
