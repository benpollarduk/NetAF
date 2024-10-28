using System;
using System.Drawing;
using NetAF.Logic;
using NetAF.Rendering.Frames;
using NetAF.Rendering.Presenter;
using NetAF.Utilities;

namespace NetAF.Adapters
{
    /// <summary>
    /// Provides an adapter for the System.Console.
    /// </summary>
    public sealed class SystemConsoleAdapter : IIOAdapter
    {
        #region StaticMethods

        /// <summary>
        /// Wait for a key press.
        /// </summary>
        /// <param name="key">The ASCII code of the key to wait for.</param>
        /// <returns>True if the key pressed returned the same ASCII character as the key property, else false.</returns>
        private static bool WaitForKeyPress(char key)
        {
            return Console.ReadKey().KeyChar == key;
        }

        #endregion

        #region Fields

        private readonly TextWriterPresenter presenter = new(Console.Out);

        #endregion

        #region Implementation of IIOAdapter

        /// <summary>
        /// Setup for a game.
        /// </summary>
        /// <param name="game">The game to set up for.</param>
        public void Setup(Game game)
        {
            Console.Title = game.Info.Name;
            Size actualDisplaySize = new(game.Configuration.DisplaySize.Width + 1, game.Configuration.DisplaySize.Height);
            Console.SetWindowSize(actualDisplaySize.Width, actualDisplaySize.Height);
            Console.SetBufferSize(actualDisplaySize.Width, actualDisplaySize.Height);
        }

        /// <summary>
        /// Render a frame.
        /// </summary>
        /// <param name="frame">The frame to render.</param>
        public void RenderFrame(IFrame frame)
        {
            Console.Clear();

            frame.Render(presenter);

            Console.CursorVisible = frame.ShowCursor;
            Console.SetCursorPosition(frame.CursorLeft, frame.CursorTop);
        }

        /// <summary>
        /// Wait for acknowledgment.
        /// </summary>
        /// <returns>True if the acknowledgment was received correctly, else false.</returns>
        public bool WaitForAcknowledge()
        {
            return WaitForKeyPress(StringUtilities.CR);
        }

        /// <summary>
        /// Wait for input.
        /// </summary>
        /// <returns>The input.</returns>
        public string WaitForInput()
        {
            return Console.In.ReadLine();
        }

        #endregion
    }
}
