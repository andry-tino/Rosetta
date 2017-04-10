/// <summary>
/// Class.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.UnitTests
{
    using System;
    using System.Reflection;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    using Rosetta.Tests.Utils;

    /// <summary>
    /// Utilities.
    /// </summary>
    internal static class Utils
    {
        /// <summary>
        /// From source code, generates the assembly via Roslyn and generates the AST from the assembly using the <see cref="IASTBuilder"/>.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static SyntaxNode ExtractASTRoot(this string source)
        {
            Assembly assembly = new AsmlDasmlAssemblyLoader(source).Load();

            var builder = new ASTBuilder(assembly);
            var astInfo = builder.Build();

            // Getting the AST node
            var generatedTree = astInfo.Tree as CSharpSyntaxTree;

            if (generatedTree == null)
            {
                throw new InvalidOperationException("Invalid generated tree");
            }

            return generatedTree.GetRoot();
        }

        #region Types

        /// <summary>
        /// Assembly loader based on <see cref="AsmlDasml"/>.
        /// </summary>
        public class AsmlDasmlAssemblyLoader : IAssemblyLoader
        {
            private readonly string source;

            public AsmlDasmlAssemblyLoader(string source)
            {
                if (source == null)
                {
                    throw new ArgumentNullException(nameof(source));
                }

                this.source = source;
            }

            public Assembly Load()
            {
                return AsmlDasml.Assemble(this.source);
            }
        }

        #endregion
    }
}
