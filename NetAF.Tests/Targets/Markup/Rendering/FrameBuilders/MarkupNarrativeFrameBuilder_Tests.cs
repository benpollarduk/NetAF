using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Narratives;
using NetAF.Targets.Markup.Rendering;
using NetAF.Targets.Markup.Rendering.FrameBuilders;

namespace NetAF.Tests.Targets.Markup.Rendering.FrameBuilders
{
    [TestClass]
    public class MarkupNarrativeFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaults_WhenBuild_ThenNoException()
        {
            Assertions.NoExceptionThrown(() =>
            {
                var markupBuilder = new MarkupBuilder();
                var builder = new MarkupNarrativeFrameBuilder(markupBuilder);

                builder.Build(new Narrative("Frame", [new Section(["", ""]), new Section(["", ""])]), new Size(80, 50));
            });
        }
    }
}
