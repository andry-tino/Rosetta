/// <summary>
/// ModuleTranslationUnitFactory.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.AST.Factories
{
    using System;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.Translation;
    using Rosetta.AST.Helpers;

    /// <summary>
    /// Generic helper.
    /// </summary>
    public class ModuleTranslationUnitFactory : ITranslationUnitFactory
    {
        private readonly CSharpSyntaxNode node;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="node"></param>
        public ModuleTranslationUnitFactory(CSharpSyntaxNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node), "A node must be specified!");
            }

            this.node = node;
        }

        /// <summary>
        /// Creates a <see cref="ModuleTranslationUnitFactory"/>.
        /// </summary>
        /// <returns>A <see cref="ModuleTranslationUnitFactory"/>.</returns>
        public ITranslationUnit Create()
        {
            var helper = new NamespaceDeclaration(this.node as NamespaceDeclarationSyntax);

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
