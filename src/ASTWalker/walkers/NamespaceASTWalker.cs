/// <summary>
/// NamespaceASTWalker.cs
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
    /// Walks a class AST node.
    /// </summary>
    public class NamespaceASTWalker : CSharpSyntaxWalker, IASTWalker
    {
        // Protected for testability
        protected CSharpSyntaxNode node;

        // Protected for testability
        protected ModuleTranslationUnit module;

        /// <summary>
        /// Initializes a new instance of the <see cref="NamespaceASTWalker"/> class.
        /// </summary>
        protected NamespaceASTWalker(CSharpSyntaxNode node)
        {
            var namespaceSyntaxNode = node as NamespaceDeclarationSyntax;
            if (namespaceSyntaxNode == null)
            {
                throw new ArgumentException(
                    string.Format("Specified node is not of type {0}",
                    typeof(NamespaceDeclarationSyntax).Name));
            }

            this.node = node;
            NamespaceDeclaration namespaceHelper = new NamespaceDeclaration(namespaceSyntaxNode);

            this.module = ModuleTranslationUnit.Create(
                IdentifierTranslationUnit.Create(namespaceHelper.Name));
        }

        /// <summary>
        /// Factory method for class <see cref="NamespaceASTWalker"/>.
        /// </summary>
        /// <param name="node"><see cref="CSharpSyntaxNode"/> Used to initialize the walker.</param>
        /// <returns></returns>
        public static NamespaceASTWalker Create(CSharpSyntaxNode node)
        {
            return new NamespaceASTWalker(node);
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
            return this.module;
        }

        #region CSharpSyntaxWalker overrides

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            base.VisitClassDeclaration(node);

            var classWalker = ClassASTWalker.Create(node);
            var translationUnit = classWalker.Walk();
            this.module.AddClass(translationUnit);

            this.InvokeClassDeclarationVisited(this, new WalkerEventArgs());
        }

        public override void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        {
            base.VisitInterfaceDeclaration(node);
            this.InvokeInterfaceDeclarationVisited(this, new WalkerEventArgs());
        }

        #endregion

        #region Walk events

        /// <summary>
        /// 
        /// </summary>
        public event WalkerEvent ClassDeclarationVisited;

        /// <summary>
        /// 
        /// </summary>
        public event WalkerEvent InterfaceDeclarationVisited;

        #endregion

        private void InvokeClassDeclarationVisited(object sender, WalkerEventArgs e)
        {
            if (this.ClassDeclarationVisited != null)
            {
                this.ClassDeclarationVisited(sender, e);
            }
        }

        private void InvokeInterfaceDeclarationVisited(object sender, WalkerEventArgs e)
        {
            if (this.InterfaceDeclarationVisited != null)
            {
                this.InterfaceDeclarationVisited(sender, e);
            }
        }
    }
}
