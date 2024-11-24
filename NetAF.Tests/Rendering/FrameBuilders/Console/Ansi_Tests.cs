using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Rendering.FrameBuilders.Console;
using System;

namespace NetAF.Tests.Rendering.FrameBuilders.Console
{
    [TestClass]
    public class Ansi_Tests
    {
        [TestMethod]
        public void GivenNoColorEnvironmentVariableSetToEmptyString_WhenIsColorSuppressed_ThenReturnFalse()
        {
            Environment.SetEnvironmentVariable(Ansi.NO_COLOR, "");

            var result = Ansi.IsColorSuppressed();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenNoColorEnvironmentVariableSetTo0_WhenIsColorSuppressed_ThenReturnFalse()
        {
            Environment.SetEnvironmentVariable(Ansi.NO_COLOR, "0");

            var result = Ansi.IsColorSuppressed();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenNoColorEnvironmentVariableSetToFalse_WhenIsColorSuppressed_ThenReturnFalse()
        {
            Environment.SetEnvironmentVariable(Ansi.NO_COLOR, "False");

            var result = Ansi.IsColorSuppressed();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenNoColorEnvironmentVariableSetTo1_WhenIsColorSuppressed_ThenReturnTrue()
        {
            Environment.SetEnvironmentVariable(Ansi.NO_COLOR, "1");

            var result = Ansi.IsColorSuppressed();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenNoColorEnvironmentVariableSetToTrue_WhenIsColorSuppressed_ThenReturnTrue()
        {
            Environment.SetEnvironmentVariable(Ansi.NO_COLOR, "True");

            var result = Ansi.IsColorSuppressed();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenRed_WhenFindNearestAnsiColor_ThenReturnAnsiColorBrightRed()
        {
            var result = Ansi.FindNearestAnsiColor(255, 0, 0);

            Assert.AreEqual(AnsiColor.BrightRed, result);
        }

        [TestMethod]
        public void GivenGreen_WhenFindNearestAnsiColor_ThenReturnAnsiColorBrightGreen()
        {
            var result = Ansi.FindNearestAnsiColor(0, 255, 0);

            Assert.AreEqual(AnsiColor.BrightGreen, result);
        }

        [TestMethod]
        public void GivenBlue_WhenFindNearestAnsiColor_ThenReturnAnsiColorBrightBlue()
        {
            var result = Ansi.FindNearestAnsiColor(0, 0, 255);

            Assert.AreEqual(AnsiColor.BrightBlue, result);
        }

        [TestMethod]
        public void GivenWhite_WhenFindNearestAnsiColor_ThenReturnAnsiColorBrightWhite()
        {
            var result = Ansi.FindNearestAnsiColor(255, 255, 255);

            Assert.AreEqual(AnsiColor.BrightWhite, result);
        }

        [TestMethod]
        public void GivenBlack_WhenFindNearestAnsiColor_ThenReturnAnsiColorBlack()
        {
            var result = Ansi.FindNearestAnsiColor(0, 0, 0);

            Assert.AreEqual(AnsiColor.Black, result);
        }
    }
}
