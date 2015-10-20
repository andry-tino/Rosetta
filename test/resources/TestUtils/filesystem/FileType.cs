/// <summary>
/// FileType.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Tests.Utils
{
    using System;

    /// <summary>
    /// Types of file.
    /// </summary>
    public enum FileType
    {
        /// <summary>
        /// 
        /// </summary>
        CSharp,

        /// <summary>
        /// 
        /// </summary>
        TypeScript,

        /// <summary>
        /// 
        /// </summary>
        None
    }

    public static class FileTypeUtilities
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileType"></param>
        /// <returns></returns>
        public static string FileExtension(this FileType fileType)
        {
            switch (fileType)
            {
                case FileType.CSharp: return ".cs";
                case FileType.TypeScript: return ".ts";
                default: return string.Empty;
            }
        }
    }
}
