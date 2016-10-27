/// <summary>
/// NamespaceDefinitionASTWalker.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.ScriptSharp.Definition.AST
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.AST;
    using Rosetta.ScriptSharp.Definition.Translation;
    using Rosetta.ScriptSharp.Definition.AST.Factories;
    using Rosetta.Translation;

    /// <summary>
    /// Walks a namespace AST node.
    /// </summary>
    public class NamespaceDefinitionASTWalker : NamespaceASTWalker
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NamespaceDefinitionASTWalker"/> class.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="module"></param>
        protected NamespaceDefinitionASTWalker(CSharpSyntaxNode node, ModuleTranslationUnit module) 
            : base(node, module)
        {
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="NamespaceDefinitionASTWalker"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public NamespaceDefinitionASTWalker(NamespaceDefinitionASTWalker other) 
            : base(other)
        {
        }

        /// <summary>
        /// Factory method for class <see cref="NamespaceDefinitionASTWalker"/>.
        /// </summary>
        /// <param name="node"><see cref="CSharpSyntaxNode"/> Used to initialize the walker.</param>
        /// <param name="context">The walking context.</param>
        /// <returns></returns>
        public static new NamespaceDefinitionASTWalker Create(CSharpSyntaxNode node, ASTWalkerContext context = null)
        {
            return new NamespaceDefinitionASTWalker(node,
                new ModuleDefinitionTranslationUnitFactory(node).Create() as ModuleTranslationUnit)
            {
                Context = context
            };
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
            var classDefinitionWalker = ClassDefinitionASTWalker.Create(node, this.CreateWalkingContext());
            var translationUnit = classDefinitionWalker.Walk();
            this.module.AddClass(translationUnit);

            this.InvokeClassDeclarationVisited(this, new WalkerEventArgs());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        {
            // We do not output interfaces for definitions
        }

        #endregion

        protected override void OnContextChanged()
        {
            this.ApplyContextDependenciesToTranslationUnit();
        }

        private void ApplyContextDependenciesToTranslationUnit()
        {
            if (this.Context == null)
            {
                // When a context is not available, we consider the module defined at root level
                this.ModuleDefinition.IsAtRootLevel = true;

                return;
            }

            this.ModuleDefinition.IsAtRootLevel = this.Context.Originator.GetType() == typeof(ProgramDefinitionASTWalker);
        }

        private ModuleDefinitionTranslationUnit ModuleDefinition
        {
            get { return this.module as ModuleDefinitionTranslationUnit; }
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
