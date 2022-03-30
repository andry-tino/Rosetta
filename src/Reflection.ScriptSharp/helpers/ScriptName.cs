/// <summary>
/// Namespace.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.ScriptSharp.Helpers
{
    using System;
    using System.Collections.Generic;
    using Rosetta.Reflection.Proxies;
    using Rosetta.ScriptSharp.AST.Helpers;

    /// <summary>
    /// Helper for <see cref="ITypeInfoProxy"/> in order to retrieve information about its namespace.
    /// </summary>
    public class ScriptName
    {
        // Cached values
        private string name;
        private bool? hasScriptNameverride;
        private readonly ITypeInfoProxy type;
        /// <summary>
        /// Initializes a new instance of the <see cref="Namespace"/> class.
        /// </summary>
        /// <param name="type"></param>
        public ScriptName(ITypeInfoProxy type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            // Supported input types
            CheckType(type);

            this.type = type;
        }

        protected ITypeInfoProxy Type => this.type;

        private static void CheckType(ITypeInfoProxy type)
        {
            if (type.IsClass) return;
            if (type.IsValueType) return;
            if (type.IsInterface) return;
            if (type.IsEnum) return;

            throw new ArgumentException("This helper only supports classes, structs, enums and interfaces", nameof(type));
        }

        /// <summary>
        /// Name, transformed from ScriptSharp rules.
        /// </summary>
        public string Name
        {
            get
            {
                if (this.name == null)
                {
                    IEnumerable<ICustomAttributeDataProxy> customAttributes = this.Type.CustomAttributes;

                    if (customAttributes != null)
                    {
                        foreach (ICustomAttributeDataProxy attribute in customAttributes)
                        {
                            if (ScriptNameAttributeDecoration.IsScriptNameAttributeDecoration(attribute.AttributeType))
                            {
                                var helper = new ScriptNameAttributeDecoration(attribute);
                                this.name = helper.OverridenName ?? this.Type.Name.ToScriptSharpName(helper.PreserveCase);

                                break;
                            }
                        }
                    }
                }
                return this.name;
            }
        }
        /// <summary>
        /// Gets a value indicating whether the underlying <see cref="ITypeInfoProxy"/> has a 
        /// name override because of the ScriptName attribute.
        /// </summary>
        public bool HasScriptNameOverride
        {
            get
            {
                if (!this.hasScriptNameverride.HasValue)
                {
                    this.hasScriptNameverride = false;
                    IEnumerable<ICustomAttributeDataProxy> customAttributes = this.Type.CustomAttributes;

                    if (customAttributes != null)
                    {
                        foreach (ICustomAttributeDataProxy attribute in customAttributes)
                        {
                            if (ScriptNameAttributeDecoration.IsScriptNameAttributeDecoration(attribute.AttributeType))
                            {
                                this.hasScriptNameverride = true;

                                break;
                            }
                        }
                    }
                }

                return this.hasScriptNameverride.Value;
            }
        }
    }
}
