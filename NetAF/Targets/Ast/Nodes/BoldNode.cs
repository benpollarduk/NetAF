using System.Collections.Generic;

namespace NetAF.Targets.Ast.Nodes
{
    /// <summary>
    /// Represents a bold node in the abstract syntax tree.
    /// </summary>
    public class BoldNode : INode
    {
        /// <summary>
        /// Get all child nodes.
        /// </summary>
        public List<INode> Children { get; } = [];
    }
}
