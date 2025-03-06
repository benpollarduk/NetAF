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
        /// <exception cref="GameExecutionException"/>
        public static void Update(string input = "")
        {
            if (game == null)
                throw new GameExecutionException("Cannot update a game when one is not being executed.");

            game.Update(input);

            if (game.State != GameState.Finished)
                return;

            switch (game.Configuration.ExitMode)
            {
                case ExitMode.ExitApplication:
                    break;
                case ExitMode.ReturnToTitleScreen:
                    if (!wasCancelled)
                        ExecuteManual(creator);
                    break;
                default:
                    throw new NotImplementedException();
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
                game.Start();

                while (game.State != GameState.Finished)
                {
                    var input = GetInput();
                    game.Update(input);
                }

                if (game.State != GameState.Finished)
                    continue;

                run = game.Configuration.ExitMode switch
                {
                    ExitMode.ExitApplication => false,
                    ExitMode.ReturnToTitleScreen => !wasCancelled,
                    _ => throw new NotImplementedException(),
                };
            }

            game = null;
        }

        /// <summary>
        /// Execute a game in manual mode.
        /// </summary>
        /// <param name="creator">The GameCreationCallback used to create instances of the game.</param>
        private static void ExecuteManual(GameCreationCallback creator)
        {
            game = creator.Invoke();
            game.Start();
        }

        /// <summary>
        /// Execute a game.
        /// </summary>
        /// <param name="creator">The GameCreationCallback used to create instances of the game.</param>
        /// <param name="mode">The mode to execute the game in.</param>
        public static void Execute(GameCreationCallback creator, GameExecutionMode mode = GameExecutionMode.Automatic) 
        {
            if (game != null)
                throw new GameExecutionException("Cannot execute a game when one is already being executed.");

            wasCancelled = false;
            GameExecutor.creator = creator;

            switch (mode)
            {
                case GameExecutionMode.Manual:
                    ExecuteManual(creator);
                    break;
                case GameExecutionMode.Automatic:
                    ExecuteAuto(creator);
                    break;
                case GameExecutionMode.BackgroundAutomatic:
                    Task.Run(() => ExecuteAuto(creator));
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Cancel execution of any executing game.
        /// </summary>
        public static void Cancel()
        {
            wasCancelled = true;
            game?.End();
            game = null;
            creator = null;
        }

        #endregion
    }
}
