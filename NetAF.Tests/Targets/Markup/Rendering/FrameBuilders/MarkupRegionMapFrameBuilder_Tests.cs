using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Commands.RegionMap;
using NetAF.Logic.Modes;
using NetAF.Rendering;
using NetAF.Targets.Markup.Rendering;
using NetAF.Targets.Markup.Rendering.FrameBuilders;

namespace NetAF.Tests.Targets.Html.Rendering.FrameBuilders
{
    [TestClass]
    public class MarkupRegionMapFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenWidthOf80HeightOf50_WhenBuild_ThenNotNull()
        {
            var markupBuilder = new MarkupBuilder();
            var builder = new MarkupRegionMapFrameBuilder(markupBuilder, new MarkupRegionMapBuilder(markupBuilder));
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(new(string.Empty, string.Empty), 0, 0, 0);
            region.Enter();

            var result = builder.Build(region, RegionMapMode.Player, RegionMapDetail.Normal, [], new Size(80, 50));

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GivenWidthOf80HeightOf50And2FloorsOnGroundFloor_WhenBuild_ThenNotNull()
        {
            var markupBuilder = new MarkupBuilder();
            var builder = new MarkupRegionMapFrameBuilder(markupBuilder, new MarkupRegionMapBuilder(markupBuilder));
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(new(string.Empty, string.Empty, [new Exit(Direction.Up)]), 0, 0, 0);
            region.AddRoom(new(string.Empty, string.Empty, [new Exit(Direction.Up)]), 0, 0, 1);
            region.Enter();

            var result = builder.Build(region, RegionMapMode.Player, RegionMapDetail.Normal, [], new Size(80, 50));

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GivenWidthOf80HeightOf50And2FloorsOnSecondFloor_WhenBuild_ThenNotNull()
        {
            var markupBuilder = new MarkupBuilder();
            var builder = new MarkupRegionMapFrameBuilder(markupBuilder, new MarkupRegionMapBuilder(markupBuilder));
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(new(string.Empty, string.Empty, [new Exit(Direction.Up)]), 0, 0, 0);
            region.AddRoom(new(string.Empty, string.Empty, [new Exit(Direction.Up)]), 0, 0, 1);
            region.SetStartRoom(0, 0, 1);
            region.Enter();

            var result = builder.Build(region, RegionMapMode.Player, RegionMapDetail.Normal, [Pan.UpCommandHelp], new Size(80, 50));

            Assert.IsNotNull(result);
        }
    }
}
