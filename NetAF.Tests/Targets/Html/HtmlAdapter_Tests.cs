using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Targets.Html;
using System.Threading;

namespace NetAF.Tests.Targets.Html
{
    [TestClass]
    public class HtmlAdapter_Tests
    {
        [TestMethod]
        public void GivenTest_WhenWaitForInput_ThenReturnTest()
        {
            HtmlAdapter adapter = new(null);

            var thread = new Thread(() =>
            {
                while (true)
                    adapter.InputReceived("Test");
            });
            thread.IsBackground = true;
            thread.Start();

            var result = adapter.WaitForInput();

            Assert.AreEqual("Test", result);
        }

        [TestMethod]
        public void GivenAcknowledge_WhenWaitForAcknowledge_ThenReturnTrue()
        {
            HtmlAdapter adapter = new(null);

            var thread = new Thread(() =>
            {
                while (true)
                    adapter.AcknowledgeReceived();
            });
            thread.IsBackground = true;
            thread.Start();

            var result = adapter.WaitForAcknowledge();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenTimeout_WhenWaitForAcknowledge_ThenReturnFalse()
        {
            HtmlAdapter adapter = new(null);

            var result = adapter.WaitForAcknowledge(1);

            Assert.IsFalse(result);
        }
    }
}
