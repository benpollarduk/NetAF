using NetAF.Assets.Locations;
using NetAF.Example.Lighthouse.Assets.Items;
using NetAF.Example.Lighthouse.Assets.NPCs;
using NetAF.Utilities;

namespace NetAF.Example.Lighthouse.Assets.Rooms
{
    /// <summary>
    /// The keeper's cottage. Home of Martha, the logbook, the empty oil can, and the
    /// winding handle.
    /// </summary>
    public class KeepersCottage : IAssetTemplate<Room>
    {
        #region Constants

        internal const string Name = "Keeper's Cottage";
        private const string Description = "The cottage is low and close, smelling of cold ash and tar. A plain table stands in the middle of the room, and a single chair faces the dead hearth. A door leads back south to the jetty.";

        #endregion

        #region Implementation of IAssetTemplate<Room>

        /// <inheritdoc/>
        public Room Instantiate()
        {
            var room = new Room(Name, Description, [new Exit(Direction.South)]);

            room.AddItem(new Logbook().Instantiate());
            room.AddItem(new OilCan().Instantiate());
            room.AddItem(new WindingHandle().Instantiate());
            room.AddCharacter(new Martha().Instantiate());

            return room;
        }

        #endregion
    }
}
