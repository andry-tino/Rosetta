/// <summary>
/// SourceOptions.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Tests.Data
{
    using System;

    /// <summary>
    /// Acts like a factory.
    /// </summary>
    [Flags]
    public enum SourceOptions
    {
        /// <summary>
        /// Has a base class.
        /// </summary>
        HasNamespace = 0x000001,

        /// <summary>
        /// Has a base class.
        /// </summary>
        HasInheritance = 0x000010,

        /// <summary>
        /// Has at least one interface.
        /// </summary>
        ImplementsInterfaces = 0x000100,

        /// <summary>
        /// Has many entities as base types.
        /// </summary>
        BaseListMany = 0x001000,

        /// <summary>
        /// Defines elements that are empty.
        /// </summary>
        EmptyElements = 0x010000,

        /// <summary>
        /// No options.
        /// </summary>
        None = 0x000000
    }
}
