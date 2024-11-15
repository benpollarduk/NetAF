using NetAF.Commands;
using NetAF.Commands.Global;
using NetAF.Commands.RegionMap;
using NetAF.Logic;
using NetAF.Logic.Modes;
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
            PanUp.CommandHelp,
            PanDown.CommandHelp,
            PanNorth.CommandHelp,
            PanSouth.CommandHelp,
            PanWest.CommandHelp,
            PanEast.CommandHelp,
            PanReset.CommandHelp,
            End.CommandHelp,
        ];

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
            if (PanUp.CommandHelp.Equals(input))
                return new(true, new PanUp());

            if (PanDown.CommandHelp.Equals(input))
                return new(true, new PanDown());

            if (PanNorth.CommandHelp.Equals(input))
                return new(true, new PanNorth());

            if (PanSouth.CommandHelp.Equals(input))
                return new(true, new PanSouth());

            if (PanWest.CommandHelp.Equals(input))
                return new(true, new PanWest());

            if (PanEast.CommandHelp.Equals(input))
                return new(true, new PanEast());

            if (PanReset.CommandHelp.Equals(input))
                return new(true, new PanReset());

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
                if (RegionMapMode.CanPanToPosition(game.Overworld.CurrentRegion, PanUp.GetPanPosition(regionMapMode.FocusPosition)))
                    commands.Add(PanUp.CommandHelp);

                if (RegionMapMode.CanPanToPosition(game.Overworld.CurrentRegion, PanDown.GetPanPosition(regionMapMode.FocusPosition)))
                    commands.Add(PanDown.CommandHelp);

                if (RegionMapMode.CanPanToPosition(game.Overworld.CurrentRegion, PanNorth.GetPanPosition(regionMapMode.FocusPosition)))
                    commands.Add(PanNorth.CommandHelp);

                if (RegionMapMode.CanPanToPosition(game.Overworld.CurrentRegion, PanSouth.GetPanPosition(regionMapMode.FocusPosition)))
                    commands.Add(PanSouth.CommandHelp);

                if (RegionMapMode.CanPanToPosition(game.Overworld.CurrentRegion, PanWest.GetPanPosition(regionMapMode.FocusPosition)))
                    commands.Add(PanWest.CommandHelp);

                if (RegionMapMode.CanPanToPosition(game.Overworld.CurrentRegion, PanEast.GetPanPosition(regionMapMode.FocusPosition)))
                    commands.Add(PanEast.CommandHelp);

                if (!regionMapMode.FocusPosition.Equals(RegionMapMode.Player))
                    commands.Add(PanReset.CommandHelp);

                commands.Add(new CommandHelp(End.CommandHelp.Command, "Finish looking at the map"));
            }

            return [.. commands];
        }

        #endregion
    }
}
