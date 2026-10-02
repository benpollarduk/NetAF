using System.Text;
using NetAF.Rendering;
using NetAF.Utilities;

namespace NetAF.Targets.Console.Rendering
{
    /// <summary>
    /// Provides a grid based frame for displaying a command based interface.
    /// </summary>
    /// <param name="builder">The builder that creates the frame.</param>
    /// <param name="cursorLeft">The cursor left position.</param>
    /// <param name="cursorTop">The cursor top position.</param>
    /// <param name="backgroundColor">The background color.</param>
    public sealed class GridTextFrame(GridStringBuilder builder, int cursorLeft, int cursorTop, AnsiColor backgroundColor) : IConsoleFrame
    {
        #region Properties

        /// <summary>
        /// Get the background color.
        /// </summary>
        public AnsiColor BackgroundColor { get; } = backgroundColor;

        #endregion

        #region Overrides of Object

        /// <inheritdoc/>
        public override string ToString()
        {
            StringBuilder stringBuilder = new();

            for (var y = 0; y < builder.DisplaySize.Height; y++)
            {
                for (var x = 0; x < builder.DisplaySize.Width; x++)
                    stringBuilder.Append(builder.GetCharacter(x, y));

                stringBuilder.Append(StringUtilities.Newline);
            }

            return stringBuilder.ToString();
        }

        #endregion

        #region Implementation of IConsoleFrame

        /// <inheritdoc/>
        public int CursorLeft { get; } = cursorLeft;

        /// <inheritdoc/>
        public int CursorTop { get; } = cursorTop;

        /// <inheritdoc/>
        public bool ShowCursor { get; set; } = true;

        #endregion

        #region Implementation of IFrame

        /// <inheritdoc/>
        public void Render(IFramePresenter presenter)
        {
            var suppressColor = Ansi.IsColorSuppressed();

            if (!suppressColor)
                presenter.Present(Ansi.GetAnsiBackgroundEscapeSequence(BackgroundColor));

            presenter.Present(Ansi.ANSI_HIDE_CURSOR);

            for (var y = 0; y < builder.DisplaySize.Height; y++)
            {
                for (var x = 0; x < builder.DisplaySize.Width; x++)
                {
                    var cell = GetCell(x, y);

                    if (cell.Character != 0)
                    {
                        if (!suppressColor)
                            presenter.Present(Ansi.GetAnsiForegroundEscapeSequence(cell.Foreground));

                        presenter.Present(cell.Character.ToString());
                    }
                    else
                    {
                        presenter.Present(" ");
                    }
                }

                if (y < builder.DisplaySize.Height - 1)
                    presenter.Present(builder.LineTerminator.ToString());
            }

            presenter.Present(Ansi.ANSI_SHOW_CURSOR);
        }

        #endregion

        #region Implementation of IAnsiGridFrame

        /// <inheritdoc/>
        public AnsiCell GetCell(int x, int y)
        {
            return new AnsiCell(builder.GetCharacter(x, y), builder.GetCellColor(x, y), BackgroundColor);
        }

        #endregion
    }
}