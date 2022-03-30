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
    public class ScriptNameAttributeDecoration
    {
        public const string ScriptNamespaceName = "ScriptNameAttribute";
        public const string ScriptNamespaceFullName = "ScriptNameAttribute"; // TODO: Find namespace used by ScriptSharp for this class
        public const string PreserveCasePropertyName = "PreserveCase";
        public const string PreserveNamePropertyName = "PreserveName";

        private readonly ICustomAttributeDataProxy attributeData;

        // Cached values
        private string overridenName;
        private readonly bool preserveCase;
        private readonly bool preserveName;

        /// <summary>
        /// Do not lowercase first char
        /// </summary>
        public bool PreserveCase => preserveCase;
        /// <summary>
        /// Doesn't allow transformation of name
        /// </summary>
        public bool PreserveName => preserveName;

        /// <summary>
        /// Initializes a new instance of the <see cref="Namespace"/> class.
        /// </summary>
        /// <param name="attributeData"></param>
        public ScriptNameAttributeDecoration(ICustomAttributeDataProxy attributeData)
        {
            if (attributeData == null)
            {
                throw new ArgumentNullException(nameof(attributeData));
            }

            this.attributeData = attributeData;
            var args = this.attributeData.ConstructorArguments;

            foreach (var arg in args)
            {
                if (arg.ArgumentType.Name.Equals(PreserveCasePropertyName, StringComparison.Ordinal))
                {
                    this.preserveCase = (bool)(arg.Value ?? false);
                }
                else if (arg.ArgumentType.Name.Equals(PreserveNamePropertyName, StringComparison.Ordinal))
                {
                    this.preserveName = (bool)(arg.Value ?? false);
                }
                else
                {
                    this.overridenName = arg.Value as string;
                    break;
                }
            }
        }



        /// <summary>
        /// Gets the name of the overriden Script Name.
        /// </summary>
        public string OverridenName => this.overridenName;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public static bool IsScriptNameAttributeDecoration(ITypeProxy attribute)
        {
            return attribute.Name == ScriptNamespaceFullName;
        }
    }
}
