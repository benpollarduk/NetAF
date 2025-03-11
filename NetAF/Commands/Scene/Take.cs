using NetAF.Assets;
using NetAF.Commands.Prompts;
using NetAF.Logic;

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
        public static CommandHelp CommandHelp { get; } = new("Take", "Take an item", "T", displayAs: "Take/T __");

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
