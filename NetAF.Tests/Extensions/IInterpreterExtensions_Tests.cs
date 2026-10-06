using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Interpretation;

namespace NetAF.Tests.Extensions
{
    [TestClass]
    public class IInterpreterExtensions_Tests
    {
        [TestMethod]
        public void GivenNull_WhenFilterExcludedCommands_ThenNoCommandsReturned()
        {
            var interpreter = new PersistenceCommandInterpreter();

            var result = interpreter.FilterExcludedCommands(null).ToArray();

            Assert.IsEmpty(result);
        }

        [TestMethod]
        public void GivenNoCommands_WhenFilterExcludedCommands_ThenNoCommandsReturned()
        {
            var interpreter = new PersistenceCommandInterpreter();

            var result = interpreter.FilterExcludedCommands([]).ToArray();

            Assert.IsEmpty(result);
        }

        [TestMethod]
        public void GivenNoExcludedCommands_WhenFilterExcludedCommands_ThenAllCommandsReturned()
        {
            var interpreter = new PersistenceCommandInterpreter();

            var result = interpreter.FilterExcludedCommands(PersistenceCommandInterpreter.DefaultSupportedCommands).ToArray();

            Assert.HasCount(PersistenceCommandInterpreter.DefaultSupportedCommands.Length, result);
        }

        [TestMethod]
        public void GivenOneExcludedCommand_WhenFilterExcludedCommands_ThenExcludedCommandNotReturned()
        {
            var interpreter = new PersistenceCommandInterpreter();
            var command = PersistenceCommandInterpreter.DefaultSupportedCommands[0];
            interpreter.ExcludedCommands.Add(command);

            var result = interpreter.FilterExcludedCommands(PersistenceCommandInterpreter.DefaultSupportedCommands).ToArray();

            Assert.HasCount(1, result);
        }

        [TestMethod]
        public void GivenAllCommandsExcluded_WhenFilterExcludedCommands_ThenNoCommandsReturned()
        {
            var interpreter = new PersistenceCommandInterpreter();
            var command1 = PersistenceCommandInterpreter.DefaultSupportedCommands[0];
            var command2 = PersistenceCommandInterpreter.DefaultSupportedCommands[1];
            interpreter.ExcludedCommands.Add(command1);
            interpreter.ExcludedCommands.Add(command2);

            var result = interpreter.FilterExcludedCommands(PersistenceCommandInterpreter.DefaultSupportedCommands).ToArray();

            Assert.IsEmpty(result);
        }

        [TestMethod]
        public void GivenExcludedCommandWithDifferentCasing_WhenFilterExcludedCommands_ThenCommandExcluded()
        {
            var interpreter = new PersistenceCommandInterpreter();
            var command1 = new CommandHelp(PersistenceCommandInterpreter.DefaultSupportedCommands[0].Command.ToLower());
            var command2 = new CommandHelp(PersistenceCommandInterpreter.DefaultSupportedCommands[1].Command.ToUpper());
            interpreter.ExcludedCommands.Add(command1);
            interpreter.ExcludedCommands.Add(command2);

            var result = interpreter.FilterExcludedCommands(PersistenceCommandInterpreter.DefaultSupportedCommands).ToArray();

            Assert.IsEmpty(result);
        }

        [TestMethod]
        public void GivenNullCommand_WhenIsCommand_ThenFalse()
        {
            var interpreter = new PersistenceCommandInterpreter();

            var result = interpreter.IsCommand(null, "Load");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenMatchingQuery_WhenIsCommand_ThenTrue()
        {
            var interpreter = new PersistenceCommandInterpreter();
            var command = PersistenceCommandInterpreter.DefaultSupportedCommands[0];

            var result = interpreter.IsCommand(command, command.Command);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenNonMatchingQuery_WhenIsCommand_ThenFalse()
        {
            var interpreter = new PersistenceCommandInterpreter();
            var command = PersistenceCommandInterpreter.DefaultSupportedCommands[0];

            var result = interpreter.IsCommand(command, "NotACommand");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenMatchingQueryWithDifferentCasing_WhenIsCommand_ThenTrue()
        {
            var interpreter = new PersistenceCommandInterpreter();
            var command = PersistenceCommandInterpreter.DefaultSupportedCommands[0];

            var result = interpreter.IsCommand(command, command.Command.ToUpper());

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenExcludedCommandAndFailIfExcluded_WhenIsCommand_ThenFalse()
        {
            var interpreter = new PersistenceCommandInterpreter();
            var command = PersistenceCommandInterpreter.DefaultSupportedCommands[0];
            interpreter.ExcludedCommands.Add(command);

            var result = interpreter.IsCommand(command, command.Command);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenExcludedCommandAndNotFailIfExcluded_WhenIsCommand_ThenTrue()
        {
            var interpreter = new PersistenceCommandInterpreter();
            var command = PersistenceCommandInterpreter.DefaultSupportedCommands[0];
            interpreter.ExcludedCommands.Add(command);

            var result = interpreter.IsCommand(command, command.Command, false);

            Assert.IsTrue(result);
        }
    }
}
