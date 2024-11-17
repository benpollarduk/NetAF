using System;
using System.Collections.Generic;
using System.Linq;
using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Interaction;
using NetAF.Logic;

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

        #region StaticMethods

        /// <summary>
        /// Handle an item expiring.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="item">The item that expired.</param>
        private static void ItemExpired(Game game, Item item)
        {
            List<IItemContainer> containers = [];

            containers.Add(game.Player);
            containers.Add(game.Overworld.CurrentRegion.CurrentRoom);
            containers.AddRange(game.Overworld.CurrentRegion.CurrentRoom.Characters ?? []);

            foreach (var container in containers)
            {
                if (container.Items.Contains(item))
                    container.RemoveItem(item);
            }
        }

        /// <summary>
        /// Handle a target expiring.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="target">The item that expired.</param>
        private static void TargetExpired(Game game, IInteractWithItem target)
        {
            if (target is IExaminable examinable && game.Overworld.CurrentRegion.CurrentRoom.ContainsInteractionTarget(examinable.Identifier.Name))
                game.Overworld.CurrentRegion.CurrentRoom.RemoveInteractionTarget(target);

            if (target is Item item)
            {
                foreach (var npc in game.Overworld.CurrentRegion.CurrentRoom.Characters ?? [])
                {
                    if (npc.HasItem(item))
                        npc.RemoveItem(item);
                }

                if (game.Player.HasItem(item))
                    game.Player.RemoveItem(item);
            }

            if (target is Character character)
                character.Kill();
        }

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
                case InteractionEffect.NeitherItemOrTargetExpired:
                    break;
                case InteractionEffect.ItemExpired:
                    ItemExpired(game, item);
                    break;
                case InteractionEffect.TargetExpired:
                    TargetExpired(game, target);
                    break;
                case InteractionEffect.ItemAndTargetExpired:
                    ItemExpired(game, item);
                    TargetExpired(game, target);
                    break;
                default:
                    throw new NotImplementedException();
            }

            return new(ReactionResult.Inform, result.Description);
        }

        #endregion
    }
}