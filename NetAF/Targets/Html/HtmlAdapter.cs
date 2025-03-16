using NetAF.Assets;
using NetAF.Logic;
using NetAF.Rendering;
using NetAF.Targets.Console.Rendering;
using System.Text;

namespace NetAF.Targets.Html
{
    /// <summary>
    /// Provides an adapter for HTML.
    /// </summary>
    /// <param name="presenter">The presenter to use for presenting frames.</param>
    public sealed class HtmlAdapter(IFramePresenter presenter) : IIOAdapter
    {
        #region Properties

        /// <summary>
        /// Get the game.
        /// </summary>
        public Game Game { get; private set; }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Convert an ANSI color to a hexadecimal representation.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns>The hexadecimal representation.</returns>
        private static string AnsiColorToHex(AnsiColor color)
        {
            return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        }

        /// <summary>
        /// Convert the contents of a GridStringBuilder to HTML.
        /// </summary>
        /// <param name="builder">The GridStringBuilder to convert.</param>
        /// <param name="padEmptyCharacters">Specify if empty characters should be padded with a space.</param>
        /// <returns>A HTML string representing the contents of the GridStringBuilder.</returns>
        public static string ConvertGridStringBuilderToHtmlString(GridStringBuilder builder, bool padEmptyCharacters = true)
        {
            StringBuilder lineBuilder = new();
            StringBuilder stringBuilder = new();

            for (var row = 0; row < builder.DisplaySize.Height; row++)
            {
                for (var column = 0; column < builder.DisplaySize.Width; column++)
                {
                    var character = builder.GetCharacter(column, row);

                    if (padEmptyCharacters && character == 0)
                        character = ' ';

                    lineBuilder.Append(character);
                }

                var line = lineBuilder.ToString();
                line = line.TrimEnd();
                lineBuilder.Clear();
                stringBuilder.Append(line);
                stringBuilder.Append("<br>");
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Convert the contents of a GridVisualBuilder to HTML.
        /// </summary>
        /// <param name="builder">The GridVisualBuilder to convert.</param>
        /// <returns>A HTML string representing the contents of the GridVisualBuilder.</returns>
        public static string ConvertGridVisualBuilderToHtmlString(GridVisualBuilder builder)
        {
            StringBuilder stringBuilder = new();

            for (var row = 0; row < builder.DisplaySize.Height; row++)
            {
                for (var column = 0; column < builder.DisplaySize.Width; column++)
                {
                    var background = builder.GetCellBackgroundColor(column, row);
                    var foreground = builder.GetCellForegroundColor(column, row);
                    var character = builder.GetCharacter(column, row);
                    character = character == 0 ? ' ' : character;
                    var span = $"<span style=\"background-color: {AnsiColorToHex(background)}; color: {AnsiColorToHex(foreground)}; display: inline-block; line-height: 1;\">{character}</span>";
                    stringBuilder.Append(span);
                }

                if (row < builder.DisplaySize.Height - 1)
                    stringBuilder.Append("<br>");
            }

            // append as raw HTML using styling to specify monospace for correct horizontal alignment and pre to preserve whitespace
            return $"<pre style=\"font-family: 'Courier New', Courier, monospace; line-height: 1; font-size: 1em;\">{stringBuilder}</pre>";
        }

        /// <summary>
        /// Convert an instance of IConsoleFrame to a HTML string.
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
                    var span = $"<span style=\"background-color: {AnsiColorToHex(cell.Background)}; color: {AnsiColorToHex(cell.Foreground)}; display: inline-block; line-height: 1;\">{character}</span>";
                    stringBuilder.Append(span);
                }

                if (row < size.Height - 1)
                    stringBuilder.Append("<br>");
            }

            // append as raw HTML using styling to specify monospace for correct horizontal alignment and pre to preserve whitespace
            return $"<pre style=\"font-family: 'Courier New', Courier, monospace; line-height: 1; font-size: 1em;\">{stringBuilder}</pre>";
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
            // convert the console frame to an HTML frame if possible
            if (frame is IConsoleFrame ansiFrame)
                presenter.Present(Convert(ansiFrame, Game.Configuration.DisplaySize));
            else
                frame.Render(presenter);
        }

        #endregion
    }
}
