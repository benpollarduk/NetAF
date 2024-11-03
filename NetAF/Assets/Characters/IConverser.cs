using NetAF.Conversations;

namespace NetAF.Assets.Characters
{
    /// <summary>
    /// Represents an object that can converse.
    /// </summary>
    public interface IConverser : IExaminable
    {
        /// <summary>
        /// Get the conversation.
        /// </summary>
        Conversation Conversation { get; }
    }
}