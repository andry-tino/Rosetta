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
    public class MethodDeclarationSyntaxFactory
    {
        private IMethodInfoProxy methodInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodDeclarationSyntaxFactory"/> class.
        /// </summary>
        /// <param name="classInfo"></param>
        public MethodDeclarationSyntaxFactory(IMethodInfoProxy methodInfo)
        {
            if (methodInfo == null)
            {
                throw new ArgumentNullException(nameof(methodInfo));
            }

            this.methodInfo = methodInfo;
        }

        /// <summary>
        /// Creates the <see cref="MethodDeclarationSyntax"/>.
        /// </summary>
        /// <returns></returns>
        public MethodDeclarationSyntax Create()
        {
            var methodDeclaration = SyntaxFactory.MethodDeclaration(SyntaxFactory.ParseTypeName(this.methodInfo.ReturnType.FullName), this.methodInfo.Name);

            // Defining accessibility
            methodDeclaration = methodDeclaration.AddModifiers(SyntaxFactory.Token(new Visibility(this.methodInfo).Token));

            // Defining parameters
            var parameters = this.methodInfo.Parameters;

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    methodDeclaration = methodDeclaration.AddParameterListParameters(SyntaxFactory.Parameter(SyntaxFactory.Identifier(parameter.Name))
                        .WithType(SyntaxFactory.ParseTypeName(parameter.ParameterType.FullName)));
                }
            }

            // Dummy body
            methodDeclaration = methodDeclaration.WithBody(DummyBody.GenerateForMerhod());

            return methodDeclaration;
        }
    }
}
