/// <summary>
/// MonoAssemblyProxy.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.Proxies
{
    using System;
    using System.Diagnostics;

    using Mono.Cecil;

    /// <summary>
    /// Proxy for Assembly incapsulating Mono.
    /// </summary>
    [DebuggerDisplay("TypeName = {StringArumentTypeFullName}, Value = {StringValue}", Name = "{StringArumentTypeName,nq} (Mono proxy)")]
    public class MonoCustomAttributeTypedArgumentProxy : ICustomAttributeTypedArgumentProxy
    {
        private readonly CustomAttributeArgument customAttributeArgument;

        /// <summary>
        /// Initializes a new instance of the <see cref="MonoCustomAttributeTypedArgumentProxy"/> class.
        /// </summary>
        /// <param name="monoModuleDefinition"></param>
        public MonoCustomAttributeTypedArgumentProxy(CustomAttributeArgument monoCustomAttributeArgument)
        {
            this.customAttributeArgument = monoCustomAttributeArgument;
        }

        /// <summary>
        /// Gets the type of the argument or of the array argument element.
        /// </summary>
        public ITypeProxy ArgumentType => new MonoTypeProxy(this.customAttributeArgument.Type);

        /// <summary>
        /// Gets the value of the argument for a simple argument or for an element of an array argument; gets a collection of values for an array argument.
        /// </summary>
        public object Value => this.customAttributeArgument.Value;

        private string StringValue => this.Value.ToString();

        // Used by debugger
        private string StringArumentTypeName => this.customAttributeArgument.Type.Name;

        // Used by debugger
        private string StringArumentTypeFullName => this.customAttributeArgument.Type.FullName;
    }
}
