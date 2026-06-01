using NetAF.Assets.Locations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Commands;

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

        [TestMethod]
        public void GivenDirectionDown_WhenGetDirection_ThenDirectionIsDown()
        {
            var exit = new Exit(Direction.Down);

            Assert.AreEqual(Direction.Down, exit.Direction);
        }

        [TestMethod]
        public void GivenNoIdentifier_WhenGetIdentifier_ThenIdentifierIsDirectionName()
        {
            var exit = new Exit(Direction.North);

            Assert.AreEqual("NORTH", exit.Identifier.IdentifiableName);
        }

        [TestMethod]
        public void GivenCustomIdentifier_WhenGetIdentifier_ThenIdentifierIsCustom()
        {
            var exit = new Exit(Direction.North, identifier: new Identifier("Door"));

            Assert.AreEqual("DOOR", exit.Identifier.IdentifiableName);
        }

        [TestMethod]
        public void GivenNoInteraction_WhenInteract_ThenNoChange()
        {
            var exit = new Exit(Direction.Down);
            var item = new Item("Key", string.Empty);

            var result = exit.Interact(item);

            Assert.AreEqual(InteractionResult.NoChange, result.Result);
        }

        [TestMethod]
        public void GivenLockedExit_WhenGetDescription_ThenDescriptionContainsLocked()
        {
            var exit = new Exit(Direction.North);
            exit.Lock();

            var description = exit.Description.GetDescription();

            Assert.IsTrue(description.Contains("locked"));
        }

        [TestMethod]
        public void GivenUnlockedExit_WhenGetDescription_ThenDescriptionContainsUnlocked()
        {
            var exit = new Exit(Direction.North);

            var description = exit.Description.GetDescription();

            Assert.IsTrue(description.Contains("unlocked"));
        }

        [TestMethod]
        public void GivenNewExit_WhenGetIsLocked_ThenFalse()
        {
            var exit = new Exit(Direction.Down);

            Assert.IsFalse(exit.IsLocked);
        }

        [TestMethod]
        public void GivenNewLockedExit_WhenGetIsLocked_ThenTrue()
        {
            var exit = new Exit(Direction.Down, true);

            Assert.IsTrue(exit.IsLocked);
        }

        [TestMethod]
        public void GivenCustomCommands_WhenGetCommands_ThenCommandsAreSet()
        {
            var commands = new[] { new CustomCommand(new CommandHelp("Test", "Test"), true, true, (_, _) => new Reaction(ReactionResult.Silent, "")) };
            var exit = new Exit(Direction.Down, commands: commands);

            Assert.AreEqual(1, exit.Commands.Length);
        }

        [TestMethod]
        public void GivenNoCommands_WhenGetCommands_ThenCommandsIsEmpty()
        {
            var exit = new Exit(Direction.Down);

            Assert.AreEqual(0, exit.Commands.Length);
        }
    }
}
