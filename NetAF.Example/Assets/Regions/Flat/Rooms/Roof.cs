using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Example.Assets.Regions.Flat.Items;
using NetAF.Utilities;

namespace NetAF.Example.Assets.Regions.Flat.Rooms
{
    public class Roof : IAssetTemplate<Room>
    {
        #region Constants

        private const string Name = "Faustos Roof";

        #endregion

        #region Implementation of IAssetTemplate<Room>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The asset.</returns>
        public Room Instantiate()
        {
            Room room = null;
            ConditionalDescription description = new("The roof is small and gravely, and it hurts your shoe-less feet to stand on it. There is a large skylight in the center of the roof, and a coffee mug sits to the side, indicating someone has been here recently. The window behind you south leads back into the bathroom.",
                                                     "The roof is small and gravely, and it hurts your shoe-less feet to stand on it. There is a large skylight in the center of the roof. The window behind you south leads back into the bathroom.",
                                                     () => room.FindItem(CoffeeMug.Name, out _));

            room = new(new Identifier(Name), description, [new Exit(Direction.South)]);

            room.AddItem(new Skylight().Instantiate());
            room.AddItem(new CoffeeMug().Instantiate());

            return room;
        }

        #endregion
    }
}
