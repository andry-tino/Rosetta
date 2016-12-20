/// <summary>
/// ScriptNamespaceAttributeHelper.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.Tests.ScriptSharp.Utils
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    /// <summary>
    /// Helper for the ScriptNamespace attribute semantics.
    /// </summary>
    public static class ScriptNamespaceAttributeHelper
    {
        private const string Source = @"
            using System;
            [AttributeUsage(AttributeTargets.All)]
            public class ScriptNamespace : Attribute {
                public ScriptNamespace(string ns) { }
            }
        ";

        /// <summary>
        /// Adds a reference to the ScriptNamespace attribute that can appear in tests.
        /// </summary>
        /// <param name="compilation">The <see cref="CSharpCompilation"/> compilation hosting the references.</param>
        /// <returns>A <see cref="CSharpCompilation"/> with the added reference to the ScriptNamespace attribute to be used in tests.</returns>
        public static CSharpCompilation AddScriptNamespaceReference(this CSharpCompilation compilation)
        {
            if (compilation == null)
            {
                throw new ArgumentNullException(nameof(compilation));
            }

            var tree = CSharpSyntaxTree.ParseText(Source);

            return compilation.AddSyntaxTrees(tree);
        }
    }
}
