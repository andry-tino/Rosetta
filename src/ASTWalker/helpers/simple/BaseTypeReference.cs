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
    public class BaseTypeReference : Helper
    {
        private Microsoft.CodeAnalysis.TypeKind? kind;

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
        /// Initializes a new instance of the <see cref="BaseTypeReference"/> class.
        /// </summary>
        /// <param name="baseTypeSyntaxNode"></param>
        /// <param name="kind"></param>
        /// <remarks>
        /// When providing the semantic model, some properites will be devised from that.
        /// </remarks>
        public BaseTypeReference(BaseTypeSyntax baseTypeSyntaxNode, SemanticModel semanticModel)
            : base(baseTypeSyntaxNode, semanticModel)
        {
            this.kind = null;
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

                if (this.SemanticModel != null)
                {
                    return this.SemanticModel.GetTypeInfo(this.BaseTypeSyntaxNode).Type.TypeKind.ToTypeKind();
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
                var simpleNameSyntaxNode = this.BaseTypeSyntaxNode.Type as SimpleNameSyntax;
                return simpleNameSyntaxNode != null ? 
                    simpleNameSyntaxNode.Identifier.ValueText : 
                    this.BaseTypeSyntaxNode.Type.ToString();
            }
        }
        
        private BaseTypeSyntax BaseTypeSyntaxNode
        {
            get { return this.syntaxNode as BaseTypeSyntax; }
        }
    }
}
