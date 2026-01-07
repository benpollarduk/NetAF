using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Commands.Global;
using NetAF.Commands.RegionMap;
using NetAF.Logic;
using NetAF.Logic.Modes;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using System.Collections.Generic;

namespace NetAF.Interpretation
{
    /// <summary>
    /// Provides an object that can be used for interpreting region map commands.
    /// </summary>
    public sealed class RegionMapCommandInterpreter : IInterpreter
    {
        #region StaticProperties

        /// <summary>
        /// Get an array of all supported commands.
        /// </summary>
        public static CommandHelp[] DefaultSupportedCommands { get; } =
        [
            Pan.NorthCommandHelp,
            Pan.SouthCommandHelp,
            Pan.EastCommandHelp,
            Pan.WestCommandHelp,
            Pan.UpCommandHelp,
            Pan.DownCommandHelp,
            PanReset.CommandHelp,
            ZoomIn.CommandHelp,
            ZoomOut.CommandHelp,
            End.CommandHelp,
        ];

        #endregion

        #region StaticMethods

        /// <summary>
        /// Get available pan contextual controls.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="regionMapMode">The region map mode.</param>
        /// <returns>An array of available pan contextual controls.</returns>
        private static CommandHelp[] GetPanContextualCommands(Game game, RegionMapMode regionMapMode)
        {
            List<CommandHelp> commands = [];

            if (RegionMapMode.CanPanToPosition(game.Overworld.CurrentRegion, Pan.GetPanPosition(regionMapMode.FocusPosition, Direction.North)))
                commands.Add(Pan.NorthCommandHelp);

            if (RegionMapMode.CanPanToPosition(game.Overworld.CurrentRegion, Pan.GetPanPosition(regionMapMode.FocusPosition, Direction.South)))
                commands.Add(Pan.SouthCommandHelp);

            if (RegionMapMode.CanPanToPosition(game.Overworld.CurrentRegion, Pan.GetPanPosition(regionMapMode.FocusPosition, Direction.East)))
                commands.Add(Pan.EastCommandHelp);

            if (RegionMapMode.CanPanToPosition(game.Overworld.CurrentRegion, Pan.GetPanPosition(regionMapMode.FocusPosition, Direction.West)))
                commands.Add(Pan.WestCommandHelp);

            if (RegionMapMode.CanPanToPosition(game.Overworld.CurrentRegion, Pan.GetPanPosition(regionMapMode.FocusPosition, Direction.Up)))
                commands.Add(Pan.UpCommandHelp);

            if (RegionMapMode.CanPanToPosition(game.Overworld.CurrentRegion, Pan.GetPanPosition(regionMapMode.FocusPosition, Direction.Down)))
                commands.Add(Pan.DownCommandHelp);

            if (!regionMapMode.FocusPosition.Equals(game.Overworld.CurrentRegion.GetPositionOfRoom(game.Overworld.CurrentRegion.CurrentRoom).Position))
                commands.Add(PanReset.CommandHelp);

            return [.. commands];
        }

        /// <summary>
        /// Try and interpret a pan command.
        /// </summary>
        /// <param name="input">The string to interpret.</param>
        /// <param name="result">The result of the interpretation.</param>
        /// <returns>True if the interpretation was successful.</returns>
        private static bool TryInterpretPan(string input, out InterpretationResult result)
        {
            if (Pan.NorthCommandHelp.Equals(input))
            {
                result = new(true, new Pan(Direction.North));
                return true;
            }

            if (Pan.SouthCommandHelp.Equals(input))
            {
                result = new(true, new Pan(Direction.South));
                return true;
            }

            if (Pan.EastCommandHelp.Equals(input))
            {
                result = new(true, new Pan(Direction.East));
                return true;
            }

            if (Pan.WestCommandHelp.Equals(input))
            {
                result = new(true, new Pan(Direction.West));
                return true;
            }

            if (Pan.UpCommandHelp.Equals(input))
            {
                result = new(true, new Pan(Direction.Up));
                return true;
            }

            if (Pan.DownCommandHelp.Equals(input))
            {
                result = new(true, new Pan(Direction.Down));
                return true;
            }

            if (PanReset.CommandHelp.Equals(input))
            {
                result = new(true, new PanReset());
                return true;
            }

            result = null;
            return false;
        }

        /// <summary>
        /// Try and interpret a zoom command.
        /// </summary>
        /// <param name="input">The string to interpret.</param>
        /// <param name="result">The result of the interpretation.</param>
        /// <returns>True if the interpretation was successful.</returns>
        private static bool TryInterpretZoom(string input, out InterpretationResult result)
        {
            if (ZoomIn.CommandHelp.Equals(input))
            {
                result = new(true, new ZoomIn());
                return true;
            }

            if (ZoomOut.CommandHelp.Equals(input))
            {
                result = new(true, new ZoomOut());
                return true;
            }

            result = null;
            return false;
        }

        #endregion

        #region Implementation of IInterpreter

        /// <summary>
        /// Get an array of all supported commands.
        /// </summary>
        public CommandHelp[] SupportedCommands { get; } = DefaultSupportedCommands;

        /// <summary>
        /// Interpret a string.
        /// </summary>
        /// <param name="input">The string to interpret.</param>
        /// <param name="game">The game.</param>
        /// <returns>The result of the interpretation.</returns>
        public InterpretationResult Interpret(string input, Game game)
        {
            var builder = game.Configuration.FrameBuilders.GetFrameBuilder<IRegionMapFrameBuilder>();

            if (builder.SupportsPan && TryInterpretPan(input, out var panResult))
                return panResult;

            if (builder.SupportsZoom && TryInterpretZoom(input, out var zoomResult))
                return zoomResult;

            if (End.CommandHelp.Equals(input))
                return new(true, new End());

            return InterpretationResult.Fail;
        }

        /// <summary>
        /// Get contextual command help for a game, based on its current state.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns>The contextual help.</returns>
        public CommandHelp[] GetContextualCommandHelp(Game game)
        {
            List<CommandHelp> commands = [];

            if (game.Mode is RegionMapMode regionMapMode)
            {
                var builder = game.Configuration.FrameBuilders.GetFrameBuilder<IRegionMapFrameBuilder>();

                if (builder.SupportsPan)
                {
                    commands.AddRange(GetPanContextualCommands(game, regionMapMode));
                }

                if (builder.SupportsZoom)
                {
                    if (regionMapMode.Detail != RegionMapDetail.Maximal)
                        commands.Add(ZoomIn.CommandHelp);

                    if (regionMapMode.Detail != RegionMapDetail.Minimal)
                        commands.Add(ZoomOut.CommandHelp);
                }

                commands.Add(new CommandHelp(End.CommandHelp.Command, "Finish looking at the map", CommandCategory.RegionMap));
            }

            return [.. commands];
        }

        #endregion
    }
}
