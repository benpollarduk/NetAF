using System;
using System.Collections.Generic;

namespace NetAF.Interpretation
{
    /// <summary>
    /// Provides functionality for providing instances of IInterpreter.
    /// </summary>
    public class InterpreterProvider
    {
        #region Properties

        /// <summary>
        /// Get or set the interpreters.
        /// </summary>
        private readonly Dictionary<Type, IInterpreter> interpreters = [];

        #endregion

        #region Methods

        /// <summary>
        /// Register an interpreter.
        /// </summary>
        /// <param name="type">The type to register the interpreter for.</param>
        /// <param name="interpreter">The interpreter to register.</param>
        public void Register(Type type, IInterpreter interpreter)
        {
            if (!interpreters.TryAdd(type, interpreter))
                interpreters[type] = interpreter;
        }

        /// <summary>
        /// Remove the interpreter for a type.
        /// </summary>
        /// <param name="type">The type to remove the interpreter for.</param>
        public void Remove(Type type)
        {
            interpreters.Remove(type);
        }

        /// <summary>
        /// Clear all interpreters.
        /// </summary>
        public void Clear()
        {
            interpreters.Clear();
        }

        /// <summary>
        /// Find the interpreter for a type.
        /// </summary>
        /// <param name="type">The type to find the interpreter for.</param>
        /// <returns>The interpreter for the type. If no interpreter is registered for the specified type this will return null.</returns>
        public IInterpreter Find(Type type)
        {
            if (interpreters.TryGetValue(type, out IInterpreter value))
                return value;

            return null;
        }

        #endregion
    }
}
