using System.IO;
using NetAF.Assets;
using NetAF.Rendering;

namespace NetAF.Targets.Console.Rendering
{
    /// <summary>
    /// Represents a presenter for TextWriter.
    /// </summary>
    /// <param name="writer">The writer.</param>
    /// <param name="presentableSize">The presentable size of the text writer.</param>
    public sealed class TextWriterPresenter(TextWriter writer, Size presentableSize) : IFramePresenter
    {
        #region Overrides of Object

        /// <inheritdoc/>
        public override string ToString()
        {
            return writer.ToString();
        }

        #endregion

        #region Implementation of IFramePresenter

        /// <inheritdoc/>
        public void Present(string frame)
        {
            writer.Write(frame);
        }

        /// <inheritdoc/>
        public Size GetPresentableSize()
        {
            return presentableSize;
        }

        #endregion
    }
}
