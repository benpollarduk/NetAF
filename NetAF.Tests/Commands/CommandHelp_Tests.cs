using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Commands;

namespace NetAF.Tests.Commands
{
    [TestClass]
    public class CommandHelp_Tests
    {
        [TestMethod]
        public void GivenACommandHelp_WhenEqualityCheckedWithEqualCommandHelp_ThenReturnTrue()
        {
            CommandHelp first = new("A", "B");
            CommandHelp second = new("A", "B");

            var result = first.Equals(second);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenACommandHelp_WhenEqualityCheckedWithUnequallyNamedCommandHelp_ThenReturnFalse()
        {
            CommandHelp first = new("A", "B");
            CommandHelp second = new("B", "B");

            var result = first.Equals(second);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenACommandHelp_WhenEqualityWithStringMatchingCommand_ThenReturnTrue()
        {
            CommandHelp command = new("A", string.Empty);

            var result = command.Equals("A");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenACommandHelp_WhenEqualityWithStringMatchingShortcut_ThenReturnTrue()
        {
            CommandHelp command = new(string.Empty, string.Empty, "A");

            var result = command.Equals("A");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenACommandHelp_WhenEqualityWithStringThatDoesNotMatch_ThenReturnFalse()
        {
            CommandHelp command = new("A", string.Empty, "B");

            var result = command.Equals("C");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenACommandHelp_WhenEqualityWithStringThatIsEmpty_ThenReturnFalse()
        {
            CommandHelp command = new("A", string.Empty, string.Empty);

            var result = command.Equals(string.Empty);

            Assert.IsFalse(result);
        }
    }
}
