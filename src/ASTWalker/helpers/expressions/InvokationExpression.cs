/// <summary>
/// InvokationExpression.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.Translation;

    /// <summary>
    /// Helper for accessing invocation expressions in AST.
    /// </summary>
    public class InvokationExpression : Helper
    {
        // Cached values
        private IEnumerable<Argument> arguments;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvokationExpression"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        public InvokationExpression(InvocationExpressionSyntax syntaxNode)
            : this(syntaxNode, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvokationExpression"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        /// <param name="semanticModel"></param>
        public InvokationExpression(InvocationExpressionSyntax syntaxNode, SemanticModel semanticModel)
            : base(syntaxNode, semanticModel)
        {
        }

        /// <summary>
        /// Gets the list of parameters.
        /// </summary>
        public IEnumerable<Argument> Arguments
        {
            get
            {
                if (this.arguments == null)
                {
                    this.arguments = this.InvokationExpressionSyntaxNode.ArgumentList.Arguments.Select(
                        (ArgumentSyntax p) => new Argument(p)).ToList();
                }

                return this.arguments;
            }
        }

        /// <summary>
        /// Gets the expression.
        /// </summary>
        public ExpressionSyntax Expression
        {
            get { return this.InvokationExpressionSyntaxNode.Expression; }
        }

        private InvocationExpressionSyntax InvokationExpressionSyntaxNode
        {
            get { return this.syntaxNode as InvocationExpressionSyntax; }
        }
    }
}
