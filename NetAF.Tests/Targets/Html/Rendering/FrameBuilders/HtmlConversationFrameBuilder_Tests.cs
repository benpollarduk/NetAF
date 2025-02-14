using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Commands;
using NetAF.Conversations;
using NetAF.Targets.Html.Rendering;
using NetAF.Targets.Html.Rendering.FrameBuilders;

namespace NetAF.Tests.Targets.Html.Rendering.FrameBuilders
{
    [TestClass]
    public class HtmlConversationFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaults_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var htmlBuilder = new HtmlBuilder();
                var builder = new HtmlConversationFrameBuilder(htmlBuilder);

                builder.Build("Test", null, null, new Size(80, 50));
            });
        }

        [TestMethod]
        public void GivenDefaultsWithLog_WhenBuild_ThenFrameReturned()
        {
            var htmlBuilder = new HtmlBuilder();
            var builder = new HtmlConversationFrameBuilder(htmlBuilder);
            var converser = new NonPlayableCharacter("Test", "Test", conversation: new(new("Line 1"), new("Line 2")));

            converser.Conversation.Next(null);
            converser.Conversation.Respond(new Response("Test2"), null);

            var result = builder.Build("Test", converser, null, new Size(80, 50));

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GivenDefaultsWith3CustomCommands_WhenBuild_ThenFrameReturned()
        {
            var htmlBuilder = new HtmlBuilder();
            var builder = new HtmlConversationFrameBuilder(htmlBuilder);
            var commands = new[]
            {
                new CommandHelp("Test", "Test"),
                new CommandHelp("Test", "Test"),
                new CommandHelp("Test", "Test")
            };

            var result = builder.Build("Test", null, commands, new Size(80, 50));

            Assert.IsNotNull(result);
        }
    }
}
