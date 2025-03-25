using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Conversations;
using NetAF.Conversations.Instructions;
using NetAF.Rendering.FrameBuilders;
using NetAF.Utilities;

namespace NetAF.Example.Assets.Regions.Hub.Rooms
{
    public class Clearing : IAssetTemplate<Room>
    {
        #region Constants

        public const string Name = "Jungle Clearing";
        private const string Description = "You are in a small clearing in a jungle, tightly enclosed by undergrowth. You have no idea how you got here. The chirps and buzzes coming from insects in the undergrowth are intense. There are some stone pedestals in front of you. Each has a small globe on top of it.";

        #endregion

        #region Implementation of IAssetTemplate<Room>

        /// <summary>
        /// Instantiate a new instance of the asset.
        /// </summary>
        /// <returns>The asset.</returns>
        public Room Instantiate()
        {
            var room = new Room(Name, Description, examination: request =>
            {
                if (request.Scene.Examiner.Attributes.GetValue("Coin") == 0)
                {
                    request.Scene.Examiner.Attributes.Add("Coin", 1);
                    return new Examination("Well look at that, you found a coin.");
                }

                return Room.DefaultRoomExamination(request);
            });

            var conversation = new Conversation(
            [
                new("Squarrrkkk!"),
                new("Would you like to change modes?", "ModeQuestion")
                {
                    Responses =
                    [
                        new("Yes please, change to ANSI."),
                        new("Yes please, change to HTML.", new Jump(2)),
                        new("Yes please, change to legacy.", new Jump(3)),
                        new("No thanks, keep things as they are.", new Jump(4))
                    ]
                },
                new("Arrk! Color it is.", g => g.Configuration.FrameBuilders = FrameBuilderCollections.Console, new ToName("ModeQuestion")),
                new("Eeek, HTML be fine too!", g => g.Configuration.FrameBuilders = FrameBuilderCollections.Html, new ToName("ModeQuestion")),
                new("Squarrk! Legacy, looks old. Shame it's been deleted. Maybe it will be implemented again one day! Arrk!", new ToName("ModeQuestion")),
                new("Fine, suit yourself! Squarrk!", new ToName("ModeQuestion"))
            ]);

            room.AddCharacter(new NonPlayableCharacter(new Identifier("Parrot"), new Description("A brightly colored parrot."), conversation: conversation, interaction: item =>
            {
                if (item.Identifier.Equals("Knife"))
                    return new Interaction(InteractionResult.TargetExpires, item, "You slash at the parrot, in a flash of red feathers it is no more! The beast is vanquished!");

                return new(InteractionResult.NoChange, item);
            }));

            return room;
        }

        #endregion
    }
}
