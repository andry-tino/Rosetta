/// <summary>
/// AssignmentExpression.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST.Helpers
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.Translation;

    /// <summary>
    /// Helper for accessing assignment expressions in AST.
    /// </summary>
    internal class AssignmentExpression : Helper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssignmentExpression"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        public AssignmentExpression(AssignmentExpressionSyntax syntaxNode)
            : this(syntaxNode, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssignmentExpression"/> class.
        /// </summary>
        /// <param name="syntaxNode"></param>
        /// <param name="semanticModel"></param>
        public AssignmentExpression(AssignmentExpressionSyntax syntaxNode, SemanticModel semanticModel)
            : base(syntaxNode, semanticModel)
        {
        }

        /// <summary>
        /// Gets the left hand expression.
        /// </summary>
        public ExpressionSyntax LeftHand
        {
            get { return this.AssignmentExpressionSyntaxNode.Left; }
        }

        /// <summary>
        /// Gets the right hand expression.
        /// </summary>
        public ExpressionSyntax RightHand
        {
            get { return this.AssignmentExpressionSyntaxNode.Right; }
        }

        /// <summary>
        /// Gets the operator.
        /// </summary>
        public OperatorToken Operator
        {
            get
            {
                switch (this.AssignmentExpressionSyntaxNode.Kind())
                {
                    case SyntaxKind.AddAssignmentExpression:
                        return OperatorToken.AdditionAssignment;
                    case SyntaxKind.AndAssignmentExpression:
                        return OperatorToken.AndAssignment;
                    case SyntaxKind.DivideAssignmentExpression:
                        return OperatorToken.DivideAssignment;
                    case SyntaxKind.ExclusiveOrAssignmentExpression:
                        return OperatorToken.XorAssignment;
                    case SyntaxKind.LeftShiftAssignmentExpression:
                        return OperatorToken.LeftShiftAssignment;
                    case SyntaxKind.ModuloAssignmentExpression:
                        return OperatorToken.ModuloAssignment;
                    case SyntaxKind.MultiplyAssignmentExpression:
                        return OperatorToken.MultiplicationAssignment;
                    case SyntaxKind.OrAssignmentExpression:
                        return OperatorToken.OrAssignment;
                    case SyntaxKind.RightShiftAssignmentExpression:
                        return OperatorToken.RightShiftAssignment;
                    case SyntaxKind.SubtractAssignmentExpression:
                        return OperatorToken.SubtractionAssignment;
                    case SyntaxKind.SimpleAssignmentExpression:
                        return OperatorToken.Equals;
                }

                return OperatorToken.Undefined;
            }
        }

        private AssignmentExpressionSyntax AssignmentExpressionSyntaxNode
        {
            get { return this.syntaxNode as AssignmentExpressionSyntax; }
        }
    }
}
