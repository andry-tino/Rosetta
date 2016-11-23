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
    using Rosetta.AST.Factories;

    /// <summary>
    /// Walks a program AST node.
    /// </summary>
    public class ProgramASTWalker : ASTWalker, IASTWalker
    {
        // Protected for testability
        protected ProgramTranslationUnit program;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramASTWalker"/> class.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="module"></param>
        /// <param name="semanticModel">The semantic model.</param>
        protected ProgramASTWalker(CSharpSyntaxNode node, ProgramTranslationUnit program, SemanticModel semanticModel) 
            : base(node, semanticModel)
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
            : base(other)
        {
            this.program = other.program;
        }

        /// <summary>
        /// Factory method for class <see cref="ProgramASTWalker"/>.
        /// </summary>
        /// <param name="node"><see cref="CSharpSyntaxNode"/> Used to initialize the walker.</param>
        /// <param name="context">The walking context.</param>
        /// <param name="semanticModel">The semantic model.</param>
        /// <returns></returns>
        public static ProgramASTWalker Create(CSharpSyntaxNode node, ASTWalkerContext context = null, SemanticModel semanticModel = null)
        {
            return new ProgramASTWalker(
                node,
                new ProgramTranslationUnitFactory(node, semanticModel).Create() as ProgramTranslationUnit,
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
            var classWalker = ClassASTWalker.Create(node, this.CreateWalkingContext(), this.semanticModel);
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
            var namespaceWalker = NamespaceASTWalker.Create(node, this.CreateWalkingContext(), this.semanticModel);
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
        public event WalkerEvent EnumDeclarationVisited;

        /// <summary>
        /// 
        /// </summary>
        public event WalkerEvent NamespaceDeclarationVisited;

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

        protected void InvokeEnumDeclarationVisited(object sender, WalkerEventArgs e)
        {
            if (this.EnumDeclarationVisited != null)
            {
                this.EnumDeclarationVisited(sender, e);
            }
        }

        protected void InvokeNamespaceDeclarationVisited(object sender, WalkerEventArgs e)
        {
            if (this.NamespaceDeclarationVisited != null)
            {
                this.NamespaceDeclarationVisited(sender, e);
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
