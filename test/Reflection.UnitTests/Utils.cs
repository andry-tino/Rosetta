/// <summary>
/// Class.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    /// <summary>
    /// Utilities.
    /// </summary>
    public static class Utils
    {
        public static SyntaxNode ExtractASTRoot(this string source)
        {
            // Adding entry point
            var finalSource = source + @"
                public class Program {
                    public static void Main(string[] args) {
                    }
                }
            ";

            // TODO: http://stackoverflow.com/questions/43140897/cannot-create-a-compilation-in-roslyn-from-source-code/43146257#43146257

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

            var builder = new ASTBuilder(assembly);
            var astInfo = builder.Build();

            // Getting the AST node
            var generatedTree = astInfo.Tree as CSharpSyntaxTree;

            if (tree == null)
            {
                throw new InvalidOperationException("Invalid generated tree");
            }

            return tree.GetRoot();
        }
    }
}
