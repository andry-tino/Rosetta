/// <summary>
/// AttributeArgument.cs
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
    /// Helper for accessing argument references in AST.
    /// </summary>
    public class AttributeArgument : Helper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeArgument"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        public AttributeArgument(AttributeArgumentSyntax syntaxNode)
            : this(syntaxNode, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeArgument"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        /// <param name="kind"></param>
        /// <remarks>
        /// When providing the semantic model, some properites will be devised from that.
        /// </remarks>
        public AttributeArgument(AttributeArgumentSyntax syntaxNode, SemanticModel semanticModel)
            : base(syntaxNode, semanticModel)
        {
        }

        /// <summary>
        /// Gets the literal expression if any. Otherwise returns <code>null</code>.
        /// </summary>
        public LiteralExpression LiteralExpression
        {
            get
            {
                var literalExpression = this.AttributeArgumentSyntaxNode.Expression as LiteralExpressionSyntax;
                
                return literalExpression == null 
                    ? null 
                    : new LiteralExpression(literalExpression, this.SemanticModel);
            }
        }

        private AttributeArgumentSyntax AttributeArgumentSyntaxNode
        {
            get { return this.SyntaxNode as AttributeArgumentSyntax; }
        }
    }
}
