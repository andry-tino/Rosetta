/// <summary>
/// EnumDefinitionASTWalker.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.AST
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Symbols;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.CodeAnalysis.Text;

    using Rosetta.ScriptSharp.Definition.AST;
    using Rosetta.ScriptSharp.Definition.AST.Factories;
    using Rosetta.ScriptSharp.Definition.Translation;

    /// <summary>
    /// Walks an interface AST node.
    /// </summary>
    public class EnumDefinitionASTWalker : EnumASTWalker, IASTWalker
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnumDefinitionASTWalker"/> class.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="enumDefinition"></param>
        /// <param name="semanticModel">The semantic model.</param>
        protected EnumDefinitionASTWalker(CSharpSyntaxNode node, EnumDefinitionTranslationUnit enumDefinition, SemanticModel semanticModel)
            : base(node, enumDefinition, semanticModel)
        {
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="EnumDefinitionASTWalker"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public EnumDefinitionASTWalker(EnumDefinitionASTWalker other)
            : base(other)
        {
        }

        /// <summary>
        /// Factory method for class <see cref="EnumDefinitionASTWalker"/>.
        /// </summary>
        /// <param name="node"><see cref="CSharpSyntaxNode"/> Used to initialize the walker.</param>
        /// <param name="context">The walking context.</param>
        /// <param name="semanticModel">The semantic model.</param>
        /// <returns></returns>
        public static EnumDefinitionASTWalker Create(CSharpSyntaxNode node, ASTWalkerContext context = null, SemanticModel semanticModel = null)
        {
            return new EnumDefinitionASTWalker(
                node,
                new EnumDefinitionTranslationUnitFactory(node, semanticModel).Create() as EnumDefinitionTranslationUnit,
                semanticModel)
            {
                Context = context
            };
        }

        protected override void OnContextChanged()
        {
            this.ApplyContextDependenciesToTranslationUnit();
        }

        private void ApplyContextDependenciesToTranslationUnit()
        {
            if (this.Context == null)
            {
                // When a context is not available, we consider the class defined at root level
                this.EnumDefinition.IsAtRootLevel = true;

                return;
            }

            this.EnumDefinition.IsAtRootLevel = this.Context.Originator.GetType() == typeof(ProgramDefinitionASTWalker);
        }

        private EnumDefinitionTranslationUnit EnumDefinition
        {
            get { return this.enumDeclaration as EnumDefinitionTranslationUnit; }
        }
    }
}
