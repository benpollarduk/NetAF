using System;
using NetAF.Assets;
using NetAF.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetAF.Tests.Extensions
{
    [TestClass]
    public class ArrayExtensions_Tests
    {
        [TestMethod]
        public void GivenOneElement_WhenRemoveThatElement_ThenNotElements()
        {
            var value = new[] { new Item(string.Empty, string.Empty) };

            var result = value.Remove(value[0]);

            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void GivenOneElement_WhenRemoveADifferentElement_Then1Element()
        {
            var value = new[] { new Item(string.Empty, string.Empty) };

            var result = value.Remove(new(string.Empty, string.Empty));

            Assert.AreEqual(1, result.Length);
        }

        [TestMethod]
        public void GivenEmptyArray_WhenRemove_ThenNotNull()
        {
            var value = Array.Empty<Item>();

            var result = value.Remove(new(string.Empty, string.Empty));

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GivenEmptyArray_WhenAdd_ThenOneElement()
        {
            var value = Array.Empty<Item>();

            var result = value.Remove(new(string.Empty, string.Empty));

            Assert.AreEqual(0, result.Length);
        }
    }
}
