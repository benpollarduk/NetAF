namespace NetAF.Logic
{
    /// <summary>
    /// Represents any object that can control the automation of game execution.
    /// </summary>
    public interface IGameExecutionAutomationController
    {
        /// <summary>
        /// Begin execution of a game.
        /// </summary>
        /// <returns>The task.</returns>
        void Begin(Game game);
        /// <summary>
        /// Cancel execution.
        /// </summary>
        void Cancel();
    }
}
