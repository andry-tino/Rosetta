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
        HasInheritance = 0x00001,

        /// <summary>
        /// Has at least one interface.
        /// </summary>
        ImplementsInterfaces = 0x00010,

        /// <summary>
        /// Has many entities as base types.
        /// </summary>
        BaseListMany = 0x00100,

        /// <summary>
        /// Defines elements that are empty.
        /// </summary>
        EmptyElements = 0x01000,

        /// <summary>
        /// No options.
        /// </summary>
        None = 0x00000
    }
}
