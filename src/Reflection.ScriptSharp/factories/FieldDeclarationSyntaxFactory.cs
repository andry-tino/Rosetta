/// <summary>
/// FieldDeclarationSyntaxFactory.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.ScriptSharp.Factories
{
    using System;

    using Rosetta.Reflection.Proxies;
    using Rosetta.Reflection.ScriptSharp.Utilities;

    /// <summary>
    /// Factory for generating a <see cref="FieldDeclarationSyntax"/>.
    /// </summary>
    public class FieldDeclarationSyntaxFactory : Rosetta.Reflection.Factories.FieldDeclarationSyntaxFactory
    {
        private readonly ITypeLookup typeLookup;

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldDeclarationSyntaxFactory"/> class.
        /// </summary>
        /// <param name="fieldInfo"></param>
        /// <param name="typeLookup"></param>
        public FieldDeclarationSyntaxFactory(IFieldInfoProxy fieldInfo, ITypeLookup typeLookup) 
            : base(fieldInfo)
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
