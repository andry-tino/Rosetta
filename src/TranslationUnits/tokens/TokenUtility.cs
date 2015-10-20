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
        public static string PublicVisibilityToken
        {
            get { return "public"; }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string PrivateVisibilityToken
        {
            get { return "private"; }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string ProtectedVisibilityToken
        {
            get { return "protected"; }
        }

        /// <summary>
        /// Converts the visibility into the appropriate TypeScript token.
        /// </summary>
        /// <param name="visibilityToken">The visibility.</param>
        /// <returns></returns>
        public static string ToString(this VisibilityToken visibilityToken)
        {
            return visibilityToken != VisibilityToken.None ? visibilityToken.ToString("G").ToLower() : string.Empty;
        }
    }
}
