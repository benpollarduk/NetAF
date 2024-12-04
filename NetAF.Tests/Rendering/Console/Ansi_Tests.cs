using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Rendering.Console;
using System;

namespace NetAF.Tests.Rendering.Console
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
        public void GivenNoColorEnvironmentVariableSetTo0_WhenIsColorSuppressed_ThenReturnTrue()
        {
            Environment.SetEnvironmentVariable(Ansi.NO_COLOR, "0");

            var result = Ansi.IsColorSuppressed();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenNoColorEnvironmentVariableSetToFalse_WhenIsColorSuppressed_ThenReturnTrue()
        {
            Environment.SetEnvironmentVariable(Ansi.NO_COLOR, "False");

            var result = Ansi.IsColorSuppressed();

            Assert.IsTrue(result);
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
    }
}
