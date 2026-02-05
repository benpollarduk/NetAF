using System.Collections.Generic;

namespace NetAF.Targets.Ast.Nodes
{
    /// <summary>
    /// Get a node representing a paragraph in the abstract syntax tree.
    /// </summary>
    public class ParagraphNode : INode
    {
        /// <summary>
        /// Get all inlines in this paragraph.
        /// </summary>
        public List<INode> Inlines { get; } = [];
    }
}
