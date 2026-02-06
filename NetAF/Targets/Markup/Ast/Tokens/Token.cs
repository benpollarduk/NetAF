namespace NetAF.Targets.Markup.Ast.Tokens
{
    /// <summary>
    /// Represents a token.
    /// </summary>
    /// <param name="Type">The type of the token.</param>
    /// <param name="Tag">A tag containing information about the token.</param>
    internal record Token(TokenType Type, string Tag);
}
