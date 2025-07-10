using NetAF.Serialization;
using NetAF.Serialization.Assets;

namespace NetAF.Variables
{
    /// <summary>
    /// Provides a variable.
    /// </summary>
    /// <param name="name">The name of the variable.</param>
    /// <param name="value">The value of the variable.</param>
    public class Variable(string name, string value) : IRestoreFromObjectSerialization<VariableSerialization>
    {
        #region Properties

        /// <summary>
        /// Get the name of this variable.
        /// </summary>
        public string Name { get; private set; } = name;

        /// <summary>
        /// Get or set the value of this variable.
        /// </summary>
        public string Value { get; set; } = value;

        #endregion

        #region StaticMethods

        /// <summary>
        /// Create a new instance of variable from a serialization.
        /// </summary>
        /// <param name="serialization">The serialization.</param>
        /// <returns>The variable.</returns>
        public static Variable FromSerialization(VariableSerialization serialization)
        {
            Variable variable = new(string.Empty, string.Empty);
            ((IRestoreFromObjectSerialization<VariableSerialization>)variable).RestoreFrom(serialization);
            return variable;
        }

        #endregion

        #region Implementation of IRestoreFromObjectSerialization<VariableSerialization>

        /// <summary>
        /// Restore this object from a serialization.
        /// </summary>
        /// <param name="serialization">The serialization to restore from.</param>
        void IRestoreFromObjectSerialization<VariableSerialization>.RestoreFrom(VariableSerialization serialization)
        {
            Name = serialization.Name;
            Value = serialization.Value;
        }

        #endregion
    }
}