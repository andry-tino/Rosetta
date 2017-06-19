/// <summary>
/// FieldDeclarationTranslationUnitFactory.cs
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
    using Rosetta.AST.Utilities;

    /// <summary>
    /// Factory for <see cref="FieldDeclarationTranslationUnit"/>.
    /// </summary>
    public class FieldDeclarationTranslationUnitFactory : TranslationUnitFactory, ITranslationUnitFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FieldDeclarationTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="semanticModel">The semantic model</param>
        public FieldDeclarationTranslationUnitFactory(CSharpSyntaxNode node, SemanticModel semanticModel = null) 
            : base(node, semanticModel)
        {
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="FieldDeclarationTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public FieldDeclarationTranslationUnitFactory(FieldDeclarationTranslationUnitFactory other) 
            : base(other)
        {
        }

        /// <summary>
        /// Creates a <see cref="MethodDeclarationTranslationUnit"/>.
        /// </summary>
        /// <returns>A <see cref="MethodDeclarationTranslationUnit"/>.</returns>
        public ITranslationUnit Create()
        {
            if (this.DoNotCreateTranslationUnit)
            {
                return null;
            }

            var helper = this.CreateHelper(this.Node as FieldDeclarationSyntax, this.SemanticModel);

            var fieldDeclaration = this.CreateTranslationUnit(
                helper.Modifiers,
                TypeIdentifierTranslationUnit.Create(helper.Type.FullName.MapType()), // TODO: Create a factory for type reference
                IdentifierTranslationUnit.Create(helper.Name));

            return fieldDeclaration;
        }

        /// <summary>
        /// Gets a value indicating whether the factory should return <code>null</code>.
        /// </summary>
        protected virtual bool DoNotCreateTranslationUnit
        {
            get { return false; }
        }

        /// <summary>
        /// Creates the translation unit.
        /// </summary>
        /// <param name="modifiers"></param>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        protected virtual ITranslationUnit CreateTranslationUnit(
            ModifierTokens modifiers, ITranslationUnit type, ITranslationUnit name)
        {
            return FieldDeclarationTranslationUnit.Create(modifiers, type, name);
        }

        /// <summary>
        /// Creates the proper helper.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="semanticModel"></param>
        /// <returns></returns>
        /// <remarks>
        /// Must return a type deriving from <see cref="FieldDeclaration"/>.
        /// </remarks>
        protected virtual FieldDeclaration CreateHelper(FieldDeclarationSyntax node, SemanticModel semanticModel)
        {
            return new FieldDeclaration(this.Node as FieldDeclarationSyntax, this.SemanticModel);
        }
    }
}
