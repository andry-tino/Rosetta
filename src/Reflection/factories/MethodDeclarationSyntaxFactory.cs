/// <summary>
/// MethodDeclarationSyntaxFactory.cs
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
    /// Factory for generating a <see cref="MethodDeclarationSyntax"/>.
    /// </summary>
    public class MethodDeclarationSyntaxFactory : ISyntaxFactory
    {
        private readonly IMethodInfoProxy methodInfo;
        private readonly bool withBody;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodDeclarationSyntaxFactory"/> class.
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <param name="withBody"></param>
        public MethodDeclarationSyntaxFactory(IMethodInfoProxy methodInfo, bool withBody = true)
        {
            if (methodInfo == null)
            {
                throw new ArgumentNullException(nameof(methodInfo));
            }

            this.methodInfo = methodInfo;
            this.withBody = withBody;
        }

        /// <summary>
        /// Creates the <see cref="MethodDeclarationSyntax"/>.
        /// </summary>
        /// <returns></returns>
        public SyntaxNode Create()
        {
            var methodDeclaration = SyntaxFactory.MethodDeclaration(SyntaxFactory.ParseTypeName(
                this.GetReturnTypeFullName(this.MethodInfo.ReturnType)), this.MethodInfo.Name);

            // Defining modifiers
            methodDeclaration = this.HandleModifiers(methodDeclaration);

            // Defining accessibility
            methodDeclaration = this.HandleAccessibility(methodDeclaration);

            // Defining parameters
            methodDeclaration = this.HandleParameters(methodDeclaration);

            // Dummy body
            methodDeclaration = this.HandleBody(methodDeclaration);

            return methodDeclaration;
        }

        protected IMethodInfoProxy MethodInfo => this.methodInfo;

        protected bool WithBody => this.withBody;

        private MethodDeclarationSyntax HandleModifiers(MethodDeclarationSyntax node)
        {
            var newNode = node;

            if (this.MethodInfo.IsStatic)
            {
                newNode = newNode.AddModifiers(SyntaxFactory.Token(SyntaxKind.StaticKeyword));
            }

            return newNode;
        }

        private MethodDeclarationSyntax HandleAccessibility(MethodDeclarationSyntax node)
        {
            var newNode = node;

            newNode = newNode.AddModifiers(SyntaxFactory.Token(new Visibility(this.MethodInfo).Token));

            return newNode;
        }

        private MethodDeclarationSyntax HandleParameters(MethodDeclarationSyntax node)
        {
            var newNode = node;

            var parameters = this.MethodInfo.Parameters;

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    //parameter.ParameterType
                    newNode = newNode.AddParameterListParameters(SyntaxFactory.Parameter(SyntaxFactory.Identifier(parameter.Name))
                        .WithType(SyntaxFactory.ParseTypeName(this.GetParameterTypeFullName(parameter.ParameterType))));
                }
            }

            return newNode;
        }

        private MethodDeclarationSyntax HandleBody(MethodDeclarationSyntax node)
        {
            var newNode = node;

            if (this.WithBody)
            {
                newNode = newNode.WithBody(DummyBody.GenerateForMerhod());
            }

            return newNode;
        }

        protected virtual string GetReturnTypeFullName(ITypeProxy type) => type.FullName;

        protected virtual string GetParameterTypeFullName(ITypeProxy type) => type.FullName;
    }
}
