/// <summary>
/// Namespace.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.Helpers
{
    using System;
    using System.Linq;

    using Rosetta.Reflection.Proxies;

    /// <summary>
    /// Helper for <see cref="ITypeInfoProxy"/> in order to retrieve information about its namespace.
    /// </summary>
    public class Namespace
    {
        private readonly ITypeInfoProxy type;

        /// <summary>
        /// Initializes a new instance of the <see cref="Namespace"/> class.
        /// </summary>
        /// <param name="type"></param>
        public Namespace(ITypeInfoProxy type)
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

        /// <summary>
        /// Replaces the namespace with another one.
        /// </summary>
        /// <param name="originalTypeFullName">The full name of a type.</param>
        /// <param name="newNamespace">The namespace to replace the original namespace.</param>
        /// <returns>The final type name with replaced namespace.</returns>
        /// <remarks>
        /// This method will consider the type name the last one after extracting from the dot notation.
        /// </remarks>
        public static string ReplaceNamespace(string originalTypeFullName, string newNamespace)
        {
            if (originalTypeFullName == null)
            {
                throw new ArgumentNullException(nameof(originalTypeFullName));
            }
            if (originalTypeFullName.Length == 0)
            {
                throw new ArgumentException("Type name should contain an actual value", nameof(originalTypeFullName));
            }

            if (newNamespace == null)
            {
                throw new ArgumentNullException(nameof(newNamespace));
            }
            if (newNamespace.Length == 0)
            {
                throw new ArgumentException("New namespace name should contain an actual value", nameof(newNamespace));
            }

            string[] extractedNames = originalTypeFullName.Split(new[] { "." }, StringSplitOptions.None);
            if (extractedNames.Length <= 1)
            {
                throw new ArgumentException("Invalid full name", nameof(originalTypeFullName));
            }

            var typeName = extractedNames.Reverse().Take(1).First();
            if (typeName.Length == 0)
            {
                throw new ArgumentException("Invalid full name", nameof(originalTypeFullName));
            }

            return $"{newNamespace}.{typeName}";
        }

        /// <summary>
        /// Replaces the namespace with another one.
        /// </summary>
        /// <param name="originalTypeFullName">The full name of a type.</param>
        /// <param name="originalTypeNamespaceName">The namespace name of the type (which will be replaced).</param>
        /// <param name="newNamespace">The namespace to replace the original namespace.</param>
        /// <returns>The final type name with replaced namespace.</returns>
        public static string ReplaceNamespace(string originalTypeFullName, string originalTypeNamespaceName, string newNamespace)
        {
            if (originalTypeFullName == null)
            {
                throw new ArgumentNullException(nameof(originalTypeFullName));
            }
            if (originalTypeFullName.Length == 0)
            {
                throw new ArgumentException("Type name should contain an actual value", nameof(originalTypeFullName));
            }

            if (originalTypeNamespaceName == null)
            {
                throw new ArgumentNullException(nameof(originalTypeNamespaceName));
            }
            if (!originalTypeFullName.Contains(originalTypeNamespaceName))
            {
                throw new ArgumentException("Namespace name should be contained in original type name", nameof(originalTypeNamespaceName));
            }

            if (newNamespace == null)
            {
                throw new ArgumentNullException(nameof(newNamespace));
            }
            if (newNamespace.Length == 0)
            {
                throw new ArgumentException("New namespace name should contain an actual value", nameof(newNamespace));
            }

            if (originalTypeNamespaceName.Length == 0)
            {
                // In this case, originalTypeFullName is just the type name
                if (originalTypeFullName.Contains("."))
                {
                    throw new ArgumentException("No original namespace provided, but the type full name has namespace separators in it", 
                        nameof(originalTypeFullName));
                }

                return $"{newNamespace}.{originalTypeFullName}";
            }

            return originalTypeFullName.Replace(originalTypeNamespaceName, newNamespace);
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
    }
}
