using NetAF.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Commands;

namespace NetAF.Tests.Extensions
{
    [TestClass]
    public class CommandHelpExtensions_Tests
    {
        [TestMethod]
        public void GivenCommandAShortcutB_WhenFormattedToDisplayShortcut_ThenCommandIsAForwardSlashB()
        {
            var result = new CommandHelp("A", string.Empty, "B").FormattedToDisplayShortcut();

            Assert.AreEqual("A/B", result.Command);
        }

        [TestMethod]
        public void GivenCommandAShortcutB_FormattedToDisplayShortcutAndVariable_ThenCommandIsAForwardSlashBSpaceUnderscoreUnderscore()
        {
            var result = new CommandHelp("A", string.Empty, "B").FormattedToDisplayShortcutAndVariable();

            Assert.AreEqual("A/B __", result.Command);
        }
    }
}
