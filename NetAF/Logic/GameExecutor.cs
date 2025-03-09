using NetAF.Logic.Callbacks;
using NetAF.Logic.Modes;
using System;
using System.ComponentModel.DataAnnotations;
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

        #endregion

        #region StaticProperties

        /// <summary>
        /// Get if a game is currently executing.
        /// </summary>
        public static bool IsExecuting => game != null;

        #endregion

        #region StaticMethods

        private static void Old()
        {
            while (game.State != GameState.Finished)
            {
                var input = GetInput();
                var result = game.Update(input);

                if (!result.Completed)
                    break;
            }
        }

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
                        Begin(creator);
                    else
                        Reset();

                    return new(true);

                default:

                    return new(false, $"No implementation for {game.Configuration.ExitMode}.");
            }
        }

        /// <summary>
        /// Execute a game.
        /// </summary>
        /// <param name="creator">The GameCreationCallback used to create instances of the game. If a game is already being executed a GameExecutionException will be thrown.</param>
        /// <exception cref="GameExecutionException"/>
        public static void Execute(GameCreationCallback creator) 
        {
            if (game != null)
                throw new GameExecutionException("Cannot execute a game when one is already being executed.");

            wasCancelled = false;
            GameExecutor.creator = creator;

            Begin(creator);
        }

        /// <summary>
        /// Begin execution of the game.
        /// </summary>
        /// <param name="creator">The GameCreationCallback used to create instances of the game. If a game is already being executed a GameExecutionException will be thrown.</param>
        private static void Begin(GameCreationCallback creator)
        {
            game = creator.Invoke();
            Update();
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
        }

        #endregion
    }
}
