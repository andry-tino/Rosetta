/// <summary>
/// StatementASTWalker.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.Translation;

    /// <summary>
    /// Describes walkers in statements (statements that have blocks).
    /// TODO: Consider making it abstract
    /// </summary>
    public class StatementASTWalker : CSharpSyntaxWalker, IASTWalker
    {
        protected CSharpSyntaxNode node;
        
        protected StatementTranslationUnit statement;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatementASTWalker"/> class.
        /// </summary>
        protected StatementASTWalker(CSharpSyntaxNode node)
        {
            var statementSyntaxNode = node as StatementSyntax;
            if (statementSyntaxNode == null)
            {
                throw new ArgumentException(
                    string.Format("Specified node is not of type {0}",
                    typeof(StatementSyntax).Name));
            }

            this.node = node;
            this.statement = null;
        }

        /// <summary>
        /// Walk the whole tree starting from specified <see cref="CSharpSyntaxNode"/> and 
        /// build the translation unit tree necessary for generating TypeScript output.
        /// </summary>
        /// <returns>The root of the translation unit tree.</returns>
        public ITranslationUnit Walk()
        {
            // Visiting
            this.Visit(node);

            this.WalkCore();

            // Returning root
            return this.statement;
        }

        /// <summary>
        /// TODO: Consider making it abstract
        /// </summary>
        protected virtual void WalkCore()
        {
        }

        #region CSharpSyntaxWalker overrides

        public override void VisitIfStatement(IfStatementSyntax node)
        {
            base.VisitIfStatement(node);

            //if (node.Parent == this.node)
            //{
                this.VisitIfStatementCore(node);
                this.InvokeIfStatementVisited(this, new WalkerEventArgs());
            //}
        }

        protected virtual void VisitIfStatementCore(IfStatementSyntax node)
        {
        }

        #endregion

        #region Walk events

        /// <summary>
        /// 
        /// </summary>
        public event WalkerEvent IfStatementVisited;

        #endregion

        private void InvokeIfStatementVisited(object sender, WalkerEventArgs e)
        {
            if (this.IfStatementVisited != null)
            {
                this.IfStatementVisited(sender, e);
            }
        }
    }
}
