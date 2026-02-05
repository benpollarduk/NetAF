using System.Collections.Generic;

namespace NetAF.Targets.Ast.Nodes
{
    /// <summary>
    /// Represents a document in the abstract syntax tree.
    /// </summary>
    public class DocumentNode : INode
    {
        /// <summary>
        /// Get all child nodes.
        /// </summary>
        public List<INode> Children { get; } = [];
    }
}
