/// <summary>
/// TypeReference.cs
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
    /// Helper for accessing type references in AST.
    /// </summary>
    public class TypeReference : Helper
    {
        // TODO: Have a common or unique class for this and `BaseTypeReference`

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
        /// Gets the type full name.
        /// </summary>
        public string FullName
        {
            get
            {
                var simpleNameSyntaxNode = this.TypeSyntaxNode as SimpleNameSyntax;

                Func<string> noSemanticAction = () => simpleNameSyntaxNode != null ? simpleNameSyntaxNode.Identifier.ValueText : this.TypeSyntaxNode.ToString();
                Func<SemanticModel, string> semanticAction = (semanticModel) =>
                {
                    var symbol = semanticModel.GetSymbolInfo(this.TypeSyntaxNode).Symbol;
                    if (symbol == null)
                    {
                        return noSemanticAction();
                    }

                    var displayFormat = new SymbolDisplayFormat(typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces);
                    return symbol.ToDisplayString(displayFormat);
                };

                // TODO: Verify the correctness of the semantic model action
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

        private TypeSyntax TypeSyntaxNode
        {
            get { return this.SyntaxNode as TypeSyntax; }
        }
    }
}
