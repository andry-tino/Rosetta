/// <summary>
/// ProgramASTWalker.cs
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
    /// Walks a program AST node.
    /// </summary>
    public class ProgramASTWalker : CSharpSyntaxWalker, IASTWalker
    {
        // Protected for testability
        protected CSharpSyntaxNode node;

        // Protected for testability
        protected ProgramTranslationUnit program;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramASTWalker"/> class.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="module"></param>
        protected ProgramASTWalker(CSharpSyntaxNode node, ProgramTranslationUnit program)
        {
            var programSyntaxNode = node as CompilationUnitSyntax;
            if (programSyntaxNode == null)
            {
                throw new ArgumentException(
                    string.Format("Specified node is not of type {0}",
                    typeof(CompilationUnitSyntax).Name));
            }

            if (program == null)
            {
                throw new ArgumentNullException(nameof(program));
            }

            this.node = node;
            this.program = program;
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="ProgramASTWalker"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public ProgramASTWalker(ProgramASTWalker other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            this.node = other.node;
            this.program = other.program;
        }

        /// <summary>
        /// Factory method for class <see cref="ProgramASTWalker"/>.
        /// </summary>
        /// <param name="node"><see cref="CSharpSyntaxNode"/> Used to initialize the walker.</param>
        /// <returns></returns>
        public static ProgramASTWalker Create(CSharpSyntaxNode node)
        {
            // No helper needed for this walker

            var program = ProgramTranslationUnit.Create();

            return new ProgramASTWalker(node, program);
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
            return this.program;
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
            var classWalker = ClassASTWalker.Create(node);
            var translationUnit = classWalker.Walk();
            this.program.AddContent(translationUnit);

            this.InvokeClassDeclarationVisited(this, new WalkerEventArgs());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <remarks>
        /// This will cause an AST walker to be created, thus we don't need to go further deeper in the
        /// tree by visiting the node.
        /// </remarks>
        public override void VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {
            var namespaceWalker = NamespaceASTWalker.Create(node);
            var translationUnit = namespaceWalker.Walk();
            this.program.AddContent(translationUnit);

            this.InvokeNamespaceDeclarationVisited(this, new WalkerEventArgs());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        {
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
        public event WalkerEvent NamespaceDeclarationVisited;

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

        private void InvokeNamespaceDeclarationVisited(object sender, WalkerEventArgs e)
        {
            if (this.NamespaceDeclarationVisited != null)
            {
                this.NamespaceDeclarationVisited(sender, e);
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
