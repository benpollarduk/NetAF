using NetAF.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Console.Rendering.FrameBuilders;
using NetAF.Console.Rendering.FrameBuilders.Color;
using NetAF.Console.Logic;

namespace NetAF.Console.Tests.Rendering.FrameBuilders.Color
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

                builder.Build(string.Empty, Game.Create(new GameInfo(string.Empty, string.Empty, string.Empty), string.Empty, AssetGenerator.Retained(null, null), GameEndConditions.NoEnd, ConsoleGameConfiguration.Default).Invoke(), 80, 50);
            });
        }
    }
}
