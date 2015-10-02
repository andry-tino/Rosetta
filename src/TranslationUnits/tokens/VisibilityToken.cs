/// <summary>
/// VisibilityToken.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Interface for describing translation elements.
    /// </summary>
    [Flags]
    public enum VisibilityToken
    {
        /// <summary>
        /// 
        /// </summary>
        Public = 0x00000001,

        /// <summary>
        /// 
        /// </summary>
        Protected = 0x00000010,

        /// <summary>
        /// 
        /// </summary>
        Private = 0x00000100,

        /// <summary>
        /// 
        /// </summary>
        Internal = 0x00001000,

        /// <summary>
        /// 
        /// </summary>
        Static = 0x00010000,

        /// <summary>
        /// 
        /// </summary>
        None = 0x00000000
    }
}
