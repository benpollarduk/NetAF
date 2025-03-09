namespace NetAF.Logic
{
    /// <summary>
    /// Handles the execution of a Game.
    /// </summary>
    public static class GameExecutor
    {
        #region StaticFields

        private static Game game;
        private static GameCreator creator;
        private static bool wasCancelled = false;
        private static IGameExecutionAutomationController controller;

        #endregion

        #region StaticProperties

        /// <summary>
        /// Get if a game is currently executing.
        /// </summary>
        public static bool IsExecuting => game != null && !wasCancelled;

        #endregion

        #region StaticMethods

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

            if (wasCancelled)
            {
                Reset();
                return new(true);
            }

            if (game.State != GameState.Finished)
                return result;

            switch (game.Configuration.FinishMode)
            {
                case FinishModes.Finish:

                    Reset();
                    return new(true);

                case FinishModes.ReturnToTitleScreen:

                    Begin();
                    return new(true);

                default:

                    return new(false, $"No implementation for {game.Configuration.FinishMode}.");
            }
        }

        /// <summary>
        /// Execute a game.
        /// </summary>
        /// <param name="creator">The GameCreator used to create instances of the game.</param>
        /// <param name="controller">An optional controller to manage game automation.</param>
        public static void Execute(GameCreator creator, IGameExecutionAutomationController controller = null) 
        {
            CancelExecution();
            Reset();

            GameExecutor.creator = creator;
            GameExecutor.controller = controller;

            Begin();
        }

        /// <summary>
        /// Restart an executing game.
        /// </summary>
        public static void Restart()
        {
            CancelExecution();
            Begin();
        }

        /// <summary>
        /// Begin execution of the game.
        /// </summary>
        private static void Begin()
        {
            wasCancelled = false;

            if (creator == null)
                return;

            game = creator.Invoke();
            Update();

            if (controller != null)
                controller.BeginAsync(game).Wait();
        }

        /// <summary>
        /// Cancel execution of any executing game.
        /// </summary>
        public static void CancelExecution()
        {
            wasCancelled = true;
            game?.End();
            controller?.CancelAsync();
        }

        /// <summary>
        /// Reset.
        /// </summary>
        private static void Reset()
        {
            game = null;
            creator = null;
            controller = null;
        }

        #endregion
    }
}
