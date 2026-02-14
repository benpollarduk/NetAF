using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Logic;
using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Console.Rendering.FrameBuilders;
using NetAF.Targets.Html.Rendering;
using NetAF.Targets.Html.Rendering.FrameBuilders;
using NetAF.Targets.Text;
using NetAF.Utilities;
using System;

namespace NetAF.Tests.Targets.Text
{
    [TestClass]
    public class TextAdapter_Tests
    {
        [TestMethod]
        public void GivenFrame_WhenRender_ThenRendered()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Item item = new(string.Empty, string.Empty) { IsPlayerVisible = false };
            Room room = new(string.Empty, string.Empty, null, [item]);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            HtmlBuilder htmlBuilder = new();
            var frame = new HtmlReactionFrameBuilder(htmlBuilder).Build("A", "B", false, new(80, 50));
            TestPresenter presenter = new();
            TextAdapter adapter = new(presenter);
            adapter.Setup(game);

            adapter.RenderFrame(frame);

            Assert.IsFalse(string.IsNullOrEmpty(presenter.ToString()));
        }

        [TestMethod]
        public void GivenIConsoleFrame_WhenRender_ThenRendered()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Item item = new(string.Empty, string.Empty) { IsPlayerVisible = false };
            Room room = new(string.Empty, string.Empty, null, [item]);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            GridStringBuilder gridStringBuilder = new();
            var frame = new ConsoleReactionFrameBuilder(gridStringBuilder).Build("A", "B", false, new(80, 50));
            TestPresenter presenter = new();
            TextAdapter adapter = new(presenter);
            adapter.Setup(game);

            adapter.RenderFrame(frame);

            Assert.IsFalse(string.IsNullOrEmpty(presenter.ToString()));
        }

        [TestMethod]
        public void GivenA3x3Grid_WhenConvertGridVisualBuilderToString_ThenReturnString()
        {
            var builder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.White);
            builder.Resize(new(3, 3));
            builder.SetCell(0, 0, 'a', AnsiColor.White);
            builder.SetCell(1, 0, 'b', AnsiColor.White);
            builder.SetCell(2, 0, 'c', AnsiColor.White);
            builder.SetCell(0, 1, 'd', AnsiColor.White);
            builder.SetCell(1, 1, 'e', AnsiColor.White);
            builder.SetCell(2, 1, 'f', AnsiColor.White);
            builder.SetCell(0, 2, 'g', AnsiColor.White);
            builder.SetCell(1, 2, 'h', AnsiColor.White);
            builder.SetCell(2, 2, 'i', AnsiColor.White);

            var result = TextAdapter.ConvertGridVisualBuilderToString(builder);

            var expected = $"abc{Environment.NewLine}def{Environment.NewLine}ghi";

            Assert.AreEqual(expected, result);
        }
    }
}
