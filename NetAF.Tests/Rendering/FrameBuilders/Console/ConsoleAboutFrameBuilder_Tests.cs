using NetAF.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Rendering.FrameBuilders;
using NetAF.Rendering.FrameBuilders.Console;
using NetAF.Assets;

namespace NetAF.Tests.Rendering.FrameBuilders.Console
{
    [TestClass]
    public class ConsoleAboutFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaults_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var gridStringBuilder = new GridStringBuilder();
                var builder = new ConsoleAboutFrameBuilder(gridStringBuilder);

                builder.Build(string.Empty, Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(null, null), GameEndConditions.NoEnd, TestGameConfiguration.Default).Invoke(), new Size(80, 50));
            });
        }
    }
}
