/// <summary>
/// SemanticUtils.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.AST.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.IO;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    /// <summary>
    /// Helper for interacting with the <see cref="SemanticModel"/>.
    /// </summary>
    public static class SemanticUtils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <param name="sourceTree"></param>
        /// <param name="loadMsCoreLib"></param>
        /// <returns></returns>
        public static SemanticModel RetrieveSemanticModel(string name, string path, CSharpSyntaxTree sourceTree, bool loadMsCoreLib = false)
        {
            return RetrieveCompilation(name, path, sourceTree, loadMsCoreLib).GetSemanticModel(sourceTree);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="compilation"></param>
        /// <param name="sourceTree"></param>
        /// <returns></returns>
        public static SemanticModel RetrieveSemanticModel(Compilation compilation, CSharpSyntaxTree sourceTree)
        {
            return compilation.GetSemanticModel(sourceTree);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <param name="sourceTree"></param>
        /// <param name="loadMsCoreLib"></param>
        /// <returns></returns>
        public static CSharpCompilation RetrieveCompilation(string name, string path, CSharpSyntaxTree sourceTree, bool loadMsCoreLib = false)
        {
            var references = new List<MetadataReference>();

            var assembly = MetadataReference.CreateFromFile(path); // The target assembly we want to translate
            references.Add(assembly);

            if (loadMsCoreLib)
            {
                var mscorelib = GetMsCoreLibMetadataReference(); // .NET native
                references.Add(mscorelib);
            }

            return CSharpCompilation.Create(name, new[] { sourceTree }, references);
        }

        private static PortableExecutableReference GetMsCoreLibMetadataReference()
        {
            var assembly = typeof(object).Assembly;

            // TODO: determine the best logic for this
            //var path = Uri.UnescapeDataString(new UriBuilder(assembly.CodeBase).Path);
            //var metadataReference = MetadataReference.CreateFromFile(path);
            var metadataReference = MetadataReference.CreateFromFile(assembly.Location);

            return metadataReference;
        }
    }
}
