/// <summary>
/// MethodDefinitionTranslationUnitFactory.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.Definition.AST.Factories
{
    using System;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.AST.Factories;
    using Rosetta.AST.Helpers;
    using Rosetta.ScriptSharp.Definition.Translation;
    using Rosetta.Translation;

    /// <summary>
    /// Generic helper.
    /// </summary>
    public class MethodDefinitionTranslationUnitFactory : MethodDeclarationTranslationUnitFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MethodDefinitionTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="node"></param>
        public MethodDefinitionTranslationUnitFactory(CSharpSyntaxNode node) 
            : base(node)
        {
        }

        /// <summary>
        /// Gets a value indicating whether the factory should return <code>null</code>.
        /// </summary>
        protected override bool DoNotCreateTranslationUnit
        {
            get
            {
                var helper = new MethodDeclaration(this.Node as MethodDeclarationSyntax);

                if (helper.Visibility.IsExposedVisibility())
                {
                    return false;
                }

                return true;
            }
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
        protected override ITranslationUnit CreateTranslationUnit(
            VisibilityToken visibility, ITranslationUnit returnType, ITranslationUnit name)
        {
            return MethodDefinitionTranslationUnit.Create(visibility, returnType, name);
        }
    }
}
