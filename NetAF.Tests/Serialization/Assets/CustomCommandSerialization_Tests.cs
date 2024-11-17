using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Commands;
using NetAF.Serialization;

namespace NetAF.Tests.Serialization.Assets
{
    [TestClass]
    public class CustomCommandSerialization_Tests
    {
        [TestMethod]
        public void GivenCommandNameIsA_ThenCommandNameIsA()
        {
            CustomCommand command = new(new CommandHelp("A", string.Empty), true, true, (_,_) => Reaction.Inform);

            CustomCommandSerialization result = new(command);

            Assert.AreEqual("A", result.CommandName);
        }

        [TestMethod]
        public void GivenIsPlayerVisibleIsTrue_ThenCommandIsPlayerVisibleIsTrue()
        {
            CustomCommand command = new(new CommandHelp("A", string.Empty), true, true, (_, _) => Reaction.Inform);

            CustomCommandSerialization result = new(command);

            Assert.IsTrue(result.IsPlayerVisible);
        }

        [TestMethod]
        public void GivenACustomCommand_WhenRestoreFrom_ThenIsPlayerVisibleSetCorrectly()
        {
            CustomCommand command1 = new(new CommandHelp("A", string.Empty), true, true, (_, _) => Reaction.Inform);
            CustomCommand command2 = new(new CommandHelp("A", string.Empty), false, true, (_, _) => Reaction.Inform);

            CustomCommandSerialization serialization = new(command1);

            serialization.Restore(command2);

            Assert.IsTrue(command2.IsPlayerVisible);
        }
    }
}
