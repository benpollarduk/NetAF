using NetAF.Rendering.FrameBuilders;
using NetAF.Targets.Html.Rendering.FrameBuilders;
using NetAF.Targets.Html.Rendering;

namespace NetAF.Example.Blazor
{
    /// <summary>
    /// Contains the frame builder collections for the game.
    /// </summary>
    internal static class FrameBuilderCollections
    {
        /// <summary>
        /// Get the frame builders for console emulation.
        /// </summary>
        internal static FrameBuilderCollection ConsoleEmulation => Rendering.FrameBuilders.FrameBuilderCollections.Console;

        /// <summary>
        /// Get the frame builders using HTML.
        /// </summary>
        internal static FrameBuilderCollection Html
        {
            get
            {
                var htmlBuilder = new HtmlBuilder();

                return new FrameBuilderCollection(
                    new HtmlTitleFrameBuilder(htmlBuilder),
                    new HtmlSceneFrameBuilder(htmlBuilder, new HtmlRoomMapBuilder(htmlBuilder) { MaxSize = new(40, 7) }),
                    new HtmlRegionMapFrameBuilder(htmlBuilder, new HtmlRegionMapBuilder(htmlBuilder) { MaxSize = new(40, 30) }),
                    new HtmlCommandListFrameBuilder(htmlBuilder),
                    new HtmlHelpFrameBuilder(htmlBuilder),
                    new HtmlCompletionFrameBuilder(htmlBuilder),
                    new HtmlGameOverFrameBuilder(htmlBuilder),
                    new HtmlAboutFrameBuilder(htmlBuilder),
                    new HtmlReactionFrameBuilder(htmlBuilder),
                    new HtmlConversationFrameBuilder(htmlBuilder),
                    new HtmlLogFrameBuilder(htmlBuilder));
            }
        }
    }
}
