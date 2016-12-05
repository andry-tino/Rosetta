/// <summary>
/// ConversionProvider.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.Executable
{
    using System;

    /// <summary>
    /// Defines a delegate for defining the logic to convert the code.
    /// </summary>
    /// <param name="arguments">The arguments.</param>
    /// <returns></returns>
    public delegate string ConversionProvider(ConversionArguments arguments);
}
