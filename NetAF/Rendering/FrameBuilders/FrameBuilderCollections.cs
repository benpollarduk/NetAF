using NetAF.Rendering.Console;
using NetAF.Rendering.Console.FrameBuilders;
using NetAF.Rendering.Html.FrameBuilders;
using NetAF.Rendering.Html;

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
                    new ConsoleConversationFrameBuilder(gridLayoutBuilder)
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
                var gridLayoutBuilder = new GridStringBuilder();
                var htmlBuilder = new HtmlBuilder();

                IFrameBuilder[] frameBuilders =
                [
                    new HtmlTitleFrameBuilder(htmlBuilder),
                    new ConsoleSceneFrameBuilder(gridLayoutBuilder, new ConsoleRoomMapBuilder(gridLayoutBuilder)),
                    new ConsoleRegionMapFrameBuilder(gridLayoutBuilder, new ConsoleRegionMapBuilder(gridLayoutBuilder)),
                    new ConsoleCommandListFrameBuilder(gridLayoutBuilder),
                    new ConsoleHelpFrameBuilder(gridLayoutBuilder),
                    new ConsoleCompletionFrameBuilder(gridLayoutBuilder),
                    new ConsoleGameOverFrameBuilder(gridLayoutBuilder),
                    new ConsoleAboutFrameBuilder(gridLayoutBuilder),
                    new ConsoleReactionFrameBuilder(gridLayoutBuilder),
                    new ConsoleConversationFrameBuilder(gridLayoutBuilder)
                ];

                return new(frameBuilders);
            }
        }
    }
}
