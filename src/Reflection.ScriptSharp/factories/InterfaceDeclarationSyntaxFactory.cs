/// <summary>
/// InterfaceDeclarationSyntaxFactory.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.ScriptSharp.Factories
{
    using System;

    using Rosetta.Reflection.Helpers;
    using Rosetta.Reflection.Factories;
    using Rosetta.Reflection.Proxies;
    using Rosetta.Reflection.ScriptSharp.Utilities;

    /// <summary>
    /// Factory for generating a <see cref="InterfaceDeclarationSyntax"/>.
    /// </summary>
    public class InterfaceDeclarationSyntaxFactory : Rosetta.Reflection.Factories.InterfaceDeclarationSyntaxFactory
    {
        private readonly ITypeLookup typeLookup;

        /// <summary>
        /// Initializes a new instance of the <see cref="InterfaceDeclarationSyntaxFactory"/> class.
        /// </summary>
        /// <param name="interfaceInfo"></param>
        /// <param name="typeLookup"></param>
        public InterfaceDeclarationSyntaxFactory(ITypeInfoProxy interfaceInfo, ITypeLookup typeLookup) 
            : base(interfaceInfo)
        {
            if (typeLookup == null)
            {
                throw new ArgumentNullException(nameof(typeLookup));
            }

            this.typeLookup = typeLookup;
        }

        protected override string GetInterfaceFullName(ITypeProxy type) => type.FullName.LookupTypeAndOverrideFullName(this.typeLookup);
        
        protected override ISyntaxFactory CreateMethodDeclarationSyntaxFactory(IMethodInfoProxy methodInfo)
            => new MethodDeclarationSyntaxFactory(methodInfo, this.typeLookup, false);
    }
}
