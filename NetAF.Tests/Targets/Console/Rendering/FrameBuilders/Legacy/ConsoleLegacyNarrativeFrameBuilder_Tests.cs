using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Narratives;
using NetAF.Rendering;
using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Console.Rendering.FrameBuilders.Legacy;

namespace NetAF.Tests.Targets.Console.Rendering.FrameBuilders.Legacy
{
    [TestClass]
    public class ConsoleLegacyNarrativeFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaults_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var gridStringBuilder = new GridStringBuilder();
                var builder = new ConsoleLegacyNarrativeFrameBuilder(gridStringBuilder, ConsoleRenderProperties.Default);
                var visualBuilder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.White);
                visualBuilder.Resize(new Size(5, 5));
                var visual = new Visual(string.Empty, string.Empty, visualBuilder);

                builder.Build(new Narrative("Frame", [new Section(["", ""], visual), new Section(["", ""])]), new Size(80, 50));
            });
        }
    }
}
