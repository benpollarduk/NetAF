using System.Collections.Generic;

namespace NetAF.Targets.Ast.Nodes
{
    /// <summary>
    /// Represents an italic node in the abstract syntax tree.
    /// </summary>
    public class ItalicNode : INode
    {
        /// <summary>
        /// Get all child nodes.
        /// </summary>
        public List<INode> Children { get; } = [];
    }
}
