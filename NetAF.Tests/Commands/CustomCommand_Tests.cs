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

            Assert.IsEmpty(result);
        }

        [TestMethod]
        public void Given1Prompt_WhenGetPrompts_ThenReturnArrayWith1Entry()
        {
            var command = new CustomCommand(new CommandHelp("A", "B"), true, true, null);
            command.AddPrompt(new("A"));

            var result = command.GetPrompts(null);

            Assert.HasCount(1, result);
        }

        [TestMethod]
        public void Given1Prompt_WhenRemoveMatchingPrompt_ThenReturnEmptyArray()
        {
            var command = new CustomCommand(new CommandHelp("A", "B"), true, true, null);
            command.AddPrompt(new("A"));
            command.RemovePrompt(new("A"));

            var result = command.GetPrompts(null);

            Assert.IsEmpty(result);
        }

        [TestMethod]
        public void Given1Prompt_WhenRemoveNonMatchingPrompt_ThenReturnArrayWith1Entry()
        {
            var command = new CustomCommand(new CommandHelp("A", "B"), true, true, null);
            command.AddPrompt(new("A"));
            command.RemovePrompt(new("B"));

            var result = command.GetPrompts(null);

            Assert.HasCount(1, result);
        }

        [TestMethod]
        public void Given1Prompt_WhenClearPrompts_ThenReturnEmptyArray()
        {
            var command = new CustomCommand(new CommandHelp("A", "B"), true, true, null);
            command.AddPrompt(new("A"));

            command.ClearPrompts();
            var prompts = command.GetPrompts(null);

            Assert.IsEmpty(prompts);
        }

        [TestMethod]
        public void Given1Prompt_WhenClone_ThenPromptsAreCloned()
        {
            var command = new CustomCommand(new CommandHelp("A", "B"), true, true, null);
            command.AddPrompt(new("A"));

            var result = command.Clone() as CustomCommand;

            Assert.HasCount(1, result.GetPrompts(null));
        }
    }
}
