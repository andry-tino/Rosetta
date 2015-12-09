/// <summary>
/// FunctionOptions.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Tests.Data
{
    using System;

    /// <summary>
    /// Flags for factory generation regarding methods and properties.
    /// </summary>
    [Flags]
    public enum FunctionOptions
    {
        /// <summary>
        /// Has variables.
        /// </summary>
        HasVariables = 0x000001,

        /// <summary>
        /// Has expressions.
        /// </summary>
        HasExpressions = 0x000010,

        /// <summary>
        /// Has loops.
        /// </summary>
        HasLoops = 0x000100,

        /// <summary>
        /// Has conditionals.
        /// </summary>
        HasConditionals = 0x001000,

        /// <summary>
        /// Has lambda expressions.
        /// </summary>
        HasLambdas = 0x010000,

        /// <summary>
        /// No options.
        /// </summary>
        None = 0x000000
    }
}
