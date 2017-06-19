/// <summary>
/// EnumTranslationUnitFactory.cs
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
    /// Factory for <see cref="EnumTranslationUnit"/>.
    /// </summary>
    public class EnumTranslationUnitFactory : TranslationUnitFactory, ITranslationUnitFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnumTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="semanticModel">The semantic model</param>
        public EnumTranslationUnitFactory(CSharpSyntaxNode node, SemanticModel semanticModel = null) 
            : base(node, semanticModel)
        {
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="EnumTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public EnumTranslationUnitFactory(EnumTranslationUnitFactory other) 
            : base(other)
        {
        }

        /// <summary>
        /// Creates a <see cref="EnumTranslationUnitFactory"/>.
        /// </summary>
        /// <returns>A <see cref="EnumTranslationUnitFactory"/>.</returns>
        public ITranslationUnit Create()
        {
            EnumDeclaration helper = new EnumDeclaration(this.Node as EnumDeclarationSyntax, this.SemanticModel);

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
        protected virtual ITranslationUnit CreateTranslationUnit(ModifierTokens visibility, ITranslationUnit name)
        {
            return EnumTranslationUnit.Create(visibility, name);
        }
    }
}
