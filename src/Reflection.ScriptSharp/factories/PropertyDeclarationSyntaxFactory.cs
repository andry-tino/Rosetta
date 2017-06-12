/// <summary>
/// PropertyDeclarationSyntaxFactory.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.ScriptSharp.Factories
{
    using System;

    using Rosetta.Reflection.Helpers;
    using Rosetta.Reflection.Proxies;
    using Rosetta.Reflection.ScriptSharp.Utilities;

    /// <summary>
    /// Factory for generating a <see cref="PropertyDeclarationSyntax"/>.
    /// </summary>
    public class PropertyDeclarationSyntaxFactory : Rosetta.Reflection.Factories.PropertyDeclarationSyntaxFactory
    {
        private readonly ITypeLookup typeLookup;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyDeclarationSyntaxFactory"/> class.
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <param name="typeLookup"></param>
        /// <param name="withBody"></param>
        public PropertyDeclarationSyntaxFactory(IPropertyInfoProxy propertyInfo, ITypeLookup typeLookup, bool withBody = true) 
            : base(propertyInfo, withBody)
        {
            if (typeLookup == null)
            {
                throw new ArgumentNullException(nameof(typeLookup));
            }

            this.typeLookup = typeLookup;
        }

        protected override string GetTypeFullName(ITypeProxy type) => type.FullName.LookupTypeAndOverrideFullName(this.typeLookup);
    }
}
