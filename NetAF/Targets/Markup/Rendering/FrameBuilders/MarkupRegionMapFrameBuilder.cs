using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;

namespace NetAF.Targets.Markup.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of region map frames.
    /// </summary>
    /// <param name="builder">A builder to use for the string layout.</param>
    /// <param name="regionMapBuilder">A builder for region maps.</param>
    public sealed class MarkupRegionMapFrameBuilder(MarkupBuilder builder, IRegionMapBuilder regionMapBuilder) : IRegionMapFrameBuilder
    {
        #region Properties

        /// <summary>
        /// Get the region map builder.
        /// </summary>
        private IRegionMapBuilder RegionMapBuilder { get; } = regionMapBuilder;

        /// <summary>
        /// Get or set the command title.
        /// </summary>
        public string CommandTitle { get; set; } = "You can:";

        #endregion

        #region Implementation of IRegionMapFrameBuilder

        /// <summary>
        /// Get if this frame builder supports panning.
        /// </summary>
        public bool SupportsPan => true;

        /// <summary>
        /// Get if this frame builder supports zooming.
        /// </summary>
        public bool SupportsZoom => true;

        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="focusPosition">The position to focus on.</param>
        /// <param name="detail">The level of detail to use.</param>
        /// <param name="contextualCommands">The contextual commands to display.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        public IFrame Build(Region region, Point3D focusPosition, RegionMapDetail detail, CommandHelp[] contextualCommands, Size size)
        {
            var matrix = region.ToMatrix();
            var room = matrix[focusPosition.X, focusPosition.Y, focusPosition.Z];
            var title = $"{region.Identifier.Name} - {room?.Identifier.Name}";

            builder.Clear();
            builder.Heading(title, HeadingLevel.H1);

            RegionMapBuilder?.BuildRegionMap(region, focusPosition, detail);

            if (contextualCommands != null && contextualCommands.Length > 0)
            {
                builder.Heading(CommandTitle, HeadingLevel.H4);

                for (var index = 0; index < contextualCommands.Length; index++)
                {
                    var contextualCommand = contextualCommands[index];
                    builder.Text($"{contextualCommand.DisplayCommand} - {contextualCommand.Description.EnsureFinishedSentence()}");
                }
            }

            return new MarkupFrame(builder);
        }

        #endregion
    }
}
