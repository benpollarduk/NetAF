using System;
using System.Collections.Generic;
using System.Linq;
using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Logging.Events;
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
        public static CommandHelp UseCommandHelp { get; } = new("Use", "Use an item on the current room or a target", CommandCategory.Scene, displayAs: "Use __/Use__ on __", instructions: "Use an item on the current room (Use __) or on another item or another character (Use__ on __).");

        /// <summary>
        /// Get the command help for on.
        /// </summary>
        public static CommandHelp OnCommandHelp { get; } = new("On", "Use an item on a target", CommandCategory.Scene);

        #endregion

        #region StaticMethods

        /// <summary>
        /// Handle an item expiring.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="item">The item that expired.</param>
        /// <param name="target">The target that the item was used on.</param>
        private static void ItemExpires(Game game, Item item, IInteractWithItem target)
        {
            List<IItemContainer> containers = [];

            containers.Add(game.Player);
            containers.Add(game.Overworld.CurrentRegion.CurrentRoom);
            containers.AddRange(game.Overworld.CurrentRegion.CurrentRoom.Characters ?? []);
            
            if (target is IItemContainer targetContainer)
                containers.Add(targetContainer);
            
            foreach (var container in from IItemContainer container in containers.Distinct() where container.Items.Contains(item) select container)
                container.RemoveItem(item);
        }

        /// <summary>
        /// Handle a target expiring.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="target">The target that expired.</param>
        private static void TargetExpires(Game game, IInteractWithItem target)
        {
            if (target is IExaminable examinable && game.Overworld.CurrentRegion.CurrentRoom.FindInteractionTarget(examinable.Identifier.Name, out _))
                game.Overworld.CurrentRegion.CurrentRoom.RemoveInteractionTarget(target);

            if (target is Item item)
            {
                foreach (var npc in from NonPlayableCharacter npc in game.Overworld.CurrentRegion.CurrentRoom.Characters ?? [] where npc.HasItem(item) select npc)
                    npc.RemoveItem(item);

                if (game.Player.HasItem(item))
                    game.Player.RemoveItem(item);
            }

            if (target is Character character)
                character.Kill();
        }

        #endregion

        #region Implementation of ICommand

        /// <summary>
        /// Get the help for this command.
        /// </summary>
        public CommandHelp Help => UseCommandHelp;

        /// <summary>
        /// Invoke the command.
        /// </summary>
        /// <param name="game">The game to invoke the command on.</param>
        /// <returns>The reaction.</returns>
        public Reaction Invoke(Game game)
        {
            if (game == null)
                return new(ReactionResult.Error, "No game specified.");

            if (item == null)
                return new(ReactionResult.Error, "You must specify an item.");

            if (target == null)
                return new(ReactionResult.Error, "You must specify a target.");

            if (game.Player == null)
                return new(ReactionResult.Error, "You must specify the character that is using this item.");

            EventBus.Publish(new ItemUsed(item, target));

            var interaction = target.Interact(item);

            switch (interaction.Result)
            {
                case InteractionResult.NoChange:
                    break;
                case InteractionResult.ItemExpires:
                    ItemExpires(game, item, target);
                    break;
                case InteractionResult.TargetExpires:
                    TargetExpires(game, target);
                    break;
                case InteractionResult.ItemAndTargetExpires:
                    ItemExpires(game, item, target);
                    TargetExpires(game, target);
                    break;
                case InteractionResult.PlayerDies:
                    game.Player.Kill();
                    break;
                default:
                    throw new NotImplementedException();
            }

            return new(ReactionResult.Inform, interaction.Description);
        }

        /// <summary>
        /// Get all prompts for this command.
        /// </summary>
        /// <param name="game">The game to get the prompts for.</param>
        /// <returns>And array of prompts.</returns>
        public Prompt[] GetPrompts(Game game)
        {
            Item[] playerItems = [.. game?.Player?.Items?.Where(x => x.IsPlayerVisible) ?? []];
            Item[] roomItems = [.. game?.Overworld?.CurrentRegion?.CurrentRoom?.Items?.Where(x => x.IsPlayerVisible) ?? []];
            Item[] allItems = [.. playerItems, .. roomItems];

            List<Prompt> all = [];
            all.AddRange(allItems.Select(x => new Prompt(x.Identifier.Name)));

            // now add all 'ons'
            var targets = game?.GetAllInteractionTargets()?.Cast<IExaminable>() ?? [];

            foreach (var i in allItems)
                foreach (var t in targets.Where(x => !x.Identifier.Equals(i.Identifier)))
                    all.Add(new($"{i.Identifier.Name} {OnCommandHelp.Command} {t.Identifier.Name}"));

            return [.. all];
        }

        #endregion
    }
}
