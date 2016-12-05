/// <summary>
/// IConversionArgumentsValidator.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.Executable
{
    using System;

    /// <summary>
    /// Interface describing validator for <see cref="ConversionArguments"/>.
    /// </summary>
    public interface IConversionArgumentsValidator
    {
        /// <summary>
        /// Validated the arguments.
        /// </summary>
        /// <returns><code>true</code> if the validation process is successful, <code>false</code> otherwise.</returns>
        bool TryValidate();

        /// <summary>
        /// Validates the <see cref="ConversionArguments"/> and if validation is not successful throws an exception.
        /// </summary>
        /// <exception cref="ArgumentException">Raised when one of the properties fails validation.</exception>
        void Validate();
    }
}
