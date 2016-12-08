/// <summary>
/// ModuleTranslationUnitFactory.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.AST.Factories
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.Translation;
    using Rosetta.AST.Helpers;

    /// <summary>
    /// Factory for <see cref="ModuleTranslationUnit"/>.
    /// </summary>
    public class ModuleTranslationUnitFactory : TranslationUnitFactory, ITranslationUnitFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="semanticModel">The semantic model</param>
        public ModuleTranslationUnitFactory(CSharpSyntaxNode node, SemanticModel semanticModel = null) 
            : base(node, semanticModel)
        {
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="ModuleTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public ModuleTranslationUnitFactory(ModuleTranslationUnitFactory other) 
            : base(other)
        {
        }

        /// <summary>
        /// Creates a <see cref="ModuleTranslationUnitFactory"/>.
        /// </summary>
        /// <returns>A <see cref="ModuleTranslationUnitFactory"/>.</returns>
        public ITranslationUnit Create()
        {
            var helper = new NamespaceDeclaration(this.Node as NamespaceDeclarationSyntax, this.SemanticModel);

            var module = this.CreateTranslationUnit(IdentifierTranslationUnit.Create(helper.Name));

            return module;
        }

        /// <summary>
        /// Creates the translation unit.
        /// </summary>
        /// <remarks>
        /// Must return a type inheriting from <see cref="ModuleTranslationUnit"/>.
        /// </remarks>
        /// <param name="name"></param>
        /// <returns></returns>
        protected virtual ITranslationUnit CreateTranslationUnit(ITranslationUnit name)
        {
            return ModuleTranslationUnit.Create(name);
        }
    }
}
