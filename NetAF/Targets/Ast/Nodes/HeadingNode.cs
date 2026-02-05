namespace NetAF.Targets.Ast.Nodes
{
    /// <summary>
    /// Represents a heading node in the abstract syntax tree.
    /// </summary>
    /// <param name="text">The text this node represents.</param>
    /// <param name="level">The level of the heading. As default H1 will be used</param>
    public class HeadingNode(string text, HeadingLevel level = HeadingLevel.H1) : INode
    {
        /// <summary>
        /// Get the text.
        /// </summary>
        public string Text => text;

        /// <summary>
        /// Get the level of the heading.
        /// </summary>
        public HeadingLevel Level => level;
    }
}
