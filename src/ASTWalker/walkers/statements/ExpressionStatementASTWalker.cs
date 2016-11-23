/// <summary>
/// ExpressionStatementASTWalker.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.Translation;
    using Rosetta.AST.Helpers;

    /// <summary>
    /// This is a walker for expresison based statements.
    /// </summary>
    /// <remarks>
    /// This walker does not actually walks into more nodes. This is a dead-end walker.
    /// </remarks>
    public class ExpressionStatementASTWalker : StatementASTWalker
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionStatementASTWalker"/> class.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="statement"></param>
        /// <param name="semanticModel">The semantic model.</param>
        protected ExpressionStatementASTWalker(CSharpSyntaxNode node, ExpressionStatementTranslationUnit expressionStatement, SemanticModel semanticModel)
            : base(node, semanticModel)
        {
            var returnSyntaxNode = node as ReturnStatementSyntax;
            var throwSyntaxNode = node as ThrowStatementSyntax;
            var expressionSyntaxNode = node as ExpressionStatementSyntax;

            if (returnSyntaxNode == null && throwSyntaxNode == null && expressionSyntaxNode == null)
            {
                throw new ArgumentException(
                    string.Format("Specified node ({0}) is not one of these types: {1}, {2}, {3}!",
                    node.GetType().Name,
                    typeof(ReturnStatementSyntax).Name,
                    typeof(ThrowStatementSyntax).Name),
                    typeof(ExpressionStatementSyntax).Name);
            }

            if (expressionStatement == null)
            {
                throw new ArgumentNullException(nameof(expressionStatement));
            }

            // Node assigned in base, no need to assign it here
            this.statement = expressionStatement;
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="ExpressionStatementASTWalker"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public ExpressionStatementASTWalker(ExpressionStatementASTWalker other)
            : base(other)
        {
        }

        /// <summary>
        /// Factory method for class <see cref="ExpressionStatementASTWalker"/>.
        /// </summary>
        /// <param name="node"><see cref="CSharpSyntaxNode"/> Used to initialize the walker.</param>
        /// <param name="semanticModel">The semantic model.</param>
        /// <returns></returns>
        public static ExpressionStatementASTWalker Create(CSharpSyntaxNode node, SemanticModel semanticModel = null)
        {
            // TODO: Use TranslationUnitFactory in order to have AST walkers decoupled from helpers 
            //       via factories (which will be using helpers)

            ExpressionStatementTranslationUnit statement;

            // Return statement
            if (node as ReturnStatementSyntax != null)
            {
                var helper = new ReturnStatement(node as ReturnStatementSyntax, semanticModel);
                statement = CreateReturnStatement(helper, semanticModel);
            }
            // Throw statement
            else if (node as ThrowStatementSyntax != null)
            {
                var helper = new ThrowStatement(node as ThrowStatementSyntax, semanticModel);
                statement = CreateThrowStatement(helper, semanticModel);
            }
            // Other
            else if (node as ExpressionStatementSyntax != null)
            {
                var helper = new ExpressionStatement(node as ExpressionStatementSyntax, semanticModel);
                var expression = new ExpressionTranslationUnitBuilder(helper.Expression, semanticModel).Build();
                statement = ExpressionStatementTranslationUnit.Create(expression as ExpressionTranslationUnit);
            }
            else
            {
                throw new InvalidOperationException("Unrecognized statement!");
            }

            return new ExpressionStatementASTWalker(node, statement, semanticModel);
        }

        private static ExpressionStatementTranslationUnit CreateReturnStatement(ReturnStatement helper, SemanticModel semanticModel)
        {
            if (helper.Expression == null)
            {
                // Void return
                return ExpressionStatementTranslationUnit.CreateReturn();
            }

            var expression = new ExpressionTranslationUnitBuilder(helper.Expression, semanticModel).Build();
            return ExpressionStatementTranslationUnit.CreateReturn(expression as ExpressionTranslationUnit);
        }

        private static ExpressionStatementTranslationUnit CreateThrowStatement(ThrowStatement helper, SemanticModel semanticModel)
        {
            if (helper.Expression == null)
            {
                // Void return
                return ExpressionStatementTranslationUnit.CreateThrow();
            }

            var expression = new ExpressionTranslationUnitBuilder(helper.Expression, semanticModel).Build();
            return ExpressionStatementTranslationUnit.CreateThrow(expression as ExpressionTranslationUnit);
        }

        protected override bool ShouldWalkInto
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the <see cref="ExpressionStatementTranslationUnit"/> associated to the AST walker.
        /// </summary>
        /// <remarks>
        /// Protected for testability.
        /// </remarks>
        protected ExpressionStatementTranslationUnit Statement
        {
            get { return this.statement as ExpressionStatementTranslationUnit; }
        }
    }
}
