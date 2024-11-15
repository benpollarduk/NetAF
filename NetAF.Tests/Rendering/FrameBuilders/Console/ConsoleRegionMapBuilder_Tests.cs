using NetAF.Assets;
using NetAF.Assets.Locations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Rendering.FrameBuilders;
using NetAF.Rendering.FrameBuilders.Console;

namespace NetAF.Tests.Rendering.FrameBuilders.Console
{
    [TestClass]
    public class ConsoleRegionMapBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaultValues_WhenBuildRegionMap_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var stringBuilder = new GridStringBuilder();
                var builder = new ConsoleRegionMapBuilder(stringBuilder);
                var region = new Region(string.Empty, string.Empty);
                region.AddRoom(new(string.Empty, string.Empty), 0, 0, 0);
                region.AddRoom(new(string.Empty, string.Empty), 0, 1, 0);
                stringBuilder.Resize(new Size(80, 50));

                builder.BuildRegionMap(region, 0, 0, new Size(80, 50));
            });
        }
    }
}
