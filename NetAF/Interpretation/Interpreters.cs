using NetAF.Logic.Modes;

namespace NetAF.Interpretation
{
    /// <summary>
    /// Provides default interpreters.
    /// </summary>
    internal static class Interpreters
    {
        #region StaticProperties

        /// <summary>
        /// Get the default scene interpreter.
        /// </summary>
        private static readonly IInterpreter DefaultSceneCommandInterpreter = new InputInterpreter
        (
            new FrameCommandInterpreter(),
            new GlobalCommandInterpreter(),
            new ExecutionCommandInterpreter(),
            new PersistenceCommandInterpreter(),
            new CustomCommandInterpreter(),
            new SceneCommandInterpreter()
        );

        /// <summary>
        /// Get the default region map interpreter.
        /// </summary>
        private static readonly IInterpreter DefaultRegionMapCommandInterpreter = new RegionMapCommandInterpreter();

        /// <summary>
        /// Get the default conversation command interpreter.
        /// </summary>
        private static readonly IInterpreter DefaultConversationCommandInterpreter = new ConversationCommandInterpreter();

        #endregion

        #region StaticMethods

        /// <summary>
        /// Create a default interpreter provider.
        /// </summary>
        /// <returns>The default interpreter provider.</returns>
        internal static InterpreterProvider CreateDefaultInterpreterProvider()
        {
            var provider = new InterpreterProvider();
            provider.Register(typeof(SceneMode), DefaultSceneCommandInterpreter);
            provider.Register(typeof(RegionMapMode), DefaultRegionMapCommandInterpreter);
            provider.Register(typeof(ConversationMode), DefaultConversationCommandInterpreter);
            return provider;
        }

        #endregion
    }
}
