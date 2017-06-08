/// <summary>
/// NativeClass.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.Helpers
{
    using System;

    using Rosetta.Reflection.Proxies;

    /// <summary>
    /// Helper for <see cref="ITypeInfoProxy"/> and <see cref="ITypeProxy"/> in order to get information whether the 
    /// type is a specific native one.
    /// </summary>
    public abstract class NativeClass
    {
        private readonly ITypeInfoProxy typeInfo;
        private readonly ITypeProxy type;

        /// <summary>
        /// Initializes a new instance of the <see cref="NativeClass"/> class.
        /// </summary>
        /// <param name="typeInfo">The <see cref="ITypeInfoProxy"/> to analyze.</param>
        public NativeClass(ITypeInfoProxy typeInfo)
        {
            if (typeInfo == null)
            {
                throw new ArgumentNullException(nameof(typeInfo));
            }

            this.typeInfo = typeInfo;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NativeClass"/> class.
        /// </summary>
        /// <param name="type">The <see cref="ITypeProxy"/> to analyze.</param>
        public NativeClass(ITypeProxy type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            this.type = type;
        }

        /// <summary>
        /// Gets a value indicating whether the class is a specific native one.
        /// </summary>
        public bool Is
        {
            get
            {
                if (this.typeInfo != null)
                {
                    return this.typeInfo.FullName == this.ObjectClassFullName;
                }

                if (this.type != null)
                {
                    return this.type.FullName == this.ObjectClassFullName;
                }

                throw new InvalidOperationException("Object was not properly constructed");
            }
        }
        
        protected abstract string ObjectClassFullName { get; }
    }
}
