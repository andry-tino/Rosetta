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
    /// Walks a method AST node.
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
        /// <param name="node"></param>
        /// <param name="methodDeclaration"></param>
        protected MethodASTWalker(CSharpSyntaxNode node, MethodDeclarationTranslationUnit methodDeclaration)
        {
            var methodDeclarationSyntaxNode = node as MethodDeclarationSyntax;
            if (methodDeclarationSyntaxNode == null)
            {
                throw new ArgumentException(
                    string.Format("Specified node is not of type {0}", 
                    typeof(MethodDeclarationSyntax).Name));
            }

            if (methodDeclaration == null)
            {
                throw new ArgumentNullException(nameof(methodDeclaration));
            }

            this.node = node;
            this.methodDeclaration = methodDeclaration;
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="MethodASTWalker"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public MethodASTWalker(MethodASTWalker other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            this.node = other.node;
            this.methodDeclaration = other.methodDeclaration;
        }

        /// <summary>
        /// Factory method for class <see cref="MethodASTWalker"/>.
        /// </summary>
        /// <param name="node"><see cref="CSharpSyntaxNode"/> Used to initialize the walker.</param>
        /// <returns></returns>
        public static MethodASTWalker Create(CSharpSyntaxNode node)
        {
            MethodDeclaration helper = new MethodDeclaration(node as MethodDeclarationSyntax);

            var methodDeclaration = MethodDeclarationTranslationUnit.Create(
                helper.Visibility,
                TypeIdentifierTranslationUnit.Create(helper.ReturnType),
                IdentifierTranslationUnit.Create(helper.Name));

            foreach (TypedIdentifier parameter in helper.Parameters)
            {
                methodDeclaration.AddArgument(ArgumentDefinitionTranslationUnit.Create(
                    TypeIdentifierTranslationUnit.Create(parameter.TypeName),
                    IdentifierTranslationUnit.Create(parameter.IdentifierName)));
            }

            return new MethodASTWalker(node, methodDeclaration);
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

        #endregion

        private void VisitStatement(StatementSyntax node)
        {
            IASTWalker walker = new StatementASTWalkerBuilder(node).Build();
            ITranslationUnit statementTranslationUnit = walker.Walk();

            this.methodDeclaration.AddStatement(statementTranslationUnit);

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
