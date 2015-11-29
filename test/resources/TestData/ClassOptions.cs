/// <summary>
/// ClassOptions.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Tests.Data
{
    using System;

    /// <summary>
    /// Flags for factory generation regarding classes.
    /// </summary>
    [Flags]
    public enum ClassOptions
    {
        /// <summary>
        /// Has fields.
        /// </summary>
        HasFields = 0x000001,

        /// <summary>
        /// Has methods.
        /// </summary>
        HasMethods = 0x000010,

        /// <summary>
        /// Has properties.
        /// </summary>
        HasProperties = 0x000100,

        /// <summary>
        /// Has constructor.
        /// </summary>
        HasConstructor = 0x001000,

        /// <summary>
        /// Methods, properties should be empty or not?
        /// </summary>
        HasContent = 0x010000,

        /// <summary>
        /// No options.
        /// </summary>
        None = 0x000000
    }
}
