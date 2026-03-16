using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Narratives;
using NetAF.Rendering;
using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Console.Rendering.FrameBuilders;

namespace NetAF.Tests.Targets.Console.Rendering.FrameBuilders
{
    [TestClass]
    public class ConsoleNarrativeFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaults_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var gridStringBuilder = new GridStringBuilder();
                var builder = new ConsoleNarrativeFrameBuilder(gridStringBuilder);
                var visualBuilder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.White);
                visualBuilder.Resize(new Size(5, 5));
                var visual = new Visual(string.Empty, string.Empty, visualBuilder);

                builder.Build(new Narrative("Frame", [new Section(["", ""], visual), new Section(["", ""])]), new Size(80, 50));
            });
        }
    }
}
