using NetAF.Assets.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NetAF.Tests.Assets.Attributes
{
    [TestClass]
    public class AttributeManager_Tests
    {
        [TestMethod]
        public void GivenNoAttributes_WhenGetAttributes_ThenReturnEmptyArray()
        {
            AttributeManager manager = new();

            var result = manager.GetAttributes();

            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void GivenNonExistentAttribute_WhenGetValue_ThenReturn0()
        {
            AttributeManager manager = new();

            var result = manager.GetValue("");

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GivenNoAttributes_WhenAdd_ThenOneAttribute()
        {
            AttributeManager manager = new();
            manager.Add("test", 0);

            var result = manager.GetAttributes();

            Assert.AreEqual(1, result.Length);
        }

        [TestMethod]
        public void GivenOneAttributes_WhenAddDuplicateAttribute_ThenOneAttribute()
        {
            AttributeManager manager = new();
            manager.Add("test", 0);
            manager.Add("test", 0);

            var result = manager.GetAttributes();

            Assert.AreEqual(1, result.Length);
        }

        [TestMethod]
        public void GivenOneAttribute_WhenAddNonDuplicateAttribute_ThenTwoAttribute()
        {
            AttributeManager manager = new();
            manager.Add("test", 0);
            manager.Add("test1", 0);

            var result = manager.GetAttributes();

            Assert.AreEqual(2, result.Length);
        }

        [TestMethod]
        public void GivenOneAttribute_WhenAddNonDuplicateAttribute_ThenAttributeValueAdded()
        {
            AttributeManager manager = new();
            manager.Add("test", 1);
            manager.Add(new Attribute("test", "", 100, 2), 2);

            var result = manager.GetValue("test");

            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void GivenNoAttributes_WhenRemove_ThenNoAttributes()
        {
            AttributeManager manager = new();
            manager.Remove("test");

            var result = manager.GetAttributes();

            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void GivenTwoAttributes_WhenRemove_ThenOneAttribute()
        {
            AttributeManager manager = new();
            manager.Add("test", 0);
            manager.Add("test1", 0);
            manager.Remove(new Attribute("test", "", 0, 100));

            var result = manager.GetAttributes();

            Assert.AreEqual(1, result.Length);
        }

        [TestMethod]
        public void GivenTwoAttributes_WhenRemoveAll_ThenNoAttributes()
        {
            AttributeManager manager = new();
            manager.Add("test", 0);
            manager.Add("test1", 0);
            manager.RemoveAll();

            var result = manager.GetAttributes();

            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void GivenNoAttributes_WhenSubtract_Then1Attribute()
        {
            AttributeManager manager = new();
            manager.Subtract("test", 0);

            var result = manager.GetAttributes();

            Assert.AreEqual(1, result.Length);
        }

        [TestMethod]
        public void GivenOneAttributes_WhenSubtractDuplicateAttribute_ThenOneAttribute()
        {
            AttributeManager manager = new();
            manager.Add("test", 0);
            manager.Subtract("test", 0);

            var result = manager.Count;

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void GivenOneAttribute_WhenSubtractAttribute_ThenAttributeValueSubtracted()
        {
            AttributeManager manager = new();
            manager.Add("test", 100);
            manager.Subtract(new Attribute("test", "", 100, 2), 50);

            var result = manager.GetValue("test");

            Assert.AreEqual(50, result);
        }

        [TestMethod]
        public void GivenNoAttributes_WhenGetAsDictionary_ThenReturnEmptyDictionary()
        {
            AttributeManager manager = new();

            var result = manager.GetAsDictionary();

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GivenOneAttribute_WhenGetAsDictionary_ThenReturnDictionaryWithOneElement()
        {
            AttributeManager manager = new();
            manager.Add("test", 1);

            var result = manager.GetAsDictionary();

            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void Given10WhenMax5_WhenAdd_ThenValueIs5()
        {
            AttributeManager manager = new();
            manager.Add(new Attribute("test", string.Empty, 0, 5), 10);

            var result = manager.GetValue("test");

            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void GivenMinus5WhenMin0_WhenAdd_ThenValueIs0()
        {
            AttributeManager manager = new();
            manager.Add(new Attribute("test", string.Empty, 0, 5), -5);

            var result = manager.GetValue("test");

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GivenSubtract10From5WhenMin0_WhenSubtract_ThenValueIs0()
        {
            AttributeManager manager = new();
            manager.Add(new Attribute("test", string.Empty, 0, 10), 5);
            manager.Subtract("test", 10);

            var result = manager.GetValue("test");

            Assert.AreEqual(0, result);
        }
    }
}
