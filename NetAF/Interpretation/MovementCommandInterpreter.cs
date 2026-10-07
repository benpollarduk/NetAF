using System.Collections.Generic;
using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Commands.Movement;
using NetAF.Commands.Scene;
using NetAF.Extensions;
using NetAF.Logic;

namespace NetAF.Interpretation
{
    /// <summary>
    /// Provides an object that can be used for interpreting movement commands.
    /// </summary>
    public sealed class MovementCommandInterpreter : IInterpreter
    {
        #region StaticProperties

        /// <summary>
        /// Get an array of all supported commands.
        /// </summary>
        public static CommandHelp[] DefaultSupportedCommands { get; } =
        [
            Move.NorthCommandHelp,
            Move.EastCommandHelp,
            Move.SouthCommandHelp,
            Move.WestCommandHelp,
            Move.UpCommandHelp,
            Move.DownCommandHelp
        ];

        #endregion

        #region Methods

        /// <summary>
        /// Try and parse a string to a Direction.
        /// </summary>
        /// <param name="text">The string to parse.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>The result of the parse.</returns>
        private bool TryParseToMoveDirection(string text, out Direction direction)
        {
            if (this.IsCommand(Move.NorthCommandHelp, text))
            {
                direction = Direction.North;
                return true;
            }

            if (this.IsCommand(Move.EastCommandHelp, text))
            {
                direction = Direction.East;
                return true;
            }

            if (this.IsCommand(Move.SouthCommandHelp, text))
            {
                direction = Direction.South;
                return true;
            }

            if (this.IsCommand(Move.WestCommandHelp, text))
            {
                direction = Direction.West;
                return true;
            }

            if (this.IsCommand(Move.UpCommandHelp, text))
            {
                direction = Direction.Up;
                return true;
            }

            if (this.IsCommand(Move.DownCommandHelp, text))
            {
                direction = Direction.Down;
                return true;
            }

            direction = Direction.East;
            return false;
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Get all movement contextual commands.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns>The contextual help.</returns>
        private static CommandHelp[] GetMovementContextualCommands(Game game)
        {
            if (game.Overworld.CurrentRegion.CurrentRoom == null)
                return [];

            List<CommandHelp> commands = [];

            if (game.Overworld.CurrentRegion.CurrentRoom.CanMove(Direction.North))
                commands.Add(Move.NorthCommandHelp);

            if (game.Overworld.CurrentRegion.CurrentRoom.CanMove(Direction.East))
                commands.Add(Move.EastCommandHelp);

            if (game.Overworld.CurrentRegion.CurrentRoom.CanMove(Direction.South))
                commands.Add(Move.SouthCommandHelp);

            if (game.Overworld.CurrentRegion.CurrentRoom.CanMove(Direction.West))
                commands.Add(Move.WestCommandHelp);

            if (game.Overworld.CurrentRegion.CurrentRoom.CanMove(Direction.Up))
                commands.Add(Move.UpCommandHelp);

            if (game.Overworld.CurrentRegion.CurrentRoom.CanMove(Direction.Down))
                commands.Add(Move.DownCommandHelp);

            return [.. commands];
        }

        #endregion

        #region Implementation of IInterpreter

        /// <inheritdoc/>
        public List<CommandHelp> ExcludedCommands { get; } = [];

        /// <inheritdoc/>
        public CommandHelp[] SupportedCommands => [.. this.FilterExcludedCommands(DefaultSupportedCommands)];

        /// <inheritdoc/>
        public InterpretationResult Interpret(string input, Game game)
        {
            // try and parse as movement
            if (TryParseToMoveDirection(input, out var direction))
                return new(true, new Move(direction));

            return new(false, new Unactionable("Invalid input."));
        }

        /// <inheritdoc/>
        public CommandHelp[] GetContextualCommandHelp(Game game)
        {
            List<CommandHelp> commands = [];
            commands.AddRange(GetMovementContextualCommands(game));
            return [.. this.FilterExcludedCommands(commands)];
        }

        #endregion
    }
}
