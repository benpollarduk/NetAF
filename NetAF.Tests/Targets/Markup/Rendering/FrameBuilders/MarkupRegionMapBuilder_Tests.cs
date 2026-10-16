using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Rendering;
using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Markup.Rendering;
using NetAF.Targets.Markup.Rendering.FrameBuilders;

namespace NetAF.Tests.Targets.Markup.Rendering.FrameBuilders
{
    [TestClass]
    public class MarkupRegionMapBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaultValues_WhenBuildRegionMap_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var markupBuilder = new MarkupBuilder();
                var mapBuilder = new MarkupRegionMapBuilder(markupBuilder, ConsoleMapRenderProperties.Default);
                var region = new Region(string.Empty, string.Empty);
                region.AddRoom(new(string.Empty, string.Empty), 0, 0, 0);
                region.AddRoom(new(string.Empty, string.Empty), 0, 1, 0);
                region.Enter();

                mapBuilder.BuildRegionMap(region, new Point3D(0, 0, 0), new RegionMapRenderOptions { MapDetail = RegionMapDetail.Normal }, new RoomMapRenderOptions(), new Size(80, 50));
            });
        }
    }
}
