using NetAF.Assets;
using NetAF.Logic;
using NetAF.Rendering.FrameBuilders;

namespace NetAF.Tests
{
    internal static class TestGameConfiguration
    {
        public static GameConfiguration Default => new GameConfiguration(new TestConsoleAdapter(), FrameBuilderCollections.Console, new Size(80, 50), FinishModes.ReturnToTitleScreen);
    }
}
