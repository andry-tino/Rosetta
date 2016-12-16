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
        private SyntaxList<AttributeListSyntax> attributeLists;

        // Cached values
        private IEnumerable<AttributeDecoration> attributes;

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeLists"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        public AttributeLists(BaseTypeDeclarationSyntax syntaxNode)
            : this(syntaxNode, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeLists"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        /// <param name="semanticModel"></param>
        /// <remarks>
        /// When providing the semantic model, some properites will be devised from that.
        /// </remarks>
        public AttributeLists(BaseTypeDeclarationSyntax syntaxNode, SemanticModel semanticModel)
            : base(syntaxNode, semanticModel)
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
