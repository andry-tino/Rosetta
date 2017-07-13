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

    using Rosetta.AST.Diagnostics.Logging;
    using Rosetta.Translation;
    using Rosetta.AST.Factories;

    /// <summary>
    /// Walks a class AST node.
    /// // TODO: Override class definition in order to create an inner class and remove the node!
    /// </summary>
    public class ClassASTWalker : ASTWalker, IASTWalker
    {
        // Protected for testability
        protected ClassDeclarationTranslationUnit classDeclaration;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ClassASTWalker"/> class.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="classDeclaration"></param>
        /// <param name="semanticModel">The semantic model.</param>
        protected ClassASTWalker(CSharpSyntaxNode node, ClassDeclarationTranslationUnit classDeclaration, SemanticModel semanticModel) 
            : base(node, semanticModel)
        {
            var classDeclarationSyntaxNode = node as ClassDeclarationSyntax;
            if (classDeclarationSyntaxNode == null)
            {
                throw new ArgumentException(
                    string.Format("Specified node is not of type {0}",
                    typeof(ClassDeclarationSyntax).Name));
            }

            if (classDeclaration == null)
            {
                throw new ArgumentNullException(nameof(classDeclaration));
            }
            
            this.classDeclaration = classDeclaration;
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="ClassASTWalker"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public ClassASTWalker(ClassASTWalker other) 
            : base(other)
        {
            this.classDeclaration = other.classDeclaration;
        }

        /// <summary>
        /// Factory method for class <see cref="ClassASTWalker"/>.
        /// </summary>
        /// <param name="node"><see cref="CSharpSyntaxNode"/> Used to initialize the walker.</param>
        /// <param name="context">The walking context.</param>
        /// <param name="semanticModel">The semantic model.</param>
        /// <returns></returns>
        public static ClassASTWalker Create(CSharpSyntaxNode node, ASTWalkerContext context = null, SemanticModel semanticModel = null)
        {
            return new ClassASTWalker(
                node,
                new ClassDeclarationTranslationUnitFactory(node, semanticModel).Create() as ClassDeclarationTranslationUnit, 
                semanticModel)
            {
                Context = context
            };
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
            var fieldDeclarationTranslationUnit = new FieldDeclarationTranslationUnitFactory(node, this.semanticModel).Create();
            this.classDeclaration.AddMemberDeclaration(fieldDeclarationTranslationUnit);
            
            this.LogVisitFieldDeclaration(node); // Logging

            this.InvokeFieldDeclarationVisited(this, new WalkerEventArgs());

            base.VisitFieldDeclaration(node); // TODO: Remove, should not be needed
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            var propertyWalker = PropertyASTWalker.Create(node, this.CreateWalkingContext(), this.semanticModel);
            var translationUnit = propertyWalker.Walk();
            this.classDeclaration.AddPropertyDeclaration(translationUnit);

            this.LogVisitPropertyDeclaration(node); // Logging

            this.InvokePropertyDeclarationVisited(this, new WalkerEventArgs());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <remarks>
        /// This will cause an AST walker to be created, thus we don't need to go further deeper in the
        /// tree by visiting the node.
        /// </remarks>
        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            var methodWalker = MethodASTWalker.Create(node, this.CreateWalkingContext(), this.semanticModel);
            methodWalker.Logger = this.Logger;

            var translationUnit = methodWalker.Walk();
            this.classDeclaration.AddMethodDeclaration(translationUnit);

            this.LogVisitMethodDeclaration(node); // Logging

            this.InvokeMethodDeclarationVisited(this, new WalkerEventArgs());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
        {
            var constructorWalker = ConstructorASTWalker.Create(node, this.CreateWalkingContext(), this.semanticModel);
            constructorWalker.Logger = this.Logger;

            var translationUnit = constructorWalker.Walk();
            this.classDeclaration.AddConstructorDeclaration(translationUnit);

            this.LogVisitConstructorDeclaration(node); // Logging

            this.InvokeConstructorDeclarationVisited(this, new WalkerEventArgs());
        }

        #endregion

        #region Logging

        protected void LogVisitFieldDeclaration(FieldDeclarationSyntax node)
        {
            if (this.IsLoggingEnabled)
            {
                new FieldDeclarationSyntaxNodeLogger(this.node as ClassDeclarationSyntax, node, this.Logger).LogVisit("Class ASTWalker");
            }
        }

        protected void LogVisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            if (this.IsLoggingEnabled)
            {
                new PropertyDeclarationSyntaxNodeLogger(this.node as ClassDeclarationSyntax, node, this.Logger).LogVisit("Class ASTWalker");
            }
        }

        protected void LogVisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            if (this.IsLoggingEnabled)
            {
                new MethodDeclarationSyntaxNodeLogger(this.node as ClassDeclarationSyntax, node, this.Logger).LogVisit("Class ASTWalker");
            }
        }

        protected void LogVisitConstructorDeclaration(ConstructorDeclarationSyntax node)
        {
            if (this.IsLoggingEnabled)
            {
                new ConstructorDeclarationSyntaxNodeLogger(this.node as ClassDeclarationSyntax, node, this.Logger).LogVisit("Class ASTWalker");
            }
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
        public event WalkerEvent MethodDeclarationVisited;

        /// <summary>
        /// 
        /// </summary>
        public event WalkerEvent ConstructorDeclarationVisited;

        #endregion

        protected void InvokeFieldDeclarationVisited(object sender, WalkerEventArgs e)
        {
            if (this.FieldDeclarationVisited != null)
            {
                this.FieldDeclarationVisited(sender, e);
            }
        }

        protected void InvokePropertyDeclarationVisited(object sender, WalkerEventArgs e)
        {
            if (this.PropertyDeclarationVisited != null)
            {
                this.PropertyDeclarationVisited(sender, e);
            }
        }

        protected void InvokeMethodDeclarationVisited(object sender, WalkerEventArgs e)
        {
            if (this.MethodDeclarationVisited != null)
            {
                this.MethodDeclarationVisited(sender, e);
            }
        }

        protected void InvokeConstructorDeclarationVisited(object sender, WalkerEventArgs e)
        {
            if (this.ConstructorDeclarationVisited != null)
            {
                this.ConstructorDeclarationVisited(sender, e);
            }
        }

        private ASTWalkerContext CreateWalkingContext()
        {
            return new ASTWalkerContext()
            {
                Originator = this
            };
        }
    }
}
