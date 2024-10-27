﻿using NetAF.Assets.Interaction;

namespace NetAF.Commands.Global
{
    /// <summary>
    /// Represents the New command.
    /// </summary>
    internal class New : ICommand
    {
        #region Implementation of ICommand

        /// <summary>
        /// Invoke the command.
        /// </summary>
        /// <param name="game">The game to invoke the command on.</param>
        /// <returns>The reaction.</returns>
        public Reaction Invoke(Logic.Game game)
        {
            if (game == null)
                return new Reaction(ReactionResult.Error, "No game specified.");

            game.End();
            return new Reaction(ReactionResult.OK, "New game.");
        }

        #endregion
    }
}