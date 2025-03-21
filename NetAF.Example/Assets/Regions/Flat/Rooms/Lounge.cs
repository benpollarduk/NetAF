﻿using NetAF.Assets;
using NetAF.Assets.Locations;
using NetAF.Example.Assets.Regions.Flat.Items;
using NetAF.Example.Assets.Regions.Flat.NPCs;
using NetAF.Extensions;
using NetAF.Utilities;

namespace NetAF.Example.Assets.Regions.Flat.Rooms
{
    public class Lounge : IAssetTemplate<Room>
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
            Room room = null;

            ConditionalDescription description = new("You're in a large sitting room. Theres a huge map hanging on the eastern wall. On the southern wall there is a canvas. Theres a large coffee table in the center of the room. Beth is sat on a green sofa watching the TV. There is what appears to be a lead of some sort poking out from underneath the sofa. The kitchen is to the north.",
                                                     "You're in a large sitting room. Theres a huge map hanging on the eastern wall. On the southern wall there is a canvas. Theres a large coffee table in the center of the room. Beth is sat on a green sofa watching the TV. The kitchen is to the north.",
                                                     () => room.FindItem(Lead.Name, out _));

            room = new(new Identifier(Name), description, [new Exit(Direction.North)], interaction: item =>
            {
                if (item != null)
                {
                    if (CoffeeMug.Name.EqualsIdentifier(item.Identifier))
                    {
                        if (room.FindCharacter(Beth.Name, out _))
                            return new(InteractionResult.ItemExpires, item, "Beth takes the cup of coffee and smiles. Brownie points to you!");

                        return new(InteractionResult.NoChange, item, "As no one is about you decide to drink the coffee yourself. Your nose wasn't lying, it is bitter but delicious.");

                    }

                    if (CoffeeMug.Name.EqualsIdentifier(item.Identifier))
                    {
                        room.AddItem(item);
                        return new(InteractionResult.ItemExpires, item, "You put the mug down on the coffee table, sick of carrying the bloody thing around. Beth is none too impressed.");
                    }

                    if (Guitar.Name.EqualsIdentifier(item.Identifier))
                        return new(InteractionResult.NoChange, item, "You strum the guitar frantically trying to impress Beth, she smiles but looks at you like you are a fool. The guitar just isn't loud enough when it is not plugged in...");
                }

                return new(InteractionResult.NoChange, item);
            });

            room.AddCharacter(new Beth().Instantiate());
            room.AddItem(new Map().Instantiate());
            room.AddItem(new Canvas().Instantiate());
            room.AddItem(new Table().Instantiate());
            room.AddItem(new LoungeTV().Instantiate());
            room.AddItem(new Lead().Instantiate());

            return room;
        }

        #endregion
    }
}
