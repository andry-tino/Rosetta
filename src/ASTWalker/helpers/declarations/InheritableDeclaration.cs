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
        public virtual ModifierTokens Visibility
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
                    if (baselist == null)
                    {
                        return this.baseTypes;
                    }

                    SeparatedSyntaxList<BaseTypeSyntax> listSyntax = this.TypeDeclarationSyntaxNode.BaseList.Types;

                    IEnumerable<BaseTypeSyntax> simpleBaseTypes = listSyntax.Where(node => node.Kind() == SyntaxKind.SimpleBaseType);
                    var unprocessedBaseTypes = new List<BaseTypeSyntax>(); // Semantic model failed for those

                    // Process each type and try using the semantic model, at the end, it might 
                    // be that not all types could not be solved using the semantic model, for 
                    // those, try using the names
                    foreach (BaseTypeSyntax baseType in simpleBaseTypes)
                    {
                        var discrimination = this.DiscriminateUsingSemanticModel(baseType);
                        if (!discrimination.HasValue)
                        {
                            unprocessedBaseTypes.Add(baseType);
                            continue;
                        }

                        switch (discrimination.Value)
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
                                // Something else other than class or interface => not of interest, skip it
                                continue;
                        }
                    }

                    // Do we have any unresolved types?
                    if (unprocessedBaseTypes.Count() > 0)
                    {
                        List<BaseTypeReference> resolvedTypes = this.DiscriminateByName(unprocessedBaseTypes);

                        ((List<BaseTypeReference>)this.baseTypes).AddRange(resolvedTypes);
                    }
                }

                return this.baseTypes;
            }
        }

        /// <summary>
        /// This strategy employs the semantic model.
        /// </summary>
        /// <param name="baseType"></param>
        /// <returns></returns>
        private Roslyn.TypeKind? DiscriminateUsingSemanticModel(BaseTypeSyntax baseType)
        {
            var symbolInfo = this.SemanticModel.GetSymbolInfo(baseType.Type);
            ITypeSymbol typeSymbol = symbolInfo.Symbol as ITypeSymbol;

            if (typeSymbol == null && symbolInfo.CandidateSymbols.Count() > 0)
            {
                typeSymbol = symbolInfo.CandidateSymbols[0] as ITypeSymbol;
            }

            if (typeSymbol == null)
            {
                return null;
            }

            switch (typeSymbol.TypeKind)
            {
                case Roslyn.TypeKind.Class: return Roslyn.TypeKind.Class;
                case Roslyn.TypeKind.Interface: return Roslyn.TypeKind.Interface;
                default: return null;
            }
        }

        /// <summary>
        /// This strategy uses the names and needs the whole array of base types, not just one type in the base list.
        /// </summary>
        /// <param name="baseTypeInfo"></param>
        private List<BaseTypeReference> DiscriminateByName(IEnumerable<BaseTypeSyntax> simpleBaseTypes)
        {
            var baseTypes = new List<BaseTypeReference>();
            IEnumerable<SemanticUtilities.BaseTypeInfo> baseTypeInfos = SemanticUtilities.SeparateClassAndInterfacesBasedOnNames(simpleBaseTypes);

            foreach (var baseTypeInfo in baseTypeInfos)
            {
                // Semantic model passed for consistency, but here is actually null
                switch (baseTypeInfo.Kind)
                {
                    case Roslyn.TypeKind.Class:
                        baseTypes.Add(this.CreateBaseTypeReferenceHelper(baseTypeInfo.Node, this.SemanticModel, Roslyn.TypeKind.Class));
                        break;
                    case Roslyn.TypeKind.Interface:
                        baseTypes.Add(this.CreateBaseTypeReferenceHelper(baseTypeInfo.Node, this.SemanticModel, Roslyn.TypeKind.Interface));
                        break;
                    default:
                        // Not recognized, skip it
                        continue;
                }
            }

            return baseTypes;
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
