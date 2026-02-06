namespace NetAF.Targets.Markup.Model.Nodes
{
    /// <summary>
    /// Represents a text node in the abstract syntax tree.
    /// </summary>
    /// <param name="text">The text this node represents.</param>
    public class TextNode(string text) : IInlineNode
    {
        /// <summary>
        /// Get the text.
        /// </summary>
        public string Text => text;
    }
}
