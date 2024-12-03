using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Rendering.Console;

namespace NetAF.Tests.Rendering.Console
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
    }
}
