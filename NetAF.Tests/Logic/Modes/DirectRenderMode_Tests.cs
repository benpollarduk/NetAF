using NetAF.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Utilities;
using NetAF.Logic.Modes;
using NetAF.Rendering.Console;

namespace NetAF.Tests.Logic.Modes
{
    [TestClass]
    public class DirectRenderMode_Tests
    {
        [TestMethod]
        public void GivenNew_WhenRender_ThenNoExceptionThrown()
        {
            Assertions.NoExceptionThrown(() =>
            {
                RegionMaker regionMaker = new(string.Empty, string.Empty);
                Room room = new(string.Empty, string.Empty);
                regionMaker[0, 0, 0] = room;
                OverworldMaker overworldMaker = new(string.Empty, string.Empty, regionMaker);
                var game = Game.Create(new(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(overworldMaker.Make(), new PlayableCharacter(string.Empty, string.Empty)), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke();
                var mode = new VisualMode(new GridTextFrame(new GridStringBuilder(), 0, 0, AnsiColor.Red));

                mode.Render(game);
            });
        }
    }
}
