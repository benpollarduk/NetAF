using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Rendering.Console;
using NetAF.Utilities;

namespace NetAF.Tests.Rendering.Console
{
    [TestClass]
    public class Texture_Tests
    {
        [TestMethod]
        public void GivenASingleCharacter_ThenWidth1Height1()
        {
            Texture t = new("A");

            Assert.AreEqual(1, t.Width);
            Assert.AreEqual(1, t.Height);
        }

        [TestMethod]
        public void GivenTwoCharacters_ThenWidth2Height1()
        {
            Texture t = new("AB");

            Assert.AreEqual(2, t.Width);
            Assert.AreEqual(1, t.Height);
        }

        [TestMethod]
        public void GivenTwoCharactersNewlineThenTwoCharacters_ThenWidth2Height2()
        {
            Texture t = new("AB\nCD");

            Assert.AreEqual(2, t.Width);
            Assert.AreEqual(2, t.Height);
        }

        [TestMethod]
        public void GivenX0Y0IsA_WhenGetX0Y0_ThenReturnA()
        {
            Texture t = new("AB\nCD");

            var result = t[0, 0];

            Assert.AreEqual('A', result);
        }


        [TestMethod]
        public void GivenX1Y0IsB_WhenGetX1Y0_ThenReturnB()
        {
            Texture t = new("AB\nCD");

            var result = t[1, 0];

            Assert.AreEqual('B', result);
        }

        [TestMethod]
        public void GivenX0Y1IsC_WhenGetX0Y1_ThenReturnC()
        {
            Texture t = new("AB\nCD");

            var result = t[0, 1];

            Assert.AreEqual('C', result);
        }

        [TestMethod]
        public void GivenX1Y1IsD_WhenGetX1Y1_ThenReturnD()
        {
            Texture t = new("AB\nCD");

            var result = t[1, 1];

            Assert.AreEqual('D', result);
        }

        [TestMethod]
        public void GivenLopsidedWidth_WhenGetUnusedCell_ThenReturnSpace()
        {
            Texture t = new("AB\nC");

            var result = t[1, 1];

            Assert.AreEqual(' ', result);
        }

        [TestMethod]
        public void GivenW2H2_WhenGetOutOfBounds_ThenReturnSpace()
        {
            Texture t = new("AB\nCD");

            var result = t[100, 100];

            Assert.AreEqual(' ', result);
        }

        [TestMethod]
        public void GivenABNewlineCD_WhenToString_ThenReturnABNewlineCD()
        {
            var original = $"AB{StringUtilities.Newline}CD";
            Texture t = new(original);

            var result = t.ToString();

            Assert.AreEqual(original, result);
        }
    }
}
