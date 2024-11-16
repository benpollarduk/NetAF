using System;
using NetAF.Assets;
using NetAF.Assets.Interaction;

namespace NetAF.Commands.Scene
{
    /// <summary>
    /// Represents the UseOn command.
    /// </summary>
    /// <param name="item">The item to use.</param>
    /// <param name="target">The target of the command.</param>
    public sealed class UseOn(Item item, IInteractWithItem target) : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help.
        /// </summary>
        public static CommandHelp UseCommandHelp { get; } = new("Use", "Use an item on the current room");

        /// <summary>
        /// Get the command help for on.
        /// </summary>
        public static CommandHelp OnCommandHelp { get; } = new("On", "Use an item on another item or character");

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

            if (item == null)
                return new(ReactionResult.Error, "You must specify an item.");

            if (target == null)
                return new(ReactionResult.Error, "You must specify a target.");

            if (game.Player == null)
                return new(ReactionResult.Error, "You must specify the character that is using this item.");

            var result = target.Interact(item);

            switch (result.Effect)
            {
                case InteractionEffect.ItemUsedUp:

                    if (game.Overworld.CurrentRegion.CurrentRoom.ContainsItem(item))
                        game.Overworld.CurrentRegion.CurrentRoom.RemoveItem(item);
                    else if (game.Player.HasItem(item))
                        game.Player.RemoveItem(item);

                    break;

                case InteractionEffect.TargetUsedUp:

                    if (target is IExaminable examinable && game.Overworld.CurrentRegion.CurrentRoom.ContainsInteractionTarget(examinable.Identifier.Name))
                        game.Overworld.CurrentRegion.CurrentRoom.RemoveInteractionTarget(target);

                    break;

                case InteractionEffect.NoEffect:
                case InteractionEffect.SelfContained:
                    break;
                default:
                    throw new NotImplementedException();
            }

            return new(ReactionResult.OK, result.Description);
        }

        #endregion
    }
}