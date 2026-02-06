using System.Collections.Generic;

namespace NetAF.Targets.Markup.Model.Nodes
{
    /// <summary>
    /// Get a node representing a paragraph in the abstract syntax tree.
    /// </summary>
    public class ParagraphNode : IBlockNode
    {
        /// <summary>
        /// Get all inlines in this node.
        /// </summary>
        public List<IInlineNode> Inlines { get; } = [];
    }
}
