﻿using System.Collections.Generic;

namespace BP.AdventureFramework.Interaction
{
    /// <summary>
    /// Represents an actionable command.
    /// </summary>
    public class ActionableCommand
    {
        #region Properties

        /// <summary>
        /// Get or set the command.
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// Get or set the description of the command.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Get or set the action of the command.
        /// </summary>
        public ActionCallback Action { get; set; } = () => new InteractionResult(InteractionEffect.NoEffect, "There was no effect");

        /// <summary>
        /// Get or set if this is visible to the player.
        /// </summary>
        public bool IsPlayerVisible { get; set; } = true;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ActionableCommand class.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="description">A description of the command.</param>
        public ActionableCommand(string command, string description)
        {
            Command = command;
            Description = description;
        }

        #endregion
    }
}