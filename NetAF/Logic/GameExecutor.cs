using NetAF.Logic.Callbacks;
using NetAF.Logic.Modes;
using System;
using System.Threading.Tasks;

namespace NetAF.Logic
{
    /// <summary>
    /// Handles the execution of a Game.
    /// </summary>
    public static class GameExecutor
    {
        #region StaticFields

        private static Game game;
        private static GameCreationCallback creator;
        private static bool wasCancelled = false;
        private static GameExecutionMode? executingMode;

        #endregion

        #region StaticProperties

        /// <summary>
        /// Get if a game is currently executing.
        /// </summary>
        public static bool IsExecuting => game != null;

        #endregion

        #region StaticMethods

        /// <summary>
        /// Get input from the user.
        /// </summary>
        /// <returns>The user input.</returns>
        private static string GetInput()
        {
            // input is handled based on the current modes type
            switch (game.Mode.Type)
            {
                case GameModeType.Information:

                    // wait for acknowledge
                    while (!game.Configuration.Adapter.WaitForAcknowledge())
                    {
                        // something other was entered, render again
                        game.Mode.Render(game);
                    }

                    // acknowledge complete
                    return string.Empty;

                case GameModeType.Interactive:

                    // get and return user input
                    return game.Configuration.Adapter.WaitForInput();

                default:

                    throw new NotImplementedException($"No handling for case {game.Mode.Type}.");
            }
        }

        /// <summary>
        /// Update to the next frame of the game.
        /// </summary>
        /// <param name="input">Any input that should be passed to the game.</param>
        /// <returns>The result of the action.</returns>
        public static UpdateResult Update(string input = "")
        {
            if (game == null)
                return new(false, "Cannot update a game when one is not being executed.");

            if (executingMode != GameExecutionMode.Step)
                return new(false, $"Cannot update a game when execution mode is {(executingMode != null ? executingMode : "null")}.");

            var result = game.Update(input);

            if (!result.Completed)
                return result;
            
            if (game.State != GameState.Finished)
                return result;

            switch (game.Configuration.ExitMode)
            {
                case ExitMode.ExitApplication:

                    Reset();
                    return new(true);

                case ExitMode.ReturnToTitleScreen:

                    if (!wasCancelled)
                        ExecuteManual(creator);
                    else
                        Reset();

                    return new(true);

                default:

                    return new(false, $"No implementation for {game.Configuration.ExitMode}.");
            }
        }

        /// <summary>
        /// Execute a game in auto mode.
        /// </summary>
        /// <param name="creator">The GameCreationCallback used to create instances of the game.</param>
        private static void ExecuteAuto(GameCreationCallback creator)
        {
            var run = true;

            while (run)
            {
                game = creator.Invoke();
                var result = game.Update();

                if (!result.Completed)
                    break;

                while (game.State != GameState.Finished)
                {
                    var input = GetInput();
                    result = game.Update(input);

                    if (!result.Completed)
                        break;
                }

                if (game.State != GameState.Finished)
                    continue;

                if (wasCancelled)
                    break;

                run = game.Configuration.ExitMode switch
                {
                    ExitMode.ExitApplication => false,
                    ExitMode.ReturnToTitleScreen => !wasCancelled,
                    _ => throw new NotImplementedException(),
                };
            }

            Reset();
        }

        /// <summary>
        /// Execute a game in manual mode.
        /// </summary>
        /// <param name="creator">The GameCreationCallback used to create instances of the game.</param>
        private static void ExecuteManual(GameCreationCallback creator)
        {
            game = creator.Invoke();
            Update();
        }

        /// <summary>
        /// Execute a game.
        /// </summary>
        /// <param name="creator">The GameCreationCallback used to create instances of the game. If a game is already being executed a GameExecutionException will be thrown.</param>
        /// <param name="mode">The mode to execute the game in.</param>
        /// <exception cref="GameExecutionException"/>
        public static void Execute(GameCreationCallback creator, GameExecutionMode mode = GameExecutionMode.Auto) 
        {
            if (game != null)
                throw new GameExecutionException("Cannot execute a game when one is already being executed.");

            wasCancelled = false;
            GameExecutor.creator = creator;
            executingMode = mode;

            switch (mode)
            {
                case GameExecutionMode.Step:

                    ExecuteManual(creator);
                    break;

                case GameExecutionMode.Auto:

                    ExecuteAuto(creator);
                    break;

                case GameExecutionMode.AutoAsync:

                    Task.Run(() => ExecuteAuto(creator));
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Cancel execution of any executing game.
        /// </summary>
        public static void CancelExecution()
        {
            wasCancelled = true;
            game?.End();
        }

        /// <summary>
        /// Reset.
        /// </summary>
        private static void Reset()
        {
            game = null;
            creator = null;
            executingMode = null;
        }

        #endregion
    }
}
