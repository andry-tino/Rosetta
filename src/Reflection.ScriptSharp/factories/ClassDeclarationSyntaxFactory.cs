/// <summary>
/// ClassDeclarationSyntaxFactory.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.ScriptSharp.Factories
{
    using System;

    using Rosetta.Reflection.Factories;
    using Rosetta.Reflection.Proxies;
    using Rosetta.Reflection.ScriptSharp.Helpers;
    using Rosetta.Reflection.ScriptSharp.Utilities;

    /// <summary>
    /// Factory for generating a <see cref="ClassDeclarationSyntax"/>.
    /// </summary>
    public class ClassDeclarationSyntaxFactory : Rosetta.Reflection.Factories.ClassDeclarationSyntaxFactory
    {
        private readonly ITypeLookup typeLookup;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassDeclarationSyntaxFactory"/> class.
        /// </summary>
        /// <param name="classInfo"></param>
        /// <param name="typeLookup"></param>
        public ClassDeclarationSyntaxFactory(ITypeInfoProxy classInfo, ITypeLookup typeLookup) 
            : base(classInfo)
        {
            if (typeLookup == null)
            {
                throw new ArgumentNullException(nameof(typeLookup));
            }

            this.typeLookup = typeLookup;
        }

        protected override string GetBaseTypeFullName(ITypeProxy type) => type.FullName.LookupTypeAndOverrideFullName(this.typeLookup);
        
        protected override string GetInterfaceFullName(ITypeProxy type) => type.FullName.LookupTypeAndOverrideFullName(this.typeLookup);
        
        protected override ISyntaxFactory CreateConstructorDeclarationSyntaxFactory(IConstructorInfoProxy ctorInfo)
            => new ConstructorDeclarationSyntaxFactory(ctorInfo, this.ClassInfo, this.typeLookup);
        
        protected override ISyntaxFactory CreateMethodDeclarationSyntaxFactory(IMethodInfoProxy methodInfo)
            => new MethodDeclarationSyntaxFactory(methodInfo, this.typeLookup);
        
        protected override ISyntaxFactory CreatePropertyDeclarationSyntaxFactory(IPropertyInfoProxy propertyInfo)
            => new PropertyDeclarationSyntaxFactory(propertyInfo, this.typeLookup);
    }
}
