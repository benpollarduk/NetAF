using NetAF.Rendering;

namespace NetAF.Targets.Console.Rendering
{
    /// <summary>
    /// Provides options for rendering a console frame.
    /// </summary>
    public sealed class ConsoleRenderProperties : RenderProperties
    {
        #region Constants

        /// <summary>
        /// Get a value indicating that there is no limit to the number of characters to display per line of narrative text.
        /// </summary>
        public const int UnlimitedNarrativeLineWidth = -1;

        #endregion

        #region StaticProperties

        /// <summary>
        /// Get the default console render properties.
        /// </summary>
        public static ConsoleRenderProperties Default => new()
        {
            LeftBoundary = '|',
            RightBoundary = '|',
            HorizontalDivider = '-',
            BackgroundColor = AnsiColor.Black,
            BorderColor = AnsiColor.BrightBlack,
            TitleColor = AnsiColor.White,
            CommandColor = NetAFPalette.NetAFGreen,
            CommandDescriptionColor = NetAFPalette.NetAFYellow,
            OptionColor = NetAFPalette.NetAFGreen,
            OptionDescriptionColor = NetAFPalette.NetAFYellow,
            MainTextColor = AnsiColor.White,
            SecondaryTextColor = AnsiColor.BrightBlack,
            InputColor = AnsiColor.White,
            MaximumNarrativeLineWidth = UnlimitedNarrativeLineWidth,
            HighlightKeywords = true,
            HighlightTextColor = NetAFPalette.NetAFBlue
        };

        #endregion

        #region Properties

        /// <summary>
        /// Get or set the character used for the left boundary.
        /// </summary>
        public char LeftBoundary { get; set; }

        /// <summary>
        /// Get or set the character used for the right boundary.
        /// </summary>
        public char RightBoundary { get; set; }

        /// <summary>
        /// Get or set the character used for the horizontal divider.
        /// </summary>
        public char HorizontalDivider { get; set; }

        /// <summary>
        /// Get or set the background color.
        /// </summary>
        public AnsiColor BackgroundColor { get; set; }

        /// <summary>
        /// Get or set the border color.
        /// </summary>
        public AnsiColor BorderColor { get; set; }

        /// <summary>
        /// Get or set the title color.
        /// </summary>
        public AnsiColor TitleColor { get; set; }

        /// <summary>
        /// Get or set the command color.
        /// </summary>
        public AnsiColor CommandColor { get; set; }

        /// <summary>
        /// Get or set the command description color.
        /// </summary>
        public AnsiColor CommandDescriptionColor { get; set; }

        /// <summary>
        /// Get or set the option color.
        /// </summary>
        public AnsiColor OptionColor { get; set; }

        /// <summary>
        /// Get or set the option description color.
        /// </summary>
        public AnsiColor OptionDescriptionColor { get; set; }

        /// <summary>
        /// Get or set the main text color.
        /// </summary>
        public AnsiColor MainTextColor { get; set; }

        /// <summary>
        /// Get or set the secondary text color.
        /// </summary>
        public AnsiColor SecondaryTextColor { get; set; }

        /// <summary>
        /// Get or set the input color.
        /// </summary>
        public AnsiColor InputColor { get; set; }

        /// <summary>
        /// Get or set the highlight text color.
        /// </summary>
        public AnsiColor HighlightTextColor { get; set; }

        /// <summary>
        /// Get or set the maximum number of characters to display per line of narrative text. For no limit use ConsoleRenderProperties.UnlimitedNarrativeLineWidth.
        /// </summary>
        public int MaximumNarrativeLineWidth { get; set; }

        #endregion
    }
}
