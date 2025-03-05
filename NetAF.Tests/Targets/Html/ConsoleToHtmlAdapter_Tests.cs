using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Assets;
using NetAF.Logic;
using NetAF.Targets.Html;
using NetAF.Utilities;
using System.Threading;
using NetAF.Targets.Console.Rendering.FrameBuilders;
using NetAF.Targets.Console.Rendering;
using System.IO;

namespace NetAF.Tests.Targets.Html
{
    [TestClass]
    public class ConsoleToHtmlAdapter_Tests
    {
        [TestMethod]
        public void GivenTest_WhenWaitForInput_ThenReturnTest()
        {
            ConsoleToHtmlAdapter adapter = new(null);

            var thread = new Thread(() =>
            {
                while (true)
                    adapter.InputReceived("Test");
            });
            thread.IsBackground = true;
            thread.Start();

            var result = adapter.WaitForInput();

            Assert.AreEqual("Test", result);
        }

        [TestMethod]
        public void GivenAcknowledge_WhenWaitForAcknowledge_ThenReturnTrue()
        {
            ConsoleToHtmlAdapter adapter = new(null);

            var thread = new Thread(() =>
            {
                while (true)
                    adapter.AcknowledgeReceived();
            });
            thread.IsBackground = true;
            thread.Start();

            var result = adapter.WaitForAcknowledge();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenTimeout_WhenWaitForAcknowledge_ThenReturnFalse()
        {
            ConsoleToHtmlAdapter adapter = new(null);

            var result = adapter.WaitForAcknowledge(1);

            Assert.IsFalse(result);
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
            MemoryStream stream = new();
            TextWriterPresenter presenter = new(new StreamWriter(stream));
            ConsoleToHtmlAdapter adapter = new(presenter);
            adapter.Setup(game);

            adapter.RenderFrame(frame);

            Assert.IsTrue(stream.Length > 0);
        }
    }
}
