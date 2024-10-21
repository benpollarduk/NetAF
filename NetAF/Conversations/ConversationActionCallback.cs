using NetAF.Logic;

namespace NetAF.Conversations
{
    /// <summary>
    /// Provides a callback that can be used in conversations invoking actions.
    /// </summary>
    /// <param name="game">The game to invoke the callback on.</param>
    public delegate void ConversationActionCallback(Game game);
}
