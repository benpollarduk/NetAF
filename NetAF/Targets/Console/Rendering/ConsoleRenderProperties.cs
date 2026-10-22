using NetAF.Rendering;
using System;

namespace NetAF.Targets.Console.Rendering
{
    /// <summary>
    /// Provides options for rendering a console frame.
    /// </summary>
    public sealed class ConsoleRenderProperties : RenderProperties, ICloneable
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
            HighlightTextColor = NetAFPalette.NetAFBlue,
            RenderConversationHistory = false,
            ShowPrompt = true,
            Prompt = ">",
            PromptColor = AnsiColor.White
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

        /// <summary>
        /// Get or set if the prompt should be displayed.
        /// </summary>
        public bool ShowPrompt { get; set; }

        /// <summary>
        /// Get or set the prompt.
        /// </summary>
        public string Prompt { get; set; }

        /// <summary>
        /// Get or set the prompt color.
        /// </summary>
        public AnsiColor PromptColor { get; set; }

        #endregion

        #region Implementation of ICloneable

        /// <inheritdoc/>
        public object Clone()
        {
            return new ConsoleRenderProperties()
            {
                LeftBoundary = LeftBoundary,
                RightBoundary = RightBoundary,
                HorizontalDivider = HorizontalDivider,
                BackgroundColor = BackgroundColor,
                BorderColor = BorderColor,
                TitleColor = TitleColor,
                CommandColor = CommandColor,
                CommandDescriptionColor = CommandDescriptionColor,
                OptionColor = OptionColor,
                OptionDescriptionColor = OptionDescriptionColor,
                MainTextColor = MainTextColor,
                SecondaryTextColor = SecondaryTextColor,
                InputColor = InputColor,
                MaximumNarrativeLineWidth = MaximumNarrativeLineWidth,
                HighlightKeywords = HighlightKeywords,
                HighlightTextColor = HighlightTextColor,
                ShowPrompt = ShowPrompt,
                Prompt = Prompt,
                PromptColor = PromptColor
            };
        }

        #endregion
    }
}
