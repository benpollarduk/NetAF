﻿using NetAF.Logic.Modes;
using NetAF.Rendering;

namespace NetAF.Commands.Frame
{
    /// <summary>
    /// Represents the KeyOff command.
    /// </summary>
    public sealed class KeyOff : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help.
        /// </summary>
        public static CommandHelp CommandHelp { get; } = new("KeyOff", "Turn the key off");

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

            SceneMode.KeyType = KeyType.None;
            return new(ReactionResult.Inform, "Key has been turned off.");
        }

        #endregion
    }
}