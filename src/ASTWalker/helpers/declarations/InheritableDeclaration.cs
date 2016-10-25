/// <summary>
/// InheritableDeclaration.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.Helpers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;
    using Roslyn = Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.Translation;

    /// <summary>
    /// Helper for accessing classes and interfaces in AST.
    /// </summary>
    public abstract class InheritableDeclaration : Helper
    {
        // Cached values
        private IEnumerable<BaseTypeReference> baseTypes;

        /// <summary>
        /// Initializes a new instance of the <see cref="InheritableDeclaration"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        public InheritableDeclaration(TypeDeclarationSyntax syntaxNode) 
            : this(syntaxNode, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InheritableDeclaration"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        /// <param name="semanticModel"></param>
        public InheritableDeclaration(TypeDeclarationSyntax syntaxNode, SemanticModel semanticModel) 
            : base(syntaxNode, semanticModel)
        {
            this.baseTypes = null;
        }

        /// <summary>
        /// Gets the visibility associated with the type.
        /// </summary>
        public virtual VisibilityToken Visibility
        {
            get { return Modifiers.Get(this.TypeDeclarationSyntaxNode.Modifiers); }
        }

        /// <summary>
        /// Gets the name of the type.
        /// </summary>
        public virtual string Name
        {
            get { return this.TypeDeclarationSyntaxNode.Identifier.ValueText; }
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
                    this.baseTypes = new List<BaseTypeReference>();

                    BaseListSyntax baselist = this.TypeDeclarationSyntaxNode.BaseList;
                    if (baselist != null)
                    {
                        SeparatedSyntaxList<BaseTypeSyntax> listSyntax = this.TypeDeclarationSyntaxNode.BaseList.Types;
                        foreach (BaseTypeSyntax baseType in listSyntax)
                        {
                            if (baseType.Kind() == SyntaxKind.SimpleBaseType)
                            {
                                ITypeSymbol typeSymbol = this.SemanticModel.GetSymbolInfo(
                                    baseType.Type).Symbol as ITypeSymbol;

                                if (typeSymbol == null)
                                {
                                    // TODO: This requires a semantic model. Add case where semantic model is not available
                                    throw new InvalidOperationException("Base type evaluation failure due to missing semantic model");
                                }

                                switch (typeSymbol.TypeKind)
                                {
                                    case Roslyn.TypeKind.Class:
                                        ((List<BaseTypeReference>)this.baseTypes).Add(
                                            new BaseTypeReference(baseType, Roslyn.TypeKind.Class));
                                        break;
                                    case Roslyn.TypeKind.Interface:
                                        ((List<BaseTypeReference>)this.baseTypes).Add(
                                            new BaseTypeReference(baseType, Roslyn.TypeKind.Interface));
                                        break;
                                    default:
                                        // Not recognized, skip it
                                        continue;
                                }
                            }
                        }
                    }
                }

                return this.baseTypes;
            }
        }
        
        private TypeDeclarationSyntax TypeDeclarationSyntaxNode
        {
            get { return this.syntaxNode as TypeDeclarationSyntax; }
        }
    }
}
