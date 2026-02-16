using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Narratives;
using NetAF.Rendering;
using NetAF.Targets.Console.Rendering;

namespace NetAF.Tests.Narratives
{
    [TestClass]
    public class VisualNarrative_Tests
    {
        [TestMethod]
        public void GivenNull_WhenIsComplete_ThenReturnTrue()
        {
            var narrative = new VisualNarrative(null);

            var result = narrative.IsComplete;

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenOneVisual_WhenIsComplete_ThenReturnFalse()
        {
            var narrative = new VisualNarrative([new Visual(string.Empty, string.Empty, new GridVisualBuilder(AnsiColor.Black, AnsiColor.White))]);

            var result = narrative.IsComplete;

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenTwoVisuals_WhenIsComplete_ThenReturnFalse()
        {
            var narrative = new VisualNarrative([new Visual(string.Empty, string.Empty, new GridVisualBuilder(AnsiColor.Black, AnsiColor.White)), new Visual(string.Empty, string.Empty, new GridVisualBuilder(AnsiColor.Black, AnsiColor.White))]);

            var result = narrative.IsComplete;

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenOneVisualAndNextCalledTwice_WhenIsComplete_ThenReturnTrue()
        {
            var narrative = new VisualNarrative([new Visual(string.Empty, string.Empty, new GridVisualBuilder(AnsiColor.Black, AnsiColor.White))]);
            narrative.Next();
            narrative.Next();

            var result = narrative.IsComplete;

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenFirstSectionIsVisual_WhenNext_ThenReturnVisual()
        {
            var visual = new Visual(string.Empty, string.Empty, new GridVisualBuilder(AnsiColor.Black, AnsiColor.White));
            var narrative = new VisualNarrative([visual]);

            var result = narrative.Next();

            Assert.AreEqual(visual, result);
        }
    }
}
