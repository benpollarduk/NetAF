﻿using NetAF.Assets;
using NetAF.Assets.Interaction;
using NetAF.Assets.Locations;
using NetAF.Examples.Assets.Regions.Flat.Items;
using NetAF.Examples.Assets.Regions.Flat.NPCs;
using NetAF.Examples.Assets.Regions.Flat.Rooms;
using NetAF.Extensions;
using NetAF.Utilities;

namespace NetAF.Examples.Assets.Regions.Flat
{
    internal class Flat : IAssetTemplate<Region>
    {
        #region Constants

        private const string Name = "Flat";
        private const string Description = "Ben and Beth's Flat";

        #endregion

        #region Implementation of IAssetTemplate<Region>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The asset.</returns>
        public Region Instantiate()
        {
            var roof = new Roof().Instantiate();
            var easternHallway = new EasternHallway().Instantiate();
            var spareBedroom = new SpareBedroom().Instantiate();
            var lounge = new Lounge().Instantiate();

            lounge.Description = new ConditionalDescription("Your in a large sitting room. Theres a huge map hanging on the eastern wall. On the southern wall there is a canvas. Theres a large coffee table in the center of the room. Beth is sat on a green sofa watching the TV. There is what appears to be a lead of some sort poking out from underneath the sofa. The kitchen is to the north.",
                "Your in a large sitting room. Theres a huge map hanging on the eastern wall. On the southern wall there is a canvas. Theres a large coffee table in the center of the room. Beth is sat on a green sofa watching the TV. The kitchen is to the north.",
                () => roof.ContainsItem(Lead.Name));

            spareBedroom.Interaction = item =>
            {
                if (Lead.Name.EqualsIdentifier(item.Identifier))
                {
                    spareBedroom.AddItem(new Item(item.Identifier, item.Description, true));
                    return new InteractionResult(InteractionEffect.ItemUsedUp, item, "The lead fits snugly into the input socket on the amp.");
                }

                if (Guitar.Name.EqualsIdentifier(item.Identifier))
                {
                    if (spareBedroom.ContainsItem(Lead.Name))
                    {
                        easternHallway[Direction.East].Unlock();

                        if (lounge.FindCharacter(Beth.Name, out var b))
                        {
                            lounge.RemoveCharacter(b);
                            return new InteractionResult(InteractionEffect.NoEffect, item, "The guitar plugs in with a satisfying click. You play some punk and the amp sings. Beth's had enough! She bolts for the front door leaving it wide open! You are free to leave the flat! You unplug the guitar.");
                        }

                        return new InteractionResult(InteractionEffect.NoEffect, item, "The guitar plugs in with a satisfying click. You play some punk and the amp sings.");
                    }

                    return new InteractionResult(InteractionEffect.NoEffect, item, "You have no lead so you can't use the guitar with the amp...");
                }

                return new InteractionResult(InteractionEffect.NoEffect, item);
            };

            var regionMaker = new RegionMaker(Name, Description)
            {
                [2, 0, 0] = new Bedroom().Instantiate(),
                [2, 1, 0] = easternHallway,
                [1, 1, 0] = new WesternHallway().Instantiate(),
                [1, 2, 0] = new Bathroom().Instantiate(),
                [1, 3, 0] = roof,
                [1, 0, 0] = spareBedroom,
                [0, 1, 0] = new Kitchen().Instantiate(),
                [0, 0, 0] = lounge,
                [3, 1, 0] = new Stairway().Instantiate(),
                [2, 0, 1] = new Attic().Instantiate()
            };

            return regionMaker.Make(2, 0, 0);
        }

        #endregion
    }
}
