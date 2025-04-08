using NetAF.Targets.Console.Rendering;
using NetAF.Targets.Console.Rendering.FrameBuilders;
using NetAF.Targets.Html.Rendering;
using NetAF.Targets.Html.Rendering.FrameBuilders;

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
                    new ConsoleSceneFrameBuilder(gridLayoutBuilder, new ConsoleRoomMapBuilder(gridLayoutBuilder)),
                    new ConsoleRegionMapFrameBuilder(gridLayoutBuilder, new ConsoleRegionMapBuilder(gridLayoutBuilder)),
                    new ConsoleCommandListFrameBuilder(gridLayoutBuilder),
                    new ConsoleHelpFrameBuilder(gridLayoutBuilder),
                    new ConsoleCompletionFrameBuilder(gridLayoutBuilder),
                    new ConsoleGameOverFrameBuilder(gridLayoutBuilder),
                    new ConsoleAboutFrameBuilder(gridLayoutBuilder),
                    new ConsoleReactionFrameBuilder(gridLayoutBuilder),
                    new ConsoleConversationFrameBuilder(gridLayoutBuilder),
                    new ConsoleInformationFrameBuilder(gridLayoutBuilder)
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
                    new HtmlInformationFrameBuilder(htmlBuilder)
                ];

                return new(frameBuilders);
            }
        }
    }
}
