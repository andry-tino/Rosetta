/// <summary>
/// TypedIdentifier.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.Helpers
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Helper for accessing type references in AST.
    /// </summary>
    internal class TypedIdentifier : Helper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypedIdentifier"/> class.
        /// </summary>
        /// <param name="parameterSyntaxNode"></param>
        public TypedIdentifier(ParameterSyntax parameterSyntaxNode)
            : this(parameterSyntaxNode, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypedIdentifier"/> class.
        /// </summary>
        /// <param name="parameterSyntaxNode"></param>
        /// <param name="kind"></param>
        /// <remarks>
        /// When providing the semantic model, some properites will be devised from that.
        /// </remarks>
        public TypedIdentifier(ParameterSyntax parameterSyntaxNode, SemanticModel semanticModel)
            : base(parameterSyntaxNode, semanticModel)
        {
        }

        /// <summary>
        /// Gets the identifier name.
        /// </summary>
        public string IdentifierName
        {
            get
            {
                return this.ParameterSyntaxNode.Identifier.ValueText;
            }
        }

        /// <summary>
        /// Gets the type name.
        /// </summary>
        public string TypeName
        {
            get
            {
                var simpleNameSyntaxNode = this.ParameterSyntaxNode.Type as SimpleNameSyntax;
                return simpleNameSyntaxNode != null ?
                    simpleNameSyntaxNode.Identifier.ValueText :
                    this.ParameterSyntaxNode.Type.ToString();
            }
        }

        private ParameterSyntax ParameterSyntaxNode
        {
            get { return this.syntaxNode as ParameterSyntax; }
        }
    }
}
