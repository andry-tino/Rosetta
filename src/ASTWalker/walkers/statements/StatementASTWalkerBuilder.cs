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

    /// <summary>
    /// Builds the appropriate statement AST walker.
    /// </summary>
    public sealed class StatementASTWalkerBuilder
    {
        private CSharpSyntaxNode node;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatementASTWalkerBuilder"/> class.
        /// </summary>
        public StatementASTWalkerBuilder(CSharpSyntaxNode node)
        {
            var statementSyntaxNode = node as StatementSyntax;
            if (statementSyntaxNode == null)
            {
                throw new ArgumentException(
                    string.Format("Specified node is not of type {0}",
                    typeof(StatementSyntax).Name));
            }

            this.node = node;
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
                case SyntaxKind.CheckedStatement:
                case SyntaxKind.ContinueStatement:
                case SyntaxKind.DoStatement:
                case SyntaxKind.EmptyStatement:
                case SyntaxKind.ExpressionStatement:
                case SyntaxKind.FixedStatement:
                case SyntaxKind.ForEachStatement:
                case SyntaxKind.ForStatement:
                case SyntaxKind.GlobalStatement:
                case SyntaxKind.GotoCaseStatement:
                case SyntaxKind.GotoDefaultStatement:
                case SyntaxKind.GotoStatement:
                    return null;

                case SyntaxKind.IfStatement:
                    return ConditionalStatementASTWalker.Create(this.node);

                case SyntaxKind.LabeledStatement:
                    return null;

                case SyntaxKind.LocalDeclarationStatement:
                    return LocalDeclarationStatementASTWalker.Create(this.node);

                case SyntaxKind.ReturnStatement:
                case SyntaxKind.SwitchStatement:
                case SyntaxKind.ThrowStatement:
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
    }
}
