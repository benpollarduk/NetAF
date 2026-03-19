using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Rendering;
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
                var builder = new ConsoleVisualFrameBuilder(gridStringBuilder);
                var designSize = new Size(100, 100);
                var size = new Size(80, 50);
                var gridVisualBuilder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.Yellow);
                gridVisualBuilder.Resize(designSize);

                builder.Build(new Visual("Title", "Description", gridVisualBuilder), size);
            });
        }

        [TestMethod]
        public void GivenDefaultsWithResizeModeScale_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var gridStringBuilder = new GridStringBuilder();
                var builder = new ConsoleVisualFrameBuilder(gridStringBuilder, VisualResizeMode.Scale);
                var size = new Size(80, 50);
                var designSize = new Size(100, 100);
                var gridVisualBuilder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.Yellow);
                gridVisualBuilder.Resize(designSize);

                builder.Build(new Visual("Title", "Description", gridVisualBuilder), size);
            });
        }

        [TestMethod]
        public void GivenDefaultsWithResizeModeScaleDown_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var gridStringBuilder = new GridStringBuilder();
                var builder = new ConsoleVisualFrameBuilder(gridStringBuilder, VisualResizeMode.ScaleDown);
                var size = new Size(80, 50);
                var designSize = new Size(100, 100);
                var gridVisualBuilder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.Yellow);
                gridVisualBuilder.Resize(designSize);

                builder.Build(new Visual("Title", "Description", gridVisualBuilder), size);
            });
        }

        [TestMethod]
        public void GivenDefaultsWithResizeModeCrop_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var gridStringBuilder = new GridStringBuilder();
                var builder = new ConsoleVisualFrameBuilder(gridStringBuilder, VisualResizeMode.Crop);
                var size = new Size(80, 50);
                var designSize = new Size(100, 100);
                var gridVisualBuilder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.Yellow);
                gridVisualBuilder.Resize(designSize);

                builder.Build(new Visual("Title", "Description", gridVisualBuilder), size);
            });
        }
    }
}
