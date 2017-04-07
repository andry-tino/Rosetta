/// <summary>
/// ASTBuilder.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.ScriptSharp
{
    using System;
    using System.Reflection;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.Reflection.ScriptSharp.Helpers;

    /// <summary>
    /// Builds an AST from an assembly.
    /// This class will return also the semantic model associated to the generated tree connected to the assembly the tree was generated from.
    /// </summary>
    public class ASTBuilder : Rosetta.Reflection.ASTBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ASTBuilder"/> class.
        /// This class builds a plain tree from all the types in the assembly and considers the ScriptSharp 
        /// namespace substitution attribute <code>ScriptNamespace</code>.
        /// </summary>
        /// <param name="assembly">The path to the assembly.</param>
        public ASTBuilder(Assembly assembly) : base(assembly)
        {
        }

        protected override Rosetta.Reflection.Helpers.Namespace CreateNamespaceHelper(System.Reflection.TypeInfo type)
        {
            return new Namespace(type);
        }
    }
}
