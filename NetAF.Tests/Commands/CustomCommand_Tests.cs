using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Commands;

namespace NetAF.Tests.Commands
{
    [TestClass]
    public class CustomCommand_Tests
    {
        [TestMethod]
        public void GivenNoPrompts_WhenGetPrompts_ThenReturnEmptyArray()
        {
            var command = new CustomCommand(new CommandHelp("A", "B"), true, true, null);

            var result = command.GetPrompts(null);

            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void Given1Prompt_WhenGetPrompts_ThenReturnArrayWith1Entry()
        {
            var command = new CustomCommand(new CommandHelp("A", "B"), true, true, null);
            command.AddPrompt(new("A"));

            var result = command.GetPrompts(null);

            Assert.AreEqual(1, result.Length);
        }

        [TestMethod]
        public void Given1Prompt_WhenRemoveMatchingPrompt_ThenReturnEmptyArray()
        {
            var command = new CustomCommand(new CommandHelp("A", "B"), true, true, null);
            command.AddPrompt(new("A"));
            command.RemovePrompt(new("A"));

            var result = command.GetPrompts(null);

            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void Given1Prompt_WhenRemoveNonMatchingPrompt_ThenReturnArrayWith1Entry()
        {
            var command = new CustomCommand(new CommandHelp("A", "B"), true, true, null);
            command.AddPrompt(new("A"));
            command.RemovePrompt(new("B"));

            var result = command.GetPrompts(null);

            Assert.AreEqual(1, result.Length);
        }
    }
}
