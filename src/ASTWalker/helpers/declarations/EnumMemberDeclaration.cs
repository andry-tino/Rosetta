/// <summary>
/// EnumMemberDeclaration.cs
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

    /// <summary>
    /// Helper for accessing enum members in AST.
    /// </summary>
    public class EnumMemberDeclaration : Helper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnumMemberDeclaration"/> class.
        /// </summary>
        /// <param name="enumMemberDeclarationNode"></param>
        public EnumMemberDeclaration(EnumMemberDeclarationSyntax enumMemberDeclarationNode)
            : this(enumMemberDeclarationNode, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumMemberDeclaration"/> class.
        /// </summary>
        /// <param name="enumMemberDeclarationNode"></param>
        /// <param name="semanticModel"></param>
        public EnumMemberDeclaration(EnumMemberDeclarationSyntax enumMemberDeclarationNode, SemanticModel semanticModel)
            : base(enumMemberDeclarationNode, semanticModel)
        {
        }

        /// <summary>
        /// Gets the visibility associated with the property.
        /// </summary>
        public ExpressionSyntax Value => this.EnumMemberDeclarationSyntaxNode.EqualsValue != null 
            ? this.EnumMemberDeclarationSyntaxNode.EqualsValue.Value 
            : null;

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name => this.EnumMemberDeclarationSyntaxNode.Identifier.ValueText;

        private EnumMemberDeclarationSyntax EnumMemberDeclarationSyntaxNode
        {
            get { return this.SyntaxNode as EnumMemberDeclarationSyntax; }
        }
    }
}
