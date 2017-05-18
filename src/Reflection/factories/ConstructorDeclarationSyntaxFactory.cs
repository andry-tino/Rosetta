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
            var ctorDeclaration = SyntaxFactory.ConstructorDeclaration(this.classInfo.Name);

            // Defining accessibility
            ctorDeclaration = ctorDeclaration.AddModifiers(SyntaxFactory.Token(new Visibility(this.ctorInfo).Token));

            // Defining parameters
            var parameters = this.ctorInfo.Parameters;

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    ctorDeclaration = ctorDeclaration.AddParameterListParameters(SyntaxFactory.Parameter(SyntaxFactory.Identifier(parameter.Name))
                        .WithType(SyntaxFactory.ParseTypeName(parameter.ParameterType.FullName)));
                }
            }

            // Dummy body
            ctorDeclaration = ctorDeclaration.WithBody(DummyBody.GenerateForMerhod());

            return ctorDeclaration;
        }
    }
}
