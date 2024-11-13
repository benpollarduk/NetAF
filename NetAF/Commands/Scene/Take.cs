using NetAF.Assets;
using NetAF.Assets.Interaction;

namespace NetAF.Commands.Scene
{
    /// <summary>
    /// Represents the Take command.
    /// </summary>
    /// <param name="item">The item to take.</param>
    public class Take(Item item) : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help.
        /// </summary>
        public static CommandHelp CommandHelp { get; } = new("Take", "Take an item", "T");

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
                return new(ReactionResult.Error, "You must specify what to take.");

            if (!game.Overworld.CurrentRegion.CurrentRoom.ContainsItem(Item))
                return new(ReactionResult.Error, "The room does not contain that item.");

            if (!Item.IsTakeable)
                return new(ReactionResult.Error, $"{Item.Identifier.Name} cannot be taken.");

            game.Overworld.CurrentRegion.CurrentRoom.RemoveItem(Item);
            game.Player.AddItem(Item);

            return new(ReactionResult.OK, $"Took {Item.Identifier.Name}");
        }

        #endregion
    }
}
