using NetAF.Interpretation;

namespace NetAF.Logic.Modes
{
    /// <summary>
    /// Represents a mode for a game.
    /// </summary>
    public interface IGameMode
    {
        /// <summary>
        /// Get the interpreter.
        /// </summary>
        IInterpreter Interpreter { get; }
        /// <summary>
        /// Get the type of mode this provides.
        /// </summary>
        GameModeType Type { get; }
        /// <summary>
        /// Render the current state of a game.
        /// </summary>
        /// <param name="game">The game.</param>
        void Render(Game game);
    }
}
