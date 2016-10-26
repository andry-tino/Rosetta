/// <summary>
/// FieldDefinitionTranslationUnitFactory.cs
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
    public class FieldDefinitionTranslationUnitFactory : FieldDeclarationTranslationUnitFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FieldDefinitionTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="node"></param>
        public FieldDefinitionTranslationUnitFactory(CSharpSyntaxNode node) 
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
                var helper = new FieldDeclaration(this.Node as FieldDeclarationSyntax);

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
        /// <param name="visibility"></param>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        protected override ITranslationUnit CreateTranslationUnit(
            VisibilityToken visibility, ITranslationUnit type, ITranslationUnit name)
        {
            return FieldDefinitionTranslationUnit.Create(visibility, type, name);
        }
    }
}
