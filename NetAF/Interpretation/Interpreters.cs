namespace NetAF.Interpretation
{
    /// <summary>
    /// Provides collections of interpreters.
    /// </summary>
    public static class Interpreters
    {
        /// <summary>
        /// Get the default interpreters.
        /// </summary>
        public static IInterpreter Default { get; } = new InputInterpreter(
            new FrameCommandInterpreter(),
            new GlobalCommandInterpreter(),
            new CustomCommandInterpreter());

        /// <summary>
        /// Get the scene command interpreter.
        /// </summary>
        public static IInterpreter SceneInterpreter { get; } = new SceneCommandInterpreter();

        /// <summary>
        /// Get the conversation command interpreter.
        /// </summary>
        public static IInterpreter ConversationInterpreter { get; } = new ConversationCommandInterpreter();

        /// <summary>
        /// Get the region map command interpreter.
        /// </summary>
        public static IInterpreter RegionMapCommandInterpreter { get; } = new RegionMapCommandInterpreter();
    }
}
