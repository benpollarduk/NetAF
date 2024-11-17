using NetAF.Assets;
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

        #endregion

        #region Implementation of IAssetTemplate<Room>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The asset.</returns>
        public Room Instantiate()
        {
            Room room = null;
            ConditionalDescription description = new ("A small stream flows east to west in front of you. The water is clear, and looks good enough to drink. On the bank is a small bush. To the south is the Kokiri forest", 
                                                      "A small stream flows east to west in front of you. The water is clear, and looks good enough to drink. On the bank is a stump where the bush was. To the south is the Kokiri forest.", 
                                                      () => room.ContainsItem(Bush.Name));

            room = new(new Identifier(Name), description, [new Exit(Direction.South)]);

            
            var rupee = new Rupee().Instantiate();
            Item bush = null;

            bush = new Bush(item =>
            {
                if (Sword.Name.EqualsExaminable(item))
                {
                    rupee.IsPlayerVisible = true;
                    return new(InteractionResult.TargetExpired, item, "You slash wildly at the bush and reduce it to a stump. This exposes a red rupee, that must have been what was glinting from within the bush...");
                }

                return new(InteractionResult.NeitherItemOrTargetExpired, item);
            }).Instantiate();

            rupee.IsPlayerVisible = false;

            room.AddItem(bush);
            room.AddItem(rupee);

            return room;
        }

        #endregion
    }
}
