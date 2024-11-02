using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Interpretation;

namespace NetAF.Tests.Interpretation
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
        public void GivenACommandHelp_WhenEqualityCheckedWithUnequalDescriptionCommandHelp_ThenReturnFalse()
        {
            CommandHelp first = new("A", "B");
            CommandHelp second = new("A", "A");

            var result = first.Equals(second);

            Assert.IsFalse(result);
        }
    }
}
