/// <summary>
/// MonoPropertyInfoProxy.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.Proxies
{
    using System;
    using System.Diagnostics;

    using Mono.Cecil;

    /// <summary>
    /// Proxy for property incapsulating Mono.
    /// </summary>
    [DebuggerDisplay("Property {Name,nq}", Name = "Property {Name,nq} (Mono proxy)")]
    public class MonoPropertyInfoProxy : IPropertyInfoProxy
    {
        private readonly PropertyDefinition propertyDefinition;

        /// <summary>
        /// Initializes a new instance of the <see cref="MonoPropertyInfoProxy"/> class.
        /// </summary>
        /// <param name="monoTypeDefinition"></param>
        public MonoPropertyInfoProxy(PropertyDefinition monoPropertyDefinition)
        {
            if (monoPropertyDefinition == null)
            {
                throw new ArgumentNullException(nameof(monoPropertyDefinition));
            }

            this.propertyDefinition = monoPropertyDefinition;
        }

        /// <summary>
        /// Gets the name of the current member.
        /// </summary>
        public string Name => this.propertyDefinition.Name;

        /// <summary>
        /// Gets the return type of this method.
        /// </summary>
        public ITypeProxy PropertyType => new MonoTypeProxy(this.propertyDefinition.PropertyType);

        /// <summary>
        /// Gets a value indicating whether the property can be read.
        /// </summary>
        public bool CanRead => this.propertyDefinition.GetMethod != null;

        /// <summary>
        /// Gets a value indicating whether the property can be written to.
        /// </summary>
        public bool CanWrite => this.propertyDefinition.SetMethod != null;

        /// <summary>
        /// Gets a value indicating whether the method or constructor is visible only within its class 
        /// and derived classes.
        /// </summary>
        public bool IsFamily
        {
            get
            {
                if (this.CanRead)
                {
                    return this.propertyDefinition.GetMethod.IsFamily;
                }

                if (this.CanWrite)
                {
                    return this.propertyDefinition.SetMethod.IsFamily;
                }

                throw new InvalidOperationException("A property must at least have a getter or a setter");
            }
        }

        /// <summary>
        /// Gets a value indicating whether this member is private.
        /// </summary>
        public bool IsPrivate
        {
            get
            {
                if (this.CanRead)
                {
                    return this.propertyDefinition.GetMethod.IsPrivate;
                }

                if (this.CanWrite)
                {
                    return this.propertyDefinition.SetMethod.IsPrivate;
                }

                throw new InvalidOperationException("A property must at least have a getter or a setter");
            }
        }

        /// <summary>
        /// Gets a value indicating whether this member is public.
        /// </summary>
        public bool IsPublic
        {
            get
            {
                if (this.CanRead)
                {
                    return this.propertyDefinition.GetMethod.IsPublic;
                }

                if (this.CanWrite)
                {
                    return this.propertyDefinition.SetMethod.IsPublic;
                }

                throw new InvalidOperationException("A property must at least have a getter or a setter");
            }
        }

        /// <summary>
        /// Gets a value indicating whether the member is static.
        /// </summary>
        public bool IsStatic => this.propertyDefinition.GetMethod.IsStatic; // Relying on the getter, both must be the same
    }
}
