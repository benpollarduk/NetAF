using System.Threading.Tasks;

namespace NetAF.Logic
{
    /// <summary>
    /// Represents any object that can control the automation of game execution.
    /// </summary>
    public interface IGameExecutionAutomationController
    {
        /// <summary>
        /// Begin execution of a game, asynchronously.
        /// </summary>
        /// <returns>The task.</returns>
        Task BeginAsync(Game game);
        /// <summary>
        /// Cancel execution, asynchronously.
        /// </summary>
        /// <returns>The task.</returns>
        Task CancelAsync();
    }
}
