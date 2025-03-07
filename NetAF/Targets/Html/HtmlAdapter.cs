using NetAF.Assets;
using NetAF.Logic;
using NetAF.Rendering;
using NetAF.Targets.Console.Rendering;
using System.Text;
using System.Threading;

namespace NetAF.Targets.Html
{
    /// <summary>
    /// Provides an adapter for HTML.
    /// </summary>
    /// <param name="presenter">The presenter to use for presenting frames.</param>
    public sealed class HtmlAdapter(IFramePresenter presenter) : IIOAdapter
    {
        #region Fields

        private readonly ManualResetEvent gate = new(false);
        private string lastReceivedInput = string.Empty;

        #endregion

        #region Properties

        /// <summary>
        /// Get the game.
        /// </summary>
        public Game Game { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Register that and acknowledge was received.
        /// </summary>
        public void AcknowledgeReceived()
        {
            gate.Set();
        }

        /// <summary>
        /// Register that input was received.
        /// </summary>
        /// <param name="input">The received input.</param>
        public void InputReceived(string input)
        {
            lastReceivedInput = input;
            gate.Set();
        }

        /// <summary>
        /// Clear the input.
        /// </summary>
        private void ClearInput()
        {
            lastReceivedInput = string.Empty;
        }

        /// <summary>
        /// Read and clear the input.
        /// </summary>
        /// <returns>The input.</returns>
        private string ReadAndClearInput()
        {
            var input = lastReceivedInput.Clone().ToString();
            ClearInput();
            return input;
        }

        /// <summary>
        /// Wait for acknowledgment.
        /// </summary>
        /// <param name="timeout">The timeout, in milliseconds.</param>
        /// <returns>True if the acknowledgment was received correctly, else false.</returns>
        public bool WaitForAcknowledge(int timeout)
        {
            gate.Reset();
            return gate.WaitOne(timeout);
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Convert the contents of a GridStringBuilder to HTML.
        /// </summary>
        /// <param name="builder">The GridStringBuilder to convert.</param>
        /// <param name="padEmptyCharacters">Specify if empty characters should be padded with a space.</param>
        /// <returns>A HTML string representing the contents of the GridStringBuilder.</returns>
        public static string ConvertGridStringBuilderToHtmlString(GridStringBuilder builder, bool padEmptyCharacters = true)
        {
            StringBuilder stringBuilder = new();

            for (var row = 0; row < builder.DisplaySize.Height; row++)
            {
                for (var column = 0; column < builder.DisplaySize.Width; column++)
                {
                    var character = builder.GetCharacter(column, row);

                    if (padEmptyCharacters && character == 0)
                        character = ' ';

                    stringBuilder.Append(character);
                }

                stringBuilder.Append("<br>");
            }

            return stringBuilder.ToString();
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

        #region Implementation of IIOAdapter

        /// <summary>
        /// Setup for a game.
        /// </summary>
        /// <param name="game">The game to set up for.</param>
        public void Setup(Game game)
        {
            Game = game;
            ClearInput();
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

        /// <summary>
        /// Wait for acknowledgment.
        /// </summary>
        /// <returns>True if the acknowledgment was received correctly, else false.</returns>
        public bool WaitForAcknowledge()
        {
            return WaitForAcknowledge(Timeout.Infinite);
        }

        /// <summary>
        /// Wait for input.
        /// </summary>
        /// <returns>The input.</returns>
        public string WaitForInput()
        {
            gate.Reset();
            ClearInput();
            gate.WaitOne(Timeout.Infinite);
            return ReadAndClearInput();
        }

        #endregion
    }
}
