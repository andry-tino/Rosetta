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
        private SemanticModel semanticModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeReference"/> class.
        /// </summary>
        /// <param name="baseTypeSyntaxNode"></param>
        public BaseTypeReference(BaseTypeSyntax baseTypeSyntaxNode) 
            : this(baseTypeSyntaxNode, Source.SemanticModel)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeReference"/> class.
        /// </summary>
        /// <param name="baseTypeSyntaxNode"></param>
        /// <param name="semanticModel"></param>
        public BaseTypeReference(BaseTypeSyntax baseTypeSyntaxNode, SemanticModel semanticModel)
        {
            this.semanticModel = semanticModel;
            this.baseTypeSyntaxNode = baseTypeSyntaxNode;
        }

        /// <summary>
        /// Gets the type name.
        /// </summary>
        public string Name
        {
            get { return this.baseTypeSyntaxNode.Type.ToString(); }
        }
    }
}
