using NetAF.Assets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetAF.Tests.Assets
{
    [TestClass]
    public class Identifier_Tests
    {
        [TestMethod]
        public void GivenName_WhenName_ThenReturnSameName()
        {
            var identifier = new Identifier("Rusty Key");

            var result = identifier.Name;

            Assert.AreEqual("Rusty Key", result);
        }

        [TestMethod]
        public void GivenNameWithSpacesAndLowerCase_WhenIdentifiableName_ThenReturnUpperCaseWithoutSpaces()
        {
            var identifier = new Identifier("Rusty Key");

            var result = identifier.IdentifiableName;

            Assert.AreEqual("RUSTYKEY", result);
        }

        [TestMethod]
        public void GivenEmpty_WhenName_ThenReturnEmptyString()
        {
            var result = Identifier.Empty.Name;

            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void GivenMatchingName_WhenEqualsString_ThenReturnTrue()
        {
            var identifier = new Identifier("Rusty Key");

            var result = identifier.Equals("Rusty Key");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenMatchingIdentifiableName_WhenEqualsString_ThenReturnTrue()
        {
            var identifier = new Identifier("Rusty Key");

            var result = identifier.Equals("RUSTYKEY");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenDifferentCaseAndSpacing_WhenEqualsString_ThenReturnTrue()
        {
            var identifier = new Identifier("Rusty Key");

            var result = identifier.Equals("rusty key");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenNonMatchingString_WhenEqualsString_ThenReturnFalse()
        {
            var identifier = new Identifier("Rusty Key");

            var result = identifier.Equals("Golden Key");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenNullString_WhenEqualsString_ThenReturnFalse()
        {
            var identifier = new Identifier("Rusty Key");

            var result = identifier.Equals((string)null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenIdentifiersWithSameIdentifiableName_WhenEqualsIdentifier_ThenReturnTrue()
        {
            var one = new Identifier("Rusty Key");
            var two = new Identifier("rusty key");

            var result = one.Equals(two);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenIdentifiersWithDifferentName_WhenEqualsIdentifier_ThenReturnFalse()
        {
            var one = new Identifier("Rusty Key");
            var two = new Identifier("Golden Key");

            var result = one.Equals(two);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenNullIdentifier_WhenEqualsIdentifier_ThenReturnFalse()
        {
            var identifier = new Identifier("Rusty Key");

            var result = identifier.Equals((Identifier)null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenName_WhenToString_ThenReturnName()
        {
            var identifier = new Identifier("Rusty Key");

            var result = identifier.ToString();

            Assert.AreEqual("Rusty Key", result);
        }
    }
}
