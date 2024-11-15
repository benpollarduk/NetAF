﻿using System;
using NetAF.Assets;
using NetAF.Assets.Interaction;

namespace NetAF.Commands.Scene
{
    /// <summary>
    /// Represents the UseOn command.
    /// </summary>
    /// <param name="item">The item to use.</param>
    /// <param name="target">The target of the command.</param>
    public class UseOn(Item item, IInteractWithItem target) : ICommand
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

        #region Properties

        /// <summary>
        /// Get the item.
        /// </summary>
        public Item Item { get; } = item;

        /// <summary>
        /// Get the target.
        /// </summary>
        public IInteractWithItem Target { get; } = target;

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

            if (Item == null)
                return new(ReactionResult.Error, "You must specify an item.");

            if (Target == null)
                return new(ReactionResult.Error, "You must specify a target.");

            if (game.Player == null)
                return new(ReactionResult.Error, "You must specify the character that is using this item.");

            var result = Target.Interact(Item);

            switch (result.Effect)
            {
                case InteractionEffect.FatalEffect:

                    game.Player.Kill();
                    return new(ReactionResult.Fatal, result.Description);

                case InteractionEffect.ItemUsedUp:

                    if (game.Overworld.CurrentRegion.CurrentRoom.ContainsItem(Item))
                        game.Overworld.CurrentRegion.CurrentRoom.RemoveItem(Item);
                    else if (game.Player.HasItem(Item))
                        game.Player.RemoveItem(Item);

                    break;

                case InteractionEffect.TargetUsedUp:

                    if (Target is IExaminable examinable && game.Overworld.CurrentRegion.CurrentRoom.ContainsInteractionTarget(examinable.Identifier.Name))
                        game.Overworld.CurrentRegion.CurrentRoom.RemoveInteractionTarget(Target);

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