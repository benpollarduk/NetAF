using System;

namespace NetAF.Logic
{
    /// <summary>
    /// Represents errors that occur during game execution.
    /// </summary>
    /// <param name="message">A message detailing the exception.</param>
    public class GameExecutionException(string message) : Exception(message)
    {
    }
}
