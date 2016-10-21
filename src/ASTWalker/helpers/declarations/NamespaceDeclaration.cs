/// <summary>
/// NamespaceDeclaration.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.Helpers
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Helper for accessing namespaces in AST.
    /// </summary>
    public class NamespaceDeclaration : Helper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NamespaceDeclaration"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        public NamespaceDeclaration(NamespaceDeclarationSyntax syntaxNode)
            : this(syntaxNode, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamespaceDeclaration"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        /// <param name="semanticModel"></param>
        public NamespaceDeclaration(NamespaceDeclarationSyntax syntaxNode, SemanticModel semanticModel)
            : base(syntaxNode, semanticModel)
        {
        }

        /// <summary>
        /// Gets the name of the type.
        /// </summary>
        public string Name
        {
            get { return this.NamespaceDeclarationSyntaxNode.Name.ToString(); }
        }

        private NamespaceDeclarationSyntax NamespaceDeclarationSyntaxNode
        {
            get { return this.syntaxNode as NamespaceDeclarationSyntax; }
        }
    }
}
