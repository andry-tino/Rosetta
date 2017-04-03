/// <summary>
/// Namespace.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.Helpers
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Abstraction for building an AST from an assembly.
    /// </summary>
    public class Namespace
    {
        private readonly TypeInfo type;

        /// <summary>
        /// Initializes a new instance of the <see cref="Namespace"/> class.
        /// </summary>
        /// <param name="type"></param>
        public Namespace(TypeInfo type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            this.type = type;
        }

        /// <summary>
        /// Gets the full name of the namespace associated with the type.
        /// </summary>
        public string FullName => type.Namespace;

        /// <summary>
        /// Gets a value indicating whether a namespace is defined for the type.
        /// </summary>
        public bool Exists => this.FullName != null && this.FullName != "";
    }
}
