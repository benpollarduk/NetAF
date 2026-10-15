using NetAF.Assets;
using NetAF.Assets.Characters;
using NetAF.Assets.Locations;

namespace NetAF.Rendering.FrameBuilders
{
    /// <summary>
    /// Represents any object that can build reaction frames.
    /// </summary>
    public interface IReactionFrameBuilder : IFrameBuilder
    {
        /// <summary>
        /// Build a frame.
        /// </summary>
        /// <param name="title">The title to display to the user.</param>
        /// <param name="message">The message to display to the user.</param>
        /// <param name="isError">If the message is an error.</param>
        /// <param name="player">The player.</param>
        /// <param name="room">The current room.</param>
        /// <param name="size">The size of the frame.</param>
        /// <returns>The frame.</returns>
        IFrame Build(string title, string message, bool isError, PlayableCharacter player, Room room, Size size);
    }
}
