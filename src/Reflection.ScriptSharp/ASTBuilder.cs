/// <summary>
/// ASTBuilder.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.ScriptSharp
{
    using System;

    using Rosetta.Reflection.Factories;
    using Rosetta.Reflection.Proxies;
    using Rosetta.Reflection.ScriptSharp.Helpers;
    using ScriptSharpFactories = Rosetta.Reflection.ScriptSharp.Factories;

    /// <summary>
    /// Builds an AST from an assembly.
    /// This class will return also the semantic model associated to the generated tree connected to the assembly the tree was generated from.
    /// </summary>
    public class ASTBuilder : Rosetta.Reflection.ASTBuilder
    {
        private ITypeLookup lookup;

        /// <summary>
        /// Initializes a new instance of the <see cref="ASTBuilder"/> class.
        /// This class builds a plain tree from all the types in the assembly and considers the ScriptSharp 
        /// namespace substitution attribute <code>ScriptNamespace</code>.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        public ASTBuilder(IAssemblyProxy assembly) 
            : base(assembly)
        {
            this.lookup = new LinearSearchTypeLookup(assembly);
        }

        protected override ISyntaxFactory CreateClassDeclarationSyntaxFactory(ITypeInfoProxy typeInfo)
            => new ScriptSharpFactories.ClassDeclarationSyntaxFactory(typeInfo, this.lookup);

        protected override ISyntaxFactory CreateInterfaceDeclarationSyntaxFactory(ITypeInfoProxy typeInfo)
            => new ScriptSharpFactories.InterfaceDeclarationSyntaxFactory(typeInfo, this.lookup);

        protected override Rosetta.Reflection.Helpers.Namespace CreateNamespaceHelper(ITypeInfoProxy type)
        {
            return new Namespace(type);
        }
    }
}
