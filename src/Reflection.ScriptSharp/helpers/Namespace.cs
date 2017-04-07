/// <summary>
/// Namespace.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.ScriptSharp.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Helper for <see cref="TypeInfo"/> in order to retrieve information about its namespace.
    /// </summary>
    public class Namespace : Rosetta.Reflection.Helpers.Namespace
    {
        // Cached values
        private string fullName;

        /// <summary>
        /// Initializes a new instance of the <see cref="Namespace"/> class.
        /// </summary>
        /// <param name="type"></param>
        public Namespace(TypeInfo type) : base(type)
        {
        }

        /// <summary>
        /// Gets the full name of the namespace associated with the type.
        /// </summary>
        /// <remarks>
        /// If the type has a <code>ScriptNamespace</code> associated, then the name spacified there 
        /// will be used for the namespace, thus effectively replacing the namespace.
        /// </remarks>
        public override string FullName
        {
            get
            {
                if (this.fullName == null)
                {
                    this.fullName = base.FullName;

                    foreach (CustomAttributeData attribute in this.Type.CustomAttributes)
                    {
                        if (ScriptNamespaceAttributeDecoration.IsScriptNamespaceAttributeDecoration(attribute.AttributeType))
                        {
                            var helper = new ScriptNamespaceAttributeDecoration(attribute);
                            this.fullName = helper.OverridenNamespace;

                            break;
                        }
                    }
                }

                return this.fullName;
            }
        }
    }
}
