using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets.Locations;
using NetAF.Serialization.Assets;

namespace NetAF.Tests.Serialization.Assets
{
    [TestClass]
    public class ExitSerialization_Tests
    {
        [TestMethod]
        public void GivenIsLocked_WhenFromExit_ThenIsLockedIsTrue()
        {
            Exit exit = new(Direction.North, true);

            ExitSerialization result = ExitSerialization.FromExit(exit);

            Assert.IsTrue(result.IsLocked);
        }

        [TestMethod]
        public void GivenExitThatIsUnlocked_WhenRestoreFromExitThatIsLocked_ThenIsLockedIsTrue()
        {
            Exit exit = new(Direction.North, false);
            Exit exit2 = new(Direction.North, true);
            ExitSerialization serialization = ExitSerialization.FromExit(exit2);

            serialization.Restore(exit);

            Assert.IsTrue(exit.IsLocked);
        }
    }
}
