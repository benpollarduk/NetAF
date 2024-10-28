using NetAF.Conversations.Instructions;

namespace NetAF.Conversations
{
    /// <summary>
    /// Provides a response to a conversation.
    /// </summary>
    /// <param name="line">The line to trigger this response.</param>
    /// <param name="instruction">Specify the end of paragraph instruction. This can be applied to a conversation to direct the conversation after this paragraph.</param>
    public sealed class Response(string line, IEndOfPargraphInstruction instruction)
    {
        #region Properties

        /// <summary>
        /// Get the line.
        /// </summary>
        public string Line { get; } = line;

        /// <summary>
        /// Get the end of paragraph instruction. This can be applied to a conversation to direct the conversation after this paragraph.
        /// </summary>
        public IEndOfPargraphInstruction Instruction { get; } = instruction;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Response class.
        /// </summary>
        /// <param name="line">The line to trigger this response.</param>
        public Response(string line) : this(line, new Next())
        {
        }

        #endregion
    }
}
