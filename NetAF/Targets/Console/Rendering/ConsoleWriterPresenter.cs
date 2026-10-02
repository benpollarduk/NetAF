using NetAF.Assets;
using NetAF.Rendering;

namespace NetAF.Targets.Console.Rendering
{
    /// <summary>
    /// Represents a presenter for the System.Console.
    /// </summary>
    public sealed class ConsoleWriterPresenter : IFramePresenter
    {
        #region Overrides of Object

        /// <inheritdoc/>
        public override string ToString()
        {
            return System.Console.Out.ToString();
        }

        #endregion

        #region Implementation of IFramePresenter

        /// <inheritdoc/>
        public void Present(string frame)
        {
            System.Console.Out.Write(frame);
        }

        /// <inheritdoc/>
        public Size GetPresentableSize()
        {
            return new Size(System.Console.WindowWidth, System.Console.WindowHeight);
        }

        #endregion
    }
}
