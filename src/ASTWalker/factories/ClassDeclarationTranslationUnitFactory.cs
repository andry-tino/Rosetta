/// <summary>
/// ClassDeclarationTranslationUnitFactory.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.AST.Factories
{
    using System;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.Translation;
    using Rosetta.AST.Helpers;

    /// <summary>
    /// Generic helper.
    /// </summary>
    public class ClassDeclarationTranslationUnitFactory : ITranslationUnitFactory
    {
        private readonly CSharpSyntaxNode node;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassDeclarationTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="node"></param>
        public ClassDeclarationTranslationUnitFactory(CSharpSyntaxNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node), "A node must be specified!");
            }

            this.node = node;
        }

        /// <summary>
        /// Creates a <see cref="MethodDeclarationTranslationUnit"/>.
        /// </summary>
        /// <returns>A <see cref="MethodDeclarationTranslationUnit"/>.</returns>
        public ITranslationUnit Create()
        {
            ClassDeclaration helper = new ClassDeclaration(node as ClassDeclarationSyntax);

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
