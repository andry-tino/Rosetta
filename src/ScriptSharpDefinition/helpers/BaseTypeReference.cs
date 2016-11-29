/// <summary>
/// BaseTypeReference.cs
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
    public class BaseTypeReference : Rosetta.AST.Helpers.BaseTypeReference
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTypeReference"/> class.
        /// </summary>
        /// <param name="baseTypeSyntaxNode"></param>
        /// <remarks>
        /// This is a minimal constructor, some properties might be unavailable.
        /// </remarks>
        public BaseTypeReference(BaseTypeSyntax baseTypeSyntaxNode)
            : this(baseTypeSyntaxNode, null)
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
        public BaseTypeReference(BaseTypeSyntax baseTypeSyntaxNode, SemanticModel semanticModel)
            : this(baseTypeSyntaxNode, semanticModel, TypeKind.Unknown)
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
        public BaseTypeReference(BaseTypeSyntax baseTypeSyntaxNode, SemanticModel semanticModel, TypeKind kind)
            : base(baseTypeSyntaxNode, semanticModel, kind)
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
                    var symbol = this.SemanticModel.GetSymbolInfo(this.BaseTypeSyntaxNode.Type).Symbol;
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
