using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Targets.Console;
using System.Diagnostics;

namespace NetAF.Tests.Targets.Console
{
    [TestClass]
    public class HtmlGameConfiguration_Tests
    {
        [TestMethod]
        public void GivenGetDefault_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var config = ConsoleGameConfiguration.Default;
                Debug.WriteLine(config);
            });
        }
    }
}
