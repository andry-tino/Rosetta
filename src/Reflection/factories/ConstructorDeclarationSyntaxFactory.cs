/// <summary>
/// ConstructorDeclarationSyntaxFactory.cs
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
    /// Factory for generating a <see cref="ConstructorDeclarationSyntax"/>.
    /// </summary>
    public class ConstructorDeclarationSyntaxFactory : ISyntaxFactory
    {
        private readonly ITypeInfoProxy classInfo;
        private readonly IConstructorInfoProxy ctorInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructorDeclarationSyntaxFactory"/> class.
        /// </summary>
        /// <param name="ctorInfo"></param>
        /// <param name="classInfo"></param>
        public ConstructorDeclarationSyntaxFactory(IConstructorInfoProxy ctorInfo, ITypeInfoProxy classInfo)
        {
            if (ctorInfo == null)
            {
                throw new ArgumentNullException(nameof(ctorInfo));
            }

            if (classInfo == null)
            {
                throw new ArgumentNullException(nameof(classInfo));
            }

            this.ctorInfo = ctorInfo;
            this.classInfo = classInfo;
        }

        /// <summary>
        /// Creates the <see cref="ConstructorDeclarationSyntax"/>.
        /// </summary>
        /// <returns></returns>
        public SyntaxNode Create()
        {
            var ctorDeclaration = SyntaxFactory.ConstructorDeclaration(this.ClassInfo.Name);

            // Defining accessibility
            ctorDeclaration = this.HandleAccessibility(ctorDeclaration);

            // Defining parameters
            ctorDeclaration = this.HandleParameters(ctorDeclaration);

            // Dummy body
            ctorDeclaration = this.HandleBody(ctorDeclaration);

            return ctorDeclaration;
        }

        protected ITypeInfoProxy ClassInfo => this.classInfo;

        protected IConstructorInfoProxy CtorInfo => this.ctorInfo;

        private ConstructorDeclarationSyntax HandleAccessibility(ConstructorDeclarationSyntax node)
        {
            var newNode = node;

            newNode = newNode.AddModifiers(SyntaxFactory.Token(new Visibility(this.CtorInfo).Token));

            return newNode;
        }

        private ConstructorDeclarationSyntax HandleParameters(ConstructorDeclarationSyntax node)
        {
            var newNode = node;

            var parameters = this.CtorInfo.Parameters;

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    newNode = newNode.AddParameterListParameters(SyntaxFactory.Parameter(SyntaxFactory.Identifier(parameter.Name))
                        .WithType(SyntaxFactory.ParseTypeName(this.GetParameterTypeFullName(parameter.ParameterType))));
                }
            }

            return newNode;
        }

        private ConstructorDeclarationSyntax HandleBody(ConstructorDeclarationSyntax node)
        {
            var newNode = node;

            newNode = newNode.WithBody(DummyBody.GenerateForMerhod());

            return newNode;
        }

        protected virtual string GetParameterTypeFullName(ITypeProxy type) => type.FullName;
    }
}
