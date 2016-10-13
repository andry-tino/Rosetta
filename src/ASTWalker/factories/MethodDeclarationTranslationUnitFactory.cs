/// <summary>
/// MethodDeclarationTranslationUnitFactory.cs
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
    public class MethodDeclarationTranslationUnitFactory : ITranslationUnitFactory
    {
        private readonly CSharpSyntaxNode node;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodDeclarationTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="node"></param>
        public MethodDeclarationTranslationUnitFactory(CSharpSyntaxNode node)
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
            MethodDeclaration helper = new MethodDeclaration(this.node as MethodDeclarationSyntax);

            var methodDeclaration = this.CreateTranslationUnit(
                helper.Visibility,
                TypeIdentifierTranslationUnit.Create(helper.ReturnType),
                IdentifierTranslationUnit.Create(helper.Name)) as MethodSignatureDeclarationTranslationUnit;

            foreach (Parameter parameter in helper.Parameters)
            {
                methodDeclaration.AddArgument(ArgumentDefinitionTranslationUnit.Create(
                    TypeIdentifierTranslationUnit.Create(parameter.TypeName),
                    IdentifierTranslationUnit.Create(parameter.IdentifierName)));
            }

            return methodDeclaration;
        }

        /// <summary>
        /// Creates the translation unit.
        /// </summary>
        /// <remarks>
        /// Must return a type inheriting from <see cref="MethodSignatureDeclarationTranslationUnit"/>.
        /// </remarks>
        /// <param name="visibility"></param>
        /// <param name="returnType"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        protected virtual ITranslationUnit CreateTranslationUnit(
            VisibilityToken visibility, ITranslationUnit returnType, ITranslationUnit name)
        {
            return MethodDeclarationTranslationUnit.Create(visibility, returnType, name);
        }
    }
}
