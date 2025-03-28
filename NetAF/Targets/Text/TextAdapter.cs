using NetAF.Assets;
using NetAF.Logic;
using NetAF.Rendering;
using NetAF.Targets.Console.Rendering;
using System.Text;

namespace NetAF.Targets.Text
{
    /// <summary>
    /// Provides an adapter for text.
    /// </summary>
    /// <param name="presenter">The presenter to use for presenting frames.</param>
    public sealed class TextAdapter(IFramePresenter presenter) : IIOAdapter
    {
        #region Properties

        /// <summary>
        /// Get the game.
        /// </summary>
        public Game Game { get; private set; }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Convert an instance of IConsoleFrame to a string.
        /// </summary>
        /// <param name="frame">The frame to convert.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The converted string.</returns>
        internal static string Convert(IConsoleFrame frame, Size size)
        {
            StringBuilder stringBuilder = new();

            for (var row = 0; row < size.Height; row++)
            {
                for (var column = 0; column < size.Width; column++)
                {
                    var cell = frame.GetCell(column, row);
                    var character = cell.Character == 0 ? ' ' : cell.Character;
                    stringBuilder.Append(character);
                }

                if (row < size.Height - 1)
                    stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }

        #endregion

        #region Implementation of IIOAdapter

        /// <summary>
        /// Setup for a game.
        /// </summary>
        /// <param name="game">The game to set up for.</param>
        public void Setup(Game game)
        {
            Game = game;
        }

        /// <summary>
        /// Render a frame.
        /// </summary>
        /// <param name="frame">The frame to render.</param>
        public void RenderFrame(IFrame frame)
        {
            // convert the console frame to text frame if possible
            if (frame is IConsoleFrame ansiFrame)
                presenter.Present(Convert(ansiFrame, Game.Configuration.DisplaySize));
            else
                frame.Render(presenter);
        }

        #endregion
    }
}