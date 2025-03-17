using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Example.Assets.Items;
using NetAF.Example.Assets.Regions.Everglades.Items;
using NetAF.Extensions;
using NetAF.Utilities;

namespace NetAF.Example.Assets.Regions.Everglades.Rooms
{
    public class InnerCave : IAssetTemplate<Room>
    {
        #region Constants

        private const string Name = "Inner Cave";

        #endregion

        #region Implementation of IAssetTemplate<Room>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The asset.</returns>
        public Room Instantiate()
        {
            Room room = null;

            var description = new ConditionalDescription("With the bats gone there is daylight to the north. To the west is the cave entrance", "As you enter the inner cave the screeching gets louder, and in the gloom you can make out what looks like a million sets of eyes looking back at you. Bats! You can just make out a few rays of light coming from the north, but the bats are blocking your way.", () => !room[Direction.North].IsLocked);
            room = new Room(new(Name), description, [new Exit(Direction.West), new Exit(Direction.North, true)], interaction: item =>
            {
                if (item != null && ConchShell.Name.EqualsExaminable(item))
                {
                    room[Direction.North].Unlock();
                    return new(InteractionResult.ItemExpires, item, "You blow into the Conch Shell. The Conch Shell howls, the  bats leave! Conch shell crumbles to pieces.");
                }

                if (item != null && Knife.Name.EqualsExaminable(item))
                    return new(InteractionResult.NoChange, item, "You slash wildly at the bats, but there are too many. Don't aggravate them!");

                return new(InteractionResult.NoChange, item);
            });

            return room;

        }

#endregion
    }
}
