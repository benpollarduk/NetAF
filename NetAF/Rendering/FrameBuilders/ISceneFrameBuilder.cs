using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;
using NetAF.Commands;
using NetAF.Rendering.Frames;

namespace NetAF.Rendering.FrameBuilders
{
    /// <summary>
    /// Represents any object that can build scene frames.
    /// </summary>
    public interface ISceneFrameBuilder
    {
        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="room">Specify the Room.</param>
        /// <param name="viewPoint">Specify the viewpoint from the room.</param>
        /// <param name="player">Specify the player.</param>
        /// <param name="contextualCommands">The contextual commands to display.</param>
        /// <param name="keyType">The type of key to use.</param>
        /// <param name="size">The size of the frame.</param>
        IFrame Build(Room room, ViewPoint viewPoint, PlayableCharacter player, CommandHelp[] contextualCommands, KeyType keyType, Size size);
    }
}
