using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Conversations;
using NetAF.Serialization.Assets;

namespace NetAF.Tests.Serialization.Assets
{
    [TestClass]
    public class ExitSerialization_Tests
    {
        [TestMethod]
        public void GivenIsLocked_ThenIsLockedIsTrue()
        {
            var exit = new Exit(Direction.North, true);

            var result = new ExitSerialization(exit);

            Assert.IsTrue(result.IsLocked);
        }

        [TestMethod]
        public void GivenExitThatIsUnlocked_WhenRestoreFromExitThatIsLocked_ThenIsLockedIsTrue()
        {
            var exit = new Exit(Direction.North, false);
            var exit2 = new Exit(Direction.North, true);
            var serialization = new ExitSerialization(exit2);

            exit.RestoreFrom(serialization);

            Assert.IsTrue(exit.IsLocked);
        }
    }
}
