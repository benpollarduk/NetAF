using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Variables;
using NetAF.Serialization.Assets;

namespace NetAF.Tests.Variables
{
    [TestClass]
    public class Variable_Tests
    {
        [TestMethod]
        public void GivenSerialization_WhenFromSerialization_ThenRestoredCorrectly()
        {
            Variable variable = new("a", "b");
            VariableSerialization serialization = VariableSerialization.FromVariable(variable);

            var result = Variable.FromSerialization(serialization);

            Assert.AreEqual("a", result.Name);
            Assert.AreEqual("b", result.Value);
        }
    }
}
