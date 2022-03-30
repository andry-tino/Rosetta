/// <summary>
/// Namespace.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.ScriptSharp.Helpers
{
    using System;

    using Rosetta.Reflection.Proxies;

    /// <summary>
    /// Abstraction for building an AST from an assembly.
    /// </summary>
    public class NonScriptableAttributeDecoration
    {
        public const string NonScriptableName = "NonScriptableAttribute";

        private readonly ICustomAttributeDataProxy attributeData;

        /// <summary>
        /// Initializes a new instance of the <see cref="Namespace"/> class.
        /// </summary>
        /// <param name="attributeData"></param>
        public NonScriptableAttributeDecoration(ICustomAttributeDataProxy attributeData)
        {
            if (attributeData == null)
            {
                throw new ArgumentNullException(nameof(attributeData));
            }

            this.attributeData = attributeData;
        }

        public static bool IsNonScriptableAttributeDecoration(ITypeProxy attribute)
        {
            return attribute.Name == NonScriptableName;
        }
    }
}
