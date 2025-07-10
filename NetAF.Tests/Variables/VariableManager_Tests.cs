using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Serialization.Assets;
using NetAF.Variables;

namespace NetAF.Tests.Variables
{
    [TestClass]
    public class VariableManager_Tests
    {
        [TestMethod]
        public void GivenNoVariables_WhenGetAll_ThenEmptyArray()
        {
            var manager = new VariableManager();

            var results = manager.GetAll();

            Assert.IsTrue(results.Length == 0);
        }

        [TestMethod]
        public void GivenNoVariables_WhenAddWithNameAndValue_ThenOneVariable()
        {
            var manager = new VariableManager();
            manager.Add("A", "B");

            var result = manager.Count;

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void GivenNoVariables_WhenAdd_ThenOneVariable()
        {
            var manager = new VariableManager();
            manager.Add(new("A", "B"));

            var result = manager.Count;

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void GivenOneVariable_WhenAddDuplicate_ThenOneVariable()
        {
            var manager = new VariableManager();
            manager.Add(new("A", "B"));
            manager.Add(new("A", "B"));

            var result = manager.Count;

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void GivenOneVariable_WhenAddNonDuplicate_ThenTwoVariables()
        {
            var manager = new VariableManager();
            manager.Add(new("A", "B"));
            manager.Add(new("C", "D"));

            var result = manager.Count;

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void GivenEmptyVariable_WhenAdd_ThenNoVariableAdded()
        {
            var manager = new VariableManager();
            manager.Add(new(string.Empty, string.Empty));

            var result = manager.Count;

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GivenOneVariable_WhenRemove_ThenNoVariables()
        {
            var manager = new VariableManager();
            manager.Add(new("A", "B"));
            manager.Remove("A");

            var result = manager.Count;

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GivenOneVariable_WhenRemoveNonExisting_ThenOneVariable()
        {
            var manager = new VariableManager();
            manager.Add(new("A", "B"));
            manager.Remove("C");

            var result = manager.Count;

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void GivenOneVariable_WhenClear_ThenNoVariables()
        {
            var manager = new VariableManager();
            manager.Add(new("A", "B"));
            manager.Clear();

            var result = manager.Count;

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GivenSerialization_WhenFromSerialization_ThenRestoredCorrectly()
        {
            VariableManager manager = new();
            manager.Add(new("a", "b"));
            VariableManagerSerialization serialization = VariableManagerSerialization.FromVariableManager(manager);

            var result = VariableManager.FromSerialization(serialization);

            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void GivenOneVariableWithMatch_WhenContainsVariable_ThenTrue()
        {
            var manager = new VariableManager();
            manager.Add(new("A", "B"));

            var result = manager.ContainsVariable("A");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenOneVariableWithNoMatch_WhenContainsVariable_ThenFalse()
        {
            var manager = new VariableManager();
            manager.Add(new("A", "B"));

            var result = manager.ContainsVariable("C");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GivenOneVariable_WhenAddDuplicateWithDifferentValue_ThenVariableValueIsUpdated()
        {
            var manager = new VariableManager();
            Variable v1 = new("A", "B");
            Variable v2 = new("A", "C");

            manager.Add(v1);
            manager.Add(v2);

            Assert.AreEqual("C", v1.Value);
        }
    }
}
