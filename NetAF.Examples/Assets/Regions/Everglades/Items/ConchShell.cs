﻿using NetAF.Assets;
using NetAF.Assets.Interaction;
using NetAF.Examples.Assets.Items;
using NetAF.Utilities;

namespace NetAF.Examples.Assets.Regions.Everglades.Items
{
    public class ConchShell : IAssetTemplate<Item>
    {
        #region Constants

        internal const string Name = "Conch Shell";
        private const string Description = "A pretty conch shell, it is about the size of a coconut";

        #endregion

        #region Implementation of IAssetTemplate<Item>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The item.</returns>
        public Item Instantiate()
        {
            var conchShell = new Item(Name, Description, true)
            {
                Interaction = item =>
                {
                    switch (item.Identifier.IdentifiableName)
                    {
                        case Knife.Name:
                            return new InteractionResult(InteractionEffect.FatalEffect, item, "You slash at the conch shell and it shatters into tiny pieces. Without the conch shell you are well and truly in trouble.");
                        default:
                            return new InteractionResult(InteractionEffect.NoEffect, item);
                    }
                }
            };

            return conchShell;
        }

        #endregion
    }
}
