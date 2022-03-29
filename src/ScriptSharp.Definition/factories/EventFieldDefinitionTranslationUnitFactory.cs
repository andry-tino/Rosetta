/// <summary>
/// FieldDefinitionTranslationUnitFactory.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.Definition.AST.Factories
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.AST.Factories;
    using Rosetta.AST.Utilities;
    using DefinitionUtilities = Rosetta.ScriptSharp.Definition.AST.Utilities;
    using Rosetta.ScriptSharp.Definition.Translation;
    using Rosetta.Translation;
    using Rosetta.ScriptSharp.AST.Helpers;

    /// <summary>
    /// Generic helper.
    /// </summary>
    public class EventFieldDefinitionTranslationUnitFactory : TranslationUnitFactory, ITranslationUnitFactory
    {
        private readonly bool createWhenProtected;


        /// <summary>
        /// Initializes a new instance of the <see cref="EventFieldDefinitionTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="node">The syntax node.</param>
        /// <param name="createWhenProtected">A value indicating whether the factory should create a <see cref="ITranslationUnit"/> when <see cref="node"/> is protected.</param>
        public EventFieldDefinitionTranslationUnitFactory(CSharpSyntaxNode node, bool createWhenProtected = false) 
            : this(node, null, createWhenProtected)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventFieldDefinitionTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="node">The syntax node.</param>
        /// <param name="semanticModel">The semantic model</param>
        /// <param name="createWhenProtected">A value indicating whether the factory should create a <see cref="ITranslationUnit"/> when <see cref="node"/> is protected.</param>
        public EventFieldDefinitionTranslationUnitFactory(CSharpSyntaxNode node, SemanticModel semanticModel = null, bool createWhenProtected = false)
            : base(node, semanticModel)
        {
            this.createWhenProtected = createWhenProtected;
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="EventFieldDefinitionTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public EventFieldDefinitionTranslationUnitFactory(EventFieldDefinitionTranslationUnitFactory other) 
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
        protected bool DoNotCreateTranslationUnit
        {
            get
            {
                var helper = new EventFieldDeclaration(this.Node as EventFieldDeclarationSyntax);
                
                if (Rosetta.AST.Helpers.Modifiers.IsExposedVisibility(helper.Modifiers))
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
        /// Creates the proper helper.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="semanticModel"></param>
        /// <returns></returns>
        /// <remarks>
        /// Must return a type deriving from <see cref="EventFieldDeclaration"/>.
        /// </remarks>
        protected EventFieldDeclaration CreateHelper(EventFieldDeclarationSyntax node, SemanticModel semanticModel)
        {
            return new EventFieldDeclaration(this.Node as EventFieldDeclarationSyntax, this.SemanticModel);
        }

        public ITranslationUnit Create()
        {
            if (this.DoNotCreateTranslationUnit)
            {
                return null;
            }

            var helper = this.CreateHelper(this.Node as EventFieldDeclarationSyntax, this.SemanticModel);

            var eventFieldDeclaration = this.CreateTranslationUnit(
                helper.Modifiers,
                this.MapType(helper.Type.FullName).MappedType,
                IdentifierTranslationUnit.Create(helper.Name));

            return eventFieldDeclaration;
        }

        protected virtual ITranslationUnit CreateTranslationUnit(
            ModifierTokens modifiers, ITranslationUnit type, ITranslationUnit name)
        {
            return EventFieldDefinitionTranslationUnit.Create(modifiers, type, name);
        }
    }
}
