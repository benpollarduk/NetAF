using NetAF.Assets.Characters;
using NetAF.Commands;
using NetAF.Interpretation;
using NetAF.Rendering;
using NetAF.Rendering.FrameBuilders;
using System;
using System.Linq;

namespace NetAF.Logic.Modes
{
    /// <summary>
    /// Provides a display mode for conversation.
    /// </summary>
    /// <param name="converser">The IConverser the conversation is being held with.</param>
    /// <param name="interpreter">Specify the interpreter used for interpreting commands in this mode.</param>
    public sealed class ConversationMode(IConverser converser, IInterpreter interpreter) : IGameMode
    {
        #region StaticProperties

        /// <summary>
        /// Get the minimal command categories.
        /// </summary>
        private static CommandCategory[] MinimalCommandCategories => [CommandCategory.Uncategorized, CommandCategory.Conversation, CommandCategory.Custom];

        #endregion

        #region Properties

        /// <summary>
        /// Get the converser.
        /// </summary>
        public IConverser Converser { get; } = converser;

        #endregion

        #region Implementation of IGameMode

        /// <summary>
        /// Get the interpreter.
        /// </summary>
        public IInterpreter Interpreter { get; } = interpreter;

        /// <summary>
        /// Get the type of mode this provides.
        /// </summary>
        public GameModeType Type { get; } = GameModeType.Interactive;

        /// <summary>
        /// Render the current state of a game.
        /// </summary>
        /// <param name="game">The game.</param>
        public void Render(Game game)
        {
            CommandHelp[] commands = FrameProperties.CommandListType switch
            {
                CommandListType.All => Interpreter?.GetContextualCommandHelp(game) ?? [],
                CommandListType.Minimal => Interpreter?.GetContextualCommandHelp(game).Where(x => MinimalCommandCategories.Contains(x.Category)).ToArray() ?? [],
                CommandListType.None => [],
                _ => throw new NotImplementedException()
            };

            var frame = game.Configuration.FrameBuilders.GetFrameBuilder<IConversationFrameBuilder>().Build($"Conversation with {Converser.Identifier.Name}", Converser, commands, game.Configuration.DisplaySize);
            game.Configuration.Adapter.RenderFrame(frame);
        }

        #endregion
    }
}
