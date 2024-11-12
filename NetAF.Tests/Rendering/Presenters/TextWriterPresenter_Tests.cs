using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Rendering.Presenters;
using System.IO;
using System.Linq;

namespace NetAF.Tests.Rendering.Presenters
{
    [TestClass]
    public class TextWriterPresenter_Tests
    {
        [TestMethod]
        public void GivenCharacter_WhenWrite_ThenCharacterIsWritten()
        {
            var textWriter = new StringWriter();
            var character = 'C';
            var presenter = new TextWriterPresenter(textWriter);

            presenter.Write(character);

            Assert.AreEqual(character, presenter.ToString().First());
        }

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
