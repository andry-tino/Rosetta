/// <summary>
/// IASTBuilder.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection
{
    using System;

    using Microsoft.CodeAnalysis;

    /// <summary>
    /// Collects information about the generated AST.
    /// </summary>
    public class ASTInfo
    {
        /// <summary>
        /// Gets or sets the tree.
        /// </summary>
        public SyntaxTree Tree { get; set; }

        /// <summary>
        /// Gets or sets the compilation unit.
        /// This might be null.
        /// </summary>
        public SemanticModel SemanticModel { get; set; }

        /// <summary>
        /// Gets or sets the number of generated classes.
        /// </summary>
        public int ClassCount { get; set; }

        /// <summary>
        /// Gets or sets the number of generated structs.
        /// </summary>
        public int StructCount { get; set; }

        /// <summary>
        /// Gets or sets the number of generated enums.
        /// </summary>
        public int EnumCount { get; set; }

        /// <summary>
        /// Gets or sets the number of generated interfaces.
        /// </summary>
        public int InterfaceCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"classes={this.ClassCount}, interfaces={this.InterfaceCount}, enums={this.EnumCount}, structs={this.StructCount}";
        }
    }
}
