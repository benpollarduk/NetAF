using System.Collections.Generic;

namespace NetAF.Targets.Markup.Ast.Nodes
{
    /// <summary>
    /// Represents a document in the abstract syntax tree.
    /// </summary>
    public class DocumentNode : INode
    {
        /// <summary>
        /// Get all blocks.
        /// </summary>
        public List<IBlockNode> Blocks { get; } = [];
    }
}
