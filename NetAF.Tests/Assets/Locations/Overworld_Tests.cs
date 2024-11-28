using NetAF.Assets.Locations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Characters;
using NetAF.Assets;

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
        public void GivenRegionPresent_WhenMoveRegion_ThenReturnTrue()
        {
            var overworld = new Overworld(string.Empty, string.Empty);
            var region = new Region("abc", string.Empty);
            overworld.AddRegion(region);

            var result = overworld.Move(region);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenRegionIsNotPresent_WhenMoveRegion_ThenReturnFalse()
        {
            var overworld = new Overworld(string.Empty, string.Empty);
            var region = new Region("abc", string.Empty);

            var result = overworld.Move(region);

            Assert.IsFalse(result);
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
