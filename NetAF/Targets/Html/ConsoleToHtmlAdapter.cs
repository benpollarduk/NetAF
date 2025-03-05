using NetAF.Assets;
using NetAF.Logic;
using NetAF.Rendering;
using NetAF.Targets.Console.Rendering;
using System.Text;

namespace NetAF.Targets.Html
{
    /// <summary>
    /// Provides an adapter for adapting console to HTML.
    /// </summary>
    public sealed class ConsoleToHtmlAdapter : HtmlAdapter
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ConsoleToHtmlAdapter class.
        /// </summary>
        /// <param name="presenter">The presenter to use for presenting frames.</param>
        public ConsoleToHtmlAdapter(IFramePresenter presenter) : base(presenter)
        {
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Convert an instance of IConsoleFrame to a HTML string.
        /// </summary>
        /// <param name="frame">The frame to convert.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The converted string.</returns>
        internal static string Convert(IConsoleFrame frame, Size size)
        {
            StringBuilder stringBuilder = new();

            static string toHex(AnsiColor color)
            {
                return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
            }

            for (var row = 0; row < size.Height; row++)
            {
                for (var column = 0; column < size.Width; column++)
                {
                    var cell = frame.GetCell(column, row);
                    var character = cell.Character == 0 ? ' ' : cell.Character;
                    var span = $"<span style=\"background-color: {toHex(cell.Background)}; color: {toHex(cell.Foreground)}; display: inline-block; line-height: 1;\">{character}</span>";
                    stringBuilder.Append(span);
                }

                if (row < size.Height - 1)
                    stringBuilder.Append("<br>");
            }

            // append as raw HTML using styling to specify monospace for correct horizontal alignment and pre to preserve whitespace
            return $"<pre style=\"font-family: 'Courier New', Courier, monospace; line-height: 1; font-size: 1em;\">{stringBuilder}</pre>";
        }

        #endregion

        #region Overrides of HtmlAdapter

        /// <summary>
        /// Render a frame.
        /// </summary>
        /// <param name="frame">The frame to render.</param>
        public override void RenderFrame(IFrame frame)
        {
            // convert the console frame to an HTML frame if possible
            if (frame is IConsoleFrame ansiFrame)
                Presenter.Present(Convert(ansiFrame, Game.Configuration.DisplaySize));
            else
                frame.Render(Presenter);
        }

        #endregion
    }
}
