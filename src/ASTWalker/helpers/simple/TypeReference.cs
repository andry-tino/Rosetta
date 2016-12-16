/// <summary>
/// TypeReference.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Helper for accessing type references in AST.
    /// </summary>
    public class TypeReference : Helper
    {
        // TODO: Have a common or unique class for this and `BaseTypeReference`

        // Cached quantities
        private IEnumerable<AttributeSemantics> attributes;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeReference"/> class.
        /// </summary>
        /// <param name="typeSyntaxNode"></param>
        /// <remarks>
        /// This is a minimal constructor, some properties might be unavailable.
        /// </remarks>
        public TypeReference(TypeSyntax typeSyntaxNode)
            : this(typeSyntaxNode, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeReference"/> class.
        /// </summary>
        /// <param name="typeSyntaxNode"></param>
        /// <param name="kind"></param>
        /// <remarks>
        /// When providing the semantic model, some properites will be devised from that.
        /// </remarks>
        public TypeReference(TypeSyntax typeSyntaxNode, SemanticModel semanticModel)
            : base(typeSyntaxNode, semanticModel)
        {
        }

        /// <summary>
        /// Gets the list of attributes applied to the type.
        /// </summary>
        /// <remarks>
        /// This is about semantically retrieved attributes.
        /// </remarks>
        public IEnumerable<AttributeSemantics> Attributes
        {
            get
            {
                if (this.attributes == null)
                {
                    if (this.SemanticModel != null)
                    {
                        ImmutableArray<AttributeData>? attributes = null;

                        // Symbol can be found via Symbol
                        ISymbol symbol = this.SemanticModel.GetSymbolInfo(this.TypeSyntaxNode).Symbol;
                        if (symbol != null)
                        {
                            attributes = symbol.GetAttributes();
                        }

                        // Symbol can be found via TypeSymbol
                        ITypeSymbol type = this.SemanticModel.GetTypeInfo(this.TypeSyntaxNode).Type;
                        if (type != null)
                        {
                            attributes = type.GetAttributes();
                        }

                        if (!attributes.HasValue)
                        {
                            // Could not find attributes
                            this.attributes = new AttributeSemantics[0];
                        }
                        else
                        {
                            // Attributes could be found
                            this.attributes = attributes.Value.Select(attribute => new AttributeSemantics(attribute));
                        }
                    }
                    else
                    {
                        this.attributes = new AttributeSemantics[0];
                    }
                }

                return this.attributes;
            }
        }

        /// <summary>
        /// Gets the type full name.
        /// </summary>
        public virtual string FullName
        {
            get
            {
                var simpleNameSyntaxNode = this.TypeSyntaxNode as SimpleNameSyntax;

                Func<string> noSemanticAction = () => simpleNameSyntaxNode != null ? simpleNameSyntaxNode.Identifier.ValueText : this.TypeSyntaxNode.ToString();
                Func<SemanticModel, string> semanticAction = (semanticModel) => TryGetTypeSymbolFullName(this.TypeSyntaxNode, this.SemanticModel) ?? noSemanticAction();
                
                return this.ChooseSymbolFrom<string>(noSemanticAction, semanticAction);
            }
        }

        /// <summary>
        /// Gets the type name.
        /// </summary>
        public string Name
        {
            get
            {
                var simpleNameSyntaxNode = this.TypeSyntaxNode as SimpleNameSyntax;

                return simpleNameSyntaxNode != null ? simpleNameSyntaxNode.Identifier.ValueText : this.TypeSyntaxNode.ToString();
            }
        }

        /// <summary>
        /// Gets a value indicating whether the type of the node is <code>void</code> or not.
        /// </summary>
        public bool IsVoid => this.FullName.ToLower().Contains("void"); // Using FullName so we can rely on SemanticModel if available

        /// <summary>
        /// Tries all possible ways to retrieve the full name using the semantic model.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="semanticModel"></param>
        /// <returns></returns>
        /// <exception cref="SymbolNotFoundException"></exception>
        internal static string GetTypeSymbolFullName(TypeSyntax node, SemanticModel semanticModel)
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
        internal static string TryGetTypeSymbolFullName(TypeSyntax node, SemanticModel semanticModel)
        {
            var displayFormat = new SymbolDisplayFormat(typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces);

            // Symbol can be found via Symbol
            ISymbol symbol = semanticModel.GetSymbolInfo(node).Symbol;
            if (symbol != null)
            {
                return symbol.ToDisplayString(displayFormat);
            }

            // Symbol can be found via TypeSymbol
            ITypeSymbol type = semanticModel.GetTypeInfo(node).Type;
            if (type != null)
            {
                return type.ToDisplayString(displayFormat);
            }

            // Could not find symbol
            return null;
        }

        /// <summary>
        /// Tries all possible ways to retrieve the full name using the semantic model.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        internal static string GetTypeSymbolFullName(ITypeSymbol typeSymbol) =>
            typeSymbol.ToDisplayString(new SymbolDisplayFormat(
                typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces));

        protected TypeSyntax TypeSyntaxNode
        {
            get { return this.SyntaxNode as TypeSyntax; }
        }
    }
}
