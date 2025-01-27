using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Targets.Console.Rendering;
using System.IO;

namespace NetAF.Tests.Targets.Console.Rendering
{
    [TestClass]
    public class TextWriterPresenter_Tests
    {
        [TestMethod]
        public void GivenString_WhenWrite_ThenStringIsWritten()
        {
            var textWriter = new StringWriter();
            var str = "TEST";
            var presenter = new TextWriterPresenter(textWriter);

            presenter.Write(str);

            Assert.AreEqual(str, presenter.ToString());
        }
    }
}
