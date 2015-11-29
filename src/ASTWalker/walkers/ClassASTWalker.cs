/// <summary>
/// ClassASTWalker.cs
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
    public class ClassASTWalker : CSharpSyntaxWalker, IASTWalker
    {
        // Protected for testability
        protected CSharpSyntaxNode node;

        // Protected for testability
        protected ClassDeclarationTranslationUnit classDeclaration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassASTWalker"/> class.
        /// </summary>
        protected ClassASTWalker(CSharpSyntaxNode node)
        {
            var classDeclarationSyntaxNode = node as ClassDeclarationSyntax;
            if (classDeclarationSyntaxNode == null)
            {
                throw new ArgumentException(
                    string.Format("Specified node is not of type {0}",
                    typeof(ClassDeclarationSyntax).Name));
            }

            this.node = node;
            ClassDeclaration classHelper = new ClassDeclaration(classDeclarationSyntaxNode);

            this.classDeclaration = ClassDeclarationTranslationUnit.Create(
                classHelper.Visibility,
                IdentifierTranslationUnit.Create(classHelper.Name),
                classHelper.BaseClass == null ? null : IdentifierTranslationUnit.Create(classHelper.BaseClass.Name));

            foreach (BaseTypeReference implementedInterface in classHelper.ImplementedInterfaces)
            {
                this.classDeclaration.AddImplementedInterface(IdentifierTranslationUnit.Create(implementedInterface.Name));
            }
        }

        /// <summary>
        /// Factory method for class <see cref="ClassASTWalker"/>.
        /// </summary>
        /// <param name="node"><see cref="CSharpSyntaxNode"/> Used to initialize the walker.</param>
        /// <returns></returns>
        public static ClassASTWalker Create(CSharpSyntaxNode node)
        {
            return new ClassASTWalker(node);
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
            return this.classDeclaration;
        }

        #region CSharpSyntaxWalker overrides

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitFieldDeclaration(FieldDeclarationSyntax node)
        {
            base.VisitFieldDeclaration(node);

            var fieldDeclaration = new FieldDeclaration(node);
            var fieldDeclarationTranslationUnit = FieldDeclarationTranslationUnit.Create(fieldDeclaration.Visibility, 
                IdentifierTranslationUnit.Create(fieldDeclaration.Type), IdentifierTranslationUnit.Create(fieldDeclaration.Name));
            this.classDeclaration.AddMemberDeclaration(fieldDeclarationTranslationUnit);

            this.InvokeFieldDeclarationVisited(this, new WalkerEventArgs());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            base.VisitPropertyDeclaration(node);
            this.InvokePropertyDeclarationVisited(this, new WalkerEventArgs());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitVariableDeclaration(VariableDeclarationSyntax node)
        {
            base.VisitVariableDeclaration(node);
            this.InvokeVariableDeclarationVisited(this, new WalkerEventArgs());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            base.VisitMethodDeclaration(node);

            var methodWalker = MethodASTWalker.Create(node);
            var translationUnit = methodWalker.Walk();
            this.classDeclaration.AddMethodDeclaration(translationUnit);

            this.InvokeMethodDeclarationVisited(this, new WalkerEventArgs());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
        {
            base.VisitConstructorDeclaration(node);
            this.InvokeConstructorDeclarationVisited(this, new WalkerEventArgs());
        }

        #endregion

        #region Walk events

        /// <summary>
        /// 
        /// </summary>
        public event WalkerEvent FieldDeclarationVisited;

        /// <summary>
        /// 
        /// </summary>
        public event WalkerEvent PropertyDeclarationVisited;

        /// <summary>
        /// 
        /// </summary>
        public event WalkerEvent VariableDeclarationVisited;

        /// <summary>
        /// 
        /// </summary>
        public event WalkerEvent MethodDeclarationVisited;

        /// <summary>
        /// 
        /// </summary>
        public event WalkerEvent ConstructorDeclarationVisited;

        #endregion

        private void InvokeFieldDeclarationVisited(object sender, WalkerEventArgs e)
        {
            if (this.FieldDeclarationVisited != null)
            {
                this.FieldDeclarationVisited(sender, e);
            }
        }

        private void InvokePropertyDeclarationVisited(object sender, WalkerEventArgs e)
        {
            if (this.PropertyDeclarationVisited != null)
            {
                this.PropertyDeclarationVisited(sender, e);
            }
        }

        private void InvokeVariableDeclarationVisited(object sender, WalkerEventArgs e)
        {
            if (this.VariableDeclarationVisited != null)
            {
                this.VariableDeclarationVisited(sender, e);
            }
        }

        private void InvokeMethodDeclarationVisited(object sender, WalkerEventArgs e)
        {
            if (this.MethodDeclarationVisited != null)
            {
                this.MethodDeclarationVisited(sender, e);
            }
        }

        private void InvokeConstructorDeclarationVisited(object sender, WalkerEventArgs e)
        {
            if (this.ConstructorDeclarationVisited != null)
            {
                this.ConstructorDeclarationVisited(sender, e);
            }
        }
    }
}
