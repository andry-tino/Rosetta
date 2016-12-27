/// <summary>
/// UsingDirective.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.AST.Helpers
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Helper for accessing interface in AST
    /// </summary>
    public class UsingDirective : Helper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UsingDirective"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        public UsingDirective(UsingDirectiveSyntax syntaxNode)
            : this(syntaxNode, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UsingDirective"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        /// <param name="semanticModel"></param>
        public UsingDirective(UsingDirectiveSyntax syntaxNode, SemanticModel semanticModel)
            : base(syntaxNode, semanticModel)
        {
        }

        /// <summary>
        /// Gets the using value.
        /// </summary>
        public string Value => this.UsingDirectiveSyntaxNode.Name.ToString();

        private UsingDirectiveSyntax UsingDirectiveSyntaxNode
        {
            get { return this.SyntaxNode as UsingDirectiveSyntax; }
        }
    }
}
