/// <summary>
/// ConstructorDefinitionTranslationUnitFactory.cs
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
    public class ConstructorDefinitionTranslationUnitFactory : ConstructorDeclarationTranslationUnitFactory
    {
        private bool createWhenProtected;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructorDefinitionTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="node">The syntax node.</param>
        /// <param name="createWhenProtected">A value indicating whether the factory should create a <see cref="ITranslationUnit"/> when <see cref="node"/> is protected.</param>
        public ConstructorDefinitionTranslationUnitFactory(CSharpSyntaxNode node, bool createWhenProtected = false) 
            : this(node, null, createWhenProtected)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructorDefinitionTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="node">The syntax node.</param>
        /// <param name="semanticModel">The semantic model</param>
        /// <param name="createWhenProtected">A value indicating whether the factory should create a <see cref="ITranslationUnit"/> when <see cref="node"/> is protected.</param>
        public ConstructorDefinitionTranslationUnitFactory(CSharpSyntaxNode node, SemanticModel semanticModel = null, bool createWhenProtected = false)
            : base(node, semanticModel)
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
                var helper = new ConstructorDeclaration(this.Node as ConstructorDeclarationSyntax);

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
        /// <remarks>
        /// Must return a type inheriting from <see cref="MethodSignatureDeclarationTranslationUnit"/>.
        /// </remarks>
        /// <param name="visibility"></param>
        /// <returns></returns>
        protected override ITranslationUnit CreateTranslationUnit(VisibilityToken visibility)
        {
            return ConstructorDefinitionTranslationUnit.Create(visibility);
        }
    }
}
