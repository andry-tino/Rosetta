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
    public class EnumMemberTranslationUnitFactory : ITranslationUnitFactory
    {
        // TODO: Create common base class for all translation unit factories

        private readonly CSharpSyntaxNode node;
        private readonly SemanticModel semanticModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumMemberTranslationUnitFactory"/> class.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="semanticModel">The semantic model</param>
        public EnumMemberTranslationUnitFactory(CSharpSyntaxNode node, SemanticModel semanticModel = null)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node), "A node must be specified!");
            }

            this.node = node;
            this.semanticModel = semanticModel;
        }

        /// <summary>
        /// Creates a <see cref="EnumMemberTranslationUnitFactory"/>.
        /// </summary>
        /// <returns>A <see cref="EnumMemberTranslationUnitFactory"/>.</returns>
        public ITranslationUnit Create()
        {
            var helper = new EnumMemberDeclaration(node as EnumMemberDeclarationSyntax, this.semanticModel);

            var enumMemberDeclaration = this.CreateTranslationUnit(IdentifierTranslationUnit.Create(helper.Name), 
                helper.Value != null ? new ExpressionTranslationUnitBuilder(helper.Value, this.semanticModel).Build() : null) 
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
