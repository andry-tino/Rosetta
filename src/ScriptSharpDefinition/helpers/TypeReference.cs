/// <summary>
/// TypeReference.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.Definition.AST.Helpers
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    ///Decorates <see cref="AttributeDecoration"/>.
    /// </summary>
    public class TypeReference : Rosetta.AST.Helpers.TypeReference
    {
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
        /// Gets the base type name.
        /// 
        /// TODO: Improve the logic of this method in order to abstract a common component for semantically detecting the ScriptNamespace
        /// </summary>
        public override string FullName
        {
            get
            {
                if (this.SemanticModel != null)
                {
                    var symbol = this.SemanticModel.GetSymbolInfo(this.TypeSyntaxNode).Symbol;
                    if (symbol != null)
                    {
                        var attributes = symbol.GetAttributes();
                        string overriddenName = null;
                        foreach (var attribute in attributes)
                        {
                            if (attribute.AttributeClass.Name.Contains(ScriptNamespaceAttributeDecoration.ScriptNamespaceName) && attribute.ConstructorArguments.Length > 0)
                            {
                                // Limitation: We consider this usage of the attribute: `[ScriptNamespace("SomeName")]`
                                overriddenName = attribute.ConstructorArguments[0].Value.ToString();
                            }
                        }

                        if (overriddenName != null)
                        {
                            return $"{overriddenName}.{symbol.Name}";
                        }
                    }
                }

                return base.FullName;
            }
        }
    }
}
