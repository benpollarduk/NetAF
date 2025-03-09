using NetAF.Assets;
using NetAF.Logic;
using NetAF.Rendering;
using NetAF.Targets.Console.Rendering;

namespace NetAF.Targets.Console
{
    /// <summary>
    /// Provides an adapter for the System.Console.
    /// </summary>
    public sealed class ConsoleAdapter : IIOAdapter
    {
        #region Fields

        private readonly TextWriterPresenter presenter = new(System.Console.Out);

        #endregion

        #region Implementation of IIOAdapter

        /// <summary>
        /// Setup for a game.
        /// </summary>
        /// <param name="game">The game to set up for.</param>
        public void Setup(Game game)
        {
            System.Console.Title = game.Info.Name;
            Size actualDisplaySize = new(game.Configuration.DisplaySize.Width + 1, game.Configuration.DisplaySize.Height);
            System.Console.SetWindowSize(actualDisplaySize.Width, actualDisplaySize.Height);
        }

        /// <summary>
        /// Render a frame.
        /// </summary>
        /// <param name="frame">The frame to render.</param>
        public void RenderFrame(IFrame frame)
        {
            System.Console.Clear();

            frame.Render(presenter);

            if (frame is IConsoleFrame consoleFrame)
            {
                System.Console.CursorVisible = consoleFrame.ShowCursor;
                System.Console.SetCursorPosition(consoleFrame.CursorLeft, consoleFrame.CursorTop);
            }
        }

        #endregion
    }
}
