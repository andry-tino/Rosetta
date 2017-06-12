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
            var propertyDeclaration = SyntaxFactory.PropertyDeclaration(SyntaxFactory.ParseTypeName(
                this.GetTypeFullName(this.PropertyInfo.PropertyType)), this.PropertyInfo.Name);

            // Defining modifiers
            propertyDeclaration = this.HandleModifiers(propertyDeclaration);

            // Defining accessibility
            propertyDeclaration = this.HandleAccessibility(propertyDeclaration);

            // Defining getter
            propertyDeclaration = this.HandleGetter(propertyDeclaration);

            // Defining setter
            propertyDeclaration = this.HandleSetter(propertyDeclaration);

            return propertyDeclaration;
        }

        protected IPropertyInfoProxy PropertyInfo => this.propertyInfo;

        protected bool WithBody => this.withBody;

        private PropertyDeclarationSyntax HandleModifiers(PropertyDeclarationSyntax node)
        {
            var newNode = node;

            if (this.PropertyInfo.IsStatic)
            {
                newNode = newNode.AddModifiers(SyntaxFactory.Token(SyntaxKind.StaticKeyword));
            }

            return newNode;
        }

        private PropertyDeclarationSyntax HandleAccessibility(PropertyDeclarationSyntax node)
        {
            var newNode = node;

            newNode = newNode.AddModifiers(SyntaxFactory.Token(new Visibility(this.PropertyInfo).Token));

            return newNode;
        }

        private PropertyDeclarationSyntax HandleGetter(PropertyDeclarationSyntax node)
        {
            var newNode = node;

            if (this.PropertyInfo.CanRead)
            {
                newNode = newNode.AddAccessorListAccessors(CreateAccessor(true, this.WithBody));
            }

            return newNode;
        }

        private PropertyDeclarationSyntax HandleSetter(PropertyDeclarationSyntax node)
        {
            var newNode = node;

            if (this.PropertyInfo.CanWrite)
            {
                newNode = newNode.AddAccessorListAccessors(CreateAccessor(false, this.WithBody));
            }

            return newNode;
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

        protected virtual string GetTypeFullName(ITypeProxy type) => type.FullName;
    }
}
