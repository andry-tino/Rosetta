/// <summary>
/// BaseTypeReference.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.Helpers
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.AST.Utilities;

    /// <summary>
    /// Helper for accessing base type references in AST.
    /// </summary>
    public class BaseTypeReference : Helper
    {
        // TODO: Is this really needed?
        private Microsoft.CodeAnalysis.TypeKind? kind;

        // TODO: Revise this contruction logic, it is odd and not consistent!

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTypeReference"/> class.
        /// </summary>
        /// <param name="baseTypeSyntaxNode"></param>
        /// <remarks>
        /// This is a minimal constructor, some properties might be unavailable.
        /// </remarks>
        public BaseTypeReference(BaseTypeSyntax baseTypeSyntaxNode) 
            : this(baseTypeSyntaxNode, null, Microsoft.CodeAnalysis.TypeKind.Unknown)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTypeReference"/> class.
        /// </summary>
        /// <param name="baseTypeSyntaxNode"></param>
        /// <param name="kind"></param>
        /// <remarks>
        /// When providing the semantic model, some properites will be devised from that.
        /// </remarks>
        public BaseTypeReference(BaseTypeSyntax baseTypeSyntaxNode, SemanticModel semanticModel)
            : this(baseTypeSyntaxNode, semanticModel, Microsoft.CodeAnalysis.TypeKind.Unknown)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeReference"/> class.
        /// </summary>
        /// <param name="baseTypeSyntaxNode"></param>
        /// <param name="kind"></param>
        /// <remarks>
        /// Type kind will be stored statically.
        /// </remarks>
        public BaseTypeReference(BaseTypeSyntax baseTypeSyntaxNode, Microsoft.CodeAnalysis.TypeKind kind) 
            : this(baseTypeSyntaxNode, null, kind)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeReference"/> class.
        /// </summary>
        /// <param name="baseTypeSyntaxNode"></param>
        /// <param name="semanticModel"></param>
        /// <param name="kind"></param>
        /// <remarks>
        /// Type kind will be stored statically.
        /// </remarks>
        public BaseTypeReference(BaseTypeSyntax baseTypeSyntaxNode, SemanticModel semanticModel, Microsoft.CodeAnalysis.TypeKind kind)
            : base(baseTypeSyntaxNode, semanticModel)
        {
            this.kind = kind;
        }

        /// <summary>
        /// Gets the type of base type.
        /// </summary>
        /// <remarks>
        /// Type kind will be provided from these sources in order:
        /// 1. Static member <see cref="kind"/>.
        /// 2. From semnatic model.
        /// 3. Error raised.
        /// </remarks>
        public TypeKind Kind
        {
            get
            {
                if (this.kind.HasValue)
                {
                    return this.kind.Value.ToTypeKind();
                }

                // TODO: Remove guess logic once semantic model is ready
                return SemanticUtilities.GuessBaseTypeKindFromName(this.BaseTypeSyntaxNode).ToTypeKind();

                // TODO: Semnatic model is not working at the moment, activate when ready
                //if (this.SemanticModel != null)
                //{
                //    return this.SemanticModel.GetTypeInfo(this.BaseTypeSyntaxNode).Type.TypeKind.ToTypeKind();
                //}

                //throw new InvalidOperationException(
                //    string.Format("Cannot get property {0}! Object was constructed with minimal constructor!", 
                //    nameof(this.Kind)));
            }
        }

        /// <summary>
        /// Gets the base type name.
        /// </summary>
        public virtual string FullName
        {
            get
            {
                var simpleNameSyntaxNode = this.BaseTypeSyntaxNode.Type as SimpleNameSyntax;

                Func<string> noSemanticAction = () => simpleNameSyntaxNode != null ? simpleNameSyntaxNode.Identifier.ValueText : this.BaseTypeSyntaxNode.Type.ToString();
                Func<SemanticModel, string> semanticAction = (semanticModel) => TryGetTypeSymbolFullName(this.BaseTypeSyntaxNode, this.SemanticModel) ?? noSemanticAction();

                return this.ChooseSymbolFrom<string>(noSemanticAction, semanticAction);
            }
        }

        /// <summary>
        /// Gets the base type name.
        /// </summary>
        public string Name
        {
            get
            {
                var simpleNameSyntaxNode = this.BaseTypeSyntaxNode.Type as SimpleNameSyntax;
                
                return simpleNameSyntaxNode != null 
                    ? simpleNameSyntaxNode.Identifier.ValueText.StripNamespaceFromTypeName() 
                    : this.BaseTypeSyntaxNode.Type.ToString().StripNamespaceFromTypeName();
            }
        }

        /// <summary>
        /// Tries all possible ways to retrieve the full name using the semantic model.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="semanticModel"></param>
        /// <returns></returns>
        /// <exception cref="SymbolNotFoundException"></exception>
        internal static string GetTypeSymbolFullName(BaseTypeSyntax node, SemanticModel semanticModel)
        {
            var value = TryGetTypeSymbolFullName(node, semanticModel);
            if (value != null)
            {
                return value;
            }

            throw new SymbolNotFoundException(node, semanticModel);
        }

        /// <summary>
        /// Tries all possible ways to retrieve the full name using the semantic model.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="semanticModel"></param>
        /// <returns></returns>
        internal static string TryGetTypeSymbolFullName(BaseTypeSyntax node, SemanticModel semanticModel)
        {
            var displayFormat = new SymbolDisplayFormat(typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces);

            // Symbol can be found via Symbol
            ISymbol symbol = semanticModel.GetSymbolInfo(node.Type).Symbol;
            if (symbol != null)
            {
                return symbol.ToDisplayString(displayFormat);
            }

            // Symbol can be found via TypeSymbol
            ITypeSymbol type = semanticModel.GetTypeInfo(node.Type).Type;
            if (type != null)
            {
                return type.ToDisplayString(displayFormat);
            }

            // Could not find symbol
            return null;
        }

        protected BaseTypeSyntax BaseTypeSyntaxNode
        {
            get { return this.SyntaxNode as BaseTypeSyntax; }
        }
    }
}
