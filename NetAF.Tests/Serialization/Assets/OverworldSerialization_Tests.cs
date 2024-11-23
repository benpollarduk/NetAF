using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Locations;
using NetAF.Serialization.Assets;

namespace NetAF.Tests.Serialization.Assets
{
    [TestClass]
    public class OverworldSerialization_Tests
    {
        [TestMethod]
        public void GivenInRegionA_WhenFromOverworld_ThenCurrentRegionIsA()
        {
            Overworld overworld = new(string.Empty, string.Empty);
            overworld.AddRegion(new("A", string.Empty));

            OverworldSerialization result = OverworldSerialization.FromOverworld(overworld);

            Assert.AreEqual("A", result.CurrentRegion);
        }

        [TestMethod]
        public void GivenNoRegions_WhenFromOverworld_ThenRegionsLengthIs0()
        {
            Overworld overworld = new(string.Empty, string.Empty);

            OverworldSerialization result = OverworldSerialization.FromOverworld(overworld);

            Assert.AreEqual(0, result.Regions.Length);
        }

        [TestMethod]
        public void Given1Region_WhenFromOverworld_ThenRegionsLengthIs1()
        {
            Overworld overworld = new(string.Empty, string.Empty);
            overworld.AddRegion(new("A", string.Empty));

            OverworldSerialization result = OverworldSerialization.FromOverworld(overworld);

            Assert.AreEqual(1, result.Regions.Length);
        }

        [TestMethod]
        public void GivenOverworldWith2Regions_WhenRestoreFromOverworldWhereSecondRegionIsCurrentRegion_ThenCurrentRegionIsSecondRegion()
        {
            Overworld overworld = new(string.Empty, string.Empty);
            overworld.AddRegion(new(string.Empty, string.Empty));
            overworld.AddRegion(new("TARGET", string.Empty));
            Overworld overworld2 = new(string.Empty, string.Empty);
            overworld2.AddRegion(new(string.Empty, string.Empty));
            overworld2.AddRegion(new("TARGET", string.Empty));
            overworld2.Move(overworld2.Regions[1]);
            OverworldSerialization serialization = OverworldSerialization.FromOverworld(overworld2);

            serialization.Restore(overworld);

            Assert.AreEqual("TARGET", overworld.CurrentRegion.Identifier.Name);
        }
    }
}
