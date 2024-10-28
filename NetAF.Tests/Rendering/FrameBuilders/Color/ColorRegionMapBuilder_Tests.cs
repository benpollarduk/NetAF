using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Rendering.FrameBuilders;
using NetAF.Rendering.FrameBuilders.Color;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetAF.Tests.Rendering.FrameBuilders.Color
{
    [TestClass]
    public class ColorRegionMapBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaultValues_WhenBuildRegionMap_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var builder = new ColorRegionMapBuilder();
                var region = new Region(string.Empty, string.Empty);
                region.AddRoom(new(string.Empty, string.Empty), 0, 0, 0);
                region.AddRoom(new(string.Empty, string.Empty), 0, 1, 0);
                var stringBuilder = new GridStringBuilder();
                stringBuilder.Resize(new Size(80, 50));

                builder.BuildRegionMap(stringBuilder, region, 0, 0, 80, 50);
            });
        }
    }
}
