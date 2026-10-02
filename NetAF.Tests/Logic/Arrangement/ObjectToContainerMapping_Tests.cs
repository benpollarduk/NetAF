using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Logic.Arrangement;

namespace NetAF.Tests.Logic.Arrangement
{
    [TestClass]
    public class ObjectToContainerMapping_Tests
    {
        [TestMethod]
        public void GivenObj_WhenConstructed_ThenObjIsSet()
        {
            ObjectToContainerMapping result = new("A", "B");

            Assert.AreEqual("A", result.Obj);
        }

        [TestMethod]
        public void GivenContainer_WhenConstructed_ThenContainerIsSet()
        {
            ObjectToContainerMapping result = new("A", "B");

            Assert.AreEqual("B", result.Container);
        }
    }
}
