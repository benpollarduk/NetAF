using Microsoft.VisualStudio.TestTools.UnitTesting;

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
