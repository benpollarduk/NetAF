using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Narratives;
using NetAF.Rendering;
using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Text.Rendering.FrameBuilders;
using System.Text;

namespace NetAF.Tests.Targets.Text.Rendering.FrameBuilders
{
    [TestClass]
    public class TextNarrativeFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaults_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var stringBuilder = new StringBuilder();
                var builder = new TextNarrativeFrameBuilder(stringBuilder);
                var visualBuilder = new GridVisualBuilder(AnsiColor.Black, AnsiColor.White);
                visualBuilder.Resize(new Size(5, 5));
                var visual = new Visual(string.Empty, string.Empty, visualBuilder);

                builder.Build(new Narrative("Frame", [new Section(["", ""], visual), new Section(["", ""])]), new Size(80, 50));
            });
        }
    }
}
