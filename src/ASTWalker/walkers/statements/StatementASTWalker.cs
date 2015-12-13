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
    using Rosetta.AST.Helpers;

    /// <summary>
    /// Describes walkers in statements (statements that have blocks).
    /// </summary>
    public class StatementASTWalker : CSharpSyntaxWalker, IASTWalker
    {
        // Protected for testability
        protected CSharpSyntaxNode node;

        // Protected for testability
        protected StatementTranslationUnit statement;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatementASTWalker"/> class.
        /// </summary>
        protected StatementASTWalker(CSharpSyntaxNode node)
        {
            var namespaceSyntaxNode = node as StatementSyntax;
            if (namespaceSyntaxNode == null)
            {
                throw new ArgumentException(
                    string.Format("Specified node is not of type {0}",
                    typeof(StatementSyntax).Name));
            }

            // TODO: use helper, generate proper translation unit
        }

        /// <summary>
        /// Factory method for class <see cref="StatementASTWalker"/>.
        /// </summary>
        /// <param name="node"><see cref="CSharpSyntaxNode"/> Used to initialize the walker.</param>
        /// <returns></returns>
        public static StatementASTWalker Create(CSharpSyntaxNode node)
        {
            return new StatementASTWalker(node);
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

            // Returning root
            return this.statement;
        }

        #region CSharpSyntaxWalker overrides

        public override void VisitIfStatement(IfStatementSyntax node)
        {
            base.VisitIfStatement(node);
            this.InvokeIfStatementVisited(this, new WalkerEventArgs());
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
