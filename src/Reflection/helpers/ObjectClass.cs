/// <summary>
/// ObjectClass.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.Helpers
{
    using System;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.Reflection.Proxies;

    /// <summary>
    /// Helper for <see cref="ITypeInfoProxy"/> in order to get information whether the type is <see cref="System.Object"/>.
    /// </summary>
    public class ObjectClass
    {
        // TODO: Use reflecrtion to get the full name
        private const string ObjectClassFullName = "System.Object";

        private readonly ITypeInfoProxy typeInfo;
        private readonly ITypeProxy type;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectClass"/> class.
        /// </summary>
        /// <param name="typeInfo">The <see cref="ITypeInfoProxy"/> to analyze.</param>
        public ObjectClass(ITypeInfoProxy typeInfo)
        {
            if (typeInfo == null)
            {
                throw new ArgumentNullException(nameof(typeInfo));
            }

            this.typeInfo = typeInfo;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectClass"/> class.
        /// </summary>
        /// <param name="type">The <see cref="ITypeProxy"/> to analyze.</param>
        public ObjectClass(ITypeProxy type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            this.type = type;
        }

        /// <summary>
        /// Gets a value indicating whether the class is <see cref="System.Object"/>.
        /// </summary>
        public bool Is
        {
            get
            {
                if (this.typeInfo != null)
                {
                    return this.typeInfo.FullName == ObjectClassFullName;
                }

                if (this.type != null)
                {
                    return this.type.FullName == ObjectClassFullName;
                }

                throw new InvalidOperationException("Object was not properly constructed");
            }
        }
    }
}
