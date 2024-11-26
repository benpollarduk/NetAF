using NetAF.Assets.Characters;
using NetAF.Conversations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Commands;
using NetAF.Assets;
using NetAF.Rendering.Console.FrameBuilders;
using NetAF.Rendering.Console;

namespace NetAF.Tests.Rendering.Console.FrameBuilders
{
    [TestClass]
    public class ConsoleConversationFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaults_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var gridStringBuilder = new GridStringBuilder();
                var builder = new ConsoleConversationFrameBuilder(gridStringBuilder);

                builder.Build("Test", null, null, new Size(80, 50));
            });
        }

        [TestMethod]
        public void GivenDefaultsWithLog_WhenBuild_ThenFrameReturned()
        {
            var gridStringBuilder = new GridStringBuilder();
            var builder = new ConsoleConversationFrameBuilder(gridStringBuilder);
            var converser = new NonPlayableCharacter("Test", "Test", conversation: new(new("Line 1"), new("Line 2")));

            converser.Conversation.Next(null);
            converser.Conversation.Respond(new Response("Test2"), null);

            var result = builder.Build("Test", converser, null, new Size(80, 50));

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GivenDefaultsWith3CustomCommands_WhenBuild_ThenFrameReturned()
        {
            var gridStringBuilder = new GridStringBuilder();
            var builder = new ConsoleConversationFrameBuilder(gridStringBuilder);
            var commands = new[]
            {
                new CommandHelp("Test", "Test"),
                new CommandHelp("Test", "Test"),
                new CommandHelp("Test", "Test")
            };

            var result = builder.Build("Test", null, commands, new Size(80, 50));

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GivenNull_WhenTruncateLog_ThenReturnEmptyArray()
        {
            var log = ConsoleConversationFrameBuilder.TruncateLog(0, 50, 10, null);

            Assert.AreEqual(0, log.Length);
        }

        [TestMethod]
        public void GivenEmptyLog_WhenTruncateLog_ThenReturnEmptyArray()
        {
            var log = ConsoleConversationFrameBuilder.TruncateLog(0, 50, 10, []);

            Assert.AreEqual(0, log.Length);
        }

        [TestMethod]
        public void GivenLogWith1EntryAnd2Spaces_WhenTruncateLog_ThenReturnArrayWith1Item()
        {
            var log = ConsoleConversationFrameBuilder.TruncateLog(0, 50, 2, [new LogItem(Participant.Other, "")]);

            Assert.AreEqual(1, log.Length);
        }

        [TestMethod]
        public void GivenLogWith2EntryAnd1Space_WhenTruncateLog_ThenReturnArrayWith1Item()
        {
            var log = ConsoleConversationFrameBuilder.TruncateLog(0, 50, 1, [new LogItem(Participant.Other, ""), new LogItem(Participant.Player, "")]);

            Assert.AreEqual(1, log.Length);
        }
    }
}
