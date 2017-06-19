/// <summary>
/// ConstructorDeclarationTranslationUnitFactory.cs
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
    /// Factory for <see cref="ConstructorDeclarationTranslationUnit"/>.
    /// </summary>
    public class ConstructorDeclarationTranslationUnitFactory : TranslationUnitFactory, ITranslationUnitFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructorDeclarationTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="semanticModel">The semantic model</param>
        public ConstructorDeclarationTranslationUnitFactory(CSharpSyntaxNode node, SemanticModel semanticModel = null) 
            : base(node, semanticModel)
        {
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="ConstructorDeclarationTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public ConstructorDeclarationTranslationUnitFactory(ConstructorDeclarationTranslationUnitFactory other) 
            : base(other)
        {
        }

        /// <summary>
        /// Creates a <see cref="ConstructorDeclarationTranslationUnitFactory"/>.
        /// </summary>
        /// <returns>A <see cref="ConstructorDeclarationTranslationUnitFactory"/>.</returns>
        public ITranslationUnit Create()
        {
            if (this.DoNotCreateTranslationUnit)
            {
                return null;
            }

            ConstructorDeclaration helper = this.CreateHelper(this.Node as ConstructorDeclarationSyntax, this.SemanticModel);

            var constructorDeclaration = this.CreateTranslationUnit(helper.Modifiers) as MethodSignatureDeclarationTranslationUnit;

            foreach (Parameter parameter in helper.Parameters)
            {
                constructorDeclaration.AddArgument(ArgumentDefinitionTranslationUnit.Create(
                    TypeIdentifierTranslationUnit.Create(parameter.Type.FullName.MapType()),
                    IdentifierTranslationUnit.Create(parameter.IdentifierName)));
            }

            return constructorDeclaration;
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
        /// <remarks>
        /// Must return a type inheriting from <see cref="MethodSignatureDeclarationTranslationUnit"/>.
        /// </remarks>
        /// <param name="modifiers"></param>
        /// <returns></returns>
        protected virtual ITranslationUnit CreateTranslationUnit(ModifierTokens modifiers)
        {
            return ConstructorDeclarationTranslationUnit.Create(modifiers);
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
        protected virtual ConstructorDeclaration CreateHelper(ConstructorDeclarationSyntax node, SemanticModel semanticModel)
        {
            return new ConstructorDeclaration(node, semanticModel);
        }
    }
}
