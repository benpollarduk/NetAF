using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Targets.Markup;
using System;

namespace NetAF.Tests.Targets.Markup
{
    [TestClass]
    public class Color_Tests
    {
        [TestMethod]
        public void GivenNull_WhenFromHtml_ThenArgumentExceptionThrown()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Color.FromHtml(null);
            });
        }

        [TestMethod]
        public void GivenEmptyString_WhenFromHtml_ThenArgumentExceptionThrown()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Color.FromHtml(string.Empty);
            });
        }

        [TestMethod]
        public void GivenNonHex_WhenFromHtml_ThenFormatExceptionThrown()
        {
            Assert.Throws<FormatException>(() =>
            {
                Color.FromHtml("chumps");
            });
        }

        [TestMethod]
        public void GivenTooShortHex_WhenFromHtml_ThenArgumentExceptionThrown()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Color.FromHtml("FF");
            });
        }

        [TestMethod]
        public void GivenTooLongHex_WhenFromHtml_ThenArgumentExceptionThrown()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Color.FromHtml("FF");
            });
        }

        [TestMethod]
        public void Given010203_WhenFromHtml_ThenRed1Green2Blue3()
        {
            var hex = "010203";

            var result = Color.FromHtml(hex);

            Assert.AreEqual(1, result.Red);
            Assert.AreEqual(2, result.Green);
            Assert.AreEqual(3, result.Blue);
        }

        [TestMethod]
        public void GivenHash010203_WhenFromHtml_ThenRed1Green2Blue3()
        {
            var hex = "#010203";

            var result = Color.FromHtml(hex);

            Assert.AreEqual(1, result.Red);
            Assert.AreEqual(2, result.Green);
            Assert.AreEqual(3, result.Blue);
        }
    }
}
