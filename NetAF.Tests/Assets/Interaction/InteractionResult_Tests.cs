using NetAF.Assets.Interaction;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetAF.Tests.Assets.Interaction
{
    [TestClass]
    public class InteractionResult_Tests
    {
        [TestMethod]
        public void GivenConstructor_WhenExplicitDescription_ThenDescriptionIsAsSpecified()
        {
            var instance = new InteractionResult(InteractionEffect.NeitherItemOrTargetExpired, null, "A");

            var result = instance.Description;

            Assert.AreEqual("A", result);
        }

        [TestMethod]
        public void GivenConstructor_WhenItemUsedUp_ThenGeneratedDescriptionIsCorrect()
        {
            var instance = new InteractionResult(InteractionEffect.ItemExpired, null);

            var result = instance.Description;

            Assert.AreEqual("The item expired.", result);
        }

        [TestMethod]
        public void GivenConstructor_WhenItemAndTargetExpired_ThenGeneratedDescriptionIsCorrect()
        {
            var instance = new InteractionResult(InteractionEffect.ItemAndTargetExpired, null);

            var result = instance.Description;

            Assert.AreEqual("Both the item and target expired.", result);
        }

        [TestMethod]
        public void GivenConstructor_WhenNeitherItemOrTargetExpired_ThenGeneratedDescriptionIsCorrect()
        {
            var instance = new InteractionResult(InteractionEffect.NeitherItemOrTargetExpired, null);

            var result = instance.Description;

            Assert.AreEqual("There was no effect.", result);
        }

        [TestMethod]
        public void GivenConstructor_WhenTargetExpired_ThenGeneratedDescriptionIsCorrect()
        {
            var instance = new InteractionResult(InteractionEffect.TargetExpired, null);

            var result = instance.Description;

            Assert.AreEqual("The target expired.", result);
        }
    }
}