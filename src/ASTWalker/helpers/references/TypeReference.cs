/// <summary>
/// TypeReference.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Helper for accessing type references in AST.
    /// </summary>
    internal class TypeReference
    {
        private SimpleBaseTypeSyntax typeSyntaxNode;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeReference"/> class.
        /// </summary>
        /// <param name="typeSyntaxNode"></param>
        public TypeReference(SimpleBaseTypeSyntax typeSyntaxNode)
        {
            this.typeSyntaxNode = typeSyntaxNode;
        }

        /// <summary>
        /// Gets the type identifier.
        /// </summary>
        public string Identifier
        {
            get { return this.typeSyntaxNode.ChildNodes().OfType<IdentifierNameSyntax>().First().Identifier.ValueText; }
        }
    }
}
