using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Console.Rendering.FrameBuilders.Modern;
using NetAF.Targets.Console.Rendering.FrameBuilders.Legacy;
using NetAF.Targets.Html.Rendering;
using NetAF.Targets.Html.Rendering.FrameBuilders;
using NetAF.Targets.Markup.Rendering;
using NetAF.Targets.Markup.Rendering.FrameBuilders;
using NetAF.Targets.Text.Rendering.FrameBuilders;
using System.Text;
using NetAF.Targets.Console.Rendering.FrameBuilders.Common;
using NetAF.Targets.Text.Rendering;

namespace NetAF.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a container for frame builder collections.
    /// </summary>
    public static class FrameBuilderCollections
    {
        /// <summary>
        /// Get the default console frame builder collection.
        /// </summary>
        public static FrameBuilderCollection Console => ConsoleModern;

        /// <summary>
        /// Get the legacy console frame builder collection.
        /// </summary>
        public static FrameBuilderCollection ConsoleLegacy
        {
            get
            {
                var renderProperties = ConsoleRenderProperties.Default;
                var mapRenderProperties = ConsoleMapRenderProperties.Default;
                var gridLayoutBuilder = new GridStringBuilder();

                IFrameBuilder[] frameBuilders =
                [
                    new ConsoleLegacyTitleFrameBuilder(gridLayoutBuilder, renderProperties),
                    new ConsoleLegacySceneFrameBuilder(gridLayoutBuilder, renderProperties, new ConsoleHighDetailRoomMapBuilder(gridLayoutBuilder, mapRenderProperties)),
                    new ConsoleLegacyRegionMapFrameBuilder(gridLayoutBuilder, renderProperties, new ConsoleRegionMapBuilder(gridLayoutBuilder, mapRenderProperties)),
                    new ConsoleLegacyCommandListFrameBuilder(gridLayoutBuilder, renderProperties),
                    new ConsoleLegacyHelpFrameBuilder(gridLayoutBuilder, renderProperties),
                    new ConsoleLegacyCompletionFrameBuilder(gridLayoutBuilder, renderProperties),
                    new ConsoleLegacyGameOverFrameBuilder(gridLayoutBuilder, renderProperties),
                    new ConsoleLegacyAboutFrameBuilder(gridLayoutBuilder, renderProperties),
                    new ConsoleLegacyReactionFrameBuilder(gridLayoutBuilder, renderProperties),
                    new ConsoleLegacyConversationFrameBuilder(gridLayoutBuilder, renderProperties),
                    new ConsoleLegacyNoteFrameBuilder(gridLayoutBuilder, renderProperties),
                    new ConsoleLegacyHistoryFrameBuilder(gridLayoutBuilder, renderProperties),
                    new ConsoleLegacyNarrativeFrameBuilder(gridLayoutBuilder, renderProperties),
                    new ConsoleLegacyVisualFrameBuilder(gridLayoutBuilder, renderProperties)
                ];

                return new(frameBuilders);
            }
        }

        /// <summary>
        /// Get the modern console frame builder collection.
        /// </summary>
        public static FrameBuilderCollection ConsoleModern
        {
            get
            {
                var renderProperties = new ConsoleRenderProperties()
                {
                    LeftBoundary = ' ',
                    RightBoundary = ' ',
                    HorizontalDivider = ' ',
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
                    MaximumNarrativeLineWidth = 70,
                    HighlightKeywords = true,
                    HighlightTextColor = NetAFPalette.NetAFBlue,
                    ShowPrompt = true,
                    Prompt = ">",
                    PromptColor = AnsiColor.White
                };

                var mapRenderProperties = ConsoleMapRenderProperties.Default;
                var gridLayoutBuilder = new GridStringBuilder();

                IFrameBuilder[] frameBuilders =
                [
                    new ConsoleModernTitleFrameBuilder(gridLayoutBuilder, renderProperties),
                    new ConsoleModernSceneFrameBuilder(gridLayoutBuilder, renderProperties, new ConsoleHighDetailRoomMapBuilder(gridLayoutBuilder, mapRenderProperties)),
                    new ConsoleModernRegionMapFrameBuilder(gridLayoutBuilder, renderProperties, new ConsoleRegionMapBuilder(gridLayoutBuilder, mapRenderProperties)),
                    new ConsoleModernCommandListFrameBuilder(gridLayoutBuilder, renderProperties),
                    new ConsoleModernHelpFrameBuilder(gridLayoutBuilder, renderProperties),
                    new ConsoleModernCompletionFrameBuilder(gridLayoutBuilder, renderProperties),
                    new ConsoleModernGameOverFrameBuilder(gridLayoutBuilder, renderProperties),
                    new ConsoleModernAboutFrameBuilder(gridLayoutBuilder, renderProperties),
                    new ConsoleModernReactionFrameBuilder(gridLayoutBuilder, renderProperties),
                    new ConsoleModernConversationFrameBuilder(gridLayoutBuilder, renderProperties),
                    new ConsoleModernNoteFrameBuilder(gridLayoutBuilder, renderProperties),
                    new ConsoleModernHistoryFrameBuilder(gridLayoutBuilder, renderProperties),
                    new ConsoleModernNarrativeFrameBuilder(gridLayoutBuilder, renderProperties),
                    new ConsoleModernVisualFrameBuilder(gridLayoutBuilder, renderProperties)
                ];

                return new(frameBuilders);
            }
        }

        /// <summary>
        /// Get the default HTML frame builder collection.
        /// </summary>
        public static FrameBuilderCollection Html
        {
            get
            {
                var renderProperties = HtmlRenderProperties.Default;
                var mapRenderProperties = ConsoleMapRenderProperties.Default;
                var htmlBuilder = new HtmlBuilder();

                IFrameBuilder[] frameBuilders =
                [
                    new HtmlTitleFrameBuilder(htmlBuilder),
                    new HtmlSceneFrameBuilder(htmlBuilder, renderProperties, new HtmlRoomMapBuilder(htmlBuilder, mapRenderProperties)),
                    new HtmlRegionMapFrameBuilder(htmlBuilder, new HtmlRegionMapBuilder(htmlBuilder, mapRenderProperties)),
                    new HtmlCommandListFrameBuilder(htmlBuilder),
                    new HtmlHelpFrameBuilder(htmlBuilder),
                    new HtmlCompletionFrameBuilder(htmlBuilder),
                    new HtmlGameOverFrameBuilder(htmlBuilder),
                    new HtmlAboutFrameBuilder(htmlBuilder),
                    new HtmlReactionFrameBuilder(htmlBuilder, renderProperties),
                    new HtmlConversationFrameBuilder(htmlBuilder, renderProperties),
                    new HtmlNoteFrameBuilder(htmlBuilder),
                    new HtmlHistoryFrameBuilder(htmlBuilder),
                    new HtmlNarrativeFrameBuilder(htmlBuilder),
                    new HtmlVisualFrameBuilder(htmlBuilder)
                ];

                return new(frameBuilders);
            }
        }

        /// <summary>
        /// Get the default text frame builder collection.
        /// </summary>
        public static FrameBuilderCollection Text
        {
            get
            {
                var renderProperties = TextRenderProperties.Default;
                var mapRenderProperties = ConsoleMapRenderProperties.Default;
                var stringBuilder = new StringBuilder();

                IFrameBuilder[] frameBuilders =
                [
                    new TextTitleFrameBuilder(stringBuilder),
                    new TextSceneFrameBuilder(stringBuilder, new TextRoomMapBuilder(stringBuilder, mapRenderProperties)),
                    new TextRegionMapFrameBuilder(stringBuilder, new TextRegionMapBuilder(stringBuilder, mapRenderProperties)),
                    new TextCommandListFrameBuilder(stringBuilder),
                    new TextHelpFrameBuilder(stringBuilder),
                    new TextCompletionFrameBuilder(stringBuilder),
                    new TextGameOverFrameBuilder(stringBuilder),
                    new TextAboutFrameBuilder(stringBuilder),
                    new TextReactionFrameBuilder(stringBuilder),
                    new TextConversationFrameBuilder(stringBuilder, renderProperties),
                    new TextNoteFrameBuilder(stringBuilder),
                    new TextHistoryFrameBuilder(stringBuilder),
                    new TextNarrativeFrameBuilder(stringBuilder),
                    new TextVisualFrameBuilder(stringBuilder)
                ];

                return new(frameBuilders);
            }
        }

        /// <summary>
        /// Get the default markup frame builder collection.
        /// </summary>
        public static FrameBuilderCollection Markup
        {
            get
            {
                var renderProperties = MarkupRenderProperties.Default;
                var mapRenderProperties = ConsoleMapRenderProperties.Default;
                var markupBuilder = new MarkupBuilder();

                IFrameBuilder[] frameBuilders =
                [
                    new MarkupTitleFrameBuilder(markupBuilder),
                    new MarkupSceneFrameBuilder(markupBuilder, renderProperties, new MarkupRoomMapBuilder(markupBuilder, mapRenderProperties)),
                    new MarkupRegionMapFrameBuilder(markupBuilder, new MarkupRegionMapBuilder(markupBuilder, mapRenderProperties)),
                    new MarkupCommandListFrameBuilder(markupBuilder),
                    new MarkupHelpFrameBuilder(markupBuilder),
                    new MarkupCompletionFrameBuilder(markupBuilder),
                    new MarkupGameOverFrameBuilder(markupBuilder),
                    new MarkupAboutFrameBuilder(markupBuilder),
                    new MarkupReactionFrameBuilder(markupBuilder, renderProperties),
                    new MarkupConversationFrameBuilder(markupBuilder, renderProperties),
                    new MarkupNoteFrameBuilder(markupBuilder),
                    new MarkupHistoryFrameBuilder(markupBuilder),
                    new MarkupNarrativeFrameBuilder(markupBuilder),
                    new MarkupVisualFrameBuilder(markupBuilder)
                ];

                return new(frameBuilders);
            }
        }
    }
}
