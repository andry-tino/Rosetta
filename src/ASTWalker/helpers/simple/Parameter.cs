/// <summary>
/// Parameter.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.Helpers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Helper for accessing parameter references in AST.
    /// </summary>
    public class Parameter : Helper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Parameter"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        public Parameter(ParameterSyntax syntaxNode)
            : this(syntaxNode, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Parameter"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        /// <param name="kind"></param>
        /// <remarks>
        /// When providing the semantic model, some properites will be devised from that.
        /// </remarks>
        public Parameter(ParameterSyntax syntaxNode, SemanticModel semanticModel)
            : base(syntaxNode, semanticModel)
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

        /// <summary>
        /// Gets the equals expression if any.
        /// </summary>
        public IEnumerable<object> EqualsExpression
        {
            get { return null; }
        }

        private ParameterSyntax ParameterSyntaxNode
        {
            get { return this.syntaxNode as ParameterSyntax; }
        }
    }
}
