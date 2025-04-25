using NetAF.Logic;
using NetAF.Logic.Modes;
using NetAF.Rendering;

namespace NetAF.Commands.Frame
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
        public static CommandHelp CommandHelp { get; } = new("Zoom In", "Zoom in to show a more detailed map", CommandCategory.RegionMap, shortcut: "I", displayAs: "Zoom In/I");

        #endregion

        #region Implementation of ICommand

        /// <summary>
        /// Invoke the command.
        /// </summary>
        /// <param name="game">The game to invoke the command on.</param>
        /// <returns>The reaction.</returns>
        public Reaction Invoke(Game game)
        {
            if (game == null)
                return new(ReactionResult.Error, "No game specified.");

            FrameProperties.MapDetail = RegionMapDetail.Detailed;

            if (game.Mode is RegionMapMode mapMode)
                mapMode.Detail = RegionMapDetail.Detailed;

            return new(ReactionResult.Silent, "Zoomed in.");
        }

        /// <summary>
        /// Get all prompts for this command.
        /// </summary>
        /// <param name="game">The game to get the prompts for.</param>
        /// <returns>And array of prompts.</returns>
        public Prompt[] GetPrompts(Game game)
        {
            return [];
        }

        #endregion
    }
}