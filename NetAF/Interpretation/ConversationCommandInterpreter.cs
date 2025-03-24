using System;
using System.Collections.Generic;
using NetAF.Commands;
using NetAF.Commands.Conversation;
using NetAF.Commands.Global;
using NetAF.Extensions;
using NetAF.Logic;
using NetAF.Logic.Modes;

namespace NetAF.Interpretation
{
    /// <summary>
    /// Provides an object that can be used for interpreting conversation commands.
    /// </summary>
    public sealed class ConversationCommandInterpreter : IInterpreter
    {
        #region StaticProperties

        /// <summary>
        /// Get an array of all supported commands.
        /// </summary>
        public static CommandHelp[] DefaultSupportedCommands { get; } =
        [
            Next.CommandHelp,
            End.CommandHelp
        ];

        #endregion

        #region Implementation of IInterpreter

        /// <summary>
        /// Get an array of all supported commands.
        /// </summary>
        public CommandHelp[] SupportedCommands { get; } = DefaultSupportedCommands;

        /// <summary>
        /// Interpret a string.
        /// </summary>
        /// <param name="input">The string to interpret.</param>
        /// <param name="game">The game.</param>
        /// <returns>The result of the interpretation.</returns>
        public InterpretationResult Interpret(string input, Game game)
        {
            var mode = game.Mode as ConversationMode;

            if (mode?.Converser == null)
                return InterpretationResult.Fail;

            if (End.CommandHelp.Equals(input))
                return new(true, new End());

            if (Next.CommandHelp.Equals(input) || Next.SilentCommandHelp.Equals(input.Trim()))
                return new(true, new Next());

            var responsesAsCommands = GetContextualCommandHelp(game);
            var responseCommand = Array.Find(responsesAsCommands ?? [], x => x.Command.InsensitiveEquals(input) || x.Shortcut.InsensitiveEquals(input));

            if (responseCommand == null)
                return InterpretationResult.Fail;

            var response = Array.Find(mode.Converser.Conversation?.CurrentParagraph?.Responses ?? [], x => x.Line.InsensitiveEquals(responseCommand.Command));

            if (response != null)
                return new(true, new Respond(response));

            return InterpretationResult.Fail;
        }

        /// <summary>
        /// Get contextual command help for a game, based on its current state.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <returns>The contextual help.</returns>
        public CommandHelp[] GetContextualCommandHelp(Game game)
        {
            var mode = game.Mode as ConversationMode;

            if (mode?.Converser?.Conversation == null) 
                return [];

            var commands = new List<CommandHelp>();

            if (mode.Converser.Conversation.CurrentParagraph?.CanRespond ?? false)
            {
                for (var i = 0; i < mode.Converser.Conversation.CurrentParagraph.Responses.Length; i++)
                {
                    var response = mode.Converser.Conversation.CurrentParagraph.Responses[i];
                    commands.Add(new CommandHelp(response.Line, response.Line.EnsureFinishedSentence().ToSpeech(), CommandCategory.Conversation, (i + 1).ToString(), displayAs: (i + 1).ToString()));
                }
            }
            else
            {
                commands.Add(Next.CommandHelp);
            }

            commands.Add(new CommandHelp(End.CommandHelp.Command, "End the conversation", CommandCategory.Conversation));

            return [.. commands];
        }

        #endregion
    }
}
