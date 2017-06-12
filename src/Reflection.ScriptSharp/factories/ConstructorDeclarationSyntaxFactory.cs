/// <summary>
/// ConstructorDeclarationSyntaxFactory.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.ScriptSharp.Factories
{
    using System;
    
    using Rosetta.Reflection.Proxies;
    using Rosetta.Reflection.ScriptSharp.Utilities;

    /// <summary>
    /// Factory for generating a <see cref="ConstructorDeclarationSyntax"/>.
    /// </summary>
    public class ConstructorDeclarationSyntaxFactory : Rosetta.Reflection.Factories.ConstructorDeclarationSyntaxFactory
    {
        private readonly ITypeLookup typeLookup;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructorDeclarationSyntaxFactory"/> class.
        /// </summary>
        /// <param name="ctorInfo"></param>
        /// <param name="classInfo"></param>
        /// <param name="typeLookup"></param>
        public ConstructorDeclarationSyntaxFactory(IConstructorInfoProxy ctorInfo, ITypeInfoProxy classInfo, ITypeLookup typeLookup) 
            : base(ctorInfo, classInfo)
        {
            if (typeLookup == null)
            {
                throw new ArgumentNullException(nameof(typeLookup));
            }

            this.typeLookup = typeLookup;
        }

        protected override string GetParameterTypeFullName(ITypeProxy type) => type.FullName.LookupTypeAndOverrideFullName(this.typeLookup);
    }
}
