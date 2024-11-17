using NetAF.Assets;

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
        public static CommandHelp CommandHelp { get; } = new("Drop", "Drop an item", "R");

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

            if (item == null)
                return new(ReactionResult.Error, "You must specify what to drop.");

            if (!game.Player.HasItem(item))
                return new(ReactionResult.Error, "You don't have that item.");

            game.Overworld.CurrentRegion.CurrentRoom.AddItem(item);
            game.Player.RemoveItem(item);

            return new(ReactionResult.Inform, $"Dropped {item.Identifier.Name}.");
        }

        #endregion
    }
}
