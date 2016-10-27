/// <summary>
/// PropertyDefinitionTranslationUnitFactory.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.Definition.AST.Factories
{
    using System;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.AST.Factories;
    using Rosetta.AST.Helpers;
    using Rosetta.ScriptSharp.Definition.Translation;
    using Rosetta.Translation;

    /// <summary>
    /// Generic helper.
    /// </summary>
    public class PropertyDefinitionTranslationUnitFactory : PropertyDeclarationTranslationUnitFactory
    {
        private bool createWhenProtected;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyDefinitionTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="node">The syntax node.</param>
        /// <param name="createWhenProtected">A value indicating whether the factory should create a <see cref="ITranslationUnit"/> when <see cref="node"/> is protected.</param>
        public PropertyDefinitionTranslationUnitFactory(CSharpSyntaxNode node, bool createWhenProtected = false) 
            : base(node)
        {
            this.createWhenProtected = createWhenProtected;
        }

        /// <summary>
        /// Gets a value indicating whether the factory should return <code>null</code>.
        /// </summary>
        protected override bool DoNotCreateTranslationUnit
        {
            get
            {
                var helper = new PropertyDeclaration(this.Node as PropertyDeclarationSyntax);

                if (helper.Visibility.IsExposedVisibility())
                {
                    return false;
                }

                if (this.createWhenProtected && helper.Visibility.HasFlag(VisibilityToken.Protected))
                {
                    return false;
                }

                return true;
            }
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
        protected override ITranslationUnit CreateTranslationUnit(
            VisibilityToken visibility, ITranslationUnit type, ITranslationUnit name, bool hasGet, bool hasSet)
        {
            return PropertyDefinitionTranslationUnit.Create(visibility, type, name, hasGet, hasSet);
        }
    }
}
