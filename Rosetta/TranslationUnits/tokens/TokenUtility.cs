/// <summary>
/// TokenUtility.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Utility class for tokens.
    /// </summary>
    public static class TokenUtility
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="visibilityToken"></param>
        /// <returns></returns>
        public static string ToString(this VisibilityToken visibilityToken)
        {
            return visibilityToken.ToString("G").ToLower();
        }
    }
}
