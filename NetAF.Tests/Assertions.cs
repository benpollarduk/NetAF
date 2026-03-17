using System;

namespace NetAF.Tests
{
    /// <summary>
    /// Provides additional assertions.
    /// </summary>
    internal static class Assertions
    {
        /// <summary>
        /// Utility method to assert that no exception is thrown.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        public static void NoExceptionThrown(Action action)
        {
            // if this call throws, the test will fail.
            action.Invoke();
        }
    }
}
