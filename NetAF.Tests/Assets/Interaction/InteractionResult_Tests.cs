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
            var instance = new InteractionResult(InteractionEffect.NoEffect, null, "A");

            var result = instance.Description;

            Assert.AreEqual("A", result);
        }

        [TestMethod]
        public void GivenConstructor_WhenFatal_ThenGeneratedDescriptionIsCorrect()
        {
            var instance = new InteractionResult(InteractionEffect.FatalEffect, null);

            var result = instance.Description;

            Assert.AreEqual("There was a fatal effect.", result);
        }

        [TestMethod]
        public void GivenConstructor_WhenItemUsedUp_ThenGeneratedDescriptionIsCorrect()
        {
            var instance = new InteractionResult(InteractionEffect.ItemUsedUp, null);

            var result = instance.Description;

            Assert.AreEqual("The item was used up.", result);
        }

        [TestMethod]
        public void GivenConstructor_WhenNoEffect_ThenGeneratedDescriptionIsCorrect()
        {
            var instance = new InteractionResult(InteractionEffect.NoEffect, null);

            var result = instance.Description;

            Assert.AreEqual("There was no effect.", result);
        }

        [TestMethod]
        public void GivenConstructor_WhenSelfContained_ThenGeneratedDescriptionIsCorrect()
        {
            var instance = new InteractionResult(InteractionEffect.SelfContained, null);

            var result = instance.Description;

            Assert.AreEqual("The effect was self contained.", result);
        }

        [TestMethod]
        public void GivenConstructor_WhenTargetUsedUp_ThenGeneratedDescriptionIsCorrect()
        {
            var instance = new InteractionResult(InteractionEffect.TargetUsedUp, null);

            var result = instance.Description;

            Assert.AreEqual("The target was used up.", result);
        }
    }
}