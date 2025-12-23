using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Narratives;

namespace NetAF.Tests.Narratives
{
    [TestClass]
    public class Narrative_Tests
    {
        [TestMethod]
        public void GivenNull_WhenIsComplete_ThenReturnTrue()
        {
            var narrative = new Narrative(string.Empty, null);

            var result = narrative.IsComplete;

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenOneSection_WhenIsComplete_ThenReturnFalse()
        {
            var narrative = new Narrative(string.Empty, [new Section([string.Empty])]);

            var result = narrative.IsComplete;

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenTwoSections_WhenIsComplete_ThenReturnFalse()
        {
            var narrative = new Narrative(string.Empty, [new Section([string.Empty]), new Section([string.Empty])]);

            var result = narrative.IsComplete;

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenOneSectionAndNextCalledTwice_WhenIsComplete_ThenReturnTrue()
        {
            var narrative = new Narrative(string.Empty, [new Section([string.Empty])]);
            narrative.Next();
            narrative.Next();

            var result = narrative.IsComplete;

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenFirstSectionIsABC_WhenNext_ThenReturnABC()
        {
            var narrative = new Narrative(string.Empty, [new Section(["ABC"])]);

            var result = narrative.Next();

            Assert.AreEqual("ABC", result);
        }

        [TestMethod]
        public void GivenFirstSectionIsABCSecondIsDEF_WhenAllUntilCurrent_ThenReturnABC()
        {
            var narrative = new Narrative(string.Empty, [new Section(["ABC"]), new Section(["DEF"])]);

            var result = narrative.AllUntilCurrent();

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("ABC", result[0]);
        }

        [TestMethod]
        public void GivenFirstSectionIsABCSecondIsDEFAndNextCalledTwice_WhenAllUntilCurrent_ThenReturnDEF()
        {
            var narrative = new Narrative(string.Empty, [new Section(["ABC"]), new Section(["DEF"])]);
            narrative.Next();
            narrative.Next();

            var result = narrative.AllUntilCurrent();

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("DEF", result[0]);
        }
    }
}
