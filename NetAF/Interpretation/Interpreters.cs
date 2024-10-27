namespace NetAF.Interpretation
{
    /// <summary>
    /// Provides interpreters.
    /// </summary>
    public static class Interpreters
    {
        /// <summary>
        /// Create the default interpreter.
        /// </summary>
        /// <returns></returns>
        public static IInterpreter Default
        {
            get
            {
                return new InputInterpreter(
                    new FrameCommandInterpreter(),
                    new GlobalCommandInterpreter(),
                    new GameCommandInterpreter(),
                    new CustomCommandInterpreter(),
                    new ConversationCommandInterpreter());
            }
        }
    }
}
