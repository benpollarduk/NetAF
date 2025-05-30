﻿using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Example.Assets.Regions.Zelda.Items;
using NetAF.Example.Assets.Regions.Zelda.NPCs;
using NetAF.Extensions;
using NetAF.Utilities;

namespace NetAF.Example.Assets.Regions.Zelda.Rooms
{
    public class OutsideLinksHouse : IAssetTemplate<Room>
    {
        #region Constants

        private const string Name = "Outside Links House";
        private const string Description = "The Kokiri forest looms in front of you. It seems duller and much smaller than you remember, with thickets of deku scrub growing in every direction, except to the north where you can hear the trickle of a small stream. To the south is you house, and to the east is the entrance to the Tail Cave.";

        #endregion

        #region Implementation of IAssetTemplate<Room>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The asset.</returns>
        public Room Instantiate()
        {
            var room = new Room(Name, Description, [new Exit(Direction.South), new Exit(Direction.North), new Exit(Direction.East, true)]);
            Item door = null;

            door = new TailDoor(item =>
            {
                if (TailKey.Name.EqualsExaminable(item))
                {
                    if (room.FindExit(Direction.East, true, out var exit))
                        exit.Unlock();

                    room.RemoveItem(door);
                    return new(InteractionResult.ItemExpires, item, "The Tail Key fits perfectly in the lock, you turn it and the door swings open, revealing a gaping cave mouth...");
                }

                if (Sword.Name.EqualsExaminable(item))
                    return new(InteractionResult.NoChange, item, "Clang clang!");

                return new(InteractionResult.NoChange, item);
            }).Instantiate();

            room.AddItem(door);
            room.AddCharacter(new Saria(room).Instantiate());

            return room;
        }

        #endregion
    }
}
