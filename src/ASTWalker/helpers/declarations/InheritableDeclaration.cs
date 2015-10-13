/// <summary>
/// InheritableDeclaration.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.Helpers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.Translation;

    /// <summary>
    /// Helper for accessing classes and interfaces in AST.
    /// </summary>
    internal abstract class InheritableDeclaration
    {
        protected TypeDeclarationSyntax syntaxNode;

        // Cached values
        private IEnumerable<BaseTypeReference> baseTypes;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Inheritable"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        public InheritableDeclaration(TypeDeclarationSyntax syntaxNode)
        {
            this.syntaxNode = syntaxNode;
            this.baseTypes = null;
        }

        /// <summary>
        /// Gets the visibility associated with the type.
        /// </summary>
        public virtual VisibilityToken Visibility
        {
            get { return Modifiers.Get(this.syntaxNode.Modifiers); }
        }

        /// <summary>
        /// Gets the name of the type.
        /// </summary>
        public virtual string Name
        {
            get { return this.syntaxNode.Identifier.ValueText; }
        }

        /// <summary>
        /// Gets the collection of base types.
        /// </summary>
        /// <remarks>
        /// Value is cached.
        /// </remarks>
        public virtual IEnumerable<BaseTypeReference> BaseTypes
        {
            get
            {
                if (this.baseTypes == null)
                {
                    BaseListSyntax listSyntax = this.syntaxNode.BaseList;
                    this.baseTypes = new List<BaseTypeReference>();

                    foreach (SyntaxNode node in listSyntax.ChildNodes())
                    {
                        if (node.Kind() == SyntaxKind.SimpleBaseType)
                        {
                            ITypeSymbol typeSymbol = Source.SemanticModel.GetTypeInfo(node).Type;

                            switch (typeSymbol.TypeKind)
                            {
                                case TypeKind.Class:
                                case TypeKind.Interface:
                                    ((List<BaseTypeReference>)this.baseTypes).Add(new BaseTypeReference(node as BaseTypeSyntax));
                                    break;
                                default:
                                    // Not recognized, skip it
                                    continue;
                            }
                        }
                    }
                }

                return this.baseTypes;
            }
        }
    }
}
