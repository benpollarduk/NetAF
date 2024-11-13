using NetAF.Assets;
using NetAF.Assets.Interaction;

namespace NetAF.Commands.Scene
{
    /// <summary>
    /// Represents the Drop command.
    /// </summary>
    /// <param name="item">The item to take.</param>
    public class Drop(Item item) : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help.
        /// </summary>
        public static CommandHelp CommandHelp { get; } = new("Drop", "Drop an item", "R");

        #endregion

        #region Properties

        /// <summary>
        /// Get the item.
        /// </summary>
        public Item Item { get; } = item;

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

            if (game.Player == null)
                return new(ReactionResult.Error, "You must specify a character.");

            if (Item == null)
                return new(ReactionResult.Error, "You must specify what to drop.");

            if (!game.Player.HasItem(Item))
                return new(ReactionResult.Error, "You don't have that item.");

            game.Overworld.CurrentRegion.CurrentRoom.AddItem(Item);
            game.Player.RemoveItem(Item);

            return new(ReactionResult.OK, $"Dropped {Item.Identifier.Name}.");
        }

        #endregion
    }
}
