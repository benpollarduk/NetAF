using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Assets.Attributes;
using NetAF.Assets.Interaction;
using NetAF.Commands;
using NetAF.Serialization.Assets;

namespace NetAF.Tests.Serialization.Assets
{
    [TestClass]
    public class ExaminableSerialization_Tests
    {
        [TestMethod]
        public void GivenIdentifierIsA_ThenIdentifierIsA()
        {
            Item examinable = new("A", string.Empty);

            ExaminableSerialization result = new(examinable);

            Assert.AreEqual("A", result.Identifier);
        }

        [TestMethod]
        public void GivenIsPlayerVisibleIsFalse_ThenIsPlayerVisibleIsFalse()
        {
            Item examinable = new(string.Empty, string.Empty) { IsPlayerVisible = false };

            ExaminableSerialization result = new(examinable);

            Assert.IsFalse(result.IsPlayerVisible);
        }

        [TestMethod]
        public void GivenIsPlayerVisibleIsTrue_ThenIsPlayerVisibleIsTrue()
        {
            Item examinable = new(string.Empty, string.Empty) { IsPlayerVisible = true };

            ExaminableSerialization result = new(examinable);

            Assert.IsTrue(result.IsPlayerVisible);
        }

        [TestMethod]
        public void GivenNoAttributes_ThenAttributeNotNull()
        {
            Item examinable = new(string.Empty, string.Empty);

            ExaminableSerialization result = new(examinable);

            Assert.IsNotNull(result.AttributeManager);
        }

        [TestMethod]
        public void Given1CustomCommand_ThenCustomCommandsHas1Element()
        {
            CustomCommand command = new(new CommandHelp("A", string.Empty), true, true, (_, _) => new Reaction(ReactionResult.OK, string.Empty));
            Item examinable = new(string.Empty, string.Empty, commands: [command]);

            ExaminableSerialization result = new(examinable);

            Assert.AreEqual(1, result.Commands.Length);
        }

        [TestMethod]
        public void GivenAnExaminable_WhenRestoreFrom_ThenIsPlayerVisibleSetCorrectly()
        {
            Item item = new(string.Empty, string.Empty) { IsPlayerVisible = false };
            Item item2 = new(string.Empty, string.Empty) { IsPlayerVisible = true };
            ExaminableSerialization serialization = new(item2);

            serialization.Restore(item);

            Assert.IsTrue(item.IsPlayerVisible);
        }

        [TestMethod]
        public void GivenAnExaminable_WhenRestoreFrom_ThenAttributesSetCorrectly()
        {
            Item item = new(string.Empty, string.Empty);
            Item item2 = new(string.Empty, string.Empty);
            item2.Attributes.Add(new Attribute(string.Empty, string.Empty, 0, 1), 1);
            ExaminableSerialization serialization = new(item2);

            serialization.Restore(item);

            Assert.AreEqual(1, item.Attributes.Count);
        }
    }
}
