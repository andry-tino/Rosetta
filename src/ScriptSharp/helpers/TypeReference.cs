/// <summary>
/// TypeReference.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.AST.Helpers
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.AST.Helpers;

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
                        var attributeDatas = symbol.GetAttributes();
                        string overriddenName = null;
                        foreach (var attributeData in attributeDatas)
                        {
                            var scriptNamespaceHelper = new ScriptNamespaceAttributeOnType(new AttributeSemantics(attributeData));
                            if (scriptNamespaceHelper.HasScriptNamespaceAttributeDecoration)
                            {
                                overriddenName = scriptNamespaceHelper.OverridenName;
                            }
                        }

                        if (overriddenName != null)
                        {
                            return $"{overriddenName}.{symbol.Name}";
                        }
                    }

                    // Trying to get the type
                    var type = this.SemanticModel.GetTypeInfo(this.TypeSyntaxNode).Type;
                    if (type != null)
                    {
                        var attributeDatas = type.GetAttributes();
                    }
                }

                return base.FullName;
            }
        }
    }
}
