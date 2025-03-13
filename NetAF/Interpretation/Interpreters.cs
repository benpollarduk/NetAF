namespace NetAF.Interpretation
{
    /// <summary>
    /// Provides collections of interpreters.
    /// </summary>
    public static class Interpreters
    {
        /// <summary>
        /// Get the frame command interpreter.
        /// </summary>
        public static IInterpreter FrameCommandInterpreter { get; } = new FrameCommandInterpreter();

        /// <summary>
        /// Get the frame command interpreter.
        /// </summary>
        public static IInterpreter GlobalCommandInterpreter { get; } = new GlobalCommandInterpreter();

        /// <summary>
        /// Get the execution command interpreter.
        /// </summary>
        public static IInterpreter ExecutionCommandInterpreter { get; } = new ExecutionCommandInterpreter();

        /// <summary>
        /// Get the custom command interpreter.
        /// </summary>
        public static IInterpreter CustomCommandInterpreter { get; } = new CustomCommandInterpreter();

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

        /// <summary>
        /// Get the default interpreters.
        /// </summary>
        public static IInterpreter Default { get; } = new InputInterpreter(
            FrameCommandInterpreter,
            GlobalCommandInterpreter,
            ExecutionCommandInterpreter,
            CustomCommandInterpreter);
    }
}
