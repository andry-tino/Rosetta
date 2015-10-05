/// <summary>
/// SourceOptions.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Tests
{
    using System;

    /// <summary>
    /// Acts like a factory.
    /// </summary>
    [Flags]
    public enum SourceOptions
    {
        /// <summary>
        /// 
        /// </summary>
        HasInheritance = 0x001,

        /// <summary>
        /// 
        /// </summary>
        ImplementsInterfaces = 0x010,

        /// <summary>
        /// 
        /// </summary>
        None = 0x000
    }
}
