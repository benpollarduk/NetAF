﻿using NetAF.Logic.Modes;

namespace NetAF.Commands.Frame
{
    /// <summary>
    /// Represents the CommandsOff command.
    /// </summary>
    public sealed class CommandsOff : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help.
        /// </summary>
        public static CommandHelp CommandHelp { get; } = new("CommandsOff", "Turn commands off");

        #endregion

        #region Implementation of ICommand

        /// <summary>
        /// Invoke the command.
        /// </summary>
        /// <param name="game">The game to invoke the command on.</param>
        /// <returns>The reaction.</returns>
        public Reaction Invoke(Logic.Game game)
        {
            if (game == null)
                return new(ReactionResult.Error, "No game specified.");

            SceneMode.DisplayCommandList = false;
            return new(ReactionResult.Inform, "Commands have been turned off.");
        }

        #endregion
    }
}