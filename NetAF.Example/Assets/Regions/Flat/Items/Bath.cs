﻿using NetAF.Assets;
using NetAF.Utilities;

namespace NetAF.Example.Assets.Regions.Flat.Items
{
    public class Bath : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Bath";
        private const string Description = "A long but narrow bath. You want to fill it but you can't because there is a wetsuit in it.";

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
