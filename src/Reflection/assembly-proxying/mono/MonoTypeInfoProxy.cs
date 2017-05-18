/// <summary>
/// MonoTypeInfoProxy.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.Proxies
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using Mono.Cecil;

    /// <summary>
    /// Proxy for Assembly incapsulating Mono.
    /// </summary>
    [DebuggerDisplay("Name = {Name}, Namespace = {Namespace}", Name = "{MetadataType,nq} {Name,nq} (Mono proxy)")]
    public class MonoTypeInfoProxy : ITypeInfoProxy
    {
        private readonly TypeDefinition typeDefinition;

        /// <summary>
        /// Initializes a new instance of the <see cref="MonoTypeInfoProxy"/> class.
        /// </summary>
        /// <param name="monoTypeDefinition"></param>
        public MonoTypeInfoProxy(TypeDefinition monoTypeDefinition)
        {
            if (monoTypeDefinition == null)
            {
                throw new ArgumentNullException(nameof(monoTypeDefinition));
            }

            this.typeDefinition = monoTypeDefinition;
        }

        /// <summary>
        /// Gets a value indicating whether the Type is a class or a delegate; that is, 
        /// not a value type or interface.
        /// </summary>
        public bool IsClass => this.typeDefinition.IsClass;

        /// <summary>
        /// Gets a value indicating whether the Type is a value type.
        /// </summary>
        public bool IsEnum => this.typeDefinition.IsEnum;

        /// <summary>
        /// Gets a value indicating whether the Type is an interface; that is, 
        /// not a class or a value type.
        /// </summary>
        public bool IsInterface => this.typeDefinition.IsInterface;

        /// <summary>
        /// Gets a value indicating whether the current Type represents an enumeration.
        /// </summary>
        public bool IsValueType => this.typeDefinition.IsValueType;

        /// <summary>
        /// Gets the namespace of the Type.
        /// </summary>
        public string Namespace => this.typeDefinition.Namespace;

        /// <summary>
        /// Gets the name of the current member.
        /// </summary>
        public string Name => this.typeDefinition.Name;

        /// <summary>
        /// Gets the fully qualified name of the type, including its namespace but not its assembly.
        /// </summary>
        public string FullName => this.typeDefinition.FullName;

        /// <summary>
        /// Gets a value indicating whether the type is declared public.
        /// </summary>
        public bool IsPublic => this.typeDefinition.IsPublic;

        /// <summary>
        /// Gets a value indicating whether the type is not declared public.
        /// </summary>
        public bool IsNotPublic => this.typeDefinition.IsNotPublic;

        /// <summary>
        /// Gets a collection that contains this member's custom attributes.
        /// </summary>
        public IEnumerable<ICustomAttributeDataProxy> CustomAttributes => this.typeDefinition.HasCustomAttributes 
            ? this.typeDefinition.CustomAttributes.Select(customAttribute => new MonoCustomAttributeDataProxy(customAttribute)) 
            : null;

        /// <summary>
        /// Gets a collection of the interfaces implemented by the current type.
        /// </summary>
        public IEnumerable<ITypeProxy> ImplementedInterfaces => this.typeDefinition.HasInterfaces 
            ? this.typeDefinition.Interfaces.Select(@interface => new MonoTypeProxy(@interface)) 
            : null;

        /// <summary>
        /// Gets the type from which the current Type directly inherits.
        /// </summary>
        public ITypeProxy BaseType => this.typeDefinition.BaseType != null 
            ? new MonoTypeProxy(this.typeDefinition.BaseType) 
            : null;

        /// <summary>
        /// Gets a collection of the methods defined by the current type.
        /// </summary>
        public IEnumerable<IMethodInfoProxy> DeclaredMethods => this.typeDefinition.HasMethods 
            ? this.typeDefinition.Methods.Where(method => !method.IsConstructor).Select(method => new MonoMethodInfoProxy(method)) 
            : null;

        /// <summary>
        /// Gets a collection of the constructors defined by the current type.
        /// </summary>
        public IEnumerable<IConstructorInfoProxy> DeclaredConstructors => this.typeDefinition.HasMethods && this.typeDefinition.Methods.Where(method => method.IsConstructor).Count() > 0
            ? this.typeDefinition.Methods.Where(method => method.IsConstructor).Select(method => new MonoConstructorInfoProxy(method))
            : null;

        // Used by debugger
        private string MetadataType
        {
            get
            {
                if (this.typeDefinition.IsClass) return "Class";
                if (this.typeDefinition.IsInterface) return "Interface";
                if (this.typeDefinition.IsEnum) return "Enum";
                if (this.typeDefinition.IsValueType) return "ValueType";

                return "Type";
            }
        }
    }
}
