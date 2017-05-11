/// <summary>
/// Class.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.ScriptSharp.UnitTests
{
    using System;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    using Rosetta.Reflection.Proxies;
    using Rosetta.Tests.Utils;
    using Rosetta.Tests.ScriptSharp.Utils;
    using System.IO;

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
            IAssemblyProxy assembly = new AsmlDasmlAssemblyLoader(source, ScriptNamespaceAttributeHelper.AttributeSourceCode).Load();

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

            public Stream RawAssembly => null;

            public IAssemblyProxy Load()
            {
                return AsmlDasml.Assemble(this.source, stream => new MonoStreamAssemblyLoader(stream).Load(), this.additionalSource) as IAssemblyProxy;
            }
        }

        #endregion
    }
}
