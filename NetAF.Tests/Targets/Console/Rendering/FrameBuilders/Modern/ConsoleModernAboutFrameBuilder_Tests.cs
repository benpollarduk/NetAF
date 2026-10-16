using NetAF.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Targets.Console.Rendering.FrameBuilders.Modern;
using NetAF.Targets.Console.Rendering;

namespace NetAF.Tests.Targets.Console.Rendering.FrameBuilders.Modern
{
    [TestClass]
    public class ConsoleModernAboutFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaults_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var gridStringBuilder = new GridStringBuilder();
                var builder = new ConsoleModernAboutFrameBuilder(gridStringBuilder, ConsoleRenderProperties.Default);

                builder.Build(string.Empty, Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(null, null), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke(), new Size(80, 50));
            });
        }
    }
}
