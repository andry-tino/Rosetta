/// <summary>
/// IFieldInfoProxy.cs
/// Andrea Tino - 2017
/// </summary>

namespace Rosetta.Reflection.Proxies
{
    using System;

    /// <summary>
    /// Abstracts field info API.
    /// </summary>
    public interface IFieldInfoProxy
    {
        /// <summary>
        /// Gets the name of the current member.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the Type of this field.
        /// </summary>
        ITypeProxy FieldType { get; }

        /// <summary>
        /// Gets a value indicating whether the field is visible only within its class 
        /// and derived classes.
        /// </summary>
        bool IsFamily { get; }

        /// <summary>
        /// Gets a value indicating whether this member is private.
        /// </summary>
        bool IsPrivate { get; }

        /// <summary>
        /// Gets a value indicating whether this member is public.
        /// </summary>
        bool IsPublic { get; }
    }
}
