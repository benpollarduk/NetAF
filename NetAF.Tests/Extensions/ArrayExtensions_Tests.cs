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

            Assert.IsEmpty(result);
        }

        [TestMethod]
        public void GivenOneElement_WhenRemoveADifferentElement_Then1Element()
        {
            var value = new[] { new Item(string.Empty, string.Empty) };

            var result = value.Remove(new(string.Empty, string.Empty));

            Assert.HasCount(1, result);
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

            Assert.IsEmpty(result);
        }
    }
}
