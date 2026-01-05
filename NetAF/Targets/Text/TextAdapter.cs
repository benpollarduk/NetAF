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
        #region Fields

        private Size displaySize;

        #endregion

        #region StaticMethods

        /// <summary>
        /// Convert the contents of a GridStringBuilder to a string.
        /// </summary>
        /// <param name="builder">The GridStringBuilder to convert.</param>
        /// <param name="padEmptyCharacters">Specify if empty characters should be padded with a space.</param>
        /// <returns>A string representing the contents of the GridStringBuilder.</returns>
        public static string ConvertGridStringBuilderToString(GridStringBuilder builder, bool padEmptyCharacters = true)
        {
            StringBuilder stringBuilder = new();

            for (var row = 0; row < builder.DisplaySize.Height; row++)
            {
                for (var column = 0; column < builder.DisplaySize.Width; column++)
                {
                    var character = builder.GetCharacter(column, row);
                    character = character == 0 && padEmptyCharacters ? ' ' : character;
                    stringBuilder.Append(character);
                }

                if (row < builder.DisplaySize.Height - 1)
                    stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }

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
            displaySize = game.Configuration.DisplaySize;
        }

        /// <summary>
        /// Render a frame.
        /// </summary>
        /// <param name="frame">The frame to render.</param>
        public void RenderFrame(IFrame frame)
        {
            // get render size
            var renderSize = displaySize != Size.Dynamic ? displaySize : GetDisplaySize();

            // convert the console frame to text frame if possible
            if (frame is IConsoleFrame ansiFrame)
                presenter.Present(Convert(ansiFrame, renderSize));
            else
                frame.Render(presenter);
        }

        /// <summary>
        /// Get the display size for this adapter.
        /// </summary>
        /// <returns>The size.</returns>
        public Size GetDisplaySize()
        {
            return displaySize;
        }

        #endregion
    }
}