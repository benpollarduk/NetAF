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
        public void GivenEmptyCharactersExceptLast_WhenConvertGridStringBuilderToHtmlStringWithPadding_ThenReturnCorrectlyFormattedOutput()
        {
            var builder = new GridStringBuilder();
            builder.Resize(new(5, 1));
            builder.SetCell(4, 0, 'a', AnsiColor.White);

            var result = HtmlAdapter.ConvertGridStringBuilderToHtmlString(builder);

            Assert.AreEqual("    a<br>", result);
        }

        [TestMethod]
        public void GivenEmptyCharacters_WhenConvertGridStringBuilderToHtmlStringWithoutPadding_ThenReturnCorrectlyFormattedOutput()
        {
            var builder = new GridStringBuilder();
            builder.Resize(new(5, 1));

            var result = HtmlAdapter.ConvertGridStringBuilderToHtmlString(builder, false);

            Assert.AreEqual("\0\0\0\0\0<br>", result);
        }
    }
}
