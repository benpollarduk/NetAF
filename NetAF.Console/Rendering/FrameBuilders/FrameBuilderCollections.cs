using NetAF.Console.Rendering.FrameBuilders.Color;
using NetAF.Rendering.FrameBuilders;

namespace NetAF.Console.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a container from frame builder collections.
    /// </summary>
    public static class FrameBuilderCollections
    {
        /// <summary>
        /// Get the default frame builder collection.
        /// </summary>
        public static FrameBuilderCollection Default
        {
            get
            {
                var gridLayoutBuilder = new GridStringBuilder();

                return new(
                    new ColorTitleFrameBuilder(gridLayoutBuilder),
                    new ColorSceneFrameBuilder(gridLayoutBuilder, new ColorRoomMapBuilder(gridLayoutBuilder)),
                    new ColorRegionMapFrameBuilder(gridLayoutBuilder, new ColorRegionMapBuilder(gridLayoutBuilder)),
                    new ColorHelpFrameBuilder(gridLayoutBuilder),
                    new ColorCompletionFrameBuilder(gridLayoutBuilder),
                    new ColorGameOverFrameBuilder(gridLayoutBuilder),
                    new ColorAboutFrameBuilder(gridLayoutBuilder),
                    new ColorTransitionFrameBuilder(gridLayoutBuilder),
                    new ColorConversationFrameBuilder(gridLayoutBuilder));
            }
        }
    }
}
