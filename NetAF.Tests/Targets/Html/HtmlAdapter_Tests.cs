using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Assets;
using NetAF.Logic;
using NetAF.Targets.Html;
using NetAF.Utilities;
using NetAF.Targets.Html.Rendering.FrameBuilders;
using NetAF.Targets.Html.Rendering;
using NetAF.Targets.Console.Rendering.FrameBuilders;
using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Text.Rendering;
using System.Text;
using NetAF.Targets.Text.Rendering.FrameBuilders;

namespace NetAF.Tests.Targets.Html
{
    [TestClass]
    public class HtmlAdapter_Tests
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
            HtmlAdapter adapter = new(presenter);
            adapter.Setup(game);

            adapter.RenderFrame(frame);

            Assert.IsTrue(!string.IsNullOrEmpty(presenter.ToString()));
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
            HtmlAdapter adapter = new(presenter);
            adapter.Setup(game);

            adapter.RenderFrame(frame);

            Assert.IsTrue(!string.IsNullOrEmpty(presenter.ToString()));
        }

        [TestMethod]
        public void GivenTextFrame_WhenRender_ThenRendered()
        {
            RegionMaker regionMaker = new(string.Empty, string.Empty);
            Item item = new(string.Empty, string.Empty) { IsPlayerVisible = false };
            Room room = new(string.Empty, string.Empty, null, [item]);
            regionMaker[0, 0, 0] = room;
            OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
            StringBuilder stringBuilder = new();
            var frame = new TextReactionFrameBuilder(stringBuilder).Build("A", "B", false, new(80, 50));
            TestPresenter presenter = new();
            HtmlAdapter adapter = new(presenter);
            adapter.Setup(game);

            adapter.RenderFrame(frame);

            Assert.IsTrue(!string.IsNullOrEmpty(presenter.ToString()));
        }

        [TestMethod]
        public void GivenEmptyCharactersExceptLast_WhenConvertGridStringBuilderToHtmlStringWithPadding_ThenReturnCorrectlyFormattedOutput()
        {
            var builder = new GridStringBuilder();
            builder.Resize(new(5, 1));
            builder.SetCell(4, 0, 'a', AnsiColor.White);

            var result = HtmlAdapter.ConvertGridStringBuilderToHtmlString(builder, true, false, false);

            Assert.AreEqual("    a", result);
        }

        [TestMethod]
        public void GivenEmptyCharactersExceptLastOverTwoLines_WhenConvertGridStringBuilderToHtmlStringWithPadding_ThenReturnCorrectlyFormattedOutput()
        {
            var builder = new GridStringBuilder();
            builder.Resize(new(5, 2));
            builder.SetCell(4, 0, 'a', AnsiColor.White);
            builder.SetCell(4, 1, 'a', AnsiColor.White);

            var result = HtmlAdapter.ConvertGridStringBuilderToHtmlString(builder, true, false, false);

            Assert.AreEqual("    a<br>    a", result);
        }

        [TestMethod]
        public void GivenEmptyCharacters_WhenConvertGridStringBuilderToHtmlStringWithoutPadding_ThenReturnCorrectlyFormattedOutput()
        {
            var builder = new GridStringBuilder();
            builder.Resize(new(5, 1));

            var result = HtmlAdapter.ConvertGridStringBuilderToHtmlString(builder, false, false, false);

            Assert.AreEqual("\0\0\0\0\0", result);
        }


        [TestMethod]
        public void GivenEmptyCharactersOver2Rows_WhenConvertGridStringBuilderToHtmlStringWithoutPadding_ThenReturnCorrectlyFormattedOutput()
        {
            var builder = new GridStringBuilder();
            builder.Resize(new(5, 2));

            var result = HtmlAdapter.ConvertGridStringBuilderToHtmlString(builder, false, false, false);

            Assert.AreEqual("\0\0\0\0\0<br>\0\0\0\0\0", result);
        }

        [TestMethod]
        public void GivenEmptyCharacters_WhenConvertGridStringBuilderToHtmlStringUsingMonospace_ThenReturnCorrectlyFormattedOutput()
        {
            var builder = new GridStringBuilder();
            builder.Resize(new(5,1));

            var result = HtmlAdapter.ConvertGridStringBuilderToHtmlString(builder, true, true, true);

            Assert.IsTrue(result.StartsWith("<pre style=\"font-family: 'Courier New', Courier, monospace; line-height: 1; font-size: 1em;"));
        }

        [TestMethod]
        public void GivenA3x3Grid_WhenConvertGridVisualBuilderToHtmlString_ThenReturnHtml()
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

            var result = HtmlAdapter.ConvertGridVisualBuilderToHtmlString(builder);

            Assert.AreEqual("<pre style=\"font-family: 'Courier New', Courier, monospace; line-height: 1; font-size: 1em;\"><span style=\"background-color: #000000; color: #C0C0C0; display: inline-block; line-height: 1;\">a</span><span style=\"background-color: #000000; color: #C0C0C0; display: inline-block; line-height: 1;\">b</span><span style=\"background-color: #000000; color: #C0C0C0; display: inline-block; line-height: 1;\">c</span><br><span style=\"background-color: #000000; color: #C0C0C0; display: inline-block; line-height: 1;\">d</span><span style=\"background-color: #000000; color: #C0C0C0; display: inline-block; line-height: 1;\">e</span><span style=\"background-color: #000000; color: #C0C0C0; display: inline-block; line-height: 1;\">f</span><br><span style=\"background-color: #000000; color: #C0C0C0; display: inline-block; line-height: 1;\">g</span><span style=\"background-color: #000000; color: #C0C0C0; display: inline-block; line-height: 1;\">h</span><span style=\"background-color: #000000; color: #C0C0C0; display: inline-block; line-height: 1;\">i</span></pre>", result);
        }

        [TestMethod]
        public void GivenTextFrame_WhenConvert_ThenReturnValidHtml()
        {
            var builder = new StringBuilder();
            builder.Append("Test");
            var textFrame = new TextFrame(builder);

            var result = HtmlAdapter.Convert(textFrame);

            Assert.AreEqual('<', result[0]);
            Assert.AreEqual('>', result[^1]);
        }
    }
}
