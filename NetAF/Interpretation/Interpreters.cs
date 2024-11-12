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
        public static readonly IInterpreter Default = new InputInterpreter(
            new FrameCommandInterpreter(),
            new GlobalCommandInterpreter(),
            new GameCommandInterpreter(),
            new CustomCommandInterpreter(),
            new ConversationCommandInterpreter());
    }
}
