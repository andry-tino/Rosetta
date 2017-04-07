/// <summary>
/// Namespace.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.Helpers
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Helper for <see cref="TypeInfo"/> in order to retrieve information about its namespace.
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

            // Supported input types
            CheckType(type);

            this.type = type;
        }

        /// <summary>
        /// Gets the full name of the namespace associated with the type.
        /// </summary>
        public virtual string FullName => this.Type.Namespace;

        /// <summary>
        /// Gets a value indicating whether a namespace is defined for the type.
        /// </summary>
        public bool Exists => this.FullName != null && this.FullName != "";

        protected TypeInfo Type => this.type;

        private static void CheckType(TypeInfo type)
        {
            if (type.IsClass) return;
            if (type.IsValueType) return;
            if (type.IsInterface) return;
            if (type.IsEnum) return;

            throw new ArgumentException("This helper only supports classes, structs, enums and interfaces", nameof(type));
        }
    }
}
