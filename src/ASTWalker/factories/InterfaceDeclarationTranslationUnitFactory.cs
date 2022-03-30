/// <summary>
/// InterfaceDeclarationTranslationUnitFactory.cs
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
    /// Factory for <see cref="InterfaceDeclarationTranslationUnit"/>.
    /// </summary>
    public class InterfaceDeclarationTranslationUnitFactory : TranslationUnitFactory, ITranslationUnitFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InterfaceDeclarationTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="semanticModel">The semantic model</param>
        public InterfaceDeclarationTranslationUnitFactory(CSharpSyntaxNode node, SemanticModel semanticModel = null) 
            : base(node, semanticModel)
        {
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="InterfaceDeclarationTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public InterfaceDeclarationTranslationUnitFactory(InterfaceDeclarationTranslationUnitFactory other) 
            : base(other)
        {
        }

        /// <summary>
        /// Creates a <see cref="InterfaceDeclarationTranslationUnit"/>.
        /// </summary>
        /// <returns>A <see cref="InterfaceDeclarationTranslationUnit"/>.</returns>
        public ITranslationUnit Create()
        {
            InterfaceDeclaration helper = this.CreateHelper(this.Node as InterfaceDeclarationSyntax, this.SemanticModel);

            var interfaceDeclaration = this.CreateTranslationUnit(helper.Visibility,
                IdentifierTranslationUnit.Create(helper.Name)) as InterfaceDeclarationTranslationUnit;

            foreach (BaseTypeReference implementedInterface in helper.ExtendedInterfaces)
            {
                interfaceDeclaration.AddExtendedInterface(IdentifierTranslationUnit.Create(implementedInterface.FullName));
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
        protected virtual ITranslationUnit CreateTranslationUnit(ModifierTokens visibility, ITranslationUnit name)
        {
            return InterfaceDeclarationTranslationUnit.Create(visibility, name);
        }

        protected virtual InterfaceDeclaration CreateHelper(InterfaceDeclarationSyntax node, SemanticModel semanticModel)
        {
            return new InterfaceDeclaration(node, semanticModel);
        }
    }
}
