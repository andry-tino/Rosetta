/// <summary>
/// AsmlDasml.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Tests.Utils
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    /// <summary>
    /// Utilities for assembling and disassembling code.
    /// </summary>
    public static class AsmlDasml
    {
        /// <summary>
        /// From source code, generates the assembly via Roslyn and generates the AST from the assembly using the <see cref="IASTBuilder"/>.
        /// 
        /// TODO: No need to specify an entry point, follow here
        ///       http://stackoverflow.com/questions/43140897/cannot-create-a-compilation-in-roslyn-from-source-code/43146257#43146257
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Assembly Assemble(this string source, string additionalSource = "")
        {
            // Adding entry point
            var finalSource = source + @"
                public class Program {
                    public static void Main(string[] args) {
                    }
                }
            ";

            // Adding additional code
            finalSource += additionalSource;

            SyntaxTree tree = CSharpSyntaxTree.ParseText(finalSource);

            var references = new List<MetadataReference>();
            var mscorelib = Rosetta.Utils.SemanticUtils.GetMsCoreLibMetadataReference(); // .NET native
            references.Add(mscorelib);

            CSharpCompilation compilation = CSharpCompilation.Create("TestCompilation", new[] { tree }, references);

            Assembly assembly = null;
            using (var stream = new MemoryStream())
            {
                var emitResult = compilation.Emit(stream);
                if (!emitResult.Success)
                {
                    var message = emitResult.Diagnostics.Select(d => d.ToString())
                        .Aggregate((d1, d2) => $"{d1}{Environment.NewLine}{d2}");

                    throw new InvalidOperationException($"Could not emit assembly.{Environment.NewLine}{message}");
                }

                stream.Seek(0, SeekOrigin.Begin);
                assembly = Assembly.Load(stream.ToArray());
            }

            return assembly;
        }
    }
}
