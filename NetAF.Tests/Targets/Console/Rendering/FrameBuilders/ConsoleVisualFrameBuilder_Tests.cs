using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Console.Rendering.FrameBuilders;

namespace NetAF.Tests.Targets.Console.Rendering.FrameBuilders
{
    [TestClass]
    public class ConsoleVisualFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaults_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var gridStringBuilder = new GridStringBuilder();
                var designSize = new Size(100, 100);
                var builder = new ConsoleVisualFrameBuilder(gridStringBuilder, designSize);
                var size = new Size(80, 50);
                var gridVisualBuilder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.Yellow);
                gridVisualBuilder.Resize(designSize);

                builder.Build("Title", "Description", gridVisualBuilder, size);
            });
        }

        [TestMethod]
        public void GivenDefaultsWithResizeModeScale_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var gridStringBuilder = new GridStringBuilder();
                var designSize = new Size(100, 100);
                var builder = new ConsoleVisualFrameBuilder(gridStringBuilder, designSize, VisualFrameResizeMode.Scale);
                var size = new Size(80, 50);
                var gridVisualBuilder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.Yellow);
                gridVisualBuilder.Resize(designSize);

                builder.Build("Title", "Description", gridVisualBuilder, size);
            });
        }

        [TestMethod]
        public void GivenDefaultsWithResizeModeCrop_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var gridStringBuilder = new GridStringBuilder();
                var designSize = new Size(100, 100);
                var builder = new ConsoleVisualFrameBuilder(gridStringBuilder, designSize, VisualFrameResizeMode.Crop);
                var size = new Size(80, 50);
                var gridVisualBuilder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.Yellow);
                gridVisualBuilder.Resize(designSize);

                builder.Build("Title", "Description", gridVisualBuilder, size);
            });
        }
    }
}
