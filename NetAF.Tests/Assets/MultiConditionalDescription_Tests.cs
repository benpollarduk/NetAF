using NetAF.Assets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetAF.Tests.Assets
{
    [TestClass]
    public class MultiConditionalDescription_Tests
    {
        [TestMethod]
        public void GivenGetDescription_WhenNull_ThenReturnFallbackDescription()
        {
            var conditional = new MultiConditionalDescription("A", null);

            var result = conditional.GetDescription();

            Assert.AreEqual("A", result);
        }

        [TestMethod]
        public void GivenGetDescription_WhenCondition1IsTrue_ThenReturnCondition1Description()
        {
            var conditional = new MultiConditionalDescription("A", new DescribedCondition(() => true, "B"));

            var result = conditional.GetDescription();

            Assert.AreEqual("B", result);
        }

        [TestMethod]
        public void GivenGetDescription_WhenAllConditionsAreFalse_ThenReturnFallbackDescription()
        {
            var conditional = new MultiConditionalDescription("A", new DescribedCondition(() => false, "B"));

            var result = conditional.GetDescription();

            Assert.AreEqual("A", result);
        }
    }
}
