using System.Collections.Generic;

namespace NetAF.Targets.Markup.Model.Nodes
{
    /// <summary>
    /// Represents a style span node in the abstract syntax tree.
    /// </summary>
    public class StyleSpanNode(TextStyle style) : IInlineNode
    {
        /// <summary>
        /// Get the style.
        /// </summary>
        public TextStyle Style => style;

        /// <summary>
        /// Get all inlines in this node.
        /// </summary>
        public List<IInlineNode> Inlines { get; } = [];
    }
}
