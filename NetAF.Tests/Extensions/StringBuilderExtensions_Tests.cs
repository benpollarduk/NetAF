using NetAF.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace NetAF.Tests.Extensions
{
    [TestClass]
    public class StringBuilderExtensions_Tests
    {
        [TestMethod]
        public void GivenEmptyString_WhenEnsureFinishedSentence_ThenEmptyString()
        {
            var builder = new StringBuilder("");
            builder.EnsureFinishedSentence();

            Assert.AreEqual("", builder.ToString());
        }

        [TestMethod]
        public void GivenA_WhenEnsureFinishedSentence_ThenAStop()
        {
            var builder = new StringBuilder("A");
            builder.EnsureFinishedSentence();

            Assert.AreEqual("A.", builder.ToString());
        }

        [TestMethod]
        public void GivenAStop_WhenEnsureFinishedSentence_ThenAStop()
        {
            var builder = new StringBuilder("A.");
            builder.EnsureFinishedSentence();

            Assert.AreEqual("A.", builder.ToString());
        }

        [TestMethod]
        public void GivenAQuestion_WhenEnsureFinishedSentence_ThenAQuestion()
        {
            var builder = new StringBuilder("A?");
            builder.EnsureFinishedSentence();

            Assert.AreEqual("A?", builder.ToString());
        }

        [TestMethod]
        public void GivenAExclamation_WhenEnsureFinishedSentence_ThenAExclamation()
        {
            var builder = new StringBuilder("A!");
            builder.EnsureFinishedSentence();

            Assert.AreEqual("A!", builder.ToString());
        }

        [TestMethod]
        public void GivenAComma_WhenEnsureFinishedSentence_ThenAStop()
        {
            var builder = new StringBuilder("A,");
            builder.EnsureFinishedSentence();

            Assert.AreEqual("A.", builder.ToString());
        }
    }
}
