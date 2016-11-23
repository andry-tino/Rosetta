/// <summary>
/// ClassDeclarationTranslationUnitFactory.cs
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
    /// Factory for <see cref="ClassDeclarationTranslationUnit"/>.
    /// </summary>
    public class ClassDeclarationTranslationUnitFactory : ITranslationUnitFactory
    {
        // TODO: Create common base class for all translation unit factories

        private readonly CSharpSyntaxNode node;
        private readonly SemanticModel semanticModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassDeclarationTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="semanticModel">The semantic model</param>
        public ClassDeclarationTranslationUnitFactory(CSharpSyntaxNode node, SemanticModel semanticModel = null)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node), "A node must be specified!");
            }

            this.node = node;
            this.semanticModel = semanticModel;
        }

        /// <summary>
        /// Creates a <see cref="MethodDeclarationTranslationUnit"/>.
        /// </summary>
        /// <returns>A <see cref="MethodDeclarationTranslationUnit"/>.</returns>
        public ITranslationUnit Create()
        {
            ClassDeclaration helper = new ClassDeclaration(this.node as ClassDeclarationSyntax, this.semanticModel);

            var classDeclaration = this.CreateTranslationUnit(
                helper.Visibility,
                IdentifierTranslationUnit.Create(helper.Name),
                helper.BaseClass == null ? null : IdentifierTranslationUnit.Create(helper.BaseClass.Name)) as ClassDeclarationTranslationUnit;

            foreach (BaseTypeReference implementedInterface in helper.ImplementedInterfaces)
            {
                classDeclaration.AddImplementedInterface(IdentifierTranslationUnit.Create(implementedInterface.Name));
            }

            return classDeclaration;
        }

        /// <summary>
        /// Creates the translation unit.
        /// </summary>
        /// <remarks>
        /// Must return a type inheriting from <see cref="ClassDeclarationTranslationUnit"/>.
        /// </remarks>
        /// <param name="visibility"></param>
        /// <param name="name"></param>
        /// <param name="baseClassName"></param>
        /// <returns></returns>
        protected virtual ITranslationUnit CreateTranslationUnit(
            VisibilityToken visibility, ITranslationUnit name, ITranslationUnit baseClassName)
        {
            return ClassDeclarationTranslationUnit.Create(visibility, name, baseClassName);
        }
    }
}
