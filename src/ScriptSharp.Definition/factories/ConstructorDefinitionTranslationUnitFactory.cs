﻿/// <summary>
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
    using Rosetta.AST.Utilities;
    using DefinitionUtilities = Rosetta.ScriptSharp.Definition.AST.Utilities;
    using Rosetta.ScriptSharp.Definition.Translation;
    using Rosetta.Translation;

    /// <summary>
    /// Generic helper.
    /// </summary>
    public class ConstructorDefinitionTranslationUnitFactory : ConstructorDeclarationTranslationUnitFactory
    {
        private readonly bool createWhenProtected;

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
        /// Copy initializes a new instance of the <see cref="ConstructorDefinitionTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public ConstructorDefinitionTranslationUnitFactory(ConstructorDefinitionTranslationUnitFactory other) 
            : base(other)
        {
            this.createWhenProtected = other.createWhenProtected;
        }

        protected override MappingResult MapType(string originalType)
        {
            // Apply mapping from base
            var mappingResult = base.MapType(originalType);

            // If no actual mapping is performed, do this mapping
            if (!mappingResult.MappingApplied)
            {
                mappingResult = DefinitionUtilities.TypeMappings.MapType(originalType);
            }

            return mappingResult;
        }

        /// <summary>
        /// Gets a value indicating whether the factory should return <code>null</code>.
        /// </summary>
        protected override bool DoNotCreateTranslationUnit
        {
            get
            {
                var helper = this.CreateHelper(this.Node as ConstructorDeclarationSyntax, null); // Semantic model not needed

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
        /// <remarks>
        /// Must return a type inheriting from <see cref="MethodSignatureDeclarationTranslationUnit"/>.
        /// </remarks>
        /// <param name="modifiers"></param>
        /// <returns></returns>
        protected override ITranslationUnit CreateTranslationUnit(ModifierTokens modifiers)
        {
            return ConstructorDefinitionTranslationUnit.Create(modifiers);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="semanticModel"></param>
        /// <returns></returns>
        /// <remarks>
        /// Must be a type derived from <see cref="ConstructorDeclaration"/>.
        /// </remarks>
        protected override Rosetta.AST.Helpers.ConstructorDeclaration CreateHelper(ConstructorDeclarationSyntax node, SemanticModel semanticModel)
        {
            return new Rosetta.ScriptSharp.AST.Helpers.ConstructorDeclaration(node, semanticModel);
        }
    }
}
