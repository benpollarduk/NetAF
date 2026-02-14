using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Console.Rendering.FrameBuilders;
using NetAF.Targets.Html.Rendering;
using NetAF.Targets.Html.Rendering.FrameBuilders;
using NetAF.Targets.Markup.Rendering;
using NetAF.Targets.Markup.Rendering.FrameBuilders;
using NetAF.Targets.Text.Rendering.FrameBuilders;
using System.Text;

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
        public static FrameBuilderCollection Console
        {
            get
            {
                var gridLayoutBuilder = new GridStringBuilder();

                IFrameBuilder[] frameBuilders =
                [
                    new ConsoleTitleFrameBuilder(gridLayoutBuilder),
                    new ConsoleSceneFrameBuilder(gridLayoutBuilder, new ConsoleHighDetailRoomMapBuilder(gridLayoutBuilder)),
                    new ConsoleRegionMapFrameBuilder(gridLayoutBuilder, new ConsoleRegionMapBuilder(gridLayoutBuilder)),
                    new ConsoleCommandListFrameBuilder(gridLayoutBuilder),
                    new ConsoleHelpFrameBuilder(gridLayoutBuilder),
                    new ConsoleCompletionFrameBuilder(gridLayoutBuilder),
                    new ConsoleGameOverFrameBuilder(gridLayoutBuilder),
                    new ConsoleAboutFrameBuilder(gridLayoutBuilder),
                    new ConsoleReactionFrameBuilder(gridLayoutBuilder),
                    new ConsoleConversationFrameBuilder(gridLayoutBuilder),
                    new ConsoleNoteFrameBuilder(gridLayoutBuilder),
                    new ConsoleHistoryFrameBuilder(gridLayoutBuilder),
                    new ConsoleNarrativeFrameBuilder(gridLayoutBuilder),
                    new ConsoleVisualFrameBuilder(gridLayoutBuilder)
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
                var htmlBuilder = new HtmlBuilder();

                IFrameBuilder[] frameBuilders =
                [
                    new HtmlTitleFrameBuilder(htmlBuilder),
                    new HtmlSceneFrameBuilder(htmlBuilder, new HtmlRoomMapBuilder(htmlBuilder)),
                    new HtmlRegionMapFrameBuilder(htmlBuilder, new HtmlRegionMapBuilder(htmlBuilder)),
                    new HtmlCommandListFrameBuilder(htmlBuilder),
                    new HtmlHelpFrameBuilder(htmlBuilder),
                    new HtmlCompletionFrameBuilder(htmlBuilder),
                    new HtmlGameOverFrameBuilder(htmlBuilder),
                    new HtmlAboutFrameBuilder(htmlBuilder),
                    new HtmlReactionFrameBuilder(htmlBuilder),
                    new HtmlConversationFrameBuilder(htmlBuilder),
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
                var stringBuilder = new StringBuilder();

                IFrameBuilder[] frameBuilders =
                [
                    new TextTitleFrameBuilder(stringBuilder),
                    new TextSceneFrameBuilder(stringBuilder, new TextRoomMapBuilder(stringBuilder)),
                    new TextRegionMapFrameBuilder(stringBuilder, new TextRegionMapBuilder(stringBuilder)),
                    new TextCommandListFrameBuilder(stringBuilder),
                    new TextHelpFrameBuilder(stringBuilder),
                    new TextCompletionFrameBuilder(stringBuilder),
                    new TextGameOverFrameBuilder(stringBuilder),
                    new TextAboutFrameBuilder(stringBuilder),
                    new TextReactionFrameBuilder(stringBuilder),
                    new TextConversationFrameBuilder(stringBuilder),
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
                var markupBuilder = new MarkupBuilder();

                IFrameBuilder[] frameBuilders =
                [
                    new MarkupTitleFrameBuilder(markupBuilder),
                    new MarkupSceneFrameBuilder(markupBuilder, new MarkupRoomMapBuilder(markupBuilder)),
                    new MarkupRegionMapFrameBuilder(markupBuilder, new MarkupRegionMapBuilder(markupBuilder)),
                    new MarkupCommandListFrameBuilder(markupBuilder),
                    new MarkupHelpFrameBuilder(markupBuilder),
                    new MarkupCompletionFrameBuilder(markupBuilder),
                    new MarkupGameOverFrameBuilder(markupBuilder),
                    new MarkupAboutFrameBuilder(markupBuilder),
                    new MarkupReactionFrameBuilder(markupBuilder),
                    new MarkupConversationFrameBuilder(markupBuilder),
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
