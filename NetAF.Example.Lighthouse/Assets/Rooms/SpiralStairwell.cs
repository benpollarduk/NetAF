using NetAF.Assets.Locations;
using NetAF.Utilities;
using NetAF.Example.Lighthouse.Assets.Items;

namespace NetAF.Example.Lighthouse.Assets.Rooms
{
    /// <summary>
    /// The spiral stairwell. Winding the seized clockwork here unlocks the hatch up to
    /// the lamp room.
    /// </summary>
    public class SpiralStairwell : IAssetTemplate<Room>
    {
        #region Constants

        internal const string Name = "Spiral Stairwell";
        private const string Description = "Iron steps wind up the inside of the tower around a shaft of silent machinery. A hatch in the ceiling leads up to the lamp room, and the stair drops back down to the jetty.";

        #endregion

        #region Implementation of IAssetTemplate<Room>

        /// <inheritdoc/>
        public Room Instantiate()
        {
            Room room = null;

            room = new Room(Name, Description, [new Exit(Direction.Down), new Exit(Direction.Up, true)]);

            var clockwork = Clockwork.Create(() =>
            {
                if (room.FindExit(Direction.Up, true, out var exit))
                    exit.Unlock();
            });

            room.AddItem(clockwork);

            return room;
        }

        #endregion
    }
}
