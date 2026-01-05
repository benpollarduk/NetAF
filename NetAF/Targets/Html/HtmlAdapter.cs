using NetAF.Assets;
using NetAF.Logic;
using NetAF.Rendering;
using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Html.Rendering;
using NetAF.Targets.Text.Rendering;
using System.Text;

namespace NetAF.Targets.Html
{
    /// <summary>
    /// Provides an adapter for HTML.
    /// </summary>
    /// <param name="presenter">The presenter to use for presenting frames.</param>
    public sealed class HtmlAdapter(IFramePresenter presenter) : IIOAdapter
    {
        #region Fields

        private Size displaySize;

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
        /// Wrap an HTML string in HTML that ensures that it uses an aligned monospace font.
        /// </summary>
        /// <param name="html">The HTML to wrap.</param>
        /// <returns>The wrapped HTML.</returns>
        private static string ToAlignedMonospace(string html)
        {
            // append as raw HTML using styling to specify monospace for correct horizontal alignment and pre to preserve whitespace
            return $"<pre style=\"font-family: 'Courier New', Courier, monospace; line-height: 1; font-size: 1em;\">{html}</pre>";
        }

        /// <summary>
        /// Convert the contents of a GridStringBuilder to HTML.
        /// </summary>
        /// <param name="builder">The GridStringBuilder to convert.</param>
        /// <param name="padEmptyCharacters">Specify if empty characters should be padded with a space.</param>
        /// <param name="retainFontColors">Specify if font colors should be retained.</param>
        /// <param name="useMonospace">Specify if the HTML should be rendered in a monospace font to retain horizontal cell spacing.</param>
        /// <returns>A HTML string representing the contents of the GridStringBuilder.</returns>
        public static string ConvertGridStringBuilderToHtmlString(GridStringBuilder builder, bool padEmptyCharacters = true, bool retainFontColors = true, bool useMonospace = true)
        {
            StringBuilder stringBuilder = new();

            for (var row = 0; row < builder.DisplaySize.Height; row++)
            {
                for (var column = 0; column < builder.DisplaySize.Width; column++)
                {
                    var foreground = builder.GetCellColor(column, row);
                    var character = builder.GetCharacter(column, row);
                    character = character == 0 && padEmptyCharacters ? ' ' : character;

                    if (retainFontColors)
                        stringBuilder.Append($"<span style=\"color: {AnsiColorToHex(foreground)}; display: inline-block; line-height: 1;\">{character}</span>");
                    else
                        stringBuilder.Append(character);
                }

                if (row < builder.DisplaySize.Height - 1)
                    stringBuilder.Append("<br>");
            }

            return useMonospace ? ToAlignedMonospace(stringBuilder.ToString()) : stringBuilder.ToString();
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

            return ToAlignedMonospace(stringBuilder.ToString());
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

            return ToAlignedMonospace(stringBuilder.ToString());
        }

        /// <summary>
        /// Convert an instance of TextFrame to a HTML string.
        /// </summary>
        /// <param name="frame">The frame to convert.</param>
        /// <returns>The converted string.</returns>
        internal static string Convert(TextFrame frame)
        {
            var builder = new HtmlBuilder();
            builder.P(ToAlignedMonospace(frame.ToString()));
            var htmlFrame = new HtmlFrame(builder);
            return htmlFrame.ToString();
        }

        #endregion

        #region Implementation of IIOAdapter

        /// <summary>
        /// Get the current size of the output.
        /// </summary>
        public Size CurrentOutputSize => presenter.GetPresentableSize();

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
            var renderSize = displaySize != Size.Dynamic ? displaySize : CurrentOutputSize;

            // convert the frames if possible
            if (frame is IConsoleFrame ansiFrame)
                presenter.Present(Convert(ansiFrame, renderSize));
            else if (frame is TextFrame textFrame)
                presenter.Present(Convert(textFrame));
            else
                frame.Render(presenter);
        }

        #endregion
    }
}
