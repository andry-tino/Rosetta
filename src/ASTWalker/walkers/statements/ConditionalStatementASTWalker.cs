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
        protected ConditionalStatementASTWalker(CSharpSyntaxNode node) : base(node)
        {
            var statementSyntaxNode = node as IfStatementSyntax;
            if (statementSyntaxNode == null)
            {
                throw new ArgumentException(
                    string.Format("Specified node is not of type {0}",
                    typeof(IfStatementSyntax).Name));
            }

            ConditionalStatement helper = new ConditionalStatement(statementSyntaxNode);

            this.statement = ConditionalStatementTranslationUnit.Create(helper.BlocksNumber, helper.HasElseBlock);
            this.VisitNode(statementSyntaxNode, 0);
        }

        /// <summary>
        /// Factory method for class <see cref="ConditionalStatementASTWalker"/>.
        /// </summary>
        /// <param name="node"><see cref="CSharpSyntaxNode"/> Used to initialize the walker.</param>
        /// <returns></returns>
        public static ConditionalStatementASTWalker Create(CSharpSyntaxNode node)
        {
            return new ConditionalStatementASTWalker(node);
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
            // Handling expression
            this.Statement.SetTestExpression(
                new ExpressionTranslationUnitBuilder(node.Condition).Build(), 
                index);

            // Handling body
            IASTWalker walker = (node.Statement as BlockSyntax != null) ? 
                BlockASTWalker.Create(node.Statement) : 
                new StatementASTWalkerBuilder(node.Statement).Build();
            ITranslationUnit translationUnit = walker.Walk();
            this.Statement.AddStatementInConditionalBlock(translationUnit, index);

            // TODO: Remember to call the event

            // To the next node
            if (node.Else != null && node.Else.Statement as IfStatementSyntax != null)
            {
                this.VisitNode(node.Else.Statement as IfStatementSyntax, ++index);
            }
        }

        /// <summary>
        /// Gets the <see cref="ConditionalStatementTranslationUnit"/> associated to the AST walker.
        /// </summary>
        private ConditionalStatementTranslationUnit Statement
        {
            get { return this.statement as ConditionalStatementTranslationUnit; }
        }
    }
}
