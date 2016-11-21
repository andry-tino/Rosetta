/// <summary>
/// InterfaceDeclarationTranslationUnitFactory.cs
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
    /// Factory for <see cref="InterfaceDeclarationTranslationUnit"/>.
    /// </summary>
    public class InterfaceDeclarationTranslationUnitFactory : ITranslationUnitFactory
    {
        private readonly CSharpSyntaxNode node;

        /// <summary>
        /// Initializes a new instance of the <see cref="InterfaceDeclarationTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="node"></param>
        public InterfaceDeclarationTranslationUnitFactory(CSharpSyntaxNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node), "A node must be specified!");
            }

            this.node = node;
        }

        /// <summary>
        /// Creates a <see cref="InterfaceDeclarationTranslationUnit"/>.
        /// </summary>
        /// <returns>A <see cref="InterfaceDeclarationTranslationUnit"/>.</returns>
        public ITranslationUnit Create()
        {
            InterfaceDeclaration helper = new InterfaceDeclaration(node as InterfaceDeclarationSyntax);

            var interfaceDeclaration = this.CreateTranslationUnit(helper.Visibility,
                IdentifierTranslationUnit.Create(helper.Name)) as InterfaceDeclarationTranslationUnit;

            foreach (BaseTypeReference implementedInterface in helper.ExtendedInterfaces)
            {
                interfaceDeclaration.AddExtendedInterface(IdentifierTranslationUnit.Create(implementedInterface.Name));
            }
            
            return interfaceDeclaration;
        }

        /// <summary>
        /// Creates the translation unit.
        /// </summary>
        /// <remarks>
        /// Must return a type inheriting from <see cref="InterfaceDeclarationTranslationUnit"/>.
        /// </remarks>
        /// <param name="visibility"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        protected virtual ITranslationUnit CreateTranslationUnit(VisibilityToken visibility, ITranslationUnit name)
        {
            return InterfaceDeclarationTranslationUnit.Create(visibility, name);
        }
    }
}
