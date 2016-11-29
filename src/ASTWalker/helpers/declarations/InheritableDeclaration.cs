/// <summary>
/// InheritableDeclaration.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.CodeAnalysis;
    using Roslyn = Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.AST.Utilities;
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
                // TODO: Simplify this logic

                if (this.baseTypes == null)
                {
                    this.baseTypes = new List<BaseTypeReference>();

                    BaseListSyntax baselist = this.TypeDeclarationSyntaxNode.BaseList;
                    if (baselist != null)
                    {
                        SeparatedSyntaxList<BaseTypeSyntax> listSyntax = this.TypeDeclarationSyntaxNode.BaseList.Types;

                        // TODO: Enable back once the semantic model use with AST transformation works again
                        /*if (this.SemanticModel != null) // Semantic model available, use it!
                        {
                            foreach (BaseTypeSyntax baseType in listSyntax)
                            {
                                if (baseType.Kind() == SyntaxKind.SimpleBaseType)
                                {
                                    var symbolInfo = this.SemanticModel.GetSymbolInfo(baseType.Type);
                                    var typeSymbol = symbolInfo.Symbol as ITypeSymbol;

                                    switch (typeSymbol.TypeKind)
                                    {
                                        case Roslyn.TypeKind.Class:
                                            ((List<BaseTypeReference>)this.baseTypes).Add(
                                                this.CreateBaseTypeReferenceHelper(baseType, this.SemanticModel, Roslyn.TypeKind.Class));
                                            break;
                                        case Roslyn.TypeKind.Interface:
                                            ((List<BaseTypeReference>)this.baseTypes).Add(
                                                this.CreateBaseTypeReferenceHelper(baseType, this.SemanticModel, Roslyn.TypeKind.Interface));
                                            break;
                                        default:
                                            // Not recognized, skip it
                                            continue;
                                    }
                                }
                            }
                        }
                        else // Semantic model is not available, guess!*/
                        {
                            IEnumerable<BaseTypeSyntax> simpleBaseTypes = listSyntax.Where(node => node.Kind() == SyntaxKind.SimpleBaseType);
                            IEnumerable<SemanticUtilities.BaseTypeInfo> baseTypeInfos = SemanticUtilities.SeparateClassAndInterfacesBasedOnNames(simpleBaseTypes);

                            foreach (var baseTypeInfo in baseTypeInfos)
                            {
                                // Semantic model passed for consistency, but here is actually null
                                switch (baseTypeInfo.Kind)
                                {
                                    case Roslyn.TypeKind.Class:
                                        ((List<BaseTypeReference>)this.baseTypes).Add(
                                            this.CreateBaseTypeReferenceHelper(baseTypeInfo.Node, this.SemanticModel, Roslyn.TypeKind.Class));
                                        break;
                                    case Roslyn.TypeKind.Interface:
                                        ((List<BaseTypeReference>)this.baseTypes).Add(
                                            this.CreateBaseTypeReferenceHelper(baseTypeInfo.Node, this.SemanticModel, Roslyn.TypeKind.Interface));
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="semanticModel"></param>
        /// <param name="typeKind"></param>
        /// <returns></returns>
        protected virtual BaseTypeReference CreateBaseTypeReferenceHelper(BaseTypeSyntax node, SemanticModel semanticModel, Roslyn.TypeKind typeKind)
        {
            return new BaseTypeReference(node, semanticModel, typeKind);
        }

        private TypeDeclarationSyntax TypeDeclarationSyntaxNode
        {
            get { return this.SyntaxNode as TypeDeclarationSyntax; }
        }
    }
}
