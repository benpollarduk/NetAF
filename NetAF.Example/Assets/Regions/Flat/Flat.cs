﻿using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Example.Assets.Regions.Flat.Items;
using NetAF.Example.Assets.Regions.Flat.NPCs;
using NetAF.Example.Assets.Regions.Flat.Rooms;
using NetAF.Extensions;
using NetAF.Utilities;

namespace NetAF.Example.Assets.Regions.Flat
{
    public class Flat : IAssetTemplate<Region>
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
            var lounge = new Lounge().Instantiate();

            Room spareBedroom = null;
            
            spareBedroom = new SpareBedroom(item =>
            {
                if (Lead.Name.EqualsIdentifier(item.Identifier))
                {
                    spareBedroom.AddItem(new(item.Identifier, item.Description, true));
                    return new(InteractionResult.ItemExpires, item, "The lead fits snugly into the input socket on the amp.");
                }

                if (Guitar.Name.EqualsIdentifier(item.Identifier))
                {
                    if (spareBedroom.FindItem(Lead.Name, out _))
                    {
                        easternHallway[Direction.East].Unlock();

                        if (lounge.FindCharacter(Beth.Name, out var b))
                        {
                            lounge.RemoveCharacter(b);
                            return new(InteractionResult.NoChange, item, "The guitar plugs in with a satisfying click. You play some punk and the amp sings. Beth's had enough! She bolts for the front door leaving it wide open! You are free to leave the flat! You unplug the guitar.");
                        }

                        return new(InteractionResult.NoChange, item, "The guitar plugs in with a satisfying click. You play some punk and the amp sings.");
                    }

                    return new(InteractionResult.NoChange, item, "You have no lead so you can't use the guitar with the amp...");
                }

                return new(InteractionResult.NoChange, item);
            }).Instantiate();

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
