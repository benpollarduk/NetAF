﻿using NetAF.Assets;
using NetAF.Utilities;

namespace NetAF.Example.Assets.Regions.Zelda.Items
{
    public class Rupee : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Rupee";
        private const string Description = "A red rupee! Wow this thing is worth 10 green rupees.";

        #endregion

        #region Implementation of IAssetTemplate<Item>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The item.</returns>
        public Item Instantiate()
        {
            return new(Name, Description, true) { IsPlayerVisible = false };
        }

        #endregion
    }
}
