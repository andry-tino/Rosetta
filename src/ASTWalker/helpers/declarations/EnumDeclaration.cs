/// <summary>
/// EnumDeclaration.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.AST.Helpers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.Translation;

    /// <summary>
    /// Helper for accessing enums in AST.
    /// </summary>
    public class EnumDeclaration : Helper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnumDeclaration"/> class.
        /// </summary>
        /// <param name="enumDeclarationNode"></param>
        public EnumDeclaration(EnumDeclarationSyntax enumDeclarationNode)
            : this(enumDeclarationNode, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumDeclaration"/> class.
        /// </summary>
        /// <param name="enumDeclarationNode"></param>
        /// <param name="semanticModel"></param>
        public EnumDeclaration(EnumDeclarationSyntax enumDeclarationNode, SemanticModel semanticModel)
            : base(enumDeclarationNode, semanticModel)
        {
        }

        /// <summary>
        /// Gets the visibility associated with the property.
        /// </summary>
        public ModifierTokens Visibility
        {
            get { return Modifiers.Get(this.EnumDeclarationSyntaxNode.Modifiers); }
        }

        /// <summary>
        /// Gets the values of the enum.
        /// </summary>
        public IEnumerable<string> Values
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
        {
            get { return this.EnumDeclarationSyntaxNode.Identifier.ValueText; }
        }

        private EnumDeclarationSyntax EnumDeclarationSyntaxNode
        {
            get { return this.SyntaxNode as EnumDeclarationSyntax; }
        }
    }
}
