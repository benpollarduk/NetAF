using NetAF.Logic;
using NetAF.Logic.Modes;
using NetAF.Utilities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NetAF.Targets.Console
{
    /// <summary>
    /// Controls the automation of a game targeting the System.Console.
    /// </summary>
    public class ConsoleExecutionController : IGameExecutionAutomationController
    {
        #region Fields

        private CancellationTokenSource tokenSource;

        #endregion

        #region Destructor

        /// <summary>
        /// Handle destruction of the ConsoleExecutionController.
        /// </summary>
        ~ConsoleExecutionController()
        {
            tokenSource?.Dispose();
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Wait for a key press asynchronously.
        /// </summary>
        /// <param name="key">The ASCII code of the key to wait for.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>True if the key pressed returned the same ASCII character as the key property, else false.</returns>
        private static async Task<bool> WaitForKeyPressAsync(char key, CancellationToken token)
        {
            return await Task.Run(() =>
            {
                bool result;

                try
                {
                    result = System.Console.ReadKey().KeyChar == key;
                }
                catch (OperationCanceledException)
                {
                    result = false;
                }

                return result;
            }, token);
        }

        /// <summary>
        /// Wait for acknowledgment.
        /// </summary>
        /// <returns>True if the acknowledgment was received correctly, else false.</returns>
        private static async Task<bool> WaitForAcknowledgeAsync(CancellationToken token)
        {
            return await Task.Run(async () =>
            {
                bool result;

                try
                {
                    result = await WaitForKeyPressAsync(StringUtilities.CR, token);
                }
                catch (OperationCanceledException)
                {
                    result = false;
                }

                return result;
            }, token);
        }

        /// <summary>
        /// Wait for input asynchronously.
        /// </summary>
        /// <param name="token">The cancellation token.</param>
        /// <returns>The input.</returns>
        public static async Task<string> WaitForInputAsync(CancellationToken token)
        {
            return await Task.Run(async () =>
            {
                string result;

                try
                {
                    result = await System.Console.In.ReadLineAsync();
                }
                catch (OperationCanceledException)
                {
                    result = string.Empty;
                }

                return result;
            }, token);
        }


        /// <summary>
        /// Get input from the user asynchronously.
        /// </summary>
        /// <param name="game">The game to get the input for.</param>
        /// <param name="token">The token.</param>
        /// <returns>The user input.</returns>
        private static async Task<string> GetInputAsync(Game game, CancellationToken token)
        {
            // input is handled based on the current modes type
            switch (game.Mode.Type)
            {
                case GameModeType.Information:

                    // wait for acknowledge
                    while (!await WaitForAcknowledgeAsync(token))
                    {
                        // something other was entered, render again
                        game.Mode.Render(game);
                    }

                    // acknowledge complete
                    return string.Empty;

                case GameModeType.Interactive:

                    // get and return user input
                    return await WaitForInputAsync(token);

                default:

                    throw new NotImplementedException($"No handling for case {game.Mode.Type}.");
            }
        }

        #endregion

        #region Implementation of IGameExecutionAutomationController

        /// <summary>
        /// Begin execution of a game, asynchronously.
        /// </summary>
        /// <returns>The task.</returns>
        public async Task BeginAsync(Game game)
        {
            tokenSource?.Dispose();
            tokenSource = new CancellationTokenSource();

            while (game.State != GameState.Finished && !tokenSource.Token.IsCancellationRequested)
            {
                var input = await GetInputAsync(game, tokenSource.Token);
                var result = GameExecutor.Update(input);

                if (!result.Completed)
                    break;
            }
        }

        /// <summary>
        /// Cancel execution, asynchronously.
        /// </summary>
        /// <returns>The task.</returns>
        public async Task CancelAsync()
        {
            await tokenSource.CancelAsync();
        }

        #endregion
    }
}
