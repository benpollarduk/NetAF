using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;

namespace NetAF.Tests.Assets
{
    [TestClass]
    public class Interaction_Tests
    {
        [TestMethod]
        public void GivenConstructor_WhenExplicitDescription_ThenDescriptionIsAsSpecified()
        {
            Interaction instance = new(InteractionResult.NeitherItemOrTargetExpired, null, "A");

            var result = instance.Description;

            Assert.AreEqual("A", result);
        }

        [TestMethod]
        public void GivenConstructor_WhenItemUsedUp_ThenGeneratedDescriptionIsCorrect()
        {
            Interaction instance = new(InteractionResult.ItemExpired, null);

            var result = instance.Description;

            Assert.AreEqual("The item expired.", result);
        }

        [TestMethod]
        public void GivenConstructor_WhenItemAndTargetExpired_ThenGeneratedDescriptionIsCorrect()
        {
            Interaction instance = new(InteractionResult.ItemAndTargetExpired, null);

            var result = instance.Description;

            Assert.AreEqual("Both the item and target expired.", result);
        }

        [TestMethod]
        public void GivenConstructor_WhenNeitherItemOrTargetExpired_ThenGeneratedDescriptionIsCorrect()
        {
            Interaction instance = new(InteractionResult.NeitherItemOrTargetExpired, null);

            var result = instance.Description;

            Assert.AreEqual("There was no effect.", result);
        }

        [TestMethod]
        public void GivenConstructor_WhenTargetExpired_ThenGeneratedDescriptionIsCorrect()
        {
            Interaction instance = new(InteractionResult.TargetExpired, null);

            var result = instance.Description;

            Assert.AreEqual("The target expired.", result);
        }
    }
}