﻿using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Extensions;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using System.Linq;
using System.Text;

namespace NetAF.Targets.Text.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a builder of region map frames.
    /// </summary>
    /// <param name="builder">A builder to use for the string layout.</param>
    /// <param name="regionMapBuilder">A builder for region maps.</param>
    public sealed class TextRegionMapFrameBuilder(StringBuilder builder, IRegionMapBuilder regionMapBuilder) : IRegionMapFrameBuilder
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
        /// Build a frame.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="focusPosition">The position to focus on.</param>
        /// <param name="contextualCommands">The contextual commands to display.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        public IFrame Build(Region region, Point3D focusPosition, CommandHelp[] contextualCommands, Size size)
        {
            var matrix = region.ToMatrix();
            var room = matrix[focusPosition.X, focusPosition.Y, focusPosition.Z];
            var title = $"{region.Identifier.Name} - {room?.Identifier.Name}";

            builder.Clear();
            builder.AppendLine(title);

            RegionMapBuilder?.BuildRegionMap(region, focusPosition);

            if (contextualCommands?.Any() ?? false)
            {
                builder.AppendLine(CommandTitle);

                for (var index = 0; index < contextualCommands.Length; index++)
                {
                    var contextualCommand = contextualCommands[index];
                    builder.AppendLine($"{contextualCommand.DisplayCommand} - {contextualCommand.Description.EnsureFinishedSentence()}");
                }
            }

            return new TextFrame(builder);
        }

        #endregion
    }
}
