using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Console.Rendering.FrameBuilders;
using NetAF.Console.Rendering.FrameBuilders.Color;

namespace NetAF.Console.Tests.Rendering.FrameBuilders.Color
{
    [TestClass]
    public class ColorCompletionFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaults_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var gridStringBuilder = new GridStringBuilder();
                var builder = new ColorCompletionFrameBuilder(gridStringBuilder);

                builder.Build(string.Empty, string.Empty, 80, 50);
            });
        }
    }
}
