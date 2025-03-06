namespace NetAF.Logic
{
    /// <summary>
    /// Enumeration of modes in which a game can be executed.
    /// </summary>
    public enum GameExecutionMode
    {
        /// <summary>
        /// Manual mode. The GameExecutor.Update() method needs to be called when an update to the game state is required. This mode should be used when blocking calls while waiting for input are available or desirable.
        /// </summary>
        Manual = 0,
        /// <summary>
        /// Automatic mode. The GameExecutor will handle all calls to GameExecutor.Update() to ensure that all updates to the game state are action as required. This mode should be used when blocking calls while waiting for input are available and desirable.
        /// </summary>
        Automatic,
        /// <summary>
        /// Automatic mode, on a background thread. The GameExecutor will handle all calls to GameExecutor.Update() to ensure that all updates to the game state are action as required. This mode should be used when blocking calls are desirable. This mode should be used when blocking calls while waiting for input are available and desirable.
        /// </summary>
        BackgroundAutomatic
    }
}
