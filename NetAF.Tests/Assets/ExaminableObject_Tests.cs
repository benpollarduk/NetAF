using NetAF.Assets;
using NetAF.Assets.Interaction;
using NetAF.Commands;
using NetAF.Interpretation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetAF.Tests.Assets
{
    [TestClass]
    public class ExaminableObject_Tests
    {
        [TestMethod]
        public void GivenExamine_WhenDefaultExamination_ThenExaminableDescriptionIsIncluded()
        {
            var i = new Item("Test", "Test Description.");

            var result = i.Examine(ExaminationScene.NoScene);

            Assert.IsTrue(result.Description.Contains(i.Description.GetDescription()));
        }

        [TestMethod]
        public void GivenExamine_WhenDefaultExaminationWith1CustomCommand_ThenCustomCommandIsIncluded()
        {
            CustomCommand[] commands =
            [
                new CustomCommand(new CommandHelp("Test Command", "Test Command Description."), true, true, (_, _) => new Reaction(ReactionResult.OK, ""))
            ];

            var i = new Item("Test", "Test Description.", commands: commands);

            var result = i.Examine(ExaminationScene.NoScene);

            Assert.IsTrue(result.Description.Contains(i.Commands[0].Help.Command));
        }

        [TestMethod]
        public void GivenExamine_WhenDefaultExaminationWith2CustomCommands_ThenCustomCommandsAreBothIncluded()
        {
            CustomCommand[] commands =
            [
                new CustomCommand(new CommandHelp("A*", "Test Command Description."), true, true, (_, _) => new Reaction(ReactionResult.OK, "")),
                new CustomCommand(new CommandHelp("B*", "Test Command Description."), true, true, (_, _) => new Reaction(ReactionResult.OK, ""))
            ];

            var i = new Item("Test", "Test Description.", commands: commands);

            var result = i.Examine(ExaminationScene.NoScene);

            Assert.IsTrue(result.Description.Contains(i.Commands[0].Help.Command));
            Assert.IsTrue(result.Description.Contains(i.Commands[1].Help.Command));
        }

        [TestMethod]
        public void GivenExamine_WhenNoDescription_ThenResultIncludesIdentifierName()
        {
            var i = new Item("Test", string.Empty);

            var result = i.Examine(ExaminationScene.NoScene);

            Assert.IsTrue(result.Description.Contains("Test"));
        }

        [TestMethod]
        public void GivenExamine_WhenNoDescriptionOrIdentifierName_ThenResultIncludesClassName()
        {
            var i = new Item(string.Empty, string.Empty);

            var result = i.Examine(ExaminationScene.NoScene);

            Assert.IsTrue(result.Description.Contains("Item"));
        }

        [TestMethod]
        public void GivenExamine_WhenSomeAttributes_ThenResultIncludesAttributeName()
        {
            var i = new Item("Test", string.Empty);
            i.Attributes.Add("Attribute", 1);

            var result = i.Examine(ExaminationScene.NoScene);

            Assert.IsTrue(result.Description.Contains("Attribute"));
        }
    }
}
