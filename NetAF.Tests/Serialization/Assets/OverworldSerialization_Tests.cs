using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Locations;
using NetAF.Serialization.Assets;

namespace NetAF.Tests.Serialization.Assets
{
    [TestClass]
    public class OverworldSerialization_Tests
    {
        [TestMethod]
        public void GivenInRegionA_ThenCurrentRegionIsA()
        {
            var overworld = new Overworld(string.Empty, string.Empty);
            overworld.AddRegion(new("A", string.Empty));

            var result = new OverworldSerialization(overworld);

            Assert.AreEqual("A", result.CurrentRegion);
        }

        [TestMethod]
        public void GivenNoRegions_ThenRegionsLengthIs0()
        {
            var overworld = new Overworld(string.Empty, string.Empty);

            var result = new OverworldSerialization(overworld);

            Assert.AreEqual(0, result.Regions.Length);
        }

        [TestMethod]
        public void Given1Region_ThenRegionsLengthIs1()
        {
            var overworld = new Overworld(string.Empty, string.Empty);
            overworld.AddRegion(new("A", string.Empty));

            var result = new OverworldSerialization(overworld);

            Assert.AreEqual(1, result.Regions.Length);
        }
    }
}
