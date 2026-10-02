using NetAF.Assets;
using NetAF.Logic;
using System.Linq;

namespace NetAF.Commands.Scene
{
    /// <summary>
    /// Represents the Take command.
    /// </summary>
    /// <param name="item">The item to take.</param>
    public sealed class Take(Item item) : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help.
        /// </summary>
        public static CommandHelp CommandHelp { get; } = new("Take", "Take an item", CommandCategory.Scene, "T", displayAs: "Take/T __");

        #endregion

        #region Implementation of ICommand

        /// <inheritdoc/>
        public CommandHelp Help => CommandHelp;

        /// <inheritdoc/>
        public Reaction Invoke(Game game)
        {
            if (game == null)
                return new(ReactionResult.Error, "No game specified.");

            if (game.Player == null)
                return new(ReactionResult.Error, "You must specify a character.");

            if (!game.Player.CanTakeAndDropItems)
                return new(ReactionResult.Error, $"{game.Player.Identifier.Name} cannot take items.");

            if (item == null)
                return new(ReactionResult.Error, "You must specify what to take.");

            if (!game.Overworld.CurrentRegion.CurrentRoom.ContainsItem(item))
                return new(ReactionResult.Error, "The room does not contain that item.");

            if (!item.IsTakeable)
                return new(ReactionResult.Error, $"{item.Identifier.Name} cannot be taken.");

            game.Overworld.CurrentRegion.CurrentRoom.RemoveItem(item);
            game.Player.AddItem(item);

            return new(ReactionResult.Inform, $"Took {item.Identifier.Name}");
        }

        /// <inheritdoc/>
        public Prompt[] GetPrompts(Game game)
        {
            return [.. game?.Overworld?.CurrentRegion?.CurrentRoom?.Items?.Where(x => x.IsTakeable && x.IsPlayerVisible).Select(x => x.Identifier.Name).Select(x => new Prompt(x))];
        }

        #endregion
    }
}
