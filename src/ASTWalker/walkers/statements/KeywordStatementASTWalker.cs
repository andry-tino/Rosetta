/// <summary>
/// KeywordStatementASTWalker.cs
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
    /// This is a walker for statements which consist in just a keyword.
    /// </summary>
    /// <remarks>
    /// This walker does not actually walks into more nodes. This is a dead-end walker.
    /// </remarks>
    public class KeywordStatementASTWalker : StatementASTWalker
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeywordStatementASTWalker"/> class.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="statement"></param>
        /// <param name="semanticModel">The semantic model.</param>
        protected KeywordStatementASTWalker(CSharpSyntaxNode node, KeywordStatementTranslationUnit keywordStatement, SemanticModel semanticModel)
            : base(node, semanticModel)
        {
            var breakSyntaxNode = node as BreakStatementSyntax;
            var continueSyntaxNode = node as ContinueStatementSyntax;

            if (breakSyntaxNode == null || continueSyntaxNode == null)
            {
                throw new ArgumentException(
                    string.Format("Specified node is not one of these types: {0}, {1}!",
                    typeof(BreakStatementSyntax).Name, 
                    typeof(ContinueStatementSyntax).Name));
            }

            if (keywordStatement == null)
            {
                throw new ArgumentNullException(nameof(keywordStatement));
            }

            // Node assigned in base, no need to assign it here
            this.statement = keywordStatement;
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="KeywordStatementASTWalker"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public KeywordStatementASTWalker(KeywordStatementASTWalker other)
            : base(other)
        {
        }

        /// <summary>
        /// Factory method for class <see cref="KeywordStatementASTWalker"/>.
        /// </summary>
        /// <param name="node"><see cref="CSharpSyntaxNode"/> Used to initialize the walker.</param>
        /// <param name="semanticModel">The semantic model.</param>
        /// <returns></returns>
        public static KeywordStatementASTWalker Create(CSharpSyntaxNode node, SemanticModel semanticModel = null)
        {
            // TODO: Use TranslationUnitFactory in order to have AST walkers decoupled from helpers 
            //       via factories (which will be using helpers)

            KeywordStatementTranslationUnit statement;

            if (node as BreakStatementSyntax != null)
            {
                statement = KeywordStatementTranslationUnit.Break;
            }
            else if (node as ContinueStatementSyntax != null)
            {
                statement = KeywordStatementTranslationUnit.Continue;
            }
            else
            {
                throw new InvalidOperationException("Unrecognized keyword based statement!");
            }

            return new KeywordStatementASTWalker(node, statement, semanticModel);
        }

        protected override bool ShouldWalkInto
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the <see cref="KeywordStatementTranslationUnit"/> associated to the AST walker.
        /// </summary>
        /// <remarks>
        /// Protected for testability.
        /// </remarks>
        protected KeywordStatementTranslationUnit Statement
        {
            get { return this.statement as KeywordStatementTranslationUnit; }
        }
    }
}
