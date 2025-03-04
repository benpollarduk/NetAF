using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Logic.Configuration;
using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Console;
using System.Diagnostics;

namespace NetAF.Tests.Logic.Configuration
{
    [TestClass]
    public class GameConfiguration_Tests
    {
        [TestMethod]
        public void GivenGetDefault_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var config = new GameConfiguration(new ConsoleAdapter(), FrameBuilderCollections.Console, new(80, 50));
                Debug.WriteLine(config);
            });
        }
    }
}
