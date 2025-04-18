﻿using NetAF.Assets;
using NetAF.Utilities;

namespace NetAF.Example.Assets.Regions.Zelda.Items
{
    public class TailDoor(InteractionCallback Interaction) : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Tail Door";
        private const string Description = "The doorway to the tail cave.";

        #endregion

        #region Implementation of IAssetTemplate<Item>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The item.</returns>
        public Item Instantiate()
        {
            return new(Name, Description, interaction: Interaction);
        }

        #endregion
    }
}
