using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetAF.Assets;
using NetAF.Narratives;
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

                builder.Build(new Narrative("Frame", [new Section(["", ""]), new Section(["", ""])]), new Size(80, 50));
            });
        }
    }
}
