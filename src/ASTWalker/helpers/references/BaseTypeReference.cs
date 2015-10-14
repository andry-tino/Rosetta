/// <summary>
/// BaseTypeReference.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.Helpers
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Helper for accessing base type references in AST.
    /// </summary>
    internal class BaseTypeReference
    {
        private BaseTypeSyntax baseTypeSyntaxNode;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeReference"/> class.
        /// </summary>
        /// <param name="baseTypeSyntaxNode"></param>
        public BaseTypeReference(BaseTypeSyntax baseTypeSyntaxNode)
        {
            this.baseTypeSyntaxNode = baseTypeSyntaxNode;
        }

        /// <summary>
        /// Gets the type name.
        /// </summary>
        public string Name
        {
            get
            {
                var simpleNameSyntaxNode = this.baseTypeSyntaxNode.Type as SimpleNameSyntax;
                return simpleNameSyntaxNode != null ? 
                    simpleNameSyntaxNode.Identifier.ValueText : 
                    this.baseTypeSyntaxNode.Type.ToString();
            }
        }
    }
}
