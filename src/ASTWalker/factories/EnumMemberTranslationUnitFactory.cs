/// <summary>
/// EnumMemberTranslationUnitFactory.cs
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
    /// Factory for <see cref="EnumMemberTranslationUnit"/>.
    /// </summary>
    public class EnumMemberTranslationUnitFactory : TranslationUnitFactory, ITranslationUnitFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnumMemberTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="semanticModel">The semantic model</param>
        public EnumMemberTranslationUnitFactory(CSharpSyntaxNode node, SemanticModel semanticModel = null) 
            : base(node, semanticModel)
        {
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="EnumMemberTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public EnumMemberTranslationUnitFactory(EnumMemberTranslationUnitFactory other) 
            : base(other)
        {
        }

        /// <summary>
        /// Creates a <see cref="EnumMemberTranslationUnitFactory"/>.
        /// </summary>
        /// <returns>A <see cref="EnumMemberTranslationUnitFactory"/>.</returns>
        public ITranslationUnit Create()
        {
            var helper = new EnumMemberDeclaration(this.Node as EnumMemberDeclarationSyntax, this.SemanticModel);

            var enumMemberDeclaration = this.CreateTranslationUnit(IdentifierTranslationUnit.Create(helper.Name), 
                helper.Value != null ? new ExpressionTranslationUnitBuilder(helper.Value, this.SemanticModel).Build() : null) 
                as EnumMemberTranslationUnit;

            return enumMemberDeclaration;
        }

        /// <summary>
        /// Creates the translation unit.
        /// </summary>
        /// <remarks>
        /// Must return a type inheriting from <see cref="EnumMemberTranslationUnit"/>.
        /// </remarks>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected virtual ITranslationUnit CreateTranslationUnit(ITranslationUnit name, ITranslationUnit value)
        {
            return EnumMemberTranslationUnit.Create(name, value);
        }
    }
}
