using NetAF.Logic;
using NetAF.Logic.Modes;
using NetAF.Rendering;
using System;

namespace NetAF.Commands.RegionMap
{
    /// <summary>
    /// Represents the Zoom In command.
    /// </summary>
    public sealed class ZoomIn : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help.
        /// </summary>
        public static CommandHelp CommandHelp { get; } = new("Zoom In", "Zoom in to show a more detailed map", CommandCategory.RegionMap, shortcut: "I");

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
                RegionMapDetail.Low => RegionMapDetail.Normal,
                RegionMapDetail.Normal => RegionMapDetail.High,
                RegionMapDetail.High => RegionMapDetail.High,
                _ => throw new NotImplementedException()
            };

            if (game.Mode is RegionMapMode mapMode)
                mapMode.RegionMapOptions.MapDetail = FrameProperties.RegionMapRenderOptions.MapDetail;

            return new(ReactionResult.Silent, "Zoomed in.");
        }

        /// <inheritdoc/>
        public Prompt[] GetPrompts(Game game)
        {
            return [];
        }

        #endregion
    }
}