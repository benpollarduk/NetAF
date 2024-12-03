using System.Collections.Generic;
using System.Linq;
using NetAF.Extensions;
using NetAF.Serialization;
using NetAF.Serialization.Assets;

namespace NetAF.Assets.Attributes
{
    /// <summary>
    /// Provides a class for managing attributes.
    /// </summary>
    public sealed class AttributeManager : IRestoreFromObjectSerialization<AttributeManagerSerialization>
    {
        #region Fields

        /// <summary>
        /// Get or set the underlying attributes.
        /// </summary>
        private readonly Dictionary<Attribute, int> attributes = [];

        #endregion

        #region Properties

        /// <summary>
        /// Get the number of attributes this manager has.
        /// </summary>
        public int Count => attributes.Count;

        #endregion

        #region Methods

        /// <summary>
        /// Ensure an attribute exists. If the attribute does not exist it is added.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        private void EnsureAttributeExists(string name)
        {
            var attribute = attributes.Keys.FirstOrDefault(x => x.Name.InsensitiveEquals(name));

            if (attribute != null)
                return;

            attribute = new(name, string.Empty, int.MinValue, int.MaxValue);
            attributes.Add(attribute, 0);
        }

        /// <summary>
        /// Ensure an attribute exists. If the attribute does not exist it is added.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        private void EnsureAttributeExists(Attribute attribute)
        {
            var match = attributes.Keys.FirstOrDefault(x => x.Name.InsensitiveEquals(attribute.Name));

            if (match != null)
                return;

            attributes.Add(attribute, 0);
        }

        /// <summary>
        /// Get the matching key from the dictionary.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The matching attribute.</returns>
        private Attribute GetMatch(Attribute key)
        {
            EnsureAttributeExists(key);
            return attributes.Keys.First(x => x.Name == key.Name);
        }

        /// <summary>
        /// Get all attributes.
        /// </summary>
        /// <returns>An array of attributes.</returns>
        public Attribute[] GetAttributes()
        {
            return [.. attributes.Keys];
        }

        /// <summary>
        /// Get all attributes as a dictionary.
        /// </summary>
        /// <returns>A dictionary where the key are the attributes and the values are the values.</returns>
        public Dictionary<Attribute, int> GetAsDictionary()
        {
            return attributes.ToDictionary(x => x.Key, x => x.Value);
        }

        /// <summary>
        /// Get the value of an attribute.
        /// </summary>
        /// <param name="attributeName">The name of the attribute.</param>
        /// <returns>The value.</returns>
        public int GetValue(string attributeName)
        {
            return GetValue(attributes.Keys.FirstOrDefault(x => x.Name.InsensitiveEquals(attributeName)));
        }

        /// <summary>
        /// Get the value of an attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <returns>The value.</returns>
        public int GetValue(Attribute attribute)
        {
            if (attribute == null)
                return 0;

            var match = GetMatch(attribute);
            return attributes.TryGetValue(match, out var value) ? value : 0;
        }

        /// <summary>
        /// Get if there is any of a specified attribute.
        /// </summary>
        /// <param name="attributeName">The name of the attribute.</param>
        /// <returns>True if there are any, else false.</returns>
        public bool Any(string attributeName)
        {
            return GetValue(attributes.Keys.FirstOrDefault(x => x.Name.InsensitiveEquals(attributeName))) > 0;
        }

        /// <summary>
        /// Get if there is any of a specified attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <returns>True if there are any, else false.</returns>
        public bool Any(Attribute attribute)
        {
            if (attribute == null)
                return false;

            return GetValue(attribute) > 0;
        }

        /// <summary>
        /// Add a value to an attribute.
        /// </summary>
        /// <param name="attributeName">The name of the attribute.</param>
        /// <param name="value">The value.</param>
        public void Add(string attributeName, int value)
        {
            EnsureAttributeExists(attributeName);
            var attribute = attributes.Keys.First(x => x.Name.InsensitiveEquals(attributeName));
            attributes[attribute] = CapValue(attributes[attribute] + value, attribute.Minimum, attribute.Maximum);
        }

        /// <summary>
        /// Add a value to an attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <param name="value">The value.</param>
        public void Add(Attribute attribute, int value)
        {
            attribute = GetMatch(attribute);
            attributes[attribute] = CapValue(attributes[attribute] + value, attribute.Minimum, attribute.Maximum);
        }

        /// <summary>
        /// Subtract a value from an attribute.
        /// </summary>
        /// <param name="attributeName">The name of the attribute.</param>
        /// <param name="value">The value.</param>
        public void Subtract(string attributeName, int value)
        {
            EnsureAttributeExists(attributeName);
            var attribute = attributes.Keys.First(x => x.Name.InsensitiveEquals(attributeName));
            attributes[attribute] = CapValue(attributes[attribute] - value, attribute.Minimum, attribute.Maximum);
        }

        /// <summary>
        /// Subtract a value from an attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <param name="value">The value.</param>
        public void Subtract(Attribute attribute, int value)
        {
            attribute = GetMatch(attribute);
            attributes[attribute] = CapValue(attributes[attribute] - value, attribute.Minimum, attribute.Maximum);
        }

        /// <summary>
        /// Remove an attribute.
        /// </summary>
        /// <param name="attributeName">The name of the attribute.</param>
        public void Remove(string attributeName)
        {
            var attribute = attributes.Keys.FirstOrDefault(x => x.Name.InsensitiveEquals(attributeName));

            if (attribute == null)
                return;

            attributes.Remove(attribute);
        }

        /// <summary>
        /// Remove an attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        public void Remove(Attribute attribute)
        {
            attribute = GetMatch(attribute);
            attributes.Remove(attribute);
        }

        /// <summary>
        /// Remove all attributes.
        /// </summary>
        public void RemoveAll()
        {
            attributes.Clear();
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Cap a value between a minimum and maximum limit.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The capped value.</returns>
        private static int CapValue(int value, int min, int max)
        {
            if (value < min)
                value = min;
            else if (value > max)
                value = max;

            return value;
        }

        #endregion

        #region Implementation of IRestoreFromObjectSerialization<AttributeManagerSerialization>

        /// <summary>
        /// Restore this object from a serialization.
        /// </summary>
        /// <param name="serialization">The serialization to restore from.</param>
        void IRestoreFromObjectSerialization<AttributeManagerSerialization>.RestoreFrom(AttributeManagerSerialization serialization)
        {
            RemoveAll();

            foreach (var keyValuePair in serialization.Values)
            {
                var attribute = new Attribute(keyValuePair.Name, keyValuePair.Description, keyValuePair.Minimum, keyValuePair.Maximum);
                Add(attribute, keyValuePair.Value);
            }    
        }

        #endregion
    }
}
