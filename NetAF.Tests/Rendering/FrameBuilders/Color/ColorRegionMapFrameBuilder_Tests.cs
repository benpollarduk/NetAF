using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Rendering.FrameBuilders;
using NetAF.Rendering.FrameBuilders.Color;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetAF.Tests.Rendering.FrameBuilders.Color
{
    [TestClass]
    public class ColorRegionMapFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenWidthOf80HeightOf50_WhenBuild_ThenNotNull()
        {
            var stringBuilder = new GridStringBuilder();
            stringBuilder.Resize(new Size(80, 50));
            var builder = new ColorRegionMapFrameBuilder(stringBuilder, new ColorRegionMapBuilder());
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(new(string.Empty, string.Empty), 0, 0, 0);

            var result = builder.Build(region, 80, 50);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GivenWidthOf80HeightOf50And2FloorsOnGroundFloor_WhenBuild_ThenNotNull()
        {
            var stringBuilder = new GridStringBuilder();
            stringBuilder.Resize(new Size(80, 50));
            var builder = new ColorRegionMapFrameBuilder(stringBuilder, new ColorRegionMapBuilder());
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(new(string.Empty, string.Empty, new Exit(Direction.Up)), 0, 0, 0);
            region.AddRoom(new(string.Empty, string.Empty, new Exit(Direction.Up)), 0, 0, 1);

            var result = builder.Build(region, 80, 50);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GivenWidthOf80HeightOf50And2FloorsOnSecondFloor_WhenBuild_ThenNotNull()
        {
            var stringBuilder = new GridStringBuilder();
            stringBuilder.Resize(new Size(80, 50));
            var builder = new ColorRegionMapFrameBuilder(stringBuilder, new ColorRegionMapBuilder());
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(new(string.Empty, string.Empty, new Exit(Direction.Up)), 0, 0, 0);
            region.AddRoom(new(string.Empty, string.Empty, new Exit(Direction.Up)), 0, 0, 1);
            region.SetStartRoom(0, 0, 1);

            var result = builder.Build(region, 80, 50);

            Assert.IsNotNull(result);
        }
    }
}
