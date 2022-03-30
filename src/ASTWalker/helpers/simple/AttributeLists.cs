/// <summary>
/// AttributeLists.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.AST.Helpers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Helper for accessing attribute lists references in AST.
    /// </summary>
    /// <remarks>
    /// This class is to be used for syntax decoration.
    /// </remarks>
    public class AttributeLists : Helper
    {
        // TODO: This class does not accept a semantic model at constrcution time. In future it might do so in order to pass it to `AttributeDecorator`
        //       when this component will use the semantic model to detect the specific attribute it is referring to.

        private SyntaxList<AttributeListSyntax> attributeLists;

        // Cached values
        private IEnumerable<AttributeDecoration> attributes;

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeLists"/> class.
        /// </summary>
        /// <param name="syntaxNode">The <see cref="BaseTypeDeclarationSyntax"/> from which attributes will be extracted.</param>
        public AttributeLists(BaseTypeDeclarationSyntax syntaxNode)
            : base(syntaxNode)
        {
            this.attributeLists = syntaxNode.AttributeLists;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeLists"/> class.
        /// </summary>
        /// <param name="syntaxNode">The <see cref="BaseFieldDeclarationSyntax"/> from which attributes will be extracted.</param>
        public AttributeLists(BaseFieldDeclarationSyntax syntaxNode)
            : base(syntaxNode)
        {
            this.attributeLists = syntaxNode.AttributeLists;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeLists"/> class.
        /// </summary>
        /// <param name="syntaxNode">The <see cref="MethodDeclarationSyntax"/> from which attributes will be extracted.</param>
        public AttributeLists(MethodDeclarationSyntax syntaxNode)
            : base(syntaxNode)
        {
            this.attributeLists = syntaxNode.AttributeLists;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeLists"/> class.
        /// </summary>
        /// <param name="syntaxNode">The <see cref="FieldDeclarationSyntax"/> from which attributes will be extracted.</param>
        public AttributeLists(FieldDeclarationSyntax syntaxNode)
            : base(syntaxNode)
        {
            this.attributeLists = syntaxNode.AttributeLists;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeLists"/> class.
        /// </summary>
        /// <param name="syntaxNode">The <see cref="PropertyDeclarationSyntax"/> from which attributes will be extracted.</param>
        public AttributeLists(PropertyDeclarationSyntax syntaxNode)
            : base(syntaxNode)
        {
            this.attributeLists = syntaxNode.AttributeLists;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeLists"/> class.
        /// </summary>
        /// <param name="syntaxNode">The <see cref="EnumMemberDeclarationSyntax"/> from which attributes will be extracted.</param>
        public AttributeLists(EnumMemberDeclarationSyntax syntaxNode)
            : base(syntaxNode)
        {
            this.attributeLists = syntaxNode.AttributeLists;
        }

        /// <summary>
        /// Gets the collections of attributes.
        /// </summary>
        public IEnumerable<AttributeDecoration> Attributes
        {
            get
            {
                if (this.attributes == null)
                {
                    this.attributes = new List<AttributeDecoration>();

                    foreach (var list in this.attributeLists)
                    {
                        foreach (var attribute in list.Attributes)
                        {
                            (this.attributes as List<AttributeDecoration>).Add(new AttributeDecoration(attribute));
                        }
                    }
                }

                return this.attributes;
            }
        }
    }
}
