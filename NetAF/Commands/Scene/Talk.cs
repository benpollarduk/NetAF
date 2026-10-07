using NetAF.Assets.Characters;
using NetAF.Logic;
using NetAF.Logic.Modes;
using System.Linq;

namespace NetAF.Commands.Scene
{
    /// <summary>
    /// Represents the Talk command.
    /// </summary>
    /// <param name="converser">The converser.</param>
    public sealed class Talk(IConverser converser) : ICommand
    {
        #region StaticProperties

        /// <summary>
        /// Get the command help.
        /// </summary>
        public static CommandHelp TalkCommandHelp { get; } = new("Talk", "Talk to a character", CommandCategory.Conversation, "L", displayAs: $"Talk/L to __", synonyms: ["Speak", "Chat"]);

        /// <summary>
        /// Get the command help for to.
        /// </summary>
        public static CommandHelp ToCommandHelp { get; } = new("To", "The character to talk to", CommandCategory.Conversation, synonyms: ["At"]);

        #endregion

        #region Implementation of ICommand

        /// <inheritdoc/>
        public CommandHelp Help => TalkCommandHelp;

        /// <inheritdoc/>
        public Reaction Invoke(Game game)
        {
            if (game == null)
                return new(ReactionResult.Error, "No game specified.");

            if (game.Player == null)
                return new(ReactionResult.Error, "No player specified.");

            if (!game.Player.CanConverse)
                return new(ReactionResult.Error, $"{game.Player.Identifier.Name} cannot converse.");

            if (converser == null)
                return new(ReactionResult.Error, "No-one is around to talk to.");

            if (converser is Character character && !character.IsAlive)
                return new(ReactionResult.Error, $"{character.Identifier.Name} is dead.");

            // begin conversation
            converser.Conversation?.Next(game);

            var interpreter = game.Configuration.InterpreterProvider.Find(typeof(ConversationMode));
            game.ChangeMode(new ConversationMode(converser, interpreter));
            return new(ReactionResult.Silent, "Engaged in conversation.");
        }

        /// <inheritdoc/>
        public Prompt[] GetPrompts(Game game)
        {
            return [.. game?.Overworld?.CurrentRegion?.CurrentRoom?.Characters?.Where(x => x.Conversation != null && x.IsPlayerVisible).Select(x => x.Identifier.Name).Select(x => new Prompt(x))];
        }

        #endregion
    }
}
