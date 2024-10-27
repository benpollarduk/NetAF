using NetAF.Logic;
using NetAF.Rendering.FrameBuilders;
using NetAF.Rendering.FrameBuilders.Color;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Characters;
using NetAF.Utilities;

namespace NetAF.Tests.Rendering.FrameBuilders.Color
{
    [TestClass]
    public class ColorAboutFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaults_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var gridStringBuilder = new GridStringBuilder();
                var builder = new ColorAboutFrameBuilder(gridStringBuilder);

                builder.Build(string.Empty, Game.Create(new GameInfo(string.Empty, string.Empty), string.Empty, new GameAssetGenerators(() => null, () => null), new GameEndConditions(GameEndConditions.NotEnded, GameEndConditions.NotEnded), GameConfiguration.Default).Invoke(), 80, 50);
            });
        }
    }
}
