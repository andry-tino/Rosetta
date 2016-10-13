/// <summary>
/// ConstructorASTWalker.cs
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
    using Rosetta.AST.Factories;

    /// <summary>
    /// Walks a constructor AST node.
    /// 
    /// TODO: Unify logic with method AST walker.
    /// </summary>
    public class ConstructorASTWalker : CSharpSyntaxWalker, IASTWalker
    {
        // Protected for testability
        protected CSharpSyntaxNode node;

        // Protected for testability
        protected ConstructorDeclarationTranslationUnit constructorDeclaration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructorASTWalker"/> class.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="methodDeclaration"></param>
        protected ConstructorASTWalker(CSharpSyntaxNode node, ConstructorDeclarationTranslationUnit constructorDeclaration)
        {
            var constructorDeclarationSyntaxNode = node as ConstructorDeclarationSyntax;
            if (constructorDeclarationSyntaxNode == null)
            {
                throw new ArgumentException(
                    string.Format("Specified node is not of type {0}",
                    typeof(ConstructorDeclarationSyntax).Name));
            }

            if (constructorDeclaration == null)
            {
                throw new ArgumentNullException(nameof(constructorDeclaration));
            }

            this.node = node;
            this.constructorDeclaration = constructorDeclaration;
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="ConstructorASTWalker"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public ConstructorASTWalker(ConstructorASTWalker other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            this.node = other.node;
            this.constructorDeclaration = other.constructorDeclaration;
        }

        /// <summary>
        /// Factory method for class <see cref="ConstructorASTWalker"/>.
        /// </summary>
        /// <param name="node"><see cref="CSharpSyntaxNode"/> Used to initialize the walker.</param>
        /// <returns></returns>
        public static ConstructorASTWalker Create(CSharpSyntaxNode node)
        {
            return new ConstructorASTWalker(node, 
                new ConstructorDeclarationTranslationUnitFactory(node).Create() as ConstructorDeclarationTranslationUnit);
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
            return this.constructorDeclaration;
        }

        // TODO: Better design, create ASTWalkerBase which inherits from CSharpSyntaxWalker.
        // Make all ASTWalker(s) inherit from it and provide virtual methods for statements in order to provide only one
        // method for statement visit.

        #region CSharpSyntaxWalker overrides

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <remarks>
        /// Statements will cause an AST walker to be created, thus we don't need to go further deeper in the
        /// tree by visiting the node.
        /// </remarks>
        public override void VisitEmptyStatement(EmptyStatementSyntax node)
        {
            this.VisitStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <remarks>
        /// Statements will cause an AST walker to be created, thus we don't need to go further deeper in the
        /// tree by visiting the node.
        /// </remarks>
        public override void VisitBreakStatement(BreakStatementSyntax node)
        {
            this.VisitStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <remarks>
        /// Statements will cause an AST walker to be created, thus we don't need to go further deeper in the
        /// tree by visiting the node.
        /// </remarks>
        public override void VisitCheckedStatement(CheckedStatementSyntax node)
        {
            this.VisitStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <remarks>
        /// Statements will cause an AST walker to be created, thus we don't need to go further deeper in the
        /// tree by visiting the node.
        /// </remarks>
        public override void VisitContinueStatement(ContinueStatementSyntax node)
        {
            this.VisitStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <remarks>
        /// Statements will cause an AST walker to be created, thus we don't need to go further deeper in the
        /// tree by visiting the node.
        /// </remarks>
        public override void VisitDoStatement(DoStatementSyntax node)
        {
            this.VisitStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <remarks>
        /// Statements will cause an AST walker to be created, thus we don't need to go further deeper in the
        /// tree by visiting the node.
        /// </remarks>
        public override void VisitFixedStatement(FixedStatementSyntax node)
        {
            this.VisitStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <remarks>
        /// Statements will cause an AST walker to be created, thus we don't need to go further deeper in the
        /// tree by visiting the node.
        /// </remarks>
        public override void VisitForEachStatement(ForEachStatementSyntax node)
        {
            this.VisitStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <remarks>
        /// Statements will cause an AST walker to be created, thus we don't need to go further deeper in the
        /// tree by visiting the node.
        /// </remarks>
        public override void VisitForStatement(ForStatementSyntax node)
        {
            this.VisitStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <remarks>
        /// Statements will cause an AST walker to be created, thus we don't need to go further deeper in the
        /// tree by visiting the node.
        /// </remarks>
        public override void VisitGotoStatement(GotoStatementSyntax node)
        {
            this.VisitStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <remarks>
        /// Statements will cause an AST walker to be created, thus we don't need to go further deeper in the
        /// tree by visiting the node.
        /// </remarks>
        public override void VisitIfStatement(IfStatementSyntax node)
        {
            this.VisitStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <remarks>
        /// Statements will cause an AST walker to be created, thus we don't need to go further deeper in the
        /// tree by visiting the node.
        /// </remarks>
        public override void VisitLabeledStatement(LabeledStatementSyntax node)
        {
            this.VisitStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <remarks>
        /// Statements will cause an AST walker to be created, thus we don't need to go further deeper in the
        /// tree by visiting the node.
        /// </remarks>
        public override void VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
        {
            this.VisitStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <remarks>
        /// Statements will cause an AST walker to be created, thus we don't need to go further deeper in the
        /// tree by visiting the node.
        /// </remarks>
        public override void VisitLockStatement(LockStatementSyntax node)
        {
            this.VisitStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <remarks>
        /// Statements will cause an AST walker to be created, thus we don't need to go further deeper in the
        /// tree by visiting the node.
        /// </remarks>
        public override void VisitReturnStatement(ReturnStatementSyntax node)
        {
            this.VisitStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <remarks>
        /// Statements will cause an AST walker to be created, thus we don't need to go further deeper in the
        /// tree by visiting the node.
        /// </remarks>
        public override void VisitSwitchStatement(SwitchStatementSyntax node)
        {
            this.VisitStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <remarks>
        /// Statements will cause an AST walker to be created, thus we don't need to go further deeper in the
        /// tree by visiting the node.
        /// </remarks>
        public override void VisitThrowStatement(ThrowStatementSyntax node)
        {
            this.VisitStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <remarks>
        /// Statements will cause an AST walker to be created, thus we don't need to go further deeper in the
        /// tree by visiting the node.
        /// </remarks>
        public override void VisitTryStatement(TryStatementSyntax node)
        {
            this.VisitStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <remarks>
        /// Statements will cause an AST walker to be created, thus we don't need to go further deeper in the
        /// tree by visiting the node.
        /// </remarks>
        public override void VisitUnsafeStatement(UnsafeStatementSyntax node)
        {
            this.VisitStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <remarks>
        /// Statements will cause an AST walker to be created, thus we don't need to go further deeper in the
        /// tree by visiting the node.
        /// </remarks>
        public override void VisitUsingStatement(UsingStatementSyntax node)
        {
            this.VisitStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <remarks>
        /// Statements will cause an AST walker to be created, thus we don't need to go further deeper in the
        /// tree by visiting the node.
        /// </remarks>
        public override void VisitWhileStatement(WhileStatementSyntax node)
        {
            this.VisitStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <remarks>
        /// Statements will cause an AST walker to be created, thus we don't need to go further deeper in the
        /// tree by visiting the node.
        /// </remarks>
        public override void VisitYieldStatement(YieldStatementSyntax node)
        {
            this.VisitStatement(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <remarks>
        /// Statements will cause an AST walker to be created, thus we don't need to go further deeper in the
        /// tree by visiting the node.
        /// </remarks>
        public override void VisitExpressionStatement(ExpressionStatementSyntax node)
        {
            this.VisitStatement(node);
        }

        #endregion

        private void VisitStatement(StatementSyntax node)
        {
            IASTWalker walker = new StatementASTWalkerBuilder(node).Build();
            ITranslationUnit statementTranslationUnit = walker.Walk();

            this.constructorDeclaration.AddStatement(statementTranslationUnit);

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
