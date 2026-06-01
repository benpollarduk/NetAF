using NetAF.Assets.Locations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;

namespace NetAF.Tests.Assets.Locations
{
    [TestClass]
    public class Exit_Tests
    {
        [TestMethod]
        public void GivenLockedExit_WhenUnlock_ThenIsLockedIsFalse()
        {
            var exit = new Exit(Direction.Down, true);

            exit.Unlock();

            Assert.IsFalse(exit.IsLocked);
        }

        [TestMethod]
        public void GivenUnlockedExit_WhenLock_ThenIsLockedIsTrue()
        {
            var exit = new Exit(Direction.Down);

            exit.Lock();

            Assert.IsTrue(exit.IsLocked);
        }

        [TestMethod]
        public void GivenLockedExit_WhenLocksWithInteraction_ThenIsLockedIsFalse()
        {
            var key = new Item("Key", string.Empty);
            Exit exit = null;
            exit = new Exit(Direction.Down, true, interaction: i =>
            {
                exit.Unlock();
                return new Interaction(InteractionResult.ItemExpires, i);
            });

            exit.Interact(key);

            Assert.IsFalse(exit.IsLocked);
        }
    }
}
