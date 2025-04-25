using NetAF.Assets;
using NetAF.Assets.Locations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Logic.Modes;
using NetAF.Commands.RegionMap;
using NetAF.Targets.Console.Rendering.FrameBuilders;
using NetAF.Targets.Console.Rendering;
using NetAF.Rendering;

namespace NetAF.Tests.Targets.Console.Rendering.FrameBuilders
{
    [TestClass]
    public class ConsoleRegionMapFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenWidthOf80HeightOf50_WhenBuild_ThenNotNull()
        {
            var stringBuilder = new GridStringBuilder();
            stringBuilder.Resize(new Size(80, 50));
            var builder = new ConsoleRegionMapFrameBuilder(stringBuilder, new ConsoleRegionMapBuilder(stringBuilder));
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(new(string.Empty, string.Empty), 0, 0, 0);
            region.Enter();

            var result = builder.Build(region, RegionMapMode.Player, RegionMapDetail.Basic, [], new Size(80, 50));

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GivenWidthOf80HeightOf50And2FloorsOnGroundFloor_WhenBuild_ThenNotNull()
        {
            var stringBuilder = new GridStringBuilder();
            stringBuilder.Resize(new Size(80, 50));
            var builder = new ConsoleRegionMapFrameBuilder(stringBuilder, new ConsoleRegionMapBuilder(stringBuilder));
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(new(string.Empty, string.Empty, [new Exit(Direction.Up)]), 0, 0, 0);
            region.AddRoom(new(string.Empty, string.Empty, [new Exit(Direction.Up)]), 0, 0, 1);
            region.Enter();

            var result = builder.Build(region, RegionMapMode.Player, RegionMapDetail.Basic, [], new Size(80, 50));

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GivenWidthOf80HeightOf50And2FloorsOnSecondFloor_WhenBuild_ThenNotNull()
        {
            var stringBuilder = new GridStringBuilder();
            stringBuilder.Resize(new Size(80, 50));
            var builder = new ConsoleRegionMapFrameBuilder(stringBuilder, new ConsoleRegionMapBuilder(stringBuilder));
            var region = new Region(string.Empty, string.Empty);
            region.AddRoom(new(string.Empty, string.Empty, [new Exit(Direction.Up)]), 0, 0, 0);
            region.AddRoom(new(string.Empty, string.Empty, [new Exit(Direction.Up)]), 0, 0, 1);
            region.SetStartRoom(0, 0, 1);
            region.Enter();

            var result = builder.Build(region, RegionMapMode.Player, RegionMapDetail.Basic, [Pan.UpCommandHelp], new Size(80, 50));

            Assert.IsNotNull(result);
        }
    }
}
