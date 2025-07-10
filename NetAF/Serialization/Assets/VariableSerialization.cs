using NetAF.Variables;

namespace NetAF.Serialization.Assets
{
    /// <summary>
    /// Represents a serialization of a Variable.
    /// </summary>
    public sealed class VariableSerialization : IObjectSerialization<Variable>
    {
        #region Properties

        /// <summary>
        /// Get or set the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get or set the value.
        /// </summary>
        public string Value { get; set; }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Create a new serialization from a NoteEntry.
        /// </summary>
        /// <param name="variable">The variable to create the serialization from.</param>
        /// <returns>The serialization.</returns>
        public static VariableSerialization FromVariable(Variable variable)
        {
            return new()
            {
                Name = variable.Name,
                Value = variable.Value,
            };
        }

        #endregion

        #region Implementation of IObjectSerialization<Variable>

        /// <summary>
        /// Restore an instance from this serialization.
        /// </summary>
        /// <param name="variable">The variable to restore.</param>
        void IObjectSerialization<Variable>.Restore(Variable variable)
        {
            ((IRestoreFromObjectSerialization<VariableSerialization>)variable).RestoreFrom(this);
        }

        #endregion
    }
}
