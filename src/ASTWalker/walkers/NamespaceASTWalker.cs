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
    using Rosetta.AST.Factories;

    /// <summary>
    /// Walks a namespace AST node.
    /// </summary>
    public class NamespaceASTWalker : ASTWalker, IASTWalker
    {
        // Protected for testability
        protected ModuleTranslationUnit module;

        /// <summary>
        /// Initializes a new instance of the <see cref="NamespaceASTWalker"/> class.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="module"></param>
        protected NamespaceASTWalker(CSharpSyntaxNode node, ModuleTranslationUnit module) 
            : base(node)
        {
            var namespaceSyntaxNode = node as NamespaceDeclarationSyntax;
            if (namespaceSyntaxNode == null)
            {
                throw new ArgumentException(
                    string.Format("Specified node is not of type {0}",
                    typeof(NamespaceDeclarationSyntax).Name));
            }

            if (module == null)
            {
                throw new ArgumentNullException(nameof(module));
            }
            
            this.module = module;
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="NamespaceASTWalker"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public NamespaceASTWalker(NamespaceASTWalker other) 
            : base(other)
        {
            this.module = other.module;
        }

        /// <summary>
        /// Factory method for class <see cref="NamespaceASTWalker"/>.
        /// </summary>
        /// <param name="node"><see cref="CSharpSyntaxNode"/> Used to initialize the walker.</param>
        /// <param name="context">The walking context.</param>
        /// <returns></returns>
        public static NamespaceASTWalker Create(CSharpSyntaxNode node, ASTWalkerContext context = null)
        {
            return new NamespaceASTWalker(node,
                new ModuleTranslationUnitFactory(node).Create() as ModuleTranslationUnit)
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
            return this.module;
        }

        #region CSharpSyntaxWalker overrides

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <remarks>
        /// This will cause an AST walker to be created, thus we don't need to go further deeper in the
        /// tree by visiting the node.
        /// </remarks>
        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            var classWalker = ClassASTWalker.Create(node, this.CreateWalkingContext());
            var translationUnit = classWalker.Walk();
            this.module.AddClass(translationUnit);

            this.InvokeClassDeclarationVisited(this, new WalkerEventArgs());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        {
            // TODO: Implement

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

        protected void InvokeClassDeclarationVisited(object sender, WalkerEventArgs e)
        {
            if (this.ClassDeclarationVisited != null)
            {
                this.ClassDeclarationVisited(sender, e);
            }
        }

        protected void InvokeInterfaceDeclarationVisited(object sender, WalkerEventArgs e)
        {
            if (this.InterfaceDeclarationVisited != null)
            {
                this.InterfaceDeclarationVisited(sender, e);
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
