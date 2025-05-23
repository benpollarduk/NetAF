﻿using NetAF.Assets;
using NetAF.Utilities;

namespace NetAF.Example.Assets.Regions.Flat.Items
{
    public class Mirror : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Mirror";
        private const string Description = "Looking in the mirror you see yourself clearly, and make a mental note to grow back some sideburns.";

        #endregion

        #region Implementation of IAssetTemplate<Item>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The item.</returns>
        public Item Instantiate()
        {
            return new(Name, Description);
        }

        #endregion
    }
}
