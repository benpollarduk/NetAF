using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Rendering;
using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Text.Rendering;
using NetAF.Targets.Text.Rendering.FrameBuilders;
using System.Text;

namespace NetAF.Tests.Targets.Text.Rendering.FrameBuilders
{
    [TestClass]
    public class MarkupVisualFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaults_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var stringBuilder = new StringBuilder();
                var builder = new TextVisualFrameBuilder(stringBuilder);
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
                var stringBuilder = new StringBuilder();
                var builder = new TextVisualFrameBuilder(stringBuilder, VisualResizeMode.Scale);
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
                var stringBuilder = new StringBuilder();
                var builder = new TextVisualFrameBuilder(stringBuilder, VisualResizeMode.Crop);
                var size = new Size(80, 50);
                var designSize = new Size(100, 100);
                var gridVisualBuilder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.Yellow);
                gridVisualBuilder.Resize(designSize);

                builder.Build(new Visual("Title", "Description", gridVisualBuilder), size);
            });
        }
    }
}
