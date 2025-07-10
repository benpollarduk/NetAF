using NetAF.Extensions;
using NetAF.Serialization;
using NetAF.Serialization.Assets;
using System;
using System.Collections.Generic;

namespace NetAF.Variables
{
    /// <summary>
    /// Provides a manager for in-game variables.
    /// </summary>
    public class VariableManager : IRestoreFromObjectSerialization<VariableManagerSerialization>
    {
        #region Fields

        private readonly List<Variable> variables = [];

        #endregion

        #region Properties

        /// <summary>
        /// Get the number of entries.
        /// </summary>
        public int Count => variables.Count;

        #endregion

        #region Methods

        /// <summary>
        /// Clear all variables.
        /// </summary>
        public void Clear()
        {
            variables.Clear();
        }

        /// <summary>
        /// Add a new variable.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <param name="value">The value of the variable.</param>
        public void Add(string name, string value)
        {
            Add(new(name, value));
        }

        /// <summary>
        /// Add a new variable.
        /// </summary>
        /// <param name="variable">The variable to add.</param>
        public void Add(Variable variable)
        {
            if (string.IsNullOrEmpty(variable.Name))
                return;

            var hit = Find(variable.Name);

            if (hit == null)
                variables.Add(variable);
            else
                hit.Value = variable.Value;
        }

        /// <summary>
        /// Get all variables.
        /// </summary>
        /// <returns>An array of all variables.</returns>
        public Variable[] GetAll()
        {
            return [.. variables];
        }

        /// <summary>
        /// Find a variable by its name.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <returns>The variable, if found. Else null.</returns>
        private Variable Find(string name)
        {
            return Array.Find([.. variables], x => x.Name.InsensitiveEquals(name));
        }

        /// <summary>
        /// Remove a variable.
        /// </summary>
        /// <param name="name">The name of the variable to remove.</param>
        public void Remove(string name)
        {
            var hit = Find(name);
            Remove(hit);
        }

        /// <summary>
        /// Remove a variable.
        /// </summary>
        /// <param name="variable">The  variable to remove.</param>
        public void Remove(Variable variable)
        {
            variables.Remove(variable);
        }

        /// <summary>
        /// Get is a variable is present.
        /// </summary>
        /// <param name="name">The name of the variable to check.</param>
        /// <returns>True if a variable with the same name is present.</returns>
        public bool ContainsVariable(string name)
        {
            return Find(name) != null;
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Create a new instance of VariableManager from a serialization.
        /// </summary>
        /// <param name="serialization">The serialization.</param>
        /// <returns>The variable manager.</returns>
        public static VariableManager FromSerialization(VariableManagerSerialization serialization)
        {
            VariableManager manager = new();
            ((IRestoreFromObjectSerialization<VariableManagerSerialization>)manager).RestoreFrom(serialization);
            return manager;
        }

        #endregion

        #region Implementation of IRestoreFromObjectSerialization<VariableManagerSerialization>

        /// <summary>
        /// Restore this object from a serialization.
        /// </summary>
        /// <param name="serialization">The serialization to restore from.</param>
        void IRestoreFromObjectSerialization<VariableManagerSerialization>.RestoreFrom(VariableManagerSerialization serialization)
        {
            variables.Clear();

            foreach (var entry in serialization.Variables)
                Add(Variable.FromSerialization(entry));
        }

        #endregion
    }
}
