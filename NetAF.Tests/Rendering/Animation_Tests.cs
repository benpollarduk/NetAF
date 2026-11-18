using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Rendering;
using NetAF.Targets.Console.Rendering;

namespace NetAF.Tests.Rendering
{
    [TestClass]
    public class Animation_Tests
    {
        [TestMethod]
        public void Given10x10NewSize5x5_WhenCrop_ThenNewSizeIs5x5()
        {
            var builder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.Blue);
            builder.Resize(new Size(10, 10));
            var frame = new GridVisualFrame(builder);
            var animatedVisualFrame = new GridAnimatedVisualFrame([frame], AnimationOptions.Single);
            var value = new Animation(string.Empty, string.Empty, animatedVisualFrame);

            var result = value.Crop(new Size(5, 5));

            Assert.AreEqual(5, result.AnimationVisualFrame.Frames[0].Width);
            Assert.AreEqual(5, result.AnimationVisualFrame.Frames[0].Height);
        }

        [TestMethod]
        public void Given10x10NewSize15x15_WhenCrop_ThenNewSizeIs10x10()
        {
            var builder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.Blue);
            builder.Resize(new Size(10, 10));
            var frame = new GridVisualFrame(builder);
            var animatedVisualFrame = new GridAnimatedVisualFrame([frame], AnimationOptions.Single);
            var value = new Animation(string.Empty, string.Empty, animatedVisualFrame);

            var result = value.Crop(new Size(15, 15));

            Assert.AreEqual(10, result.AnimationVisualFrame.Frames[0].Width);
            Assert.AreEqual(10, result.AnimationVisualFrame.Frames[0].Height);
        }

        [TestMethod]
        public void Given10x10NewSize5x5_WhenScale_ThenNewSizeIs5x5()
        {
            var builder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.Blue);
            builder.Resize(new Size(10, 10));
            var frame = new GridVisualFrame(builder);
            var animatedVisualFrame = new GridAnimatedVisualFrame([frame], AnimationOptions.Single);
            var value = new Animation(string.Empty, string.Empty, animatedVisualFrame);

            var result = value.Scale(new Size(5, 5));

            Assert.AreEqual(5, result.AnimationVisualFrame.Frames[0].Width);
            Assert.AreEqual(5, result.AnimationVisualFrame.Frames[0].Height);
        }

        [TestMethod]
        public void Given10x10NewSize15x15_WhenScale_ThenNewSizeIs15x15()
        {
            var builder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.Blue);
            builder.Resize(new Size(10, 10));
            var frame = new GridVisualFrame(builder);
            var animatedVisualFrame = new GridAnimatedVisualFrame([frame], AnimationOptions.Single);
            var value = new Animation(string.Empty, string.Empty, animatedVisualFrame);

            var result = value.Scale(new Size(15, 15));

            Assert.AreEqual(15, result.AnimationVisualFrame.Frames[0].Width);
            Assert.AreEqual(15, result.AnimationVisualFrame.Frames[0].Height);
        }

        [TestMethod]
        public void Given10x10NewSize15x15AndScale_WhenResizeIfNeeded_ThenNewSizeIs15x15()
        {
            var builder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.Blue);
            builder.Resize(new Size(10, 10));
            var frame = new GridVisualFrame(builder);
            var animatedVisualFrame = new GridAnimatedVisualFrame([frame], AnimationOptions.Single);
            var value = new Animation(string.Empty, string.Empty, animatedVisualFrame);

            var result = value.ResizeIfNeeded(new Size(15, 15), VisualResizeMode.Scale);

            Assert.AreEqual(15, result.AnimationVisualFrame.Frames[0].Width);
            Assert.AreEqual(15, result.AnimationVisualFrame.Frames[0].Height);
        }

        [TestMethod]
        public void Given10x10NewSize5x5AndCrop_WhenResizeIfNeeded_ThenNewSizeIs5x5()
        {
            var builder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.Blue);
            builder.Resize(new Size(10, 10));
            var frame = new GridVisualFrame(builder);
            var animatedVisualFrame = new GridAnimatedVisualFrame([frame], AnimationOptions.Single);
            var value = new Animation(string.Empty, string.Empty, animatedVisualFrame);

            var result = value.ResizeIfNeeded(new Size(5, 5), VisualResizeMode.Crop);

            Assert.AreEqual(5, result.AnimationVisualFrame.Frames[0].Width);
            Assert.AreEqual(5, result.AnimationVisualFrame.Frames[0].Height);
        }

        [TestMethod]
        public void Given10x10NewSize15x15AndScaleDown_WhenResizeIfNeeded_ThenNewSizeIs10x10()
        {
            var builder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.Blue);
            builder.Resize(new Size(10, 10));
            var frame = new GridVisualFrame(builder);
            var animatedVisualFrame = new GridAnimatedVisualFrame([frame], AnimationOptions.Single);
            var value = new Animation(string.Empty, string.Empty, animatedVisualFrame);

            var result = value.ResizeIfNeeded(new Size(15, 15), VisualResizeMode.ScaleDown);

            Assert.AreEqual(10, result.AnimationVisualFrame.Frames[0].Width);
            Assert.AreEqual(10, result.AnimationVisualFrame.Frames[0].Height);
        }

        [TestMethod]
        public void Given15x15NewSize10x10AndScaleDown_WhenResizeIfNeeded_ThenNewSizeIs10x10()
        {
            var builder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.Blue);
            builder.Resize(new Size(15, 15));
            var frame = new GridVisualFrame(builder);
            var animatedVisualFrame = new GridAnimatedVisualFrame([frame], AnimationOptions.Single);
            var value = new Animation(string.Empty, string.Empty, animatedVisualFrame);

            var result = value.ResizeIfNeeded(new Size(10, 10), VisualResizeMode.ScaleDown);

            Assert.AreEqual(10, result.AnimationVisualFrame.Frames[0].Width);
            Assert.AreEqual(10, result.AnimationVisualFrame.Frames[0].Height);
        }
    }
}
