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
    internal static class Source
    {
        private static SyntaxTree programRoot;

        /// <summary>
        /// Gets the compilation object for <see cref="ProgramRoot"/> allowing access to its semantic model.
        /// </summary>
        public static CSharpCompilation Compilation
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the semantic model for <see cref="ProgramRoot"/>.
        /// </summary>
        public static SemanticModel SemanticModel
        {
            get
            {
                return Compilation.GetSemanticModel(ProgramRoot);
            }
        }

        /// <summary>
        /// Gets the program root's <see cref="SyntaxTree"/>.
        /// </summary>
        public static SyntaxTree ProgramRoot
        {
            get { return programRoot; }
            set
            {
                programRoot = value;
                BuildSemanticModel();
            }
        }

        /// <summary>
        /// Builds the semantic model associated to the root program <see cref="ProgramRoot"/>.
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
