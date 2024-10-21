using NetAF.Assets.Interaction;
using NetAF.Assets.Locations;
using NetAF.Examples.Assets.Regions.Flat.Items;
using NetAF.Examples.Assets.Regions.Flat.NPCs;
using NetAF.Extensions;
using NetAF.Utilities;

namespace NetAF.Examples.Assets.Regions.Flat.Rooms
{
    internal class Lounge : IAssetTemplate<Room>
    {
        #region Constants

        private const string Name = "Lounge";

        #endregion

        #region Implementation of IAssetTemplate<Room>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The asset.</returns>
        public Room Instantiate()
        {
            var room = new Room(Name, string.Empty, new Exit(Direction.North));

            room.AddCharacter(new Beth().Instantiate());
            room.AddItem(new Map().Instantiate());
            room.AddItem(new Canvas().Instantiate());
            room.AddItem(new Table().Instantiate());
            room.AddItem(new LoungeTV().Instantiate());
            room.AddItem(new Lead().Instantiate());

            room.Interaction = item =>
            {
                if (item != null)
                {
                    if (MugOfCoffee.Name.EqualsIdentifier(item.Identifier))
                    {
                        if (room.ContainsCharacter(Beth.Name))
                            return new InteractionResult(InteractionEffect.ItemUsedUp, item, "Beth takes the cup of coffee and smiles. Brownie points to you!");

                        item.Morph(new EmptyCoffeeMug().Instantiate());
                        return new InteractionResult(InteractionEffect.ItemMorphed, item, "As no one is about you decide to drink the coffee yourself. Your nose wasn't lying, it is bitter but delicious.");

                    }

                    if (EmptyCoffeeMug.Name.EqualsIdentifier(item.Identifier))
                    {
                        room.AddItem(item);
                        return new InteractionResult(InteractionEffect.ItemUsedUp, item, "You put the mug down on the coffee table, sick of carrying the bloody thing around. Beth is none too impressed.");
                    }

                    if (Guitar.Name.EqualsIdentifier(item.Identifier))
                        return new InteractionResult(InteractionEffect.NoEffect, item, "You strum the guitar frantically trying to impress Beth, she smiles but looks at you like you are a fool. The guitar just isn't loud enough when it is not plugged in...");
                }

                return new InteractionResult(InteractionEffect.NoEffect, item);
            };

            return room;
        }

        #endregion
    }
}
