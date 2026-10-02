using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Rendering;
using NetAF.Targets.Console.Rendering;
using NetAF.Targets.General.FrameBuilders;

namespace NetAF.Tests.Targets.General.FrameBuilders
{
    [TestClass]
    public class GeneralRegionMapBuilder_Tests
    {
        private sealed class TestBuilder : GeneralRegionMapBuilder
        {
            public int AdaptCallCount { get; private set; }
            public GridStringBuilder LastAdapted { get; private set; }

            protected override void Adapt(GridStringBuilder regionMapBuilder)
            {
                AdaptCallCount++;
                LastAdapted = regionMapBuilder;
            }
        }

        [TestMethod]
        public void GivenDefault_WhenSupportsPan_ThenTrue()
        {
            var builder = new TestBuilder();

            Assert.IsTrue(builder.SupportsPan);
        }

        [TestMethod]
        public void GivenDefault_WhenSupportsZoom_ThenTrue()
        {
            var builder = new TestBuilder();

            Assert.IsTrue(builder.SupportsZoom);
        }

        [TestMethod]
        public void GivenRegion_WhenBuildRegionMap_ThenAdaptIsCalled()
        {
            var builder = new TestBuilder();
            Room room = new("ROOM", string.Empty);
            Region region = new("REGION", string.Empty);
            region.AddRoom(room, 0, 0, 0);
            region.Enter();

            builder.BuildRegionMap(region, new Point3D(0, 0, 0), new RegionMapRenderOptions(), new Size(40, 20));

            Assert.AreEqual(1, builder.AdaptCallCount);
        }

        [TestMethod]
        public void GivenRegion_WhenBuildRegionMap_ThenAdaptedBuilderIsNotNull()
        {
            var builder = new TestBuilder();
            Room room = new("ROOM", string.Empty);
            Region region = new("REGION", string.Empty);
            region.AddRoom(room, 0, 0, 0);
            region.Enter();

            builder.BuildRegionMap(region, new Point3D(0, 0, 0), new RegionMapRenderOptions(), new Size(40, 20));

            Assert.IsNotNull(builder.LastAdapted);
        }
    }
}
