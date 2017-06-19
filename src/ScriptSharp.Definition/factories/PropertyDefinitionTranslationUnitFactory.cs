/// <summary>
/// PropertyDefinitionTranslationUnitFactory.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.Definition.AST.Factories
{
    using System;
    using Microsoft.CodeAnalysis;
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
        private readonly bool createWhenProtected;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyDefinitionTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="node">The syntax node.</param>
        /// <param name="createWhenProtected">A value indicating whether the factory should create a <see cref="ITranslationUnit"/> when <see cref="node"/> is protected.</param>
        public PropertyDefinitionTranslationUnitFactory(CSharpSyntaxNode node, bool createWhenProtected = false) 
            : this(node, null, createWhenProtected)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyDefinitionTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="node">The syntax node.</param>
        /// <param name="semanticModel">The semantic model</param>
        /// <param name="createWhenProtected">A value indicating whether the factory should create a <see cref="ITranslationUnit"/> when <see cref="node"/> is protected.</param>
        public PropertyDefinitionTranslationUnitFactory(CSharpSyntaxNode node, SemanticModel semanticModel = null, bool createWhenProtected = false)
            : base(node, semanticModel)
        {
            this.createWhenProtected = createWhenProtected;
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="PropertyDefinitionTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public PropertyDefinitionTranslationUnitFactory(PropertyDefinitionTranslationUnitFactory other) 
            : base(other)
        {
            this.createWhenProtected = other.createWhenProtected;
        }

        /// <summary>
        /// Gets a value indicating whether the factory should return <code>null</code>.
        /// </summary>
        protected override bool DoNotCreateTranslationUnit
        {
            get
            {
                var helper = new PropertyDeclaration(this.Node as PropertyDeclarationSyntax);

                if (helper.Modifiers.IsExposedVisibility())
                {
                    return false;
                }

                if (this.createWhenProtected && helper.Modifiers.HasFlag(ModifierTokens.Protected))
                {
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        /// Creates the translation unit.
        /// </summary>
        /// <param name="modifiers"></param>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="hasGet"></param>
        /// <param name="hasSet"></param>
        /// <returns></returns>
        protected override ITranslationUnit CreateTranslationUnit(
            ModifierTokens modifiers, ITranslationUnit type, ITranslationUnit name, bool hasGet, bool hasSet)
        {
            return PropertyDefinitionTranslationUnit.Create(modifiers, type, name, hasGet, hasSet);
        }

        /// <summary>
        /// Creates the helper.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="semanticModel"></param>
        /// <returns></returns>
        /// <remarks>
        /// Returned type must be a derived type of <see cref="PropertyDeclaration"/>.
        /// </remarks>
        protected override Rosetta.AST.Helpers.PropertyDeclaration CreateHelper(PropertyDeclarationSyntax node, SemanticModel semanticModel)
        {
            return new Rosetta.ScriptSharp.AST.Helpers.PropertyDeclaration(node, semanticModel);
        }
    }
}
