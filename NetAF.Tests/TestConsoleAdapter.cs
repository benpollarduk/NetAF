using NetAF.Assets;
using NetAF.Logic;
using NetAF.Rendering;
using System.IO;

namespace NetAF.Tests
{
    /// <summary>
    /// Provides a console adapter for tests.
    /// </summary>
    internal class TestConsoleAdapter : IIOAdapter
    {
        #region Fields

        private Size displaySize;

        #endregion

        #region Methods

        private void HandleFrameRender(IFrame frame)
        {
            Out?.WriteLine(frame?.ToString());
        }

        #endregion

        #region Properties

        /// <summary>
        /// Get the input stream.
        /// </summary>
        public TextReader In
        {
            get
            {
                var memoryStream = new MemoryStream();
                return new StreamReader(memoryStream);
            }
        }

        /// <summary>
        /// Get the output stream.
        /// </summary>
        public TextWriter Out
        {
            get
            {
                var memoryStream = new MemoryStream();
                return new StreamWriter(memoryStream);
            }
        }

        /// <summary>
        /// Get the error output stream.
        /// </summary>
        public TextWriter Error
        {
            get
            {
                var memoryStream = new MemoryStream();
                return new StreamWriter(memoryStream);
            }
        }

        #endregion

        #region Implementation of IIOAdapter

        /// <inheritdoc/>
        public Size CurrentOutputSize => displaySize;

        /// <inheritdoc/>
        public void Setup(Game game)
        {
            displaySize = game.Configuration.DisplaySize;
        }

        /// <inheritdoc/>
        public void RenderFrame(IFrame frame)
        {
            UpdatableFrameManager.ManageFrameTransition(frame, RenderFrame);
            HandleFrameRender(frame);
        }

        #endregion
    }
}
