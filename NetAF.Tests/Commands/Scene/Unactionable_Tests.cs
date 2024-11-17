using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Commands.Scene;
using NetAF.Commands;

namespace NetAF.Tests.Commands.Scene
{
    [TestClass]
    public class Unactionable_Tests
    {
        [TestMethod]
        public void GivenDefault_WhenInvoke_ThenError()
        {
            var command = new Unactionable();

            var result = command.Invoke(null);

            Assert.AreEqual(ReactionResult.Error, result.Result);
        }
    }
}
