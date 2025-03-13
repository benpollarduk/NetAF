using NetAF.Logic;
using System.Linq;
using System.Text;

namespace NetAF.Commands.Scene
{
    /// <summary>
    /// Represents the Take all command.
    /// </summary>
    public sealed class TakeAll : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help.
        /// </summary>
        public static CommandHelp CommandHelp { get; } = new("Take all", "Take all items in the current room", CommandCategory.Scene);

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

            if (game.Player == null)
                return new(ReactionResult.Error, "You must specify a character.");

            if (!game.Player.CanTakeAndDropItems)
                return new(ReactionResult.Error, $"{game.Player.Identifier.Name} cannot take items.");

            if (game.Overworld.CurrentRegion.CurrentRoom == null)
                return new(ReactionResult.Error, "Not in a room.");

            StringBuilder builder = new();

            var items = game.Overworld.CurrentRegion.CurrentRoom.Items.Where(x => x.IsTakeable && x.IsPlayerVisible).ToArray();

            for (var i = 0; i < items.Length; i++)
            {
                var item = items[i];
                game.Overworld.CurrentRegion.CurrentRoom.RemoveItem(item);
                game.Player.AddItem(item);

                if (i < items.Length - 2)
                    builder.Append($"{item.Identifier.Name}, ");
                else if (i < items.Length - 1)
                    builder.Append($"{item.Identifier.Name} and ");
                else
                    builder.Append($"{item.Identifier.Name}");
            }

            if (builder.Length > 0)
                return new(ReactionResult.Inform, $"Took {builder}.");
            else
                return new(ReactionResult.Error, "Nothing to take.");
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
