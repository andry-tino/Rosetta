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
    internal abstract class InheritableDeclaration
    {
        protected TypeDeclarationSyntax syntaxNode;
        protected SemanticModel semanticModel;

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
            this.semanticModel = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Inheritable"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        /// <param name="semanticModel"></param>
        public InheritableDeclaration(TypeDeclarationSyntax syntaxNode, SemanticModel semanticModel) 
            : this(syntaxNode)
        {
            this.semanticModel = semanticModel;
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
                    this.baseTypes = new List<BaseTypeReference>();

                    BaseListSyntax baselist = this.syntaxNode.BaseList;
                    if (baselist != null)
                    {
                        SeparatedSyntaxList<BaseTypeSyntax> listSyntax = this.syntaxNode.BaseList.Types;
                        foreach (BaseTypeSyntax baseType in listSyntax)
                        {
                            if (baseType.Kind() == SyntaxKind.SimpleBaseType)
                            {
                                ITypeSymbol typeSymbol = this.SemanticModel.GetSymbolInfo(
                                    baseType.Type).Symbol as ITypeSymbol;

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

        protected SemanticModel SemanticModel
        {
            get
            {
                if (this.semanticModel == null)
                {
                    return Source.SemanticModel;
                }

                return this.semanticModel;
            }
        }
    }
}
