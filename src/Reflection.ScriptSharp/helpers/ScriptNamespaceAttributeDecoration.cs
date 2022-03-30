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
    public class ScriptNamespaceAttributeDecoration
    {
        public const string ScriptNamespaceName = "ScriptNamespaceAttribute";
        public const string ScriptNamespaceFullName = "ScriptNamespaceAttribute"; // TODO: Find namespace used by ScriptSharp for this class

        private readonly ICustomAttributeDataProxy attributeData;

        // Cached values
        private string overridenNamespace;

        /// <summary>
        /// Initializes a new instance of the <see cref="Namespace"/> class.
        /// </summary>
        /// <param name="attributeData"></param>
        public ScriptNamespaceAttributeDecoration(ICustomAttributeDataProxy attributeData)
        {
            if (attributeData == null)
            {
                throw new ArgumentNullException(nameof(attributeData));
            }

            this.attributeData = attributeData;
        }

        /// <summary>
        /// Gets the full name of the namespace associated with the type.
        /// </summary>
        /// <remarks>
        public string OverridenNamespace
        {
            get
            {
                if (this.overridenNamespace == null)
                {
                    var args = this.attributeData.ConstructorArguments;

                    foreach (var arg in args)
                    {
                        // TODO: For now we take the first string value, make implementation more generic
                        string value = arg.Value as string;

                        if (value != null)
                        {
                            this.overridenNamespace = value;
                            break;
                        }
                    }
                }

                return this.overridenNamespace;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public static bool IsScriptNamespaceAttributeDecoration(ITypeProxy attribute)
        {
            return attribute.Name == ScriptNamespaceFullName;
        }
        
    }
}
