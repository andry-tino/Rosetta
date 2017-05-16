/// <summary>
/// ObjectCreationExpression.cs
/// Andrea Tino - 2017
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
    /// Helper for accessing object creation expressions in AST.
    /// </summary>
    public class ObjectCreationExpression : Helper
    {
        // Cached values
        private IEnumerable<Argument> arguments;
        private TypeReference type;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectCreationExpression"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        public ObjectCreationExpression(ObjectCreationExpressionSyntax syntaxNode)
            : this(syntaxNode, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectCreationExpression"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        /// <param name="semanticModel"></param>
        public ObjectCreationExpression(ObjectCreationExpressionSyntax syntaxNode, SemanticModel semanticModel)
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
                    this.arguments = this.ObjectCreationExpressionSyntaxNode.ArgumentList.Arguments.Select(
                        (ArgumentSyntax p) => new Argument(p)).ToList();
                }

                return this.arguments;
            }
        }

        /// <summary>
        /// Gets the type being constructed.
        /// </summary>
        public TypeReference Type
        {
            get
            {
                if (this.type == null)
                {
                    this.type = new TypeReference(this.ObjectCreationExpressionSyntaxNode.Type, this.SemanticModel);
                }

                return this.type;
            }
        }

        private ObjectCreationExpressionSyntax ObjectCreationExpressionSyntaxNode
        {
            get { return this.SyntaxNode as ObjectCreationExpressionSyntax; }
        }
    }
}
