using NetAF.Variables;
using System.Collections.Generic;
using System.Linq;

namespace NetAF.Serialization.Assets
{
    /// <summary>
    /// Represents a serialization of a VariableManager.
    /// </summary>
    public sealed class VariableManagerSerialization : IObjectSerialization<VariableManager>
    {
        #region Properties

        /// <summary>
        /// Get or set the variables.
        /// </summary>
        public List<VariableSerialization> Variables { get; set; }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Create a new serialization from an VariableManager.
        /// </summary>
        /// <param name="variableManager">The VariableManager to create the serialization from.</param>
        /// <returns>The serialization.</returns>
        public static VariableManagerSerialization FromVariableManager(VariableManager variableManager)
        {
            return new()
            {
                Variables = variableManager.GetAll()?.Select(VariableSerialization.FromVariable).ToList() ?? []
            };
        }

        #endregion

        #region Implementation of IObjectSerialization<VariableManager>

        /// <summary>
        /// Restore an instance from this serialization.
        /// </summary>
        /// <param name="variableManager">The VariableManager to restore.</param>
        void IObjectSerialization<VariableManager>.Restore(VariableManager variableManager)
        {
            ((IRestoreFromObjectSerialization<VariableManagerSerialization>)variableManager).RestoreFrom(this);
        }

        #endregion
    }
}
