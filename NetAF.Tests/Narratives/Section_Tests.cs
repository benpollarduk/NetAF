using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Narratives;

namespace NetAF.Tests.Narratives
{
    [TestClass]
    public class Section_Tests
    {
        [TestMethod]
        public void GivenNull_WhenIsComplete_ThenReturnTrue()
        {
            var section = new Section(null);

            var result = section.IsComplete;

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenOneElement_WhenIsComplete_ThenReturnFalse()
        {
            var section = new Section([string.Empty]);

            var result = section.IsComplete;

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenTwoElements_WhenIsComplete_ThenReturnFalse()
        {
            var section = new Section([string.Empty, string.Empty]);

            var result = section.IsComplete;

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenOneElementAndNextCalled_WhenIsComplete_ThenReturnTrue()
        {
            var section = new Section([string.Empty]);
            section.Next();

            var result = section.IsComplete;

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenFirstElementIsABC_WhenNext_ThenReturnABC()
        {
            var section = new Section(["ABC"]);

            var result = section.Next();

            Assert.AreEqual("ABC", result);
        }

        [TestMethod]
        public void GivenFirstElementIsABCSecondIsDEF_WhenAllUntilCurrent_ThenReturnABC()
        {
            var section = new Section(["ABC", "DEF"]);

            var result = section.AllUntilCurrent();

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("ABC", result[0]);
        }

        [TestMethod]
        public void GivenFirstElementIsABCSecondIsDEFAndNextCalledTwice_WhenAllUntilCurrent_ThenReturnABCAndDEF()
        {
            var section = new Section(["ABC", "DEF"]);
            section.Next();
            section.Next();

            var result = section.AllUntilCurrent();

            Assert.AreEqual(2, result.Length);
            Assert.AreEqual("ABC", result[0]);
            Assert.AreEqual("DEF", result[1]);
        }
    }
}
