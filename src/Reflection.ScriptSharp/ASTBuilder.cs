/// <summary>
/// ASTBuilder.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.ScriptSharp
{
    using System;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        /// <param name="retrieveSemanticModel">
        /// A value indicating whether the Mscorlib assembly should be 
        /// loaded and its semantic model employed.
        /// </param>
        public ASTBuilder(IAssemblyProxy assembly, bool retrieveSemanticModel = true) 
            : base(assembly, retrieveSemanticModel)
        {
            this.lookup = new LinearSearchTypeLookup(assembly);
        }

        protected override ISyntaxFactory CreateEnumDeclarationSyntaxFactory(ITypeInfoProxy typeInfo)
        {
            var helper = new Namespace(typeInfo);
            if (helper.HasIgnoreOutput)
            {
                return null;
            }
            else
            {
                return base.CreateEnumDeclarationSyntaxFactory(typeInfo);
            }
        }

        protected override ISyntaxFactory CreateClassDeclarationSyntaxFactory(ITypeInfoProxy typeInfo)
        {
            var helper = new Namespace(typeInfo);
            if (helper.HasIgnoreOutput)
            {
                return null;
            }
            else
            {
                return new ScriptSharpFactories.ClassDeclarationSyntaxFactory(typeInfo, this.lookup);
            }
        }

        protected override ISyntaxFactory CreateInterfaceDeclarationSyntaxFactory(ITypeInfoProxy typeInfo)
        {
            var helper = new Namespace(typeInfo);
            if (helper.HasIgnoreOutput)
            {
                return null;
            }
            else
            {
                return new ScriptSharpFactories.InterfaceDeclarationSyntaxFactory(typeInfo, this.lookup);
            }
        }

        protected override Rosetta.Reflection.Helpers.Namespace CreateNamespaceHelper(ITypeInfoProxy type)
        {
            return new Namespace(type);
        }

        protected override MemberDeclarationSyntax BuildClassNode(ITypeInfoProxy type) => new Namespace(type).HasIgnoreOutput ? null : base.BuildClassNode(type);
    
        protected override MemberDeclarationSyntax BuildEnumNode(ITypeInfoProxy type) => new Namespace(type).HasIgnoreOutput ? null : base.BuildEnumNode(type);

        protected override MemberDeclarationSyntax BuildInterfaceNode(ITypeInfoProxy type) => new Namespace(type).HasIgnoreOutput ? null : base.BuildInterfaceNode(type);

        protected override MemberDeclarationSyntax BuildStructNode(ITypeInfoProxy type) => new Namespace(type).HasIgnoreOutput ? null : base.BuildStructNode(type);

    }
}
