﻿using NetAF.Assets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetAF.Tests.Assets
{
    [TestClass]
    public class ConditionalDescription_Tests
    {
        [TestMethod]
        public void GivenGetDescription_WhenNull_ThenReturnFalseDescription()
        {
            var conditional = new ConditionalDescription("A", "B", null);

            var result = conditional.GetDescription();

            Assert.AreEqual("B", result);
        }

        [TestMethod]
        public void GivenGetDescription_WhenTrue_ThenReturnTrueDescription()
        {
            var conditional = new ConditionalDescription("A", "B", () => true);

            var result = conditional.GetDescription();

            Assert.AreEqual("A", result);
        }

        [TestMethod]
        public void GivenGetDescription_WhenFalse_ThenReturnFalseDescription()
        {
            var conditional = new ConditionalDescription("A", "B", () => false);

            var result = conditional.GetDescription();

            Assert.AreEqual("B", result);
        }
    }
}
