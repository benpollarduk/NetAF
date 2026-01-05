using System.IO;
using NetAF.Assets;
using NetAF.Rendering;

namespace NetAF.Targets.Console.Rendering
{
    /// <summary>
    /// Represents a presenter for TextWriter.
    /// </summary>
    /// <param name="writer">The writer.</param>
    public sealed class TextWriterPresenter(TextWriter writer) : IFramePresenter
    {
        #region Overrides of Object

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return writer.ToString();
        }

        #endregion

        #region Implementation of IFramePresenter

        /// <summary>
        /// Present a frame.
        /// </summary>
        /// <param name="frame">The frame to write, as a string.</param>
        public void Present(string frame)
        {
            writer.Write(frame);
        }

        /// <summary>
        /// Get the size of the presentable area.
        /// </summary>
        /// <returns>The size.</returns>
        public Size GetPresentableSize()
        {
            return new Size(System.Console.WindowWidth, System.Console.WindowHeight);
        }

        #endregion
    }
}
