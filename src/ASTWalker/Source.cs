/// <summary>
/// Source.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.AST
{
    using System;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Interface for describing AST walkers.
    /// </summary>
    public static class Source
    {
        private static SyntaxTree programRoot;

        /// <summary>
        /// 
        /// </summary>
        public static CSharpCompilation Compilation
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public static SyntaxTree ProgramRoot
        {
            get { return programRoot; }
            set
            {
                programRoot = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static void BuildSemanticModel()
        {
            if (programRoot == null)
            {
                throw new InvalidOperationException("We need a syntax tree to build the semantic model!");
            }

            Compilation = CSharpCompilation.Create("Class").AddReferences(
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location)).AddSyntaxTrees(programRoot);
        }
    }
}
