﻿using NetAF.Assets;
using NetAF.Utilities;

namespace NetAF.Example.Assets.Regions.Flat.Items
{
    public class Bed : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Bed";
        private const string Description = "The bed is neatly made, Beth makes it every day. By your reckoning there are way too many cushions on it though...";

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
