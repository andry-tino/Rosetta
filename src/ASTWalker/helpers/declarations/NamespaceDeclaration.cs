/// <summary>
/// NamespaceDeclaration.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

        /// <summary>
        /// Gets the collection of <see cref="SyntaxNode"/> in the namespace.
        /// </summary>
        public IEnumerable<SyntaxNode> Members
        {
            get { return this.NamespaceDeclarationSyntaxNode.Members.Select(node => node); }
        }

        /// <summary>
        /// Gets the collection of <see cref="ClassDeclarationSyntax"/> in the namespace.
        /// </summary>
        public IEnumerable<SyntaxNode> Classes
        {
            get { return this.NamespaceDeclarationSyntaxNode.Members.Where(node => node as ClassDeclarationSyntax != null); }
        }

        /// <summary>
        /// Gets the collection of <see cref="InterfaceDeclarationSyntax"/> in the namespace.
        /// </summary>
        public IEnumerable<SyntaxNode> Interfaces
        {
            get { return this.NamespaceDeclarationSyntaxNode.Members.Where(node => node as InterfaceDeclarationSyntax != null); }
        }

        /// <summary>
        /// Gets the collection of <see cref="EnumDeclarationSyntax"/> in the namespace.
        /// </summary>
        public IEnumerable<SyntaxNode> Enums
        {
            get { return this.NamespaceDeclarationSyntaxNode.Members.Where(node => node as EnumDeclarationSyntax != null); }
        }

        /// <summary>
        /// Gets the collection of all types in the namespace.
        /// A union of <see cref="Classes"/>
        /// </summary>
        public IEnumerable<SyntaxNode> Types
        {
            get { return this.Classes.Union(this.Interfaces).Union(this.Enums); }
        }

        private NamespaceDeclarationSyntax NamespaceDeclarationSyntaxNode
        {
            get { return this.SyntaxNode as NamespaceDeclarationSyntax; }
        }
    }
}
