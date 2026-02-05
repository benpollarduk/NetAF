using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Assets.Attributes;
using NetAF.Commands;
using NetAF.Serialization;
using NetAF.Serialization.Assets;

namespace NetAF.Tests.Serialization.Assets
{
    [TestClass]
    public class ExaminableSerialization_Tests
    {
        [TestMethod]
        public void GivenIdentifierIsA_WhenFromIExaminable_ThenIdentifierIsA()
        {
            Item examinable = new("A", string.Empty);

            ExaminableSerialization result = ExaminableSerialization.FromIExaminable(examinable);

            Assert.AreEqual("A", result.Identifier);
        }

        [TestMethod]
        public void GivenIsPlayerVisibleIsFalse_WhenFromIExaminable_ThenIsPlayerVisibleIsFalse()
        {
            Item examinable = new(string.Empty, string.Empty) { IsPlayerVisible = false };

            ExaminableSerialization result = ExaminableSerialization.FromIExaminable(examinable);

            Assert.IsFalse(result.IsPlayerVisible);
        }

        [TestMethod]
        public void GivenIsPlayerVisibleIsTrue_WhenFromIExaminable_ThenIsPlayerVisibleIsTrue()
        {
            Item examinable = new(string.Empty, string.Empty) { IsPlayerVisible = true };

            ExaminableSerialization result = ExaminableSerialization.FromIExaminable(examinable);

            Assert.IsTrue(result.IsPlayerVisible);
        }

        [TestMethod]
        public void GivenNoAttributes_WhenFromIExaminable_ThenAttributeNotNull()
        {
            Item examinable = new(string.Empty, string.Empty);

            ExaminableSerialization result = ExaminableSerialization.FromIExaminable(examinable);

            Assert.IsNotNull(result.AttributeManager);
        }

        [TestMethod]
        public void Given1CustomCommand_WhenFromIExaminable_ThenCustomCommandsHas1Element()
        {
            CustomCommand command = new(new CommandHelp("A", string.Empty), true, true, (_, _) => new Reaction(ReactionResult.Inform, string.Empty));
            Item examinable = new(string.Empty, string.Empty, commands: [command]);

            ExaminableSerialization result = ExaminableSerialization.FromIExaminable(examinable);

            Assert.HasCount(1, result.Commands);
        }

        [TestMethod]
        public void GivenAnExaminable_WhenRestoreFrom_ThenIsPlayerVisibleSetCorrectly()
        {
            Item item = new(string.Empty, string.Empty) { IsPlayerVisible = false };
            Item item2 = new(string.Empty, string.Empty) { IsPlayerVisible = true };
            ExaminableSerialization serialization = ExaminableSerialization.FromIExaminable(item2);

            ((IObjectSerialization<IExaminable>)serialization).Restore(item);

            Assert.IsTrue(item.IsPlayerVisible);
        }

        [TestMethod]
        public void GivenAnExaminable_WhenRestoreFrom_ThenAttributesSetCorrectly()
        {
            Item item = new(string.Empty, string.Empty);
            Item item2 = new(string.Empty, string.Empty);
            item2.Attributes.Add(new Attribute(string.Empty, string.Empty, 0, 1, true), 1);
            ExaminableSerialization serialization = ExaminableSerialization.FromIExaminable(item2);

            ((IObjectSerialization<IExaminable>)serialization).Restore(item);

            Assert.AreEqual(1, item.Attributes.Count);
        }
    }
}
