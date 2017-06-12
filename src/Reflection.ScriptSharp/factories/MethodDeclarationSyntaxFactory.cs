/// <summary>
/// MethodDeclarationSyntaxFactory.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.ScriptSharp.Factories
{
    using System;

    using Rosetta.Reflection.Helpers;
    using Rosetta.Reflection.Proxies;
    using Rosetta.Reflection.ScriptSharp.Utilities;

    /// <summary>
    /// Factory for generating a <see cref="MethodDeclarationSyntax"/>.
    /// </summary>
    public class MethodDeclarationSyntaxFactory : Rosetta.Reflection.Factories.MethodDeclarationSyntaxFactory
    {
        private readonly ITypeLookup typeLookup;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodDeclarationSyntaxFactory"/> class.
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <param name="typeLookup"></param>
        /// <param name="withBody"></param>
        public MethodDeclarationSyntaxFactory(IMethodInfoProxy methodInfo, ITypeLookup typeLookup, bool withBody = true) 
            : base(methodInfo, withBody)
        {
            if (typeLookup == null)
            {
                throw new ArgumentNullException(nameof(typeLookup));
            }

            this.typeLookup = typeLookup;
        }

        protected override string GetReturnTypeFullName(ITypeProxy type) => type.FullName.LookupTypeAndOverrideFullName(this.typeLookup);

        protected override string GetParameterTypeFullName(ITypeProxy type) => type.FullName.LookupTypeAndOverrideFullName(this.typeLookup);
    }
}
