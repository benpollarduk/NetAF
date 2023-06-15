﻿using System;
using BP.AdventureFramework.Assets;

namespace BP.AdventureFramework.Utilities.Templates
{
    /// <summary>
    /// Provides a template class to help with the creation of items.
    /// </summary>
    /// <typeparam name="TDerived">The derived type.</typeparam>
    public class ItemTemplate<TDerived> where TDerived : ItemTemplate<TDerived>
    {
        #region Methods

        /// <summary>
        /// Create a new instance of the item.
        /// </summary>
        /// <returns>The region.</returns>
        protected virtual Item OnCreate()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Get an instance of the derived type.
        /// </summary>
        /// <returns>The instance.</returns>
        private static TDerived GetInstance()
        {
            var type = typeof(TDerived);
            return (TDerived)Activator.CreateInstance(type);
        }

        /// <summary>
        /// Create a new instance of the item.
        /// </summary>
        /// <returns>The playable character.</returns>
        public static Item Create()
        {
            return GetInstance().OnCreate();
        }

        /// <summary>
        /// Get an identifier for the templated item.
        /// </summary>
        /// <returns>The identifier for the templated item.</returns>
        public static Identifier GetIdentifier()
        {
            return Create().Identifier;
        }

        #endregion
    }
}
