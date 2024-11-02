using System.Linq;
using System.Text;
using NetAF.Assets.Interaction;

namespace NetAF.Commands.Game
{
    /// <summary>
    /// Represents the Take all command.
    /// </summary>
    internal class TakeAll : ICommand
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
                return new(ReactionResult.Error, "No game specified.");

            if (game.Player == null)
                return new(ReactionResult.Error, "You must specify a character.");

            if (game.Overworld.CurrentRegion.CurrentRoom == null)
                return new(ReactionResult.Error, "Not in a room.");

            StringBuilder builder = new();

            foreach (var item in game.Overworld.CurrentRegion.CurrentRoom.Items.Where(x => x.IsTakeable && x.IsPlayerVisible))
            {
                game.Overworld.CurrentRegion.CurrentRoom.RemoveItem(item);
                game.Player.AddItem(item);

                builder.Append($"{item.Identifier.Name}, ");
            }

            if (builder.Length > 0)
            {
                builder.Remove(builder.Length - 2, 2);
                return new(ReactionResult.OK, $"Took {builder}.");
            }
            else
            {
                return new(ReactionResult.Error, "Nothing to take.");
            }
        }

        #endregion
    }
}
