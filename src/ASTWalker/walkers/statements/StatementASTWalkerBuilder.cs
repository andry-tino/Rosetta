/// <summary>
/// StatementASTWalkerBuilder.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.AST.Helpers;

    /// <summary>
    /// Builds the appropriate statement AST walker.
    /// </summary>
    public sealed class StatementASTWalkerBuilder
    {
        private readonly CSharpSyntaxNode node;
        private readonly SemanticModel semanticModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatementASTWalkerBuilder"/> class.
        /// </summary>
        public StatementASTWalkerBuilder(CSharpSyntaxNode node, SemanticModel semanticModel = null)
        {
            var statementSyntaxNode = node as StatementSyntax;
            if (statementSyntaxNode == null)
            {
                throw new ArgumentException(
                    string.Format("Specified node is not of type {0}",
                    typeof(StatementSyntax).Name));
            }

            this.node = node;
            this.semanticModel = semanticModel;
        }

        /// <summary>
        /// Builds the proper <see cref="StatementASTWalker"/> given the provided <see cref="StatementSyntax"/>.
        /// </summary>
        /// <returns></returns>
        public IASTWalker Build()
        {
            switch (this.node.Kind())
            {
                case SyntaxKind.BreakStatement:
                case SyntaxKind.ContinueStatement:
                    return KeywordStatementASTWalker.Create(this.node, this.semanticModel);

                case SyntaxKind.CheckedStatement:
                case SyntaxKind.DoStatement:
                case SyntaxKind.EmptyStatement:
                    return null;

                case SyntaxKind.FixedStatement:
                case SyntaxKind.ForEachStatement:
                case SyntaxKind.ForStatement:
                case SyntaxKind.GlobalStatement:
                case SyntaxKind.GotoCaseStatement:
                case SyntaxKind.GotoDefaultStatement:
                case SyntaxKind.GotoStatement:
                    return null;

                case SyntaxKind.IfStatement:
                    return ConditionalStatementASTWalker.Create(this.node, this.semanticModel);

                case SyntaxKind.LabeledStatement:
                    return null;

                case SyntaxKind.LocalDeclarationStatement:
                    return LocalDeclarationStatementASTWalker.Create(this.node, this.semanticModel);

                case SyntaxKind.ExpressionStatement:
                case SyntaxKind.ReturnStatement:
                case SyntaxKind.ThrowStatement:
                    return ExpressionStatementASTWalker.Create(this.node, this.semanticModel);

                case SyntaxKind.SwitchStatement:
                case SyntaxKind.TryStatement:
                case SyntaxKind.UncheckedStatement:
                case SyntaxKind.UsingStatement:
                case SyntaxKind.WhileStatement:
                    return null;

                // Unsupported stuff
                case SyntaxKind.UnsafeStatement:
                case SyntaxKind.LockStatement:
                case SyntaxKind.YieldBreakStatement:
                case SyntaxKind.YieldReturnStatement:
                    throw new UnsupportedSyntaxException(this.node);
            }

            throw new InvalidOperationException("Building path reached an invalid non decidible state!");
        }

        private IASTWalker BuildExpressionStatementTranslationUnit(CSharpSyntaxNode node)
        {
            var expressionStatementNode = node as ExpressionStatementSyntax;
            var expressionTranslationUnit = new ExpressionTranslationUnitBuilder(expressionStatementNode.Expression, this.semanticModel).Build();

            return null;
        }
    }
}
