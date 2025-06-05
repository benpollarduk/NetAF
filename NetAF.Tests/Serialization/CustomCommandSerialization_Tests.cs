using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Commands;
using NetAF.Serialization;

namespace NetAF.Tests.Serialization
{
    [TestClass]
    public class CustomCommandSerialization_Tests
    {
        [TestMethod]
        public void GivenCommandNameIsA_WhenFromCustomCommand_ThenCommandNameIsA()
        {
            CustomCommand command = new(new CommandHelp("A", string.Empty), true, true, (_, _) => new Reaction(ReactionResult.Inform, string.Empty));

            CustomCommandSerialization result = CustomCommandSerialization.FromCustomCommand(command);

            Assert.AreEqual("A", result.CommandName);
        }

        [TestMethod]
        public void GivenIsPlayerVisibleIsTrue_WhenFromCustomCommand_ThenCommandIsPlayerVisibleIsTrue()
        {
            CustomCommand command = new(new CommandHelp("A", string.Empty), true, true, (_, _) => new Reaction(ReactionResult.Inform, string.Empty));

            CustomCommandSerialization result = CustomCommandSerialization.FromCustomCommand(command);

            Assert.IsTrue(result.IsPlayerVisible);
        }

        [TestMethod]
        public void GivenAPrompt_WhenFromCustomCommand_ThenPromptIsSet()
        {
            CustomCommand command = new(new CommandHelp("A", string.Empty), true, true, (_, _) => new Reaction(ReactionResult.Inform, string.Empty));
            command.AddPrompt(new("A"));

            CustomCommandSerialization result = CustomCommandSerialization.FromCustomCommand(command);

            Assert.AreEqual("A", result.Prompts[0]);
        }

        [TestMethod]
        public void GivenACustomCommand_WhenRestoreFrom_ThenIsPlayerVisibleSetCorrectly()
        {
            CustomCommand command1 = new(new CommandHelp("A", string.Empty), true, true, (_, _) => new Reaction(ReactionResult.Inform, string.Empty));
            CustomCommand command2 = new(new CommandHelp("A", string.Empty), false, true, (_, _) => new Reaction(ReactionResult.Inform, string.Empty));

            CustomCommandSerialization serialization = CustomCommandSerialization.FromCustomCommand(command1);
            ((IObjectSerialization<CustomCommand>)serialization).Restore(command2);

            Assert.IsTrue(command2.IsPlayerVisible);
        }

        [TestMethod]
        public void GivenACustomCommand_WhenRestoreFrom_ThenPromptsSetCorrectly()
        {
            CustomCommand command1 = new(new CommandHelp("A", string.Empty), false, true, (_, _) => new Reaction(ReactionResult.Inform, string.Empty));
            command1.AddPrompt(new("A"));
            CustomCommand command2 = new(new CommandHelp("A", string.Empty), false, true, (_, _) => new Reaction(ReactionResult.Inform, string.Empty));
            
            CustomCommandSerialization serialization = CustomCommandSerialization.FromCustomCommand(command1);
            ((IObjectSerialization<CustomCommand>)serialization).Restore(command2);

            var result = command2.GetPrompts(null);

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("A", result[0].Entry);
        }
    }
}
