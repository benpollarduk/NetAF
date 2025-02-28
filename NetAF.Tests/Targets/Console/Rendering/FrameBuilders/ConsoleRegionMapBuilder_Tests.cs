using NetAF.Assets;
using NetAF.Assets.Locations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Console.Rendering.FrameBuilders;

namespace NetAF.Tests.Targets.Console.Rendering.FrameBuilders
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
                region.Enter();
                stringBuilder.Resize(new Size(80, 50));

                builder.BuildRegionMap(region, new Point3D(0, 0, 0), new(0, 0), new(80, 60));
            });
        }
    }
}
