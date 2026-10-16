using NetAF.Logic;
using NetAF.Logic.Modes;
using NetAF.Rendering;
using System;

namespace NetAF.Commands.RegionMap
{
    /// <summary>
    /// Represents the Zoom Out command.
    /// </summary>
    public sealed class ZoomOut : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help.
        /// </summary>
        public static CommandHelp CommandHelp { get; } = new("Zoom Out", "Zoom out to show a less detailed map", CommandCategory.RegionMap, shortcut: "O");

        #endregion

        #region Implementation of ICommand

        /// <inheritdoc/>
        public CommandHelp Help => CommandHelp;

        /// <inheritdoc/>
        public Reaction Invoke(Game game)
        {
            if (game == null)
                return new(ReactionResult.Error, "No game specified.");

            FrameProperties.RegionMapRenderOptions.MapDetail = FrameProperties.RegionMapRenderOptions.MapDetail switch
            {
                RegionMapDetail.Low => RegionMapDetail.Low,
                RegionMapDetail.Normal => RegionMapDetail.Low,
                RegionMapDetail.High => RegionMapDetail.Normal,
                _ => throw new NotImplementedException()
            };

            if (game.Mode is RegionMapMode mapMode)
                mapMode.RegionMapOptions.MapDetail = FrameProperties.RegionMapRenderOptions.MapDetail;

            return new(ReactionResult.Silent, "Zoomed out.");
        }

        /// <inheritdoc/>
        public Prompt[] GetPrompts(Game game)
        {
            return [];
        }

        #endregion
    }
}