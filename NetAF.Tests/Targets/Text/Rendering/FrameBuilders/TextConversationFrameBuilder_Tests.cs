using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Commands;
using NetAF.Conversations;
using NetAF.Targets.Text.Rendering;
using NetAF.Targets.Text.Rendering.FrameBuilders;
using System.Text;

namespace NetAF.Tests.Targets.Text.Rendering.FrameBuilders
{
    [TestClass]
    public class TextConversationFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaults_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var stringBuilder = new StringBuilder();
                var builder = new TextConversationFrameBuilder(stringBuilder, TextRenderProperties.Default);

                builder.Build("Test", null, null, new Size(80, 50));
            });
        }

        [TestMethod]
        public void GivenDefaultsWithLogAndRenderLog_WhenBuild_ThenFrameReturned()
        {
            var stringBuilder = new StringBuilder();
            var builder = new TextConversationFrameBuilder(stringBuilder, new TextRenderProperties { RenderConversationHistory = true });
            var converser = new NonPlayableCharacter("Test", "Test", conversation: new(new("Line 1"), new("Line 2")));

            converser.Conversation.Next(null);
            converser.Conversation.Respond(new Response("Test2"), null);

            var result = builder.Build("Test", converser, null, new Size(80, 50));

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GivenDefaultsWithLogAndDontRenderLog_WhenBuild_ThenFrameReturned()
        {
            var stringBuilder = new StringBuilder();
            var builder = new TextConversationFrameBuilder(stringBuilder, new TextRenderProperties { RenderConversationHistory = false });
            var converser = new NonPlayableCharacter("Test", "Test", conversation: new(new("Line 1"), new("Line 2")));

            converser.Conversation.Next(null);
            converser.Conversation.Respond(new Response("Test2"), null);

            var result = builder.Build("Test", converser, null, new Size(80, 50));

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GivenDefaultsWith3CustomCommands_WhenBuild_ThenFrameReturned()
        {
            var stringBuilder = new StringBuilder();
            var builder = new TextConversationFrameBuilder(stringBuilder, TextRenderProperties.Default);
            var commands = new[]
            {
                new CommandHelp("Test"),
                new CommandHelp("Test"),
                new CommandHelp("Test")
            };

            var result = builder.Build("Test", null, commands, new Size(80, 50));

            Assert.IsNotNull(result);
        }
    }
}
