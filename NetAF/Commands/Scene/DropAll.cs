using NetAF.Logic;
using System.Linq;
using System.Text;

namespace NetAF.Commands.Scene
{
    /// <summary>
    /// Represents the Drop All command.
    /// </summary>
    public sealed class DropAll : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help.
        /// </summary>
        public static CommandHelp CommandHelp { get; } = new("Drop All", "Drop all items", CommandCategory.Scene);

        #endregion

        #region Implementation of ICommand

        /// <summary>
        /// Get the help for this command.
        /// </summary>
        public CommandHelp Help => CommandHelp;

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
                return new(ReactionResult.Error, $"{game.Player.Identifier.Name} cannot drop items.");

            if (game.Overworld.CurrentRegion.CurrentRoom == null)
                return new(ReactionResult.Error, "Not in a room.");

            StringBuilder builder = new();

            var items = game.Player.Items.Where(x => x.IsPlayerVisible && x.IsTakeable).ToArray();

            for (var i = 0; i < items.Length; i++)
            {
                var item = items[i];
                game.Player.RemoveItem(item);
                game.Overworld.CurrentRegion.CurrentRoom.AddItem(item);

                if (i < items.Length - 2)
                    builder.Append($"{item.Identifier.Name}, ");
                else if (i < items.Length - 1)
                    builder.Append($"{item.Identifier.Name} and ");
                else
                    builder.Append($"{item.Identifier.Name}");
            }

            if (builder.Length > 0)
                return new(ReactionResult.Inform, $"Dropped {builder}.");
            else
                return new(ReactionResult.Error, "Nothing to drop.");
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
