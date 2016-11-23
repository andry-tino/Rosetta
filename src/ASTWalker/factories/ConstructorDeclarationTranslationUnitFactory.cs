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

    /// <summary>
    /// Factory for <see cref="ConstructorDeclarationTranslationUnit"/>.
    /// </summary>
    public class ConstructorDeclarationTranslationUnitFactory : ITranslationUnitFactory
    {
        // TODO: Create common base class for all translation unit factories

        private readonly CSharpSyntaxNode node;
        private readonly SemanticModel semanticModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructorDeclarationTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="semanticModel">The semantic model</param>
        public ConstructorDeclarationTranslationUnitFactory(CSharpSyntaxNode node, SemanticModel semanticModel = null)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node), "A node must be specified!");
            }

            this.node = node;
            this.semanticModel = semanticModel;
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

            ConstructorDeclaration helper = new ConstructorDeclaration(this.node as ConstructorDeclarationSyntax, this.semanticModel);

            var constructorDeclaration = this.CreateTranslationUnit(helper.Visibility) as MethodSignatureDeclarationTranslationUnit;

            foreach (Parameter parameter in helper.Parameters)
            {
                constructorDeclaration.AddArgument(ArgumentDefinitionTranslationUnit.Create(
                    TypeIdentifierTranslationUnit.Create(parameter.TypeName),
                    IdentifierTranslationUnit.Create(parameter.IdentifierName)));
            }

            return constructorDeclaration;
        }

        /// <summary>
        /// Gets the <see cref="CSharpSyntaxNode"/>.
        /// </summary>
        protected CSharpSyntaxNode Node
        {
            get { return this.node; }
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
        /// <param name="visibility"></param>
        /// <returns></returns>
        protected virtual ITranslationUnit CreateTranslationUnit(VisibilityToken visibility)
        {
            return ConstructorDeclarationTranslationUnit.Create(visibility);
        }
    }
}
