using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Rendering.Console;
using System.IO;
using System.Linq;

namespace NetAF.Tests.Rendering.Console
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
