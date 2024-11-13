using NetAF.Commands;
using NetAF.Commands.Scene;

namespace NetAF.Interpretation
{
    /// <summary>
    /// Represents the result of an interpretation.
    /// </summary>
    /// <param name="wasInterpretedSuccessfully">If interpretation was successful.</param>
    /// <param name="command">The command.</param>
    public class InterpretationResult(bool wasInterpretedSuccessfully, ICommand command)
    {
        #region StaticProperties

        /// <summary>
        /// Get a default result for failure.
        /// </summary>
        public static InterpretationResult Fail => new(false, new Unactionable("Interpretation failed."));

        #endregion

        #region Properties

        /// <summary>
        /// Get if interpretation was successful.
        /// </summary>
        public bool WasInterpretedSuccessfully { get; } = wasInterpretedSuccessfully;

        /// <summary>
        /// Get the command.
        /// </summary>
        public ICommand Command { get; } = command;

        #endregion
    }
}
