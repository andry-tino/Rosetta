/// <summary>
/// EnumTranslationUnitFactory.cs
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
    /// Factory for <see cref="EnumTranslationUnitFactory"/>.
    /// </summary>
    public class EnumTranslationUnitFactory : ITranslationUnitFactory
    {
        private readonly CSharpSyntaxNode node;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="node"></param>
        public EnumTranslationUnitFactory(CSharpSyntaxNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node), "A node must be specified!");
            }

            this.node = node;
        }

        /// <summary>
        /// Creates a <see cref="EnumTranslationUnitFactory"/>.
        /// </summary>
        /// <returns>A <see cref="EnumTranslationUnitFactory"/>.</returns>
        public ITranslationUnit Create()
        {
            EnumDeclaration helper = new EnumDeclaration(node as EnumDeclarationSyntax);

            var enumDeclaration = this.CreateTranslationUnit(helper.Visibility,
                IdentifierTranslationUnit.Create(helper.Name)) as EnumTranslationUnit;

            return enumDeclaration;
        }

        /// <summary>
        /// Creates the translation unit.
        /// </summary>
        /// <remarks>
        /// Must return a type inheriting from <see cref="EnumTranslationUnit"/>.
        /// </remarks>
        /// <param name="visibility"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        protected virtual ITranslationUnit CreateTranslationUnit(VisibilityToken visibility, ITranslationUnit name)
        {
            return EnumTranslationUnit.Create(visibility, name);
        }
    }
}
