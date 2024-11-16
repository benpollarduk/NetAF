using NetAF.Assets;
using NetAF.Assets.Interaction;

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
        public static CommandHelp CommandHelp { get; } = new("Take", "Take an item", "T");

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
                return new(ReactionResult.Error, "You must specify what to take.");

            if (!game.Overworld.CurrentRegion.CurrentRoom.ContainsItem(item))
                return new(ReactionResult.Error, "The room does not contain that item.");

            if (!item.IsTakeable)
                return new(ReactionResult.Error, $"{item.Identifier.Name} cannot be taken.");

            game.Overworld.CurrentRegion.CurrentRoom.RemoveItem(item);
            game.Player.AddItem(item);

            return new(ReactionResult.Inform, $"Took {item.Identifier.Name}");
        }

        #endregion
    }
}
