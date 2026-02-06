using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Assets;
using NetAF.Logic;
using NetAF.Utilities;
using NetAF.Targets.Html.Rendering.FrameBuilders;
using NetAF.Targets.Html.Rendering;
using NetAF.Targets.Console.Rendering.FrameBuilders;
using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Text.Rendering.FrameBuilders;
using NetAF.Targets.Markup;
using System.Text;

namespace NetAF.Tests.Targets.Markup
{
    [TestClass]
    public class MarkupAdapter_Tests
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
            MarkupAdapter adapter = new(presenter);
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
            MarkupAdapter adapter = new(presenter);
            adapter.Setup(game);

            adapter.RenderFrame(frame);

            Assert.IsFalse(string.IsNullOrEmpty(presenter.ToString()));
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
            MarkupAdapter adapter = new(presenter);
            adapter.Setup(game);

            adapter.RenderFrame(frame);

            Assert.IsFalse(string.IsNullOrEmpty(presenter.ToString()));
        }

        [TestMethod]
        public void GivenEmptyCharactersExceptLast_WhenConvertGridStringBuilderToMarkupStringWithPadding_ThenReturnCorrectlyFormattedOutput()
        {
            var builder = new GridStringBuilder();
            builder.Resize(new(5, 1));
            builder.SetCell(4, 0, 'a', AnsiColor.White);

            var result = MarkupAdapter.ConvertGridStringBuilderToMarkupString(builder, true, false, false);

            Assert.AreEqual("    a", result);
        }

        [TestMethod]
        public void GivenEmptyCharactersExceptLastOverTwoLines_WhenConvertGridStringBuilderToMarkupStringWithPadding_ThenReturnCorrectlyFormattedOutput()
        {
            var builder = new GridStringBuilder();
            builder.Resize(new(5, 2));
            builder.SetCell(4, 0, 'a', AnsiColor.White);
            builder.SetCell(4, 1, 'a', AnsiColor.White);

            var result = MarkupAdapter.ConvertGridStringBuilderToMarkupString(builder, true, false, false);

            Assert.AreEqual($"    a{MarkupSyntax.NewLine}    a", result);
        }

        [TestMethod]
        public void GivenEmptyCharacters_WhenConvertGridStringBuilderToMarkupStringWithoutPadding_ThenReturnCorrectlyFormattedOutput()
        {
            var builder = new GridStringBuilder();
            builder.Resize(new(5, 1));

            var result = MarkupAdapter.ConvertGridStringBuilderToMarkupString(builder, false, false, false);

            Assert.AreEqual("\0\0\0\0\0", result);
        }


        [TestMethod]
        public void GivenEmptyCharactersOver2Rows_WhenConvertGridStringBuilderToMarkupStringWithoutPadding_ThenReturnCorrectlyFormattedOutput()
        {
            var builder = new GridStringBuilder();
            builder.Resize(new(5, 2));

            var result = MarkupAdapter.ConvertGridStringBuilderToMarkupString(builder, false, false, false);

            Assert.AreEqual($"\0\0\0\0\0{MarkupSyntax.NewLine}\0\0\0\0\0", result);
        }

        [TestMethod]
        public void GivenEmptyCharacters_WhenConvertGridStringBuilderToMarkupStringUsingMonospace_ThenReturnCorrectlyFormattedOutput()
        {
            var builder = new GridStringBuilder();
            builder.Resize(new(5,1));

            var result = MarkupAdapter.ConvertGridStringBuilderToMarkupString(builder, true, true, true);
            var foreground = $"{MarkupSyntax.OpenTag}{MarkupSyntax.Foregound}{MarkupSyntax.Delimiter}#000000{MarkupSyntax.CloseTag} {MarkupSyntax.OpenTag}{MarkupSyntax.EndTag}{MarkupSyntax.Foregound}{MarkupSyntax.CloseTag}";
            var eachCellForeground = $"{foreground}{foreground}{foreground}{foreground}{foreground}";

            Assert.AreEqual($"{MarkupSyntax.OpenTag}{MarkupSyntax.Monospace}{MarkupSyntax.CloseTag}{eachCellForeground}{MarkupSyntax.OpenTag}{MarkupSyntax.EndTag}{MarkupSyntax.Monospace}{MarkupSyntax.CloseTag}", result);
        }

        [TestMethod]
        public void GivenA3x3Grid_WhenConvertGridVisualBuilderToMarkupString_ThenReturnMarkup()
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

            var result = MarkupAdapter.ConvertGridVisualBuilderToMarkupString(builder);

            var foreground = "#C0C0C0";
            var background = "#000000";
            string getCharacter(string foregorund, string background, char character) => $"{MarkupSyntax.OpenTag}{MarkupSyntax.Background}{MarkupSyntax.Delimiter}{background}{MarkupSyntax.CloseTag}{MarkupSyntax.OpenTag}{MarkupSyntax.Foregound}{MarkupSyntax.Delimiter}{foreground}{MarkupSyntax.CloseTag}{character}{MarkupSyntax.OpenTag}{MarkupSyntax.EndTag}{MarkupSyntax.Foregound}{MarkupSyntax.CloseTag}{MarkupSyntax.OpenTag}{MarkupSyntax.EndTag}{MarkupSyntax.Background}{MarkupSyntax.CloseTag}";

            var line1 = $"{getCharacter(foreground, background, 'a')}{getCharacter(foreground, background, 'b')}{getCharacter(foreground, background, 'c')}";
            var line2 = $"{getCharacter(foreground, background, 'd')}{getCharacter(foreground, background, 'e')}{getCharacter(foreground, background, 'f')}";
            var line3 = $"{getCharacter(foreground, background, 'g')}{getCharacter(foreground, background, 'h')}{getCharacter(foreground, background, 'i')}";
            var expected = $"{MarkupSyntax.OpenTag}{MarkupSyntax.Monospace}{MarkupSyntax.CloseTag}{line1}{MarkupSyntax.NewLine}{line2}{MarkupSyntax.NewLine}{line3}{MarkupSyntax.OpenTag}{MarkupSyntax.EndTag}{MarkupSyntax.Monospace}{MarkupSyntax.CloseTag}";

            Assert.AreEqual(expected, result);
        }
    }
}
