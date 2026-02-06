namespace NetAF.Targets.Markup.Model.Tokens
{
    /// <summary>
    /// Enumeration of token types.
    /// </summary>
    internal enum TokenType
    {
        /// <summary>
        /// General text.
        /// </summary>
        Text,
        /// <summary>
        /// New line.
        /// </summary>
        NewLine,
        /// <summary>
        /// Heading 1, 2, 3, 4.
        /// </summary>
        Heading,
        /// <summary>
        /// Open tag.
        /// </summary>
        OpenTag,
        /// <summary>
        /// Close tag.
        /// </summary>
        CloseTag
    }
}
