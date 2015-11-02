/// <summary>
/// MethodASTWalker.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Symbols;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.CodeAnalysis.Text;

    using Rosetta.Translation;
    using Rosetta.AST.Helpers;

    /// <summary>
    /// Walks a class AST node.
    /// </summary>
    public class MethodASTWalker : CSharpSyntaxWalker, IASTWalker
    {
        // Protected for testability
        protected CSharpSyntaxNode node;

        // Protected for testability
        protected MethodDeclarationTranslationUnit methodDeclaration;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodASTWalker"/> class.
        /// </summary>
        protected MethodASTWalker(CSharpSyntaxNode node)
        {
            var methodDeclarationSyntaxNode = node as MethodDeclarationSyntax;
            if (methodDeclarationSyntaxNode == null)
            {
                throw new ArgumentException(
                    string.Format("Specified node is not of type {0}", 
                    typeof(MethodDeclarationSyntax).Name));
            }

            this.node = node;
            MethodDeclaration methodHelper = new MethodDeclaration(methodDeclarationSyntaxNode);

            this.methodDeclaration = MethodDeclarationTranslationUnit.Create(
                methodHelper.Visibility,
                IdentifierTranslationUnit.Create(methodHelper.ReturnType),
                IdentifierTranslationUnit.Create(methodHelper.Name));

            foreach (TypedIdentifier parameter in methodHelper.Parameters)
            {
                this.methodDeclaration.AddArgument(ArgumentDefinitionTranslationUnit.Create(
                    IdentifierTranslationUnit.Create(parameter.TypeName), 
                    IdentifierTranslationUnit.Create(parameter.IdentifierName)));
            }
        }

        /// <summary>
        /// Factory method for class <see cref="MethodASTWalker"/>.
        /// </summary>
        /// <param name="node"><see cref="CSharpSyntaxNode"/> Used to initialize the walker.</param>
        /// <returns></returns>
        public static MethodASTWalker Create(CSharpSyntaxNode node)
        {
            return new MethodASTWalker(node);
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
            return this.methodDeclaration;
        }

        #region CSharpSyntaxWalker overrides

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitEmptyStatement(EmptyStatementSyntax node)
        {
            base.VisitEmptyStatement(node);
            this.VisitStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitBreakStatement(BreakStatementSyntax node)
        {
            base.VisitBreakStatement(node);
            this.VisitStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitCheckedStatement(CheckedStatementSyntax node)
        {
            base.VisitCheckedStatement(node);
            this.VisitStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitContinueStatement(ContinueStatementSyntax node)
        {
            base.VisitContinueStatement(node);
            this.VisitStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitDoStatement(DoStatementSyntax node)
        {
            base.VisitDoStatement(node);
            this.VisitStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitExpressionStatement(ExpressionStatementSyntax node)
        {
            base.VisitExpressionStatement(node);
            this.VisitStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitFixedStatement(FixedStatementSyntax node)
        {
            base.VisitFixedStatement(node);
            this.VisitStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitForEachStatement(ForEachStatementSyntax node)
        {
            base.VisitForEachStatement(node);
            this.VisitStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitForStatement(ForStatementSyntax node)
        {
            base.VisitForStatement(node);
            this.VisitStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitGotoStatement(GotoStatementSyntax node)
        {
            base.VisitGotoStatement(node);
            this.VisitStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitIfStatement(IfStatementSyntax node)
        {
            base.VisitIfStatement(node);
            this.VisitStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitLabeledStatement(LabeledStatementSyntax node)
        {
            base.VisitLabeledStatement(node);
            this.VisitStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
        {
            base.VisitLocalDeclarationStatement(node);
            this.VisitStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitLockStatement(LockStatementSyntax node)
        {
            base.VisitLockStatement(node);
            this.VisitStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitReturnStatement(ReturnStatementSyntax node)
        {
            base.VisitReturnStatement(node);
            this.VisitStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitSwitchStatement(SwitchStatementSyntax node)
        {
            base.VisitSwitchStatement(node);
            this.VisitStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitThrowStatement(ThrowStatementSyntax node)
        {
            base.VisitThrowStatement(node);
            this.VisitStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitTryStatement(TryStatementSyntax node)
        {
            base.VisitTryStatement(node);
            this.VisitStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitUnsafeStatement(UnsafeStatementSyntax node)
        {
            throw new NotSupportedException("We do not support translation of unsafe statements!");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitUsingStatement(UsingStatementSyntax node)
        {
            base.VisitUsingStatement(node);
            this.VisitStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitWhileStatement(WhileStatementSyntax node)
        {
            base.VisitWhileStatement(node);
            this.VisitStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitYieldStatement(YieldStatementSyntax node)
        {
            throw new NotSupportedException("We do not support translation of yield statements!");
        }

        #endregion

        private void VisitStatement(StatementSyntax node)
        {
            this.InvokeStatementVisited(this, new WalkerEventArgs());
        }

        #region Walk events

        /// <summary>
        /// 
        /// </summary>
        public event WalkerEvent StatementVisited;

        #endregion

        private void InvokeStatementVisited(object sender, WalkerEventArgs e)
        {
            if (this.StatementVisited != null)
            {
                this.StatementVisited(sender, e);
            }
        }
    }
}
