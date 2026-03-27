using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Targets.Console.Rendering;

namespace NetAF.Tests.Targets.Console.Rendering
{
    [TestClass]
    public class AnsiColor_Tests
    {
        [TestMethod]
        public void GivenBlackAndBlack_WhenEquals_ThenReturnTrue()
        {
            var a = AnsiColor.Black;
            var b = AnsiColor.Black;

            var result = a.Equals(b);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenBlackAndWhite_WhenEquals_ThenReturnFalse()
        {
            var a = AnsiColor.Black;
            var b = AnsiColor.White;

            var result = a.Equals(b);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenBlackAndWhite_WhenEquality_ThenReturnFalse()
        {
            var a = AnsiColor.Black;
            var b = AnsiColor.White;

            var result = a == b;

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenBlackAndWhite_WhenNotEquality_ThenReturnTrue()
        {
            var a = AnsiColor.Black;
            var b = AnsiColor.White;

            var result = a != b;

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenR10G15B20_WhenToGray_ThenReturnColorWithR15G15B15()
        {
            var result = new AnsiColor(10, 15, 20).ToGray();

            Assert.AreEqual(15, result.R);
            Assert.AreEqual(15, result.G);
            Assert.AreEqual(15, result.B);
        }

        [TestMethod]
        public void GivenNull_WhenFromString_ThenReturnDefault()
        {
            var result = AnsiColor.FromString(null);

            Assert.AreEqual(default(AnsiColor).R, result.R);
            Assert.AreEqual(default(AnsiColor).G, result.G);
            Assert.AreEqual(default(AnsiColor).B, result.B);
        }

        [TestMethod]
        public void GivenEmptyString_WhenFromString_ThenReturnDefault()
        {
            var result = AnsiColor.FromString(string.Empty);

            Assert.AreEqual(default(AnsiColor).R, result.R);
            Assert.AreEqual(default(AnsiColor).G, result.G);
            Assert.AreEqual(default(AnsiColor).B, result.B);
        }

        [TestMethod]
        public void GivenIncorrectFormat_WhenFromString_ThenReturnDefault()
        {
            var result = AnsiColor.FromString("sausages");

            Assert.AreEqual(default(AnsiColor).R, result.R);
            Assert.AreEqual(default(AnsiColor).G, result.G);
            Assert.AreEqual(default(AnsiColor).B, result.B);
        }

        [TestMethod]
        public void GivenR1G2B3_WhenFromString_ThenReturnR1G2B3()
        {
            var result = AnsiColor.FromString("1-2-3");

            Assert.AreEqual(1, result.R);
            Assert.AreEqual(2, result.G);
            Assert.AreEqual(3, result.B);
        }
    }
}
