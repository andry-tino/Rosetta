/// <summary>
/// EnumDeclarationSyntaxFactory.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.Factories
{
    using System;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.Reflection.Helpers;
    using Rosetta.Reflection.Proxies;

    /// <summary>
    /// Factory for generating a <see cref="EnumDeclarationSyntax"/>.
    /// </summary>
    public class EnumDeclarationSyntaxFactory : ISyntaxFactory
    {
        private readonly ITypeInfoProxy enumInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumDeclarationSyntaxFactory"/> class.
        /// </summary>
        /// <param name="classInfo"></param>
        public EnumDeclarationSyntaxFactory(ITypeInfoProxy enumInfo)
        {
            if (enumInfo == null)
            {
                throw new ArgumentNullException(nameof(enumInfo));
            }

            this.enumInfo = enumInfo;
        }

        /// <summary>
        /// Creates the <see cref="InterfaceDeclarationSyntax"/>.
        /// </summary>
        /// <returns></returns>
        public SyntaxNode Create()
        {
            var enumNode = SyntaxFactory.EnumDeclaration(this.enumInfo.Name);

            // Defining accessibility
            var visibility = new Visibility(this.enumInfo).Token;
            if (visibility != SyntaxKind.None)
            {
                enumNode = enumNode.AddModifiers(SyntaxFactory.Token(visibility));
            }

            // Values
            var values = this.enumInfo.DeclaredFields;

            if (values != null)
            {
                foreach (var value in values)
                {
                    enumNode = enumNode.AddMembers(SyntaxFactory.EnumMemberDeclaration(value.Name));
                }
            }

            return enumNode;
        }
    }
}
