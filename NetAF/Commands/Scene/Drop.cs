using NetAF.Assets;
using NetAF.Logic;
using System.Linq;

namespace NetAF.Commands.Scene
{
    /// <summary>
    /// Represents the Drop command.
    /// </summary>
    /// <param name="item">The item to take.</param>
    public sealed class Drop(Item item) : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help.
        /// </summary>
        public static CommandHelp CommandHelp { get; } = new("Drop", "Drop an item", "R", displayAs: "Drop/R __");

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
                return new(ReactionResult.Error, $"{game.Player.Identifier.Name} cannot drop items.");

            if (item == null)
                return new(ReactionResult.Error, "You must specify what to drop.");

            if (!game.Player.HasItem(item))
                return new(ReactionResult.Error, $"{game.Player.Identifier.Name} does not have that item.");

            game.Overworld.CurrentRegion.CurrentRoom.AddItem(item);
            game.Player.RemoveItem(item);

            return new(ReactionResult.Inform, $"Dropped {item.Identifier.Name}.");
        }

        /// <summary>
        /// Get all prompts for this command.
        /// </summary>
        /// <param name="game">The game to get the prompts for.</param>
        /// <returns>And array of prompts.</returns>
        public Prompt[] GetPrompts(Game game)
        {
            return [.. game?.Player?.Items?.Where(x => x.IsTakeable && x.IsPlayerVisible).Select(x => x.Identifier.Name).Select(x => new Prompt(x))];
        }

        #endregion
    }
}
