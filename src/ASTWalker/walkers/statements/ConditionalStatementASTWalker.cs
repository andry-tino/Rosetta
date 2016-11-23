/// <summary>
/// ConditionalStatementASTWalker.cs
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
    /// Describes walkers in conditional statements.
    /// TODO: Make internal
    /// TODO: This is a walker but maybe it is not necessary to use walking funcitonalities.
    /// </summary>
    public class ConditionalStatementASTWalker : StatementASTWalker
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionalStatementASTWalker"/> class.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="conditionalStatement"></param>
        /// <param name="semanticModel">The semantic model.</param>
        protected ConditionalStatementASTWalker(CSharpSyntaxNode node, ConditionalStatementTranslationUnit conditionalStatement, SemanticModel semanticModel) 
            : base(node, semanticModel)
        {
            var statementSyntaxNode = node as IfStatementSyntax;
            if (statementSyntaxNode == null)
            {
                throw new ArgumentException(
                    string.Format("Specified node is not of type {0}",
                    typeof(IfStatementSyntax).Name));
            }

            if (conditionalStatement == null)
            {
                throw new ArgumentNullException(nameof(conditionalStatement));
            }

            // Node assigned in base, no need to assign it here
            this.statement = conditionalStatement;

            // Going through parts in the statement and filling the translation unit with initial data
            this.VisitNode(statementSyntaxNode, 0);
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="ConditionalStatementASTWalker"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public ConditionalStatementASTWalker(ConditionalStatementASTWalker other)
            : base(other)
        {
        }

        /// <summary>
        /// Factory method for class <see cref="ConditionalStatementASTWalker"/>.
        /// </summary>
        /// <param name="node"><see cref="CSharpSyntaxNode"/> Used to initialize the walker.</param>
        /// <param name="semanticModel">The semantic model.</param>
        /// <returns></returns>
        public static ConditionalStatementASTWalker Create(CSharpSyntaxNode node, SemanticModel semanticModel = null)
        {
            // TODO: Use TranslationUnitFactory in order to have AST walkers decoupled from helpers 
            //       via factories (which will be using helpers)

            ConditionalStatement helper = new ConditionalStatement(node as IfStatementSyntax);

            var statement = ConditionalStatementTranslationUnit.Create(helper.BlocksNumber, helper.HasElseBlock);

            return new ConditionalStatementASTWalker(node, statement, semanticModel);
        }

        /// <summary>
        /// TODO: Remove
        /// </summary>
        protected override bool ShouldWalkInto
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// In charge of executing a fixed visit of this node.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="index"></param>
        private void VisitNode(IfStatementSyntax node, int index)
        {
            // Handling conditional expression
            this.Statement.SetTestExpression(
                new ExpressionTranslationUnitBuilder(node.Condition, this.semanticModel).Build(), 
                index);

            // Handling body
            IASTWalker walker = (node.Statement as BlockSyntax != null) ? 
                BlockASTWalker.Create(node.Statement) : 
                new StatementASTWalkerBuilder(node.Statement).Build();
            ITranslationUnit translationUnit = walker.Walk();
            this.Statement.SetStatementInConditionalBlock(translationUnit, index);

            // TODO: Remember to call the event for node traversal

            if (node.Else != null && node.Else.Statement != null)
            {
                if (node.Else.Statement as IfStatementSyntax != null)
                {
                    // To the next node
                    this.VisitNode(node.Else.Statement as IfStatementSyntax, ++index);
                }
                else
                {
                    walker = (node.Else.Statement as BlockSyntax != null) ?
                        BlockASTWalker.Create(node.Else.Statement) :
                        new StatementASTWalkerBuilder(node.Else.Statement).Build();
                    translationUnit = walker.Walk();
                    this.Statement.SetStatementInElseBlock(translationUnit);
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="ConditionalStatementTranslationUnit"/> associated to the AST walker.
        /// </summary>
        protected ConditionalStatementTranslationUnit Statement
        {
            get { return this.statement as ConditionalStatementTranslationUnit; }
        }
    }
}
