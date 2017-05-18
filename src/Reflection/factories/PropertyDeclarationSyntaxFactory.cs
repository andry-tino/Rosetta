/// <summary>
/// PropertyDeclarationSyntaxFactory.cs
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
    /// Factory for generating a <see cref="PropertyDeclarationSyntax"/>.
    /// </summary>
    public class PropertyDeclarationSyntaxFactory : ISyntaxFactory
    {
        private readonly IPropertyInfoProxy propertyInfo;
        private readonly bool withBody;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyDeclarationSyntaxFactory"/> class.
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <param name="withBody"></param>
        public PropertyDeclarationSyntaxFactory(IPropertyInfoProxy propertyInfo, bool withBody = true)
        {
            if (propertyInfo == null)
            {
                throw new ArgumentNullException(nameof(propertyInfo));
            }

            this.propertyInfo = propertyInfo;
            this.withBody = withBody;
        }

        /// <summary>
        /// Creates the <see cref="PropertyDeclarationSyntax"/>.
        /// </summary>
        /// <returns></returns>
        public SyntaxNode Create()
        {
            var propertyDeclaration = SyntaxFactory.PropertyDeclaration(SyntaxFactory.ParseTypeName(this.propertyInfo.PropertyType.FullName), this.propertyInfo.Name);

            // Defining accessibility
            propertyDeclaration = propertyDeclaration.AddModifiers(SyntaxFactory.Token(new Visibility(this.propertyInfo).Token));

            // Defining getter
            if (this.propertyInfo.CanRead)
            {
                propertyDeclaration = propertyDeclaration.AddAccessorListAccessors(CreateAccessor(true, this.withBody));
            }

            // Defining setter
            if (this.propertyInfo.CanWrite)
            {
                propertyDeclaration = propertyDeclaration.AddAccessorListAccessors(CreateAccessor(false, this.withBody));
            }

            return propertyDeclaration;
        }

        private static AccessorDeclarationSyntax CreateAccessor(bool read, bool withBody)
        {
            var accessor = SyntaxFactory.AccessorDeclaration(read ? SyntaxKind.GetAccessorDeclaration : SyntaxKind.SetAccessorDeclaration);

            if (withBody)
            {
                accessor = accessor.WithBody(DummyBody.GenerateForProperty());
            }
            else
            {
                accessor = accessor.WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken));
            }

            return accessor;
        }
    }
}
