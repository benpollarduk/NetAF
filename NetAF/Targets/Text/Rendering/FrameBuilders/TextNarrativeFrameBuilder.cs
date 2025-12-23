using NetAF.Assets;
using NetAF.Narratives;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using NetAF.Utilities;
using System.Text;

namespace NetAF.Targets.Text.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of narrative frames.
    /// </summary>
    /// <param name="builder">A builder to use for the text layout.</param>
    public sealed class TextNarrativeFrameBuilder(StringBuilder builder) : INarrativeFrameBuilder
    {
        #region Implementation of INarrativeFrameBuilder

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="narrative">The narrative.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        public IFrame Build(Narrative narrative, Size size)
        {
            builder.Clear();

            StringBuilder stringBuilder = new();

            foreach (var line in narrative.AllUntilCurrent())
                stringBuilder.AppendLine(line + StringUtilities.Newline);

            builder.AppendLine(narrative.Title);
            builder.AppendLine();
            builder.AppendLine(stringBuilder.ToString());

            return new TextFrame(builder);
        }

        #endregion
    }
}
