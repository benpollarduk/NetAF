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
            Interaction instance = new(InteractionResult.NoChange, null, "A");

            var result = instance.Description;

            Assert.AreEqual("A", result);
        }

        [TestMethod]
        public void GivenConstructor_WhenItemUsedUp_ThenGeneratedDescriptionIsCorrect()
        {
            Interaction instance = new(InteractionResult.ItemExpires, null);

            var result = instance.Description;

            Assert.AreEqual("The item expires.", result);
        }

        [TestMethod]
        public void GivenConstructor_WhenItemAndTargetExpires_ThenGeneratedDescriptionIsCorrect()
        {
            Interaction instance = new(InteractionResult.ItemAndTargetExpires, null);

            var result = instance.Description;

            Assert.AreEqual("Both the item and target expires.", result);
        }

        [TestMethod]
        public void GivenConstructor_WhenNoChange_ThenGeneratedDescriptionIsCorrect()
        {
            Interaction instance = new(InteractionResult.NoChange, null);

            var result = instance.Description;

            Assert.AreEqual("There was no effect.", result);
        }

        [TestMethod]
        public void GivenConstructor_WhenTargetExpires_ThenGeneratedDescriptionIsCorrect()
        {
            Interaction instance = new(InteractionResult.TargetExpires, null);

            var result = instance.Description;

            Assert.AreEqual("The target expires.", result);
        }

        [TestMethod]
        public void GivenConstructor_WhenPlayerDies_ThenGeneratedDescriptionIsCorrect()
        {
            Interaction instance = new(InteractionResult.PlayerDies, null);

            var result = instance.Description;

            Assert.AreEqual("The player dies.", result);
        }

        [TestMethod]
        public void GivenConstructor_WhenPlayerReceivesItem_ThenGeneratedDescriptionIsCorrect()
        {
            Interaction instance = new(InteractionResult.PlayerReceivesItem, null);

            var result = instance.Description;

            Assert.AreEqual("The player receives the item.", result);
        }

        [TestMethod]
        public void GivenConstructor_WhenNonPlayableCharacterReceivesItem_ThenGeneratedDescriptionIsCorrect()
        {
            Interaction instance = new(InteractionResult.NonPlayableCharacterReceivesItem, null);

            var result = instance.Description;

            Assert.AreEqual("A non-playable character receives the item.", result);
        }
    }
}