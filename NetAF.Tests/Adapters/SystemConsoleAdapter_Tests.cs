using System.IO;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Logic;
using NetAF.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Adapters;
using NetAF.Rendering.FrameBuilders.Console;

namespace NetAF.Tests.Logic
{
    [TestClass]
    public class SystemConsoleAdapter_Tests
    {
        [TestMethod]
        public void GivenNoConsoleAccess_WhenRenderFrame_ThenIOExceptionThrown()
        {
            Assert.ThrowsException<IOException>(() =>
            {
                RegionMaker regionMaker = new(string.Empty, string.Empty);
                Room room = new("Room", string.Empty);
                regionMaker[0, 0, 0] = room;
                OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
                var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default);
                var aboutFrame = new ConsoleAboutFrameBuilder(new NetAF.Rendering.FrameBuilders.GridStringBuilder()).Build("abc", game.Invoke(), 50, 50);
                var adapter = new SystemConsoleAdapter();

                adapter.RenderFrame(aboutFrame);
            });
        }
    }
}