/// <summary>
/// PropertyDeclarationTranslationUnitFactory.cs
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
    public class PropertyDeclarationTranslationUnitFactory : ITranslationUnitFactory
    {
        private readonly CSharpSyntaxNode node;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyDeclarationTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="node"></param>
        public PropertyDeclarationTranslationUnitFactory(CSharpSyntaxNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node), "A node must be specified!");
            }

            this.node = node;
        }

        /// <summary>
        /// Creates a <see cref="PropertyDeclarationTranslationUnitFactory"/>.
        /// </summary>
        /// <returns>A <see cref="PropertyDeclarationTranslationUnitFactory"/>.</returns>
        public ITranslationUnit Create()
        {
            PropertyDeclaration helper = new PropertyDeclaration(this.node as PropertyDeclarationSyntax);

            var propertyDeclaration = this.CreateTranslationUnit(
                helper.Visibility,
                TypeIdentifierTranslationUnit.Create(helper.Type),
                IdentifierTranslationUnit.Create(helper.Name),
                helper.HasGet,
                helper.HasSet);

            return propertyDeclaration;
        }

        /// <summary>
        /// Creates the translation unit.
        /// </summary>
        /// <param name="visibility"></param>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="hasGet"></param>
        /// <param name="hasSet"></param>
        /// <returns></returns>
        protected virtual ITranslationUnit CreateTranslationUnit(
            VisibilityToken visibility, ITranslationUnit type, ITranslationUnit name, bool hasGet, bool hasSet)
        {
            return PropertyDeclarationTranslationUnit.Create(visibility, type, name, hasGet, hasSet);
        }
    }
}
