/// <summary>
/// MethodDeclarationTranslationUnitFactory.cs
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
    /// Factory for <see cref="MethodDeclarationTranslationUnit"/>.
    /// </summary>
    public class MethodDeclarationTranslationUnitFactory : TranslationUnitFactory, ITranslationUnitFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MethodDeclarationTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="semanticModel">The semantic model</param>
        public MethodDeclarationTranslationUnitFactory(CSharpSyntaxNode node, SemanticModel semanticModel = null) 
            : base(node, semanticModel)
        {
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="MethodDeclarationTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public MethodDeclarationTranslationUnitFactory(MethodDeclarationTranslationUnitFactory other) 
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

            MethodDeclaration helper = this.CreateHelper(this.Node as MethodDeclarationSyntax, this.SemanticModel);

            var methodDeclaration = this.CreateTranslationUnit(
                helper.Visibility,
                TypeIdentifierTranslationUnit.Create(helper.ReturnType.FullName.MapType()),
                IdentifierTranslationUnit.Create(helper.Name)) as MethodSignatureDeclarationTranslationUnit;

            foreach (Parameter parameter in helper.Parameters)
            {
                methodDeclaration.AddArgument(ArgumentDefinitionTranslationUnit.Create(
                    TypeIdentifierTranslationUnit.Create(parameter.Type.FullName.MapType()),
                    IdentifierTranslationUnit.Create(parameter.IdentifierName)));
            }

            return methodDeclaration;
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
        /// <param name="returnType"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        protected virtual ITranslationUnit CreateTranslationUnit(
            VisibilityToken visibility, ITranslationUnit returnType, ITranslationUnit name)
        {
            return MethodDeclarationTranslationUnit.Create(visibility, returnType, name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="semanticModel"></param>
        /// <returns></returns>
        /// <remarks>
        /// Must be a type derived from <see cref="MethodDeclaration"/>.
        /// </remarks>
        protected virtual MethodDeclaration CreateHelper(MethodDeclarationSyntax node, SemanticModel semanticModel)
        {
            return new MethodDeclaration(node, semanticModel);
        }
    }
}
