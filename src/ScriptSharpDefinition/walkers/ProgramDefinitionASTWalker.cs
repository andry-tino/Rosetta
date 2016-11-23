/// <summary>
/// ProgramASTWalker.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.Definition.AST
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.AST;
    using Rosetta.ScriptSharp.Definition.AST.Factories;
    using Rosetta.ScriptSharp.Definition.Translation;
    using Rosetta.Translation;

    /// <summary>
    /// Walks a program AST node.
    /// </summary>
    public class ProgramDefinitionASTWalker : ProgramASTWalker
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramDefinitionASTWalker"/> class.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="module"></param>
        /// <param name="semanticModel">The semantic model.</param>
        protected ProgramDefinitionASTWalker(CSharpSyntaxNode node, ProgramTranslationUnit program, SemanticModel semanticModel) 
            : base(node, program, semanticModel)
        {
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="ProgramDefinitionASTWalker"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public ProgramDefinitionASTWalker(ProgramDefinitionASTWalker other) 
            : base(other)
        {
        }

        /// <summary>
        /// Factory method for class <see cref="ProgramASTWalker"/>.
        /// </summary>
        /// <param name="node"><see cref="ProgramDefinitionASTWalker"/> Used to initialize the walker.</param>
        /// <param name="context">The walking context.</param>
        /// <param name="semanticModel">The semantic model.</param>
        /// <returns></returns>
        public static new ProgramDefinitionASTWalker Create(CSharpSyntaxNode node, ASTWalkerContext context = null, SemanticModel semanticModel = null)
        {
            return new ProgramDefinitionASTWalker(
                node,
                new ProgramDefinitionTranslationUnitFactory(node).Create() as ProgramTranslationUnit,
                semanticModel)
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
            var classDefinitionWalker = ClassDefinitionASTWalker.Create(node, this.CreateWalkingContext(), this.semanticModel);
            var translationUnit = classDefinitionWalker.Walk();
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
            var namespaceWalker = NamespaceDefinitionASTWalker.Create(node, this.CreateWalkingContext(), this.semanticModel);
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
            var translationUnit = new InterfaceDefinitionTranslationUnitFactory(node, this.semanticModel).Create();
            (translationUnit as InterfaceDefinitionTranslationUnit).IsAtRootLevel = true;
            this.program.AddContent(translationUnit);

            this.InvokeInterfaceDeclarationVisited(this, new WalkerEventArgs());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitEnumDeclaration(EnumDeclarationSyntax node)
        {
            var translationUnit = new EnumDefinitionTranslationUnitFactory(node, this.semanticModel).Create();
            (translationUnit as EnumDefinitionTranslationUnit).IsAtRootLevel = true;
            this.program.AddContent(translationUnit);

            this.InvokeInterfaceDeclarationVisited(this, new WalkerEventArgs());
        }

        #endregion

        private ASTWalkerContext CreateWalkingContext()
        {
            return new ASTWalkerContext()
            {
                Originator = this
            };
        }
    }
}
