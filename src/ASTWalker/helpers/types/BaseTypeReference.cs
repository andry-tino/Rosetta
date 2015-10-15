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

    /// <summary>
    /// Helper for accessing base type references in AST.
    /// </summary>
    internal class BaseTypeReference
    {
        private BaseTypeSyntax baseTypeSyntaxNode;
        private SemanticModel semanticModel;
        private Microsoft.CodeAnalysis.TypeKind? kind;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeReference"/> class.
        /// </summary>
        /// <param name="baseTypeSyntaxNode"></param>
        /// <remarks>
        /// This is a minimal constructor, some properties might be unavailable.
        /// </remarks>
        public BaseTypeReference(BaseTypeSyntax baseTypeSyntaxNode)
        {
            this.baseTypeSyntaxNode = baseTypeSyntaxNode;
            this.semanticModel = null;
            this.kind = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeReference"/> class.
        /// </summary>
        /// <param name="baseTypeSyntaxNode"></param>
        /// <param name="kind"></param>
        /// <remarks>
        /// When providing the semantic model, some properites will be devised from that.
        /// </remarks>
        public BaseTypeReference(BaseTypeSyntax baseTypeSyntaxNode, SemanticModel semanticModel)
            : this(baseTypeSyntaxNode)
        {
            this.semanticModel = semanticModel;
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
            : this(baseTypeSyntaxNode)
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

                if (this.semanticModel != null)
                {
                    return this.semanticModel.GetTypeInfo(this.baseTypeSyntaxNode).Type.TypeKind.ToTypeKind();
                }

                throw new InvalidOperationException(
                    string.Format("Cannot get property {0}! Object was constructed with minimal constructor!", 
                    nameof(this.Kind)));
            }
        }

        /// <summary>
        /// Gets the type name.
        /// </summary>
        public string Name
        {
            get
            {
                var simpleNameSyntaxNode = this.baseTypeSyntaxNode.Type as SimpleNameSyntax;
                return simpleNameSyntaxNode != null ? 
                    simpleNameSyntaxNode.Identifier.ValueText : 
                    this.baseTypeSyntaxNode.Type.ToString();
            }
        }
    }
}
