using NetAF.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Utilities;
using NetAF.Logic.Modes;
using NetAF.Narratives;
using NetAF.Rendering;
using NetAF.Targets.Console.Rendering;
using NetAF.Assets;

namespace NetAF.Tests.Logic.Modes
{
    [TestClass]
    public class VisualNarrativeMode_Tests
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
                var builder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.White);
                builder.Resize(new Size(80, 50));
                var mode = new VisualNarrativeMode(new VisualNarrative([new Visual(string.Empty, string.Empty, builder)]));

                mode.Render(game);
            });
        }
    }
}
