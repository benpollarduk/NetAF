using NetAF.Rendering.Console;
using NetAF.Rendering.Console.FrameBuilders;
using System;
using System.Collections.Generic;

namespace NetAF.Rendering.FrameBuilders
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

                Dictionary<Type, IFrameBuilder> frameBuilders = new()
                {
                    { typeof(ITitleFrameBuilder), new ConsoleTitleFrameBuilder(gridLayoutBuilder) },
                    { typeof(ISceneFrameBuilder), new ConsoleSceneFrameBuilder(gridLayoutBuilder, new ConsoleRoomMapBuilder(gridLayoutBuilder)) },
                    { typeof(IRegionMapFrameBuilder), new ConsoleRegionMapFrameBuilder(gridLayoutBuilder, new ConsoleRegionMapBuilder(gridLayoutBuilder)) },
                    { typeof(ICommandListFrameBuilder), new ConsoleCommandListFrameBuilder(gridLayoutBuilder) },
                    { typeof(IHelpFrameBuilder), new ConsoleHelpFrameBuilder(gridLayoutBuilder) },
                    { typeof(ICompletionFrameBuilder), new ConsoleCompletionFrameBuilder(gridLayoutBuilder) },
                    { typeof(IGameOverFrameBuilder), new ConsoleGameOverFrameBuilder(gridLayoutBuilder) },
                    { typeof(IAboutFrameBuilder), new ConsoleAboutFrameBuilder(gridLayoutBuilder) },
                    { typeof(IReactionFrameBuilder), new ConsoleReactionFrameBuilder(gridLayoutBuilder) },
                    { typeof(IConversationFrameBuilder), new ConsoleConversationFrameBuilder(gridLayoutBuilder) }
                };

                return new(frameBuilders);
            }
        }
    }
}
