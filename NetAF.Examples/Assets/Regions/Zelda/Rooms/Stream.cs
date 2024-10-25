﻿using NetAF.Assets;
using NetAF.Assets.Interaction;
using NetAF.Assets.Locations;
using NetAF.Examples.Assets.Regions.Zelda.Items;
using NetAF.Extensions;
using NetAF.Utilities;

namespace NetAF.Examples.Assets.Regions.Zelda.Rooms
{
    internal class Stream : IAssetTemplate<Room>
    {
        #region Constants

        private const string Name = "Stream";
        private const string Description = "";

        #endregion

        #region Implementation of IAssetTemplate<Room>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The asset.</returns>
        public Room Instantiate()
        {
            var room = new Room(Name, Description, new Exit(Direction.South));

            room.Description = new ConditionalDescription("A small stream flows east to west in front of you. The water is clear, and looks good enough to drink. On the bank is a small bush. To the south is the Kokiri forest", "A small stream flows east to west infront of you. The water is clear, and looks good enough to drink. On the bank is a stump where the bush was. To the south is the Kokiri forest.", () => room.ContainsItem(Bush.Name));

            var bush = new Bush().Instantiate();
            var rupee = new Rupee().Instantiate();

            bush.Interaction = item =>
            {
                if (Sword.Name.EqualsExaminable(item))
                {
                    bush.Morph(new Stump().Instantiate());
                    rupee.IsPlayerVisible = true;
                    return new InteractionResult(InteractionEffect.ItemMorphed, item, "You slash wildly at the bush and reduce it to a stump. This exposes a red rupee, that must have been what was glinting from within the bush...");
                }

                return new InteractionResult(InteractionEffect.NoEffect, item);
            };

            room.AddItem(bush);
            room.AddItem(rupee);

            return room;
        }

        #endregion
    }
}
