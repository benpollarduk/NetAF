using NetAF.Assets.Characters;
using NetAF.Logic.Modes;

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
        public static CommandHelp TalkCommandHelp { get; } = new("Talk", "Talk to a character", "L", displayAs: $"Talk/L to __");

        /// <summary>
        /// Get the command help for to.
        /// </summary>
        public static CommandHelp ToCommandHelp { get; } = new("To", "The character to talk to");

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
                return new(ReactionResult.Error, "No player specified.");

            if (!game.Player.CanConverse)
                return new(ReactionResult.Error, $"{game.Player.Identifier.Name} cannot converse.");

            if (converser == null)
                return new(ReactionResult.Error, "No-one is around to talk to.");

            if (converser is Character character && !character.IsAlive)
                return new(ReactionResult.Error, $"{character.Identifier.Name} is dead.");

            // begin conversation
            converser.Conversation?.Next(game);

            game.ChangeMode(new ConversationMode(converser));
            return new(ReactionResult.Silent, "Engaged in conversation.");
        }

        #endregion
    }
}
