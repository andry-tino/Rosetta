/// <summary>
/// Class.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.ScriptSharp.UnitTests
{
    using System;
    using System.Reflection;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    using Rosetta.Tests.Utils;
    using Rosetta.Tests.ScriptSharp.Utils;

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
            Assembly assembly = new AsmlDasmlAssemblyLoader(source, ScriptNamespaceAttributeHelper.AttributeSourceCode).Load();

            var builder = new ASTBuilder(assembly); // ScriptSharp Reflector
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
            private readonly string additionalSource;

            public AsmlDasmlAssemblyLoader(string source, string additionalSource = null)
            {
                if (source == null)
                {
                    throw new ArgumentNullException(nameof(source));
                }

                this.source = source;
                this.additionalSource = additionalSource ?? string.Empty;
            }

            public Assembly Load()
            {
                return AsmlDasml.Assemble(this.source, this.additionalSource);
            }
        }

        #endregion
    }
}
