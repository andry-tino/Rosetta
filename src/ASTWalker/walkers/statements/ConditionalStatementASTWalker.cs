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
        private int ifBlockCursor;

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

            this.ifBlockCursor = 0;
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

        #region StatementASTWalker overrides

        // TODO: Create BlockASTWalkers to handle statements in if bodies

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        protected override void VisitIfStatementCore(IfStatementSyntax node)
        {
            this.Statement.SetTestExpression(LiteralTranslationUnit<bool>.Create(true), this.ifBlockCursor);
            this.Statement.AddStatementInConditionalBlock(StetementsGroupTranslationUnit.Create(), this.ifBlockCursor);

            // Update the cursor
            this.ifBlockCursor++;
        }

        #endregion

        /// <summary>
        /// Gets the <see cref="ConditionalStatementTranslationUnit"/> associated to the AST walker.
        /// </summary>
        private ConditionalStatementTranslationUnit Statement
        {
            get { return this.statement as ConditionalStatementTranslationUnit; }
        }
    }
}
