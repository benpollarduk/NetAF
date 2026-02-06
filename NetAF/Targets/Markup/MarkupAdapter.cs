using NetAF.Logic;
using NetAF.Rendering;
using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Markup.Rendering;
using System.Drawing;
using Size = NetAF.Assets.Size;

namespace NetAF.Targets.Markup
{
    /// <summary>
    /// Provides an adapter for markup.
    /// </summary>
    /// <param name="presenter">The presenter to use for presenting frames.</param>
    public sealed class MarkupAdapter(IFramePresenter presenter) : IIOAdapter
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
        /// Convert the contents of a GridStringBuilder to markup.
        /// </summary>
        /// <param name="builder">The GridStringBuilder to convert.</param>
        /// <param name="padEmptyCharacters">Specify if empty characters should be padded with a space.</param>
        /// <param name="retainFontColors">Specify if font colors should be retained.</param>
        /// <param name="useMonospace">Specify if the markup should be rendered in a monospace font to retain horizontal cell spacing.</param>
        /// <returns>A markup string representing the contents of the GridStringBuilder.</returns>
        public static string ConvertGridStringBuilderToMarkupString(GridStringBuilder builder, bool padEmptyCharacters = true, bool retainFontColors = true, bool useMonospace = true)
        {
            var markupBuilder = new MarkupBuilder();

            for (var row = 0; row < builder.DisplaySize.Height; row++)
            {
                for (var column = 0; column < builder.DisplaySize.Width; column++)
                {
                    var foreground = builder.GetCellColor(column, row);
                    var character = builder.GetCharacter(column, row);
                    character = character == 0 && padEmptyCharacters ? ' ' : character;

                    if (retainFontColors)
                        markupBuilder.Text(character.ToString(), new TextStyle(Foreground: ColorTranslator.FromHtml(AnsiColorToHex(foreground))));
                    else
                        markupBuilder.Text(character.ToString());
                }

                if (row < builder.DisplaySize.Height - 1)
                    markupBuilder.Newline();
            }

            var content = markupBuilder.ToString();
            markupBuilder.Clear();
            markupBuilder.Text(content, new TextStyle(Monospace: useMonospace));
            return markupBuilder.ToString();
        }

        /// <summary>
        /// Convert the contents of a GridVisualBuilder to markup.
        /// </summary>
        /// <param name="builder">The GridVisualBuilder to convert.</param>
        /// <returns>A markup string representing the contents of the GridVisualBuilder.</returns>
        public static string ConvertGridVisualBuilderToMarkupString(GridVisualBuilder builder)
        {
            var markupBuilder = new MarkupBuilder();

            for (var row = 0; row < builder.DisplaySize.Height; row++)
            {
                for (var column = 0; column < builder.DisplaySize.Width; column++)
                {
                    var background = builder.GetCellBackgroundColor(column, row);
                    var foreground = builder.GetCellForegroundColor(column, row);
                    var character = builder.GetCharacter(column, row);
                    character = character == 0 ? ' ' : character;
                    var foregoundColor = ColorTranslator.FromHtml(AnsiColorToHex(foreground));
                    var backgroundColor = ColorTranslator.FromHtml(AnsiColorToHex(background));
                    markupBuilder.Text(character.ToString(), new TextStyle(Foreground: foregoundColor, Background: backgroundColor));
                }

                if (row < builder.DisplaySize.Height - 1)
                    markupBuilder.Newline();
            }

            var content = markupBuilder.ToString();
            markupBuilder.Clear();
            markupBuilder.Text(content, new TextStyle(Monospace: true));
            return markupBuilder.ToString();
        }

        /// <summary>
        /// Convert an instance of IConsoleFrame to a HTML string.
        /// </summary>
        /// <param name="frame">The frame to convert.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The converted string.</returns>
        internal static string Convert(IConsoleFrame frame, Size size)
        {
            var markupBuilder = new MarkupBuilder();

            for (var row = 0; row < size.Height; row++)
            {
                for (var column = 0; column < size.Width; column++)
                {
                    var cell = frame.GetCell(column, row);
                    var character = cell.Character == 0 ? ' ' : cell.Character;
                    var foregoundColor = ColorTranslator.FromHtml(AnsiColorToHex(cell.Foreground));
                    var backgroundColor = ColorTranslator.FromHtml(AnsiColorToHex(cell.Background));
                    markupBuilder.Text(character.ToString(), new TextStyle(Foreground: foregoundColor, Background: backgroundColor));
                }

                if (row < size.Height - 1)
                    markupBuilder.Newline();
            }

            var content = markupBuilder.ToString();
            markupBuilder.Clear();
            markupBuilder.Text(content, new TextStyle(Monospace: true));
            return markupBuilder.ToString();
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
            else
                frame.Render(presenter);
        }

        #endregion
    }
}
