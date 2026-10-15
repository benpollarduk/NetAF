using NetAF.Assets.Locations;
using NetAF.Example.Lighthouse.Assets.Items;
using NetAF.Utilities;

namespace NetAF.Example.Lighthouse.Assets.Rooms
{
    /// <summary>
    /// The lamp room at the top of the tower. The final puzzle: fill the lamp with oil
    /// and light it with dry matches to win the game.
    /// </summary>
    public class LampRoom : IAssetTemplate<Room>
    {
        #region Constants

        internal const string Name = "Lamp Room";
        private const string Description = "You stand at the top of the tower, ringed by the huge glass panels of the lens. Beyond them the storm throws itself at the night. The great lamp sits at the centre, and a small stove glows in the corner. The hatch leads back down.";

        #endregion

        #region Implementation of IAssetTemplate<Room>

        /// <inheritdoc/>
        public Room Instantiate()
        {
            var room = new Room(Name, Description, [new Exit(Direction.Down)]);

            room.AddItem(new Lamp().Instantiate());
            room.AddItem(new Stove().Instantiate());

            return room;
        }

        #endregion
    }
}
