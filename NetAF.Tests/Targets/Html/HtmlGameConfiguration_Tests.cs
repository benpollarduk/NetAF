using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Targets.Html;
using System.Diagnostics;

namespace NetAF.Tests.Targets.Html
{
    [TestClass]
    public class HtmlGameConfiguration_Tests
    {
        [TestMethod]
        public void GivenCreate_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                HtmlGameConfiguration config = new(new HtmlAdapter(null), NetAF.Logic.ExitMode.ExitApplication);
                Debug.WriteLine(config);
            });
        }
    }
}