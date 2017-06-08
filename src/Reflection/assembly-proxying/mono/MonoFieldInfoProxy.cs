/// <summary>
/// MonoFieldInfoProxy.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.Proxies
{
    using System;
    using System.Diagnostics;

    using Mono.Cecil;

    /// <summary>
    /// Proxy for member incapsulating Mono.
    /// </summary>
    [DebuggerDisplay("Field {Name,nq}", Name = "MonoFieldInfoProxy {Name,nq} (Mono proxy)")]
    public class MonoFieldInfoProxy : IFieldInfoProxy
    {
        private readonly FieldDefinition fieldDefinition;

        /// <summary>
        /// Initializes a new instance of the <see cref="MonoFieldInfoProxy"/> class.
        /// </summary>
        /// <param name="monoFieldDefinition"></param>
        public MonoFieldInfoProxy(FieldDefinition monoFieldDefinition)
        {
            if (monoFieldDefinition == null)
            {
                throw new ArgumentNullException(nameof(monoFieldDefinition));
            }

            this.fieldDefinition = monoFieldDefinition;
        }

        /// <summary>
        /// Gets the name of the parameter.
        /// </summary>
        public string Name => this.fieldDefinition.Name;

        /// <summary>
        /// Gets the Type of this field.
        /// </summary>
        public ITypeProxy FieldType => new MonoTypeProxy(this.fieldDefinition.FieldType);

        /// <summary>
        /// Gets a value indicating whether the field or constructor is visible only within its class 
        /// and derived classes.
        /// </summary>
        public bool IsFamily => this.fieldDefinition.IsFamily;

        /// <summary>
        /// Gets a value indicating whether this member is private.
        /// </summary>
        public bool IsPrivate => this.fieldDefinition.IsPrivate;

        /// <summary>
        /// Gets a value indicating whether this member is public.
        /// </summary>
        public bool IsPublic => this.fieldDefinition.IsPublic;
    }
}
